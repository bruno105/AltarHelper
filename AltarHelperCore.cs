using ExileCore;
using ExileCore.PoEMemory.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AltarHelper
{
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

    public class AltarHelperCore : BaseSettingsPlugin<Settings>
    {

        private const string FILTER_FILE = "Filter.txt";
        public List<Filter> FilterList = new List<Filter>();
        //public Altar altar = new Altar();

        public override bool Initialise()
        {
            Name = "AltarHelper";
            Settings.RefreshFile.OnPressed += () => { ReadFilterFile(); };
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
            foreach (string line in lines)
            {
                if (line.Length < 4) continue;
                if (line.Contains("//")) continue;
                if (line.Contains("#Good"))
                {
                    DebugWindow.LogMsg("Entrou Good");
                    good = true;
                    continue;
                }
                if (line.Contains("#Bad"))
                {
                    DebugWindow.LogMsg("Entrou Bad");
                    good = false;
                    continue;
                }

                string[] splitedLine = line.Split('|');
                Filter f = new Filter();
                f.Mod = splitedLine[0].Length > 0 ? splitedLine[0] :  "-1";
                f.Weight = splitedLine[1].Length > 0 ? Int32.Parse(splitedLine[1]) : -1;
                f.Choice = splitedLine[2].Length > 0 ? splitedLine[2] : "Any";
                f.Good = good;

                if (f.Mod == "-1" || f.Weight == -1) continue;

                if (f.Mod.Contains(")") && !f.Mod.Contains("chance to be Duplicated"))
                {
                    f.Mod = f.Mod.Substring(f.Mod.IndexOf(")") + 1);
                }
                else if(f.Mod.Contains("chance to be Duplicated") && f.Mod.Contains("("))
                {
                    f.Mod = f.Mod.Substring(0, f.Mod.IndexOf("("));
                }

                if (f.Mod.Contains("Scarabs")) f.Mod = f.Mod.Replace("Scarabs", "Scarab");
                if (f.Mod.Contains("Items")) f.Mod = f.Mod.Replace("Items", "Item");
                if (f.Mod.Contains("Gems")) f.Mod = f.Mod.Replace("Gems", "Gem");

                // f.Mod = f.Mod.Trim();

                FilterList.Add(f);


            }


            FilterList.OrderBy(x => x.Weight);

        }
        public override void Render()
        {
            if(GameController.Area.CurrentArea.IsHideout ||
                GameController.Area.CurrentArea.IsTown)
            {
                return;
            }
            
            foreach(var label in GameController.IngameState.IngameUi.ItemsOnGroundLabelsVisible)
            {


    

                if (!label.ItemOnGround.Metadata.Contains("TangleAltar") && !label.ItemOnGround.Metadata.Contains("FireAltar")) continue;
             
                //DebugWindow.LogError($"Entrou");
                var upper = label.Label?.GetChildAtIndex(0);
                var downer = label.Label?.GetChildAtIndex(1);
                if (upper == null || downer == null) continue;

                string upperText = upper.GetChildAtIndex(1)?.Text;
                string downerText = downer.GetChildAtIndex(1)?.Text;


                if (upperText == null || downerText == null) continue;


                Altar altar = getAltarData(upperText, downerText);

                if(Settings.Debug == true)
                {
                    DebugWindow.LogError($"AltarUpperBuff {altar.Upper.Buff} | AltarUpperDebuff {altar.Upper.Debuff}");
                    DebugWindow.LogError($"AltarUpperBuffWheight {altar.Upper.BuffWeight} | AltarUpperDebuffWeight {altar.Upper.DebuffWeight}");

                    DebugWindow.LogError($"AltarDownerBuff {altar.Downer.Buff} | AltarDownerDebuff {altar.Upper.Debuff}");
                    DebugWindow.LogError($"AltarDownerBuffWheight {altar.Downer.BuffWeight} | AltarDownerDebuffWeight {altar.Downer.DebuffWeight}");
                }
                


                if (altar == null) continue;

                if(altar.Upper.BuffWeight > altar.Downer.BuffWeight)
                {
                   if(altar.Upper.BuffGood) Graphics.DrawFrame(upper.GetClientRectCache, Settings.GoodColor, Settings.FrameThickness);
                   if(altar.Upper.DebuffWeight - altar.Upper.BuffWeight > 0) Graphics.DrawFrame(upper.GetClientRectCache, Settings.BadColor, Settings.FrameThickness);
                }
                else
                {
                   if(altar.Downer.BuffGood) Graphics.DrawFrame(downer.GetClientRectCache, Settings.GoodColor, Settings.FrameThickness);
                   if(altar.Downer.DebuffWeight - altar.Downer.BuffWeight > 0) Graphics.DrawFrame(downer.GetClientRectCache, Settings.BadColor, Settings.FrameThickness);
                }


            


            }
            
            base.Render();
        }

        public override Job Tick()
        {

            return base.Tick();
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

            int start = selecterText.IndexOf("{") + 1;
            int len = selecterText.IndexOf("}") - start;
            string selecterChoice = selecterText.Substring(start, len);

            start = selecterText.IndexOf("}") + 1;
            len = selecterText.LastIndexOf("<") - start;
            string selecterDebuff = selecterText.Substring(start, len).Trim();

            start = selecterText.LastIndexOf("{") + 1;
            len = selecterText.LastIndexOf("}") - start;
            string selecterBuff = selecterText.Substring(start, len).Trim();


            selecterDebuff = Regex.Replace(selecterDebuff, @"[\d-]", "|");
            selecterDebuff = selecterDebuff.Contains("chance to be Duplicated") ? selecterDebuff.Substring(0, selecterDebuff.IndexOf("|")) : selecterDebuff.Substring(selecterDebuff.LastIndexOf("|") + 1);

            selecterBuff = Regex.Replace(selecterBuff, @"[\d-]", "|");
            selecterBuff = selecterBuff.Contains("chance to be Duplicated") ? selecterBuff.Substring(0,selecterBuff.IndexOf("|")) : selecterBuff.Substring(selecterBuff.LastIndexOf("|") + 1);


            if (selecterBuff.Contains("Scarabs")) selecterBuff =  selecterBuff.Replace("Scarabs", "Scarab");
            
            if (selecterBuff.Contains("Items")) selecterBuff =  selecterBuff.Replace("Items", "Item");
            
            if (selecterBuff.Contains("Gems")) selecterBuff = selecterBuff.Replace("Gems", "Gem");


            if (Settings.Debug == true)
            {
                DebugWindow.LogError($"SearchBuffer {selecterBuff} | SearchDebuff {selecterDebuff}");
            }




            Filter f1 = FilterList.FirstOrDefault(x => x.Mod.Contains(selecterBuff));
            Filter f2 = FilterList.FirstOrDefault(x => x.Mod.Contains(selecterDebuff));

            //DebugWindow.LogError(selecterDebuff);
            //foreach(Filter f in FilterList)
            //{
            //    DebugWindow.LogError(f.Mod);
            //    if (f.Mod.Contains(selecterDebuff.Trim())) DebugWindow.LogError("ENTROU");
            //}

            Select s = new Select();
            s.Buff = selecterBuff;
            s.Debuff = selecterDebuff;
            s.Choice = selecterChoice;
            s.BuffWeight = (f1 != null) ? f1.Weight : -1;
            s.DebuffWeight = (f2 != null) ? f2.Weight: -1;
            s.BuffGood = (f1 != null) ? f1.Good : false;
            s.DebuffGood = (f2 != null) ? f2.Good : false;



            
            

            return  s;
        }
    }
}
