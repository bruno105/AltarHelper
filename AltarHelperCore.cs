using ExileCore;
using ExileCore.PoEMemory.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using SharpDX;
using ExileCore.Shared.SomeMagic;

namespace AltarHelper
{
    public class AltarHelperCore : BaseSettingsPlugin<Settings>
    {
        private const string FILTER_FILE = "Filter.txt";
        public List<Filter> FilterList = new List<Filter>();
        //public Altar altar = new Altar();

        public override bool Initialise()
        {
            Name = "AltarHelper";
            Settings.AltarSettings.RefreshFile.OnPressed += () => { ReadFilterFile(); };
            ReadFilterFile();
    
            return base.Initialise();
        }
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
            
            string[] lines = System.IO.File.ReadAllLines($"{DirectoryFullName}\\{FILTER_FILE}");
            bool good = false;
            List<string> localList = new List<string>();
            localList.Clear();
            Settings.FilterList.ListFilter.SetListValues(localList);

            foreach (string line in lines)
            {
                if (line.Length < 4) continue;
                if (line.Contains("/")) continue;
                if (line.Contains("#Good"))
                {
                    DebugWindow.LogMsg("Entrou Good");
                    good = true;
                    continue;
                }
                if (line.Trim() == "#Bad")
                {
                    DebugWindow.LogMsg("Entrou Bad");
                    good = false;
                    continue;
                }

                string[] splitedLine = line.Split('|');
                Filter f = new Filter();

                f.Mod = splitedLine[0].Length > 0 ? splitedLine[0] : "-1";
                f.Weight = splitedLine[1].Length > 0 ? Int32.Parse(splitedLine[1]) : 0;

                if (splitedLine.Length == 2)
                {
                    f.Choice = "Any";
                }
                else
                {
                    f.Choice = splitedLine[2].Length > 0 ? splitedLine[2] : "Any";
                }

                f.Good = good;

                if (f.Mod == "-1") continue;

                f.Mod = splitedLine[0].Contains("(") && splitedLine[0].Contains(")") ? Regex.Replace(splitedLine[0], @"\([^()]*\)", "#"): Regex.Replace(splitedLine[0], @"(\d+)(?:.\d)|\d+", "#");
              

                FilterList.Add(f);
                localList.Add(string.Format($"{f.Mod} | {f.Weight} |  Good? {f.Good}"));

            }

