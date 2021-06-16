using PropertyGridHelpers.Attributes;
using System;

/// <summary>
///Enum for selecting what scrollbar will show in the control
/// </summary>
[Flags]
public enum ScrollBars
{
    /// <summary>
    /// The none
    /// </summary>
    [EnumText("None")]
    None = 0,
    /// <summary>
    /// The first entry
    /// </summary>
    [EnumText("Vertical Scrollbar")]
    Vertical = 1,
    /// <summary>
    /// The second entry
    /// </summary>
    [EnumText("Horizontal Scrollbar")]
    Horizontal = 2,
    /// <summary>
    /// All entries
    /// </summary>
    [EnumText("Both")]
    Both = Vertical + Horizontal,
}
