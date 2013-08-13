namespace Headless.IntegrationTests
{
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The <see cref="Initialization" /> class is used to run assembly initialization work for the test assembly.
    /// </summary>
    [TestClass]
    public static class Initialization
    {
        /// <summary>
        ///     Stores the IIS Express reference entity.
        /// </summary>
        private static IisExpress _iisExpress;

        #region Setup/Teardown

        /// <summary>
        ///     Cleans up after running the unit tests in an assembly.
        /// </summary>
        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            _iisExpress.Dispose();
            _iisExpress = null;
        }

        /// <summary>
        ///     Initializes the assembly for running unit tests.
        /// </summary>
        /// <param name="context">
        ///     The context.
        /// </param>
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            var solutionDirectory = context.FindSolutionDirectory();
            var projectDirectory = Path.Combine(solutionDirectory, "Headless.DemoSite");
            var address = Config.BaseWebAddress;

            _iisExpress = new IisExpress();

            _iisExpress.Start(projectDirectory, address.Port, address);
        }

        #endregion
    }
}