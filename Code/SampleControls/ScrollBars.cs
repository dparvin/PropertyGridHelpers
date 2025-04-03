using PropertyGridHelpers.Attributes;
using System;

namespace SampleControls
{
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
        /// The Vertical Scrollbar
        /// </summary>
        [LocalizedEnumText("Vertical_Scrollbar")]
        Vertical = 1,
        /// <summary>
        /// The Horizontal Scrollbar
        /// </summary>
        [LocalizedEnumText("Horizontal_Scrollbar")]
        Horizontal = 2,
        /// <summary>
        /// Both a horizontal scrollbar and a vertical scrollbar
        /// </summary>
        [EnumText("Both")]
        Both = Vertical + Horizontal,
    }
}