            Settings.FilterList.ListFilter.SetListValues(localList);
            FilterList.OrderBy(x => x.Weight);

        }
        public override void Render()
        {
            if (GameController.Area.CurrentArea.IsHideout ||
                GameController.Area.CurrentArea.IsTown || GameController.IngameState.IngameUi == null || GameController.IngameState.IngameUi.ItemsOnGroundLabelsVisible == null)
            {
                return;
            }


            if (Settings.AltarSettings.HotkeyMode.PressedOnce())
            {
                Settings.AltarSettings.SwitchMode.Value += 1;
                if (Settings.AltarSettings.SwitchMode.Value == 4) Settings.AltarSettings.SwitchMode.Value = 1;
                switch (Settings.AltarSettings.SwitchMode.Value)
                {
                    case 1:
                        DebugWindow.LogMsg("Changed to Any Choice");
                        break;
                    case 2:
                        DebugWindow.LogMsg("Changed to only Minions and Player Choices");
                        break;
                    case 3:
                        DebugWindow.LogMsg("Changed to only bosses and Players Choices");
                        break;


                }


            }



            foreach (var label in GameController.IngameState.IngameUi.ItemsOnGroundLabelsVisible)
            {

                if (label == null || label.Label == null) continue;
                if (label.ItemOnGround == null || label.ItemOnGround.Metadata == null) continue;


                var Altar = label.ItemOnGround?.Metadata;

                if (Altar == null || (!Altar.Contains("TangleAltar") && !Altar.Contains("FireAltar"))) continue;


                var upper = label.Label?.GetChildAtIndex(0);
                var downer = label.Label?.GetChildAtIndex(1);
                if (upper == null || downer == null) continue;

                string? upperText = upper.GetChildAtIndex(1)?.GetText(512).Trim();
                string? downerText = downer.GetChildAtIndex(1)?.GetText(512).Trim();

                if (Settings.DebugSettings.DebugDrawners == true) DebugWindow.LogError($"AltarDowner Lenght 512 : {downerText}");
                if (Settings.DebugSettings.DebugDrawners == true) DebugWindow.LogError($"AltarUpper Lenght 512 : {upperText}");
                if (upperText == null || downerText == null) continue;


                // continue;
                //if (upperText.Contains("Gain Projectiles are fired in random directions") || downerText.Contains("Gain Projectiles are fired in random directions")) DebugWindow.LogError("PROJECTILEESSSSSSSSS");
                Altar altar = getAltarData(upperText, downerText);




                if (altar == null) continue;
                
                int UpperWeight = 0;
                int DownerWeight = 0;


                if (altar.Upper.BuffWeight == 0 && altar.Downer.BuffWeight == 0 && altar.Downer.DebuffWeight == 0 && altar.Upper.DebuffWeight == 0 ) continue;



                if (Settings.AltarSettings.SwitchMode.Value == 2)
                {
                    if (altar.Upper.Choice.Contains("Minion") || altar.Upper.Choice.Contains("Player"))
                    {
                        UpperWeight += altar.Upper.BuffWeight - altar.Upper.DebuffWeight;
                    }
                    if (altar.Downer.Choice.Contains("Minion") || altar.Downer.Choice.Contains("Player"))
                    {
                        DownerWeight += altar.Downer.BuffWeight - altar.Downer.DebuffWeight;
                    }
                }
                else if (Settings.AltarSettings.SwitchMode.Value == 3)
                {
                    if (altar.Upper.Choice.Contains("boss") || altar.Upper.Choice.Contains("Player"))
                    {
                        UpperWeight += altar.Upper.BuffWeight - altar.Upper.DebuffWeight;
                    }
                    if (altar.Downer.Choice.Contains("boss") || altar.Downer.Choice.Contains("Player"))
                    {
                        DownerWeight += altar.Downer.BuffWeight - altar.Downer.DebuffWeight;
                    }

                }
                else
                {
                    UpperWeight += altar.Upper.BuffWeight - altar.Upper.DebuffWeight;
                    DownerWeight += altar.Downer.BuffWeight - altar.Downer.DebuffWeight;
                }

            
                if (altar.Upper.Choice.Contains("Minion")) UpperWeight += Settings.AltarSettings.MinionWeight.Value;
                if (altar.Upper.Choice.Contains("boss")) UpperWeight += Settings.AltarSettings.BossWeight.Value;
                if (altar.Downer.Choice.Contains("Minion")) DownerWeight += Settings.AltarSettings.MinionWeight.Value;
                if (altar.Downer.Choice.Contains("boss")) DownerWeight += Settings.AltarSettings.BossWeight.Value;



                if (Settings.DebugSettings.DebugWeight == true)
                {
                    Graphics.DrawText(UpperWeight.ToString(), new System.Numerics.Vector2(upper.GetClientRectCache.Center.X-10, upper.GetClientRectCache.Top-25),Color.Cyan);
                    Graphics.DrawText(DownerWeight.ToString(), new System.Numerics.Vector2(downer.GetClientRectCache.Center.X-10, downer.GetClientRectCache.Bottom+15),Color.Cyan);
                  //  DebugWindow.LogError($"UpperWeight: {UpperWeight} | DownerWeight: {DownerWeight}");
                }




                if (UpperWeight < 0 || DownerWeight < 0)
                {
                    if (UpperWeight < 0) Graphics.DrawFrame(upper.GetClientRectCache, Settings.AltarSettings.BadColor, Settings.AltarSettings.FrameThickness);
                    if (DownerWeight < 0) Graphics.DrawFrame(downer.GetClientRectCache, Settings.AltarSettings.BadColor, Settings.AltarSettings.FrameThickness);
                    //continue;
                }



                if (UpperWeight >= 0 || DownerWeight >= 0)
                {                    
                    if (UpperWeight >= DownerWeight && UpperWeight > 0) Graphics.DrawFrame(upper.GetClientRectCache, getColor(altar.Upper.Choice), Settings.AltarSettings.FrameThickness);
                    if (DownerWeight > UpperWeight && DownerWeight > 0) Graphics.DrawFrame(downer.GetClientRectCache, getColor(altar.Downer.Choice), Settings.AltarSettings.FrameThickness);
                    continue;
                }


            }


        }

