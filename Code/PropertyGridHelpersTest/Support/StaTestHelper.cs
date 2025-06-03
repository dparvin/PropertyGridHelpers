using System;
using System.Threading;
#if NET35
using Xunit;
#else
using Xunit.Sdk;
#endif

namespace PropertyGridHelpersTest.Support
{
    /// <summary>
    /// Helper class for running tests in a single-threaded apartment (STA) state.
    /// </summary>
    public static class StaTestHelper
    {
#if NET35
        /// <summary>
        /// Runs the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
#else
        /// <summary>
        /// Runs the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <exception cref="XunitException">STA test failed</exception>
#endif
        public static void Run(Action action)
        {
            Exception exception = null;
            var thread = new Thread(() =>
            {
                try
                {
                    action();
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            if (exception != null)
#if NET35
                Assert.True(false, $"STA test failed: {exception}");
#else
                throw new XunitException("STA test failed", exception);
#endif
        }
    }
}
