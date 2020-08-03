using PropertyGridHelpers.Attributes;
using System;

/// <summary>
///
/// </summary>
[Flags]
public enum TestEnums
{
    /// <summary>
    /// The none
    /// </summary>
    [EnumText("None")]
    None = 0,
    /// <summary>
    /// The first entry
    /// </summary>
    [EnumText("First Entry")]
    FirstEntry = 1,
    /// <summary>
    /// The second entry
    /// </summary>
    [EnumText("Second Entry")]
    SecondEntry = 2,
    /// <summary>
    /// All entries
    /// </summary>
    [EnumText("All Entries")]
    AllEntries = FirstEntry + SecondEntry,
}