        public override Job Tick()
        {

            return base.Tick();
        }

        public SharpDX.Color getColor(string choice)
        {

            SharpDX.Color c = SharpDX.Color.Transparent;

            if (choice.ToUpper().Contains("MINION")) return Settings.AltarSettings.MinionColor;
            if (choice.ToUpper().Contains("BOSS")) return Settings.AltarSettings.BossColor;
            if (choice.ToUpper().Contains("PLAYER")) return Settings.AltarSettings.PlayerColor;

            return c;

        }



        public Altar getAltarData(string upperText, string downerText)
        {
            Altar a = new Altar();
            Select sUpper = new Select();
            Select sDowner = new Select();

            sUpper = getSelectData(upperText);
            sDowner = getSelectData(downerText);
            a.Downer = sDowner;
            a.Upper = sUpper;
            return a;
        }


        public Select getSelectData(string selecterText)
        {

            //Spliting the Object Text
            string selecterChoice = Regex.Matches(selecterText, @"(?<={)(\n?.*)(?=})")[0].ToString();


            int start = selecterText.IndexOf(":}") + 2;
            int len = selecterText.IndexOf("<enchanted") - start;
            string selecterDebuff = selecterText.Substring(start, len).Trim();


            //End Spliting


            //sorting out the debuffs
            List<string> debuffs = new List<string>();
            //sorting out the buffs
            List<string> buffs = new List<string>();

            debuffs.Clear();


            if (selecterDebuff.Split('\n').Count() > 0)
            {
                foreach (string d in selecterDebuff.Split('\n'))
                {

                    string debugProcessed = Regex.Replace(d, @"((\d+)(?:.\d)|\d+)", "#").Replace("}","").Replace("{", ""); ;

                    debuffs.Add(debugProcessed);

                }
            }
            else
            {
                debuffs.Add(selecterDebuff);
            }

            //sorting out End




            //sorting out Buffs Strings
            string selecterBuff = Regex.Matches(selecterText, @"(?<=<enchanted>{)([^.]+.*?)(?=})")[0].ToString();


            if (selecterBuff.Split('\n').Count() > 0)
            {
                foreach (string d in selecterBuff.Split('\n'))
                {
                    string debugProcessed = Regex.Replace(d, @"((\d+)(?:.\d)|\d+)", "#").Replace("}", "").Replace("{", "");                    

                    buffs.Add(debugProcessed);
                    

                }
            }
            else
            {
                buffs.Add(selecterBuff);
            }




            // Checking buffs on Filter List
            List<Filter> f1List = new List<Filter>();
            // Checking debuffs on Filter List
            List<Filter> f2List = new List<Filter>();

            foreach (string y in debuffs)
            {
                if (Settings.DebugSettings.DebugDebuffs) DebugWindow.LogError(y);
                Filter f = FilterList.FirstOrDefault(x => x.Mod.Contains(y));
                if (f != null)
                {
                    f2List.Add(f);
                    if (Settings.DebugSettings.DebugDebuffs) DebugWindow.LogMsg($"Bad Mod: {f.Mod}  | Weight {f.Weight}");
                }

            }

            foreach (string y in buffs)
            {
                string buff = y;
                buff = Regex.Replace(buff, @"((\d+)(?:.\d)|\d+)", "#");

                if (Settings.DebugSettings.DebugBuffs) DebugWindow.LogError(y);
                Filter f = FilterList.FirstOrDefault(x => x.Mod.Contains(buff));
                if (f != null)
                {
                    f1List.Add(f);
                    if (Settings.DebugSettings.DebugBuffs) DebugWindow.LogMsg($"Good Mod: {f.Mod}  | Weight {f.Weight}");
                }

            }
            //End Check of debuffs




            Select s = new Select();
            s.Buff = selecterBuff;
            s.Debuff = selecterDebuff;
            s.Choice = selecterChoice;
            s.BuffWeight = (f1List.Count > 0) ? f1List.Sum(x => x.Weight) : 0;
            s.DebuffWeight = f2List.Sum(x => x.Weight);
            s.BuffGood = (f1List.FirstOrDefault(x => x.Good == false) != null) ? true : false;
            s.DebuffGood = (f2List.FirstOrDefault(x => x.Good == false) != null) ? true : false;



            return s;
        }


        public class Filter
        {
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
}