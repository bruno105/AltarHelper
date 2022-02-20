using System.Windows.Forms;
using ExileCore.Shared.Attributes;
using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;

namespace AltarHelper
{
    public class Settings : ISettings
    {


        public ToggleNode Enable { get; set; } = new ToggleNode(false);

        public ToggleNode Debug { get; set; } = new ToggleNode(false);
        public ButtonNode RefreshFile { get; set; } = new ButtonNode();
        public ColorNode GoodColor { get; set; } = new ColorNode(SharpDX.Color.LightGreen);
        public ColorNode BadColor { get; set; } = new ColorNode(SharpDX.Color.Red);
        public RangeNode<int> FrameThickness { get; set; } = new RangeNode<int>(2, 1, 5);







    }
}
