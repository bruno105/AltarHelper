using ExileCore;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using ExileCore.PoEMemory.Elements;
using ExileCore.Shared.Cache;
using ExileCore.Shared.Enums;
using SharpDX;
using Vector2 = System.Numerics.Vector2;
using System.Runtime.Versioning;

namespace AltarHelper;
[SupportedOSPlatform("windows")]
public class AltarHelperCore : BaseSettingsPlugin<Settings>
{
    private const string FilterFileName = "Filter.txt";
    private List<Filter> _filterList = new List<Filter>();
    private CachedValue<List<LabelOnGround>> _labelCache;

    public override bool Initialise()
    {
        Name = "AltarHelper";
        Settings.AltarSettings.RefreshFile.OnPressed += ReadFilterFile;
        ReadFilterFile();
        _labelCache = new TimeCache<List<LabelOnGround>>(GetLabels, 250);
        return base.Initialise();
    }

    private void ReadFilterFile()
    {
        var path = $"{DirectoryFullName}\\{FilterFileName}";
        if (File.Exists(path))
        {
            ReadFile();
        }
        else
            CreateFilterFile();
    }

    private void CreateFilterFile()
    {
        var path = $"{DirectoryFullName}\\{FilterFileName}";
        if (File.Exists(path)) return;
        using (var streamWriter = new StreamWriter(path, true))
        {
            streamWriter.WriteLine("//Name|Weight|Choice");
            streamWriter.WriteLine("#Good");
            //  streamWriter.WriteLine("");
            streamWriter.WriteLine("Drops (1–3) additional Scarab|10|Boss");
            streamWriter.WriteLine("#Bad");
            streamWriter.WriteLine("");
            streamWriter.Close();
        }
    }

    private void ReadFile()
    {
        _filterList.Clear();

        var lines = File.ReadAllLines($"{DirectoryFullName}\\{FilterFileName}");
        var good = false;
        var localList = new List<string>();
        localList.Clear();
        Settings.FilterList.ListFilter.SetListValues(localList);

        foreach (var line in lines)
        {
            if (line.Length < 4) continue;
            if (line.Contains("/")) continue;
            if (line.Contains("#Good"))
            {
                DebugWindow.LogMsg("Entrou Good");
                good = true;
                continue;
            }

            if (line.Trim().ToUpper() == "#BAD")
            {
                DebugWindow.LogMsg("Entrou Bad");
                good = false;
                continue;
            }

            var splittedLine = line.Split('|');
            var f = new Filter
            {
                Mod = splittedLine[0].Length > 0 ? splittedLine[0] : "-1",
                Weight = splittedLine[1].Length > 0 ? int.Parse(splittedLine[1]) : 0,
                Choice = splittedLine.Length == 2 ? "Any" : splittedLine[2].Length > 0 ? splittedLine[2] : "Any",
                Good = good
            };

            if (f.Mod == "-1") continue;
            f.Matcher = f.Mod.RegexForLikeSubstring();
            _filterList.Add(f);
            localList.Add(string.Format($"{f.Mod} | {f.Weight} |  Good? {f.Good}"));
        }

        Settings.FilterList.ListFilter.SetListValues(localList);
        _filterList = _filterList.OrderBy(x => x.Weight).ToList();
    }

    private List<LabelOnGround> GetLabels()
    {
        if (GameController.IngameState.IngameUi?.ItemsOnGroundLabelsVisible is not { Count: > 0 } labels)
        {
            return new List<LabelOnGround>();
        }

        return labels.Where(x => x?.ItemOnGround?.Metadata is { } metadata && (metadata.Contains("TangleAltar") || metadata.Contains("FireAltar"))).ToList();
    }

