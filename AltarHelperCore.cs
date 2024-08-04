﻿using ExileCore;
using ExileCore.PoEMemory.Elements;
using ExileCore.Shared.Cache;
using SharpDX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Linq;
using System.Runtime.Versioning;

namespace AltarHelper
{
    [SupportedOSPlatform("windows")]
    public class AltarHelperCore : BaseSettingsPlugin<Settings>
    {
        private const string FILTER_FILE = "Filter.txt";
        public List<FilterEntry> FilterList = new();
        public List<Tuple<RectangleF, Color, int>> RectangleDrawingList = new();
        public List<Tuple<string, Vector2, Color>> TextDrawingList = new();
        // Sound Area
        private readonly object _locker = new object();
        private DateTime _lastPlayed;
        // Sound Area
        private FrameCache<List<LabelOnGround>> LabelCache { get; set; }

        public override bool Initialise()
        {
            Name = "AltarHelper";
            Settings.AltarSettings.RefreshFile.OnPressed += ReadFilterFile;
            ReadFilterFile();
            //PlaySound();

            LabelCache = new FrameCache<List<LabelOnGround>>(UpdateAltarLabelList);
            return true;
        }
        private List<LabelOnGround> UpdateAltarLabelList() => GameController.IngameState.IngameUi.ItemsOnGroundLabelsVisible.Count == 0 ? new List<LabelOnGround>() :
            GameController.IngameState.IngameUi.ItemsOnGroundLabelsVisible.
                Where(x =>
                    x.ItemOnGround.Metadata == "Metadata/MiscellaneousObjects/PrimordialBosses/TangleAltar" ||
                    x.ItemOnGround.Metadata == "Metadata/MiscellaneousObjects/PrimordialBosses/CleansingFireAltar").ToList();
        private void ReadFilterFile()
        {
            var path = $"{DirectoryFullName}\\{FILTER_FILE}";
            if (File.Exists(path))
            {
                ReadFile();
            }
            else
                CreateFilterFile();
        }

        private void CreateFilterFile()
        {
            var path = $"{DirectoryFullName}\\{FILTER_FILE}";
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
            FilterList.Clear();
            List<string> lines = File.ReadAllLines($"{DirectoryFullName}\\{FILTER_FILE}").ToList();
            bool isGood = true;

            List<string> listAux = new List<string>();
            foreach (string line in lines)
            {
                if (line.Length < 4 || line.StartsWith("//")) continue;

                if (line.StartsWith("#Bad"))
                {
                    isGood = false;
                    continue;
                }
                if (line.StartsWith("#Good"))
                {
                    isGood = true;
                    continue;
                }

                string[] splitLine = line.Split('|');
                var mod = splitLine[0];

                if (mod.Length <= 0 || splitLine[1].Length <= 0) continue;

                FilterEntry filter = new()
                {
                    Mod = mod.Contains('(') && mod.Contains(')') ?
                        Regex.Replace(mod, @"\([^()]*\)", "#") :
                        Regex.Replace(mod, @"(\d+)(?:.\d)|\d+", "#"),
                    Weight = int.Parse(splitLine[1]),
                    IsUpside = isGood,
                    Target = splitLine.Length <= 2 ?
                        AffectedTarget.Any :
                        AltarModsConstants.FilterTargetDict[splitLine[2]],
                };
                FilterList.Add(filter);
                listAux.Add(mod);
            }
        }
        public override void Render()
        {
            foreach (var frame in RectangleDrawingList)
            {
                Graphics.DrawFrame(frame.Item1, frame.Item2, frame.Item3);
            }
            foreach (var text in TextDrawingList)
            {
                Graphics.DrawText(text.Item1, text.Item2, text.Item3);
            }
        }

        public override Job Tick()
        {
            RectangleDrawingList.Clear();
            TextDrawingList.Clear();

            //Mode switching
            if (Settings.AltarSettings.HotkeyMode.PressedOnce())
            {
                Settings.AltarSettings.SwitchMode.Value += 1;
                if (Settings.AltarSettings.SwitchMode.Value == 4) Settings.AltarSettings.SwitchMode.Value = 1;
                switch (Settings.AltarSettings.SwitchMode.Value)
                {
                    case 1:
                        DebugWindow.LogMsg("AltarHelper: Changed to Any Choice");
                        break;
                    case 2:
                        DebugWindow.LogMsg("AltarHelper: Changed to only Minions and Player Choices");
                        break;
                    case 3:
                        DebugWindow.LogMsg("AltarHelper: Changed to only bosses and Players Choices");
                        break;
                }
            }

            if (!CanRun()) return null;

            CompareWeights();

            return null;
        }

