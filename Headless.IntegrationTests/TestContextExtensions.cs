namespace Headless.IntegrationTests
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The <see cref="TestContextExtensions" /> class is used to provide extension methods to the <see cref="TestContext" /> class.
    /// </summary>
    public static class TestContextExtensions
    {
        /// <summary>
        /// Finds the solution directory.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// A <see cref="string"/> instance.
        /// </returns>
        public static string FindSolutionDirectory(this TestContext context)
        {
            string startPath;

            if (context != null)
            {
                startPath = context.TestDir;
            }
            else
            {
                startPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            }

            if (string.IsNullOrWhiteSpace(startPath))
            {
                throw new InvalidOperationException(
                    "No reference point was determined in order to search for the solution directory.");
            }

            var directory = new DirectoryInfo(startPath);

            while (directory.Exists)
            {
                var solutionFiles = directory.EnumerateFiles("*.sln", SearchOption.TopDirectoryOnly);

                if (solutionFiles.Any())
                {
                    // We have found the first parent directory that a the solution file
                    return directory.FullName;
                }

                if (directory.Parent == null)
                {
                    throw new InvalidOperationException("Failed to identify the solution directory.");
                }

                directory = directory.Parent;
            }

            throw new InvalidOperationException("Failed to identify the solution directory.");
        }
    }
}