    public override void Render()
    {
        if (GameController.Area.CurrentArea.IsHideout ||
            GameController.Area.CurrentArea.IsTown ||
            _labelCache.Value is not { } labels)
        {
            return;
        }

        foreach (var label in labels)
        {
            if (label?.Label == null) continue;

            var itemMetadata = label.ItemOnGround?.Metadata;

            if (itemMetadata == null || (!itemMetadata.Contains("TangleAltar") && !itemMetadata.Contains("FireAltar"))) continue;

            var upper = label.Label?.GetChildAtIndex(0);
            var downer = label.Label?.GetChildAtIndex(1);
            if (upper == null || downer == null) continue;

            var upperText = upper.GetChildAtIndex(1)?.GetText(1024).Trim();
            var downerText = downer.GetChildAtIndex(1)?.GetText(1024).Trim();

            if (upperText == null || downerText == null) continue;


            var altar = GetAltarData(upperText, downerText);

            if (altar == null) continue;

            if (altar.Upper.BuffWeight == 0 && altar.Downer.BuffWeight == 0 && altar.Downer.DebuffWeight == 0 && altar.Upper.DebuffWeight == 0) continue;

            int? upperDiff = altar.Upper.BuffWeight - altar.Upper.DebuffWeight;
            int? downerDiff = altar.Downer.BuffWeight - altar.Downer.DebuffWeight;


            if (Settings.DebugSettings.DebugWeight)
            {
                Graphics.DrawText(altar.Upper.BuffWeight.ToString(), new Vector2(upper.GetClientRectCache.Center.X - 5, upper.GetClientRectCache.Top - 25), Color.Cyan,
                    FontAlign.Right);
                Graphics.DrawText(altar.Upper.DebuffWeight.ToString(), new Vector2(upper.GetClientRectCache.Center.X + 5, upper.GetClientRectCache.Top - 25), Color.Red);
                Graphics.DrawText(altar.Downer.BuffWeight.ToString(), new Vector2(downer.GetClientRectCache.Center.X - 5, downer.GetClientRectCache.Bottom + 15), Color.Cyan,
                    FontAlign.Right);
                Graphics.DrawText(altar.Downer.DebuffWeight.ToString(), new Vector2(downer.GetClientRectCache.Center.X + 5, downer.GetClientRectCache.Bottom + 15), Color.Red);
            }

            if (upperDiff < 0)
                Graphics.DrawFrame(upper.GetClientRectCache, Settings.AltarSettings.BadColor, Settings.AltarSettings.FrameThickness);
            else if (upperDiff > 0 && altar.Upper.BuffWeight >= altar.Downer.BuffWeight)
                Graphics.DrawFrame(upper.GetClientRectCache, GetColor(altar.Upper.Choice), Settings.AltarSettings.FrameThickness);

            if (downerDiff < 0)
                Graphics.DrawFrame(downer.GetClientRectCache, Settings.AltarSettings.BadColor, Settings.AltarSettings.FrameThickness);
            else if (downerDiff > 0 && altar.Downer.BuffWeight >= altar.Upper.BuffWeight)
                Graphics.DrawFrame(downer.GetClientRectCache, GetColor(altar.Downer.Choice), Settings.AltarSettings.FrameThickness);
        }
    }

    public Color GetColor(string choice)

    {
        var c = Color.Transparent;

        if (choice.ToUpper().Contains("MINION")) return Settings.AltarSettings.MinionColor;
        if (choice.ToUpper().Contains("BOSS")) return Settings.AltarSettings.BossColor;
        if (choice.ToUpper().Contains("PLAYER")) return Settings.AltarSettings.PlayerColor;

        return c;
    }


    public Altar GetAltarData(string upperText, string downerText)
    {
        return new Altar
        {
            Downer = GetSelectData(downerText),
            Upper = GetSelectData(upperText)
        };
    }

    private static readonly Regex AltarTextRegex =
        new Regex(@"^<valuedefault>{(?<whoGains>.*):}\s*(?<debuffText>.*?)\s*(?:<enchanted>{(?<buffText>.*?)}\s*)+$", RegexOptions.Compiled | RegexOptions.Singleline);

    public Select GetSelectData(string selecterText)
    {
        var match = AltarTextRegex.Match(selecterText);
        var target = match.Groups["whoGains"].Value;
        var buffTexts = match.Groups["buffText"].Captures.Select(x => x.Value).ToList();
        var debuffText = match.Groups["debuffText"].Value;
        var foundBuffs = _filterList.Where(x => x.Good).Where(x => buffTexts.Any(text => x.Matcher.IsMatch(text))).ToList();
        if (Settings.DebugSettings.DebugBuffs)
        {
            foreach (var foundBuff in foundBuffs)
            {
                DebugWindow.LogMsg($"Good Mod: {foundBuff.Mod}  | Weight {foundBuff.Weight}");
            }
        }

        var foundDebuffs = _filterList.Where(x => !x.Good).Where(x => x.Matcher.IsMatch(debuffText)).ToList();
        if (Settings.DebugSettings.DebugDebuffs)
        {
            foreach (var foundDebuff in foundDebuffs)
            {
                DebugWindow.LogMsg($"Bad Mod: {foundDebuff.Mod}  | Weight {foundDebuff.Weight}");
            }
        }

        var s = new Select
        {
            Choice = target,
            BuffWeight = foundBuffs.Select(x => x.Weight).DefaultIfEmpty(0).Max(),
            DebuffWeight = foundDebuffs.Select(x => x.Weight).DefaultIfEmpty(0).Max(),
            BuffGood = foundBuffs.FirstOrDefault(x => x.Good == false) != null,
            DebuffGood = foundDebuffs.FirstOrDefault(x => x.Good == false) != null
        };
        return s;
    }

    public class Filter
    {
        public Regex Matcher { get; set; }
        public string Mod { get; set; }
        public int Weight { get; set; }
        public string Choice { get; set; }
        public bool Good { get; set; }
    }

    public class Altar
    {
        public Select Upper { get; set; }
        public Select Downer { get; set; }
    }

    public class Select
    {
        public string Choice { get; set; }
        public string Buff { get; set; }
        public string Debuff { get; set; }
        public int BuffWeight { get; set; }
        public int DebuffWeight { get; set; }
        public bool BuffGood { get; set; }
        public bool DebuffGood { get; set; }
    }
}

public static class Extensions
{
    public static Regex RegexForLikeSubstring(this string str)
    {
        return new Regex(Regex.Escape(str).Replace("\\*", ".*").Replace("\\?", "."), RegexOptions.IgnoreCase);
    }
}