        private void CompareWeights()
        {
            //filter visible labels and work only on the actual altar ones
            var Altars = LabelCache.Value;
            if (Altars.Count <= 0) return;
            //complicated looking weight logic starts here....
            foreach (var altarlabel in Altars)
            {
                var topOptionLabel = altarlabel.Label.GetChildAtIndex(0);
                var bottomOptionLabel = altarlabel.Label.GetChildAtIndex(1);
                string? topOptionText = topOptionLabel.GetChildAtIndex(1)?.GetText(512);
                string? bottomOptionText = bottomOptionLabel.GetChildAtIndex(1)?.GetText(512);

                if (Settings.DebugSettings.DebugRawText == true) DebugWindow.LogError($"AltarBottom Length 512 : {bottomOptionText}");
                if (Settings.DebugSettings.DebugRawText == true) DebugWindow.LogError($"AltarTop Length 512 : {topOptionText}");
                if (topOptionText == null || bottomOptionText == null) continue;

                Altar altar = new(GetSelectionData(topOptionText), GetSelectionData(bottomOptionText));
                if (altar == null) continue;

                if (altar.Top.UpsideWeight == 0 &&
                    altar.Bottom.UpsideWeight == 0 &&
                    altar.Top.DownsideWeight == 0 &&
                    altar.Bottom.DownsideWeight == 0) continue;

                int topOptionWeight = 0;
                int bottomOptionWeight = 0;

               

                if (Settings.AltarSettings.SwitchMode.Value == 2)
                {
                    if (altar.Top.Target == AffectedTarget.Minions || altar.Top.Target == AffectedTarget.Player)
                    {
                        topOptionWeight += altar.Top.UpsideWeight - altar.Top.DownsideWeight;
                    }
                    if (altar.Bottom.Target == AffectedTarget.Minions || altar.Bottom.Target == AffectedTarget.Player)
                    {
                        bottomOptionWeight += altar.Bottom.UpsideWeight - altar.Bottom.DownsideWeight;
                    }
                }
                else if (Settings.AltarSettings.SwitchMode.Value == 3)
                {
                    if (altar.Top.Target == AffectedTarget.FinalBoss || altar.Top.Target == AffectedTarget.Player)
                    {
                        topOptionWeight += altar.Top.UpsideWeight - altar.Top.DownsideWeight;
                    }
                    if (altar.Bottom.Target == AffectedTarget.FinalBoss || altar.Bottom.Target == AffectedTarget.Player)
                    {
                        bottomOptionWeight += altar.Bottom.UpsideWeight - altar.Bottom.DownsideWeight;
                    }
                }
                else
                {
                    topOptionWeight += altar.Top.UpsideWeight + altar.Top.DownsideWeight;
                    bottomOptionWeight += altar.Bottom.UpsideWeight + altar.Bottom.DownsideWeight;
                }

                if (altar.Top.Target == AffectedTarget.Minions) topOptionWeight += Settings.AltarSettings.MinionWeight.Value;
                if (altar.Top.Target == AffectedTarget.FinalBoss) topOptionWeight += Settings.AltarSettings.BossWeight.Value;
                if (altar.Bottom.Target == AffectedTarget.Minions) bottomOptionWeight += Settings.AltarSettings.MinionWeight.Value;
                if (altar.Bottom.Target == AffectedTarget.FinalBoss) bottomOptionWeight += Settings.AltarSettings.BossWeight.Value;
                if (altar.Bottom.PlayAlert || altar.Top.PlayAlert) PlaySound();

                if (Settings.DebugSettings.DebugWeight)
                {
                    TextDrawingList.Add(new(topOptionWeight.ToString(), new Vector2(topOptionLabel.GetClientRectCache.Center.X - 10, topOptionLabel.GetClientRectCache.Top - 25), Color.Cyan));
                    TextDrawingList.Add(new(bottomOptionWeight.ToString(), new Vector2(bottomOptionLabel.GetClientRectCache.Center.X - 10, bottomOptionLabel.GetClientRectCache.Bottom + 15), Color.Cyan));
                    //  DebugWindow.LogError($"UpperWeight: {UpperWeight} | DownerWeight: {DownerWeight}");
                }

                if (topOptionWeight < 0 || bottomOptionWeight < 0)
                {
                    if (topOptionWeight < 0) RectangleDrawingList.Add(new(topOptionLabel.GetClientRectCache, Settings.AltarSettings.BadColor, Settings.AltarSettings.FrameThickness));
                    if (bottomOptionWeight < 0) RectangleDrawingList.Add(new(bottomOptionLabel.GetClientRectCache, Settings.AltarSettings.BadColor, Settings.AltarSettings.FrameThickness));
                }

                if (topOptionWeight >= 0 || bottomOptionWeight >= 0)
                {
                    if (topOptionWeight >= bottomOptionWeight && topOptionWeight > 0) RectangleDrawingList.Add(new(topOptionLabel.GetClientRectCache, GetColor(altar.Top.Target), Settings.AltarSettings.FrameThickness));
                    if (bottomOptionWeight > topOptionWeight && bottomOptionWeight > 0) RectangleDrawingList.Add(new(bottomOptionLabel.GetClientRectCache, GetColor(altar.Bottom.Target), Settings.AltarSettings.FrameThickness));
                    continue;
                }
            }
        }





