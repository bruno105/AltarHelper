using ExileCore.Shared.Attributes;
using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;
using SharpDX;
using System.Windows.Forms;
namespace AltarHelper
{
    
    public class Settings : ISettings
    {

        public ToggleNode Enable { get; set; } = new ToggleNode(false);

        public ToggleNode Debug { get; set; } = new ToggleNode(false);
        public AltarSettings AltarSettings { get; set; } = new AltarSettings();

    }
    [Submenu]
    public class AltarSettings 
    {

        public ButtonNode RefreshFile { get; set; } = new ButtonNode();
        public RangeNode<int> FrameThickness { get; set; } = new RangeNode<int>(2, 1, 5);
        public ColorNode MinionColor { get; set; } = new ColorNode(SharpDX.Color.LightGreen);
        public ColorNode PlayerColor { get; set; } = new ColorNode(SharpDX.Color.LightCyan);
        public ColorNode BossColor { get; set; } = new ColorNode(SharpDX.Color.LightBlue);
        public ColorNode BadColor { get; set; } = new ColorNode(SharpDX.Color.Transparent);
        [Menu("Switch Mode", "1 = Any | 2 =  Only Minions and Player | 3 = Only Boss and Players ")]
        public RangeNode<int> SwitchMode { get; set; } = new RangeNode<int>(1, 1, 3); // Any | Only Minions and Player | Only Boss and Player
        [Menu("Minion Weight", "Add this value to minions mod Type")]
        public RangeNode<int> MinionWeight { get; set; } = new RangeNode<int>(10, 1, 10);
        [Menu("Boss Weight", "Add this value to boss mod Type")]
        public RangeNode<int> BossWeight { get; set; } = new RangeNode<int>(1, 1, 10);
        public HotkeyNode HotkeyMode { get; set; } = new HotkeyNode(Keys.F7);
    }
}
