using ExileCore.Shared.Attributes;
using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace AltarHelper;
[SupportedOSPlatform("windows")]
public class Settings : ISettings
{
    public ToggleNode Enable { get; set; } = new ToggleNode(false);
    public AltarSettings AltarSettings { get; set; } = new AltarSettings();
    public FilterList FilterList { get; set; } = new FilterList();
    public DebugSettings DebugSettings { get; set; } = new DebugSettings();
}

[Submenu]
public class AltarSettings
{
    public ButtonNode RefreshFile { get; set; } = new ButtonNode();
    public RangeNode<int> FrameThickness { get; set; } = new RangeNode<int>(2, 1, 5);
    public ColorNode MinionColor { get; set; } = new ColorNode(SharpDX.Color.LightGreen);
    public ColorNode PlayerColor { get; set; } = new ColorNode(SharpDX.Color.LightCyan);
    public ColorNode BossColor { get; set; } = new ColorNode(SharpDX.Color.LightBlue);
    public ColorNode BadColor { get; set; } = new ColorNode(SharpDX.Color.Red);
    public ToggleNode ConsiderPlayerChoices { get; set; } = new ToggleNode(true);
    public ToggleNode ConsiderMinionChoices { get; set; } = new ToggleNode(true);
    public ToggleNode ConsiderBossChoices { get; set; } = new ToggleNode(true);
}

[Submenu]
public class FilterList
{
    public ListNode ListFilter { get; set; } = new ListNode();
}

[Submenu]
public class DebugSettings
{
    public ToggleNode DebugAltarText { get; set; } = new ToggleNode(false);
    public ToggleNode DebugBuffs { get; set; } = new ToggleNode(false);
    public ToggleNode DebugDebuffs { get; set; } = new ToggleNode(false);
    public ToggleNode DebugWeight { get; set; } = new ToggleNode(false);
}