        #region helperfunctons
        public bool CanRun()
        {
            if (GameController.Area.CurrentArea.IsHideout ||
                GameController.Area.CurrentArea.IsTown ||
                GameController.IngameState.IngameUi == null ||
                GameController.IngameState.IngameUi.ItemsOnGroundLabelsVisible == null ||
                LabelCache.Value.Count < 1)
                return false;
            return true;
        }


        private void PlaySound()
        {
            lock (_locker)
            {
                if ((DateTime.Now - _lastPlayed).TotalMilliseconds > Settings.AltarSettings.DelayBetweenAlerts && (DateTime.Now - _lastPlayed).TotalMilliseconds > 500)
                {
                     GameController.SoundController.PlaySound(Path.Combine(@"..\Sounds\", "diviners").Replace('\\', '/'));
                    _lastPlayed = DateTime.Now;
                }
            }
        }


        #endregion

        public Color GetColor(AffectedTarget choice)
        {
            Color color = Color.Transparent;
            if (choice == AffectedTarget.Minions) return Settings.AltarSettings.MinionColor;
            if (choice == AffectedTarget.FinalBoss) return Settings.AltarSettings.BossColor;
            if (choice == AffectedTarget.Player) return Settings.AltarSettings.PlayerColor;

            return color;
        }

        public Selection GetSelectionData(string altarLabelText)
        {
            AffectedTarget Target;
            List<string> downsides = new();
            List<string> upsides = new();

            using (StringReader stringreader = new(altarLabelText))
            {
                string targetLine = stringreader.ReadLine();
                Target = AltarModsConstants.AltarTargetDict[targetLine[("<valuedefault>{".Length)..^1]];

                string line;
                bool upsideSectionReached = false;
                // LogError("Ets");
                while ((line = stringreader.ReadLine()) != null)
                {

                    if (line.StartsWith("<enchanted>"))
                    {
                        upsideSectionReached = true;
                    }
                    if (upsideSectionReached)
                    {
                        line = (line.StartsWith("<enchanted>")) ? line.Replace("<enchanted>{", "") : line.Replace("}", "");
                        if (line.Contains('}')) line = line.Replace("}", "");
                        if (line.StartsWith("<rgb"))
                        {
                            line = line[(line.IndexOf('{') + 1)..^1];
                        }
                        //String with range operator to cut trim unneeded tags
                        upsides.Add(line);
                        continue;
                    }
                    downsides.Add(line);
                }
            }
            //if (Settings.DebugSettings.DebugRawText)
            //{
            //    DebugWindow.LogMsg($"Target: {Target}");
            //    DebugWindow.LogMsg($"Downsides:\n{string.Join('\n', downsides)}");
            //    DebugWindow.LogMsg($"Upsides:\n{string.Join('\n', upsides)}");
            //}

            List<FilterEntry> UpsideFilterEntryMatches = new();
            List<FilterEntry> DownsideFilterEntryMatches = new();

            foreach (string entry in upsides)
            {

                var upside = Regex.Replace(entry, @"((\d+)(?:.\d)|\d+)", "#");


                if (Settings.DebugSettings.DebugBuffs) DebugWindow.LogMsg(upside);
                var filterentry = GetEntry(upside);
               // FilterEntry filterentry = FilterList.FirstOrDefault(element => element.Mod.Contains(upside));
                if (filterentry == null) continue;

                UpsideFilterEntryMatches.Add(filterentry);
                if (Settings.DebugSettings.DebugBuffs) DebugWindow.LogMsg($"Good Mod: {filterentry.Mod}  | Weight {filterentry.Weight}");
            }

            foreach (string entry in downsides)
            {
                var downside = Regex.Replace(entry, @"((\d+)(?:.\d)|\d+)", "#");
                if (Settings.DebugSettings.DebugDebuffs) DebugWindow.LogMsg(entry);

                var filterentry = GetEntry(downside);

                //FilterEntry filterentry = FilterList.FirstOrDefault(element => element.Mod.Contains(downside));
                if (filterentry == null) continue;

                DownsideFilterEntryMatches.Add(filterentry);
                if (Settings.DebugSettings.DebugDebuffs) DebugWindow.LogMsg($"Bad Mod: {filterentry.Mod}  | Weight {filterentry.Weight}");
            }

            Selection selection = new()
            {
                Upsides = upsides,
                Downsides = downsides,
                Target = Target,

                UpsideWeight = (UpsideFilterEntryMatches.Count > 0) ? UpsideFilterEntryMatches.Sum(x => x.Weight) : 0,
                DownsideWeight = (DownsideFilterEntryMatches.Count > 0) ? DownsideFilterEntryMatches.Sum(x => x.Weight) : 0,
                BuffGood = (UpsideFilterEntryMatches.FirstOrDefault(x => x.IsUpside) != null),
                DebuffGood = (DownsideFilterEntryMatches.FirstOrDefault(x => x.IsUpside) != null),
                PlayAlert = (UpsideFilterEntryMatches.FirstOrDefault(x => x.Alert == true) != null) || (DownsideFilterEntryMatches.FirstOrDefault(x => x.Alert == true) != null)
            };

            return selection;
        }




        public FilterEntry GetEntry(string mod)
        {
            var modWeight = Settings.GetModTier(mod);

            var modAlert = Settings.GetModAlert(mod);

            var modName = mod.Contains('(') && mod.Contains(')') ?
                            Regex.Replace(mod, @"\([^()]*\)", "#") :
                            Regex.Replace(mod, @"(\d+)(?:.\d)|\d+", "#");

            var modType = AltarModsConstants.AltarTypes.FirstOrDefault(t => t.Id.Contains(mod, StringComparison.InvariantCultureIgnoreCase)).Type;


            FilterEntry filter = new()
            {
                        Mod = modName,
                        Weight = modWeight,
                        IsUpside = modWeight > 0? true : false,
                        Target = modType == null ?
                AffectedTarget.Any :
                AltarModsConstants.FilterTargetDict[modType],
                Alert = modAlert
            };
            return filter;
        }

        public class FilterEntry
        {
            public string Mod { get; set; }
            public int Weight { get; set; }
            public AffectedTarget Target { get; set; }
            public bool IsUpside { get; set; }
            public bool? Alert { get; set; } = false;
        }


        public class Altar
        {
            public Selection Top { get; set; }
            public Selection Bottom { get; set; }
            public Altar(Selection top, Selection bottom)
            {
                Top = top;
                Bottom = bottom;
            }
        }

        public class Selection
        {
            public AffectedTarget Target { get; set; }
            public List<string> Downsides { get; set; }
            public List<string> Upsides { get; set; }
            public int UpsideWeight { get; set; }
            public int DownsideWeight { get; set; }
            public bool BuffGood { get; set; }
            public bool DebuffGood { get; set; }
            public bool PlayAlert { get; set; } = false;

        }
    }
}