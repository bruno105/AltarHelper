using ExileCore.Shared.Attributes;
using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;
using System.Runtime.Versioning;
using System.Windows.Forms;
namespace AltarHelper
{
    [SupportedOSPlatform("windows")]
    public class Settings : ISettings
    {
       
        public ToggleNode Enable { get; set; } = new ToggleNode(false);
        public AltarSettings AltarSettings { get; set; } = new AltarSettings();
        public FilterList FilterList { get; set; } = new FilterList();
        public DebugSettings DebugSettings { get; set; } = new DebugSettings();

    }
    [Submenu]
    [SupportedOSPlatform("windows")]
    public class AltarSettings
    {
       
        public ButtonNode RefreshFile { get; set; } = new ButtonNode();
        public RangeNode<int> FrameThickness { get; set; } = new RangeNode<int>(2, 1, 5);
        public ColorNode MinionColor { get; set; } = new ColorNode(SharpDX.Color.LightGreen);
        public ColorNode PlayerColor { get; set; } = new ColorNode(SharpDX.Color.LightCyan);
        public ColorNode BossColor { get; set; } = new ColorNode(SharpDX.Color.LightBlue);
        public ColorNode BadColor { get; set; } = new ColorNode(SharpDX.Color.Red);
        [Menu("Switch Mode", "1 = Anny | 2 =  Only Minions and Player | 3 = Only Boss and Players ")]
        public RangeNode<int> SwitchMode { get; set; } = new RangeNode<int>(1, 1, 3); // Any | Only Minions and Player | Only Boss and Player
        [Menu("Minion Weight", "Add this value to minions mod Type")]
        public RangeNode<int> MinionWeight { get; set; } = new RangeNode<int>(0, 0, 100);
        [Menu("Bosss Weight", "Add this value to boss mod Type")]
        public RangeNode<int> BossWeight { get; set; } = new RangeNode<int>(0, 0, 100);
        public HotkeyNode HotkeyMode { get; set; } = new HotkeyNode(Keys.F7);
    }
    [Submenu]
    [SupportedOSPlatform("windows")]
    public class FilterList
    {

        public ListNode ListFilter { get; set; } = new ListNode();

    }

    [Submenu]
    [SupportedOSPlatform("windows")]
    public class DebugSettings
    {
       
        public ToggleNode DebugRawText { get; set; } = new ToggleNode(false);
        public ToggleNode DebugBuffs { get; set; } = new ToggleNode(false);
        public ToggleNode DebugDebuffs { get; set; } = new ToggleNode(false);
        public ToggleNode DebugWeight { get; set; } = new ToggleNode(false);

    }

}