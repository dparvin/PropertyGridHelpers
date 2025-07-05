#if NET5_0_OR_GREATER
#nullable enable
#endif

// Ignore Spelling: Parsable

using System;

namespace PropertyGridHelpers.Converters
{
    /// <summary>
    /// Defines a contract for parsing a string representation into an instance of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type that implements the parsing logic.</typeparam>
    /// <remarks>
    /// This interface provides a way to support round-tripping between string values
    /// and strongly typed values, allowing custom parsing logic to be used in property grids,
    /// type converters, or other serialization scenarios. It is similar in purpose to
    /// the .NET built-in <c>IParsable&lt;T&gt;</c> introduced in .NET 7+, but can be used
    /// on earlier frameworks to maintain consistency across multiple target frameworks.
    /// </remarks>
    /// <example>
    /// <code>
    /// public class MyType : IParsable&lt;MyType&gt;
    /// {
    ///     public string Name { get; set; }
    ///     
    ///     public MyType Parse(string s, IFormatProvider? provider = null)
    ///     {
    ///         return new MyType { Name = s };
    ///     }
    /// }
    /// </code>
    /// </example>
    public interface IParsable<T>
    {
        /// <summary>
        /// Converts the specified string representation into an instance of type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="s">The input string to parse.</param>
        /// <param name="provider">
        /// An optional format provider, which can be used to apply culture-specific or
        /// format-specific parsing rules.
        /// </param>
        /// <returns>
        /// An instance of <typeparamref name="T"/> that represents the parsed string.
        /// </returns>
#if NET5_0_OR_GREATER
        T Parse(string s, IFormatProvider? provider = null);
#else
        T Parse(string s, IFormatProvider provider = null);
#endif
    }
}
