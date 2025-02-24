using System;
using System.Diagnostics;
using System.Reflection;
using Xunit;

namespace PropertyGridHelpersTest.Support
{
    /// <summary>
    /// Test for the FindPropertyForAttribute method
    /// </summary>
    public class FindPropertyForAttributeTest
    {
        private static StackFrame GetCurrentStackFrame() => new StackTrace().GetFrame(0);

        /// <summary>
        /// Finds the property for attribute should return null when declaring type is null.
        /// </summary>
        [Fact]
        public void FindPropertyForAttribute_ShouldReturnNull_WhenDeclaringTypeIsNull()
        {
            // Arrange
            var frame = new StackFrame(0, false); // No method info in frame

            // Act
            var result = CallFindPropertyForAttribute(frame);

            // Assert
            Assert.Null(result);
        }

        /// <summary>
        /// Finds the property for attribute should return null when no matching property exists.
        /// </summary>
        [Fact]
        public void FindPropertyForAttribute_ShouldReturnNull_WhenNoMatchingPropertyExists()
        {
            // Arrange
            var frame = GetStackFrameFromMethod(typeof(TestClassWithoutAttributes), nameof(TestClassWithoutAttributes.SomeMethod));

            // Act
            var result = CallFindPropertyForAttribute(frame);

            // Assert
            Assert.Null(result);
        }

        /// <summary>
        /// Finds the property for attribute null frame returns null.
        /// </summary>
        [Fact]
        public void FindPropertyForAttribute_NullFrame_ReturnsNull()
        {
            // Act
            var result = PropertyGridHelpers.Support.Support.FindPropertyForAttribute(null);

            // Assert
            Assert.Null(result);
        }

        /// <summary>
        /// Helper method to call private static method via reflection
        /// </summary>
        /// <param name="frame">The frame.</param>
        /// <returns></returns>
        private static PropertyInfo CallFindPropertyForAttribute(
            StackFrame frame)
        {
            var method = typeof(PropertyGridHelpers.Support.Support).GetMethod("FindPropertyForAttribute", BindingFlags.NonPublic | BindingFlags.Static);
            return (PropertyInfo)method.Invoke(null, new object[] { frame });
        }


        /// <summary>
        /// Helper class without ResourcePathAttribute on properties
        /// </summary>
        private class TestClassWithoutAttributes
        {
            /// <summary>
            /// Gets or sets some property.
            /// </summary>
            /// <value>
            /// Some property.
            /// </value>
            public string SomeProperty
            {
                get; set;
            }

            /// <summary>
            /// Some method to test
            /// </summary>
            public void SomeMethod() =>
                CallFindPropertyForAttribute(GetCurrentStackFrame());
        }

        /// <summary>
        /// Helper to get stack frame from a specific method
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <returns></returns>
        private static StackFrame GetStackFrameFromMethod(
            Type type,
            string methodName)
        {
            var instance = Activator.CreateInstance(type); // Create instance of the type (if needed)
            var method = type.GetMethod(methodName);

            method?.Invoke(instance, null); // Invoke the method so it appears in the stack trace

            return new StackTrace(true).GetFrame(1); // Capture the caller's stack frame
        }
    }
}
