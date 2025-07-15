using System;
using System.ComponentModel;

namespace PropertyGridHelpers.Enums
{
    /// <summary>
    /// Specifies the intended usage of a resource path in a localization or UI editing context.
    /// </summary>
    /// <remarks>
    /// This enum is primarily used by attributes like <see cref="Attributes.ResourcePathAttribute"/> and 
    /// <see cref="Attributes.DynamicPathSourceAttribute"/> to differentiate between various kinds of resources
    /// (e.g., strings for display, images for dropdowns, cursors for styling, etc.).
    /// 
    /// It supports bitwise flags, allowing multiple usage types to be combined when the resource path
    /// is applicable to more than one purpose.
    /// </remarks>
    [Flags]
    public enum ResourceUsage
    {
        /// <summary>
        /// No usage specified. This is an invalid value and should not be passed to any lookup method.
        /// </summary>
        /// <remarks>
        /// Most helper functions will throw an <see cref="ArgumentException"/> if <c>None</c> is passed.
        /// </remarks>
        [EditorBrowsable(EditorBrowsableState.Never)]
        None = 0,

        /// <summary>
        /// Indicates the resource path is used for localized strings (e.g., enum display names, category labels).
        /// </summary>
        Strings = 1 << 0,

        /// <summary>
        /// Indicates the resource path is used for image resources (e.g., dropdown icons, UI previews).
        /// </summary>
        Images = 1 << 1,

        /// <summary>
        /// Indicates the resource path is used for custom cursors.
        /// </summary>
        Cursors = 1 << 2,

        /// <summary>
        /// Indicates the resource path is used for icon resources (e.g., window icons, small UI glyphs).
        /// </summary>
        Icons = 1 << 3,
        // Extend as needed...

        /// <summary>
        /// Represents all supported resource usages. Can be used as a fallback or default match.
        /// </summary>
        All = Strings | Images | Cursors | Icons
    }
}
