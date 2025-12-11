using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BetterTeleport;
public class WindowColourManager
{
    public enum Colours { DalamudDefault, ClearPurple, ClearBlue};

    public static readonly Dictionary<Colours, WindowColours> ColourData = new()
    {
        { Colours.DalamudDefault, new WindowColours { windowBackground = new Vector4(0.06f, 0.06f, 0.06f, 0.85f), windowTitles = new Vector4(0.25f, 0.25f, 0.25f, 1f)}},
        { Colours.ClearPurple,    new WindowColours { windowBackground = new Vector4(0.2f, 0.1f, 0.3f, 0.85f), windowTitles = new Vector4(0.4f, 0.2f, 0.6f, 1f)}},
        { Colours.ClearBlue,      new WindowColours { windowBackground = new Vector4(0.15f, 0.3f, 0.6f, 0.85f), windowTitles = new Vector4(0.41f, 0.55f, 0.85f, 0.9f)}}
    };
}

public class WindowColours
{
    public Vector4 windowBackground { get; set; }
    public Vector4 windowTitles { get; set; }
}
