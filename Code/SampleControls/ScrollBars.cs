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
        /// The Virtical Scrollbar
        /// </summary>
        [EnumText("Vertical Scrollbar")]
        Vertical = 1,
        /// <summary>
        /// The Horizontal Scrollbar
        /// </summary>
        [EnumText("Horizontal Scrollbar")]
        Horizontal = 2,
        /// <summary>
        /// Both a horizontal scrollbar and a virtical scrollbar
        /// </summary>
        [EnumText("Both")]
        Both = Vertical + Horizontal,
    }
}
