namespace NuGetVersionSync
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Xml;
    using Neovolve.BuildTaskExecutor;
    using Neovolve.BuildTaskExecutor.Extensibility;
    using Neovolve.BuildTaskExecutor.Services;
    using Neovolve.BuildTaskExecutor.Tasks;

    /// <summary>
    ///     The <see cref="SyncNuGetVersionTask" />
    ///     class is used to sync the NuGet package version number with the assembly version number.
    /// </summary>
    [Export(typeof(ITask))]
    internal class SyncNuGetVersionTask : WildcardFileSearchTask
    {
        /// <summary>
        ///     The source binary path.
        /// </summary>
        private string _sourceBinaryPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="SyncNuGetVersionTask"/> class.
        /// </summary>
        /// <param name="eventWriter">
        /// The event writer.
        /// </param>
        /// <param name="binaryVersionReader">
        /// The binary version reader.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="eventWriter"/> parameter is <c>null</c>.
        /// </exception>
        /// <exception cref="System.ArgumentNullException">
        /// The <paramref name="binaryVersionReader"/> parameter is <c>null</c>.
        /// </exception>
        [ImportingConstructor]
        public SyncNuGetVersionTask(
            EventWriter eventWriter, 
            [Import("BinaryVersionManager")] IVersionManager binaryVersionReader) : base(eventWriter)
        {
            if (eventWriter == null)
            {
                throw new ArgumentNullException("eventWriter");
            }

            if (binaryVersionReader == null)
            {
                throw new ArgumentNullException("binaryVersionReader");
            }

            BinaryVersionReader = binaryVersionReader;
        }

        /// <summary>
        /// Determines whether the specified arguments are valid.
        /// </summary>
        /// <param name="arguments">
        /// The arguments for the task.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified arguments are valid; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsValidArgumentSet(IEnumerable<string> arguments)
        {
            var args = arguments.ToList();

            if (!base.IsValidArgumentSet(args))
            {
                return false;
            }

            var sourceBinaryPath = GetSourceBinaryPath(args);

            if (string.IsNullOrWhiteSpace(sourceBinaryPath))
            {
                return false;
            }

            return File.Exists(sourceBinaryPath);
        }

        /// <inheritdoc />
        protected override FileMatchResult FileMatchFound(string filePath)
        {
            var doc = new XmlDocument();

            doc.Load(filePath);

            var versionNode = doc.SelectSingleNode("/package/metadata/version");

            if (versionNode == null)
            {
                Writer.WriteMessage(TraceEventType.Error, "The file '{0}' does not contain a version node.", filePath);

                return FileMatchResult.FailTask;
            }

            var versionData = versionNode.InnerText;

            // The version data will be in the format D[(.D)1-3][-prereleaseTag]
            var expression = new Regex(@"(?<version>\d+(\.\d+){0,3})(?<suffix>.*)", RegexOptions.Singleline);
            var match = expression.Match(versionData);

            if (match.Success == false)
            {
                Writer.WriteMessage(
                    TraceEventType.Error, 
                    "The file '{0}' contains version information ({1}) in an unexpected format.", 
                    filePath, 
                    versionData);

                return FileMatchResult.FailTask;
            }

            var suffix = match.Groups["suffix"].Value;

            var binaryVersion = BinaryVersionReader.ReadVersion(_sourceBinaryPath);

            var newVersion = binaryVersion.Major + "." + binaryVersion.Minor + "." + binaryVersion.Build + suffix;

            Writer.WriteMessage(
                TraceEventType.Information, 
                "Updated file '{0}' from version {1} to {2}.", 
                filePath, 
                versionData, 
                newVersion);

            var fileInfo = new FileInfo(filePath);

            if (fileInfo.IsReadOnly)
            {
                fileInfo.IsReadOnly = false;
            }

            versionNode.InnerText = newVersion;
            doc.Save(filePath);

            return FileMatchResult.Continue;
        }

        /// <inheritdoc />
        protected override void OnBeforeSearch(IEnumerable<string> arguments)
        {
            _sourceBinaryPath = GetSourceBinaryPath(arguments);
        }

        /// <summary>
        /// Gets the source binary path.
        /// </summary>
        /// <param name="arguments">
        /// The arguments.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string GetSourceBinaryPath(IEnumerable<string> arguments)
        {
            return arguments.ParseArgument(
                new[]
                {
                    "/source:"
                });
        }

        /// <inheritdoc />
        public override string CommandLineArgumentHelp
        {
            get
            {
                var builder = new StringBuilder("/pattern:<fileSearch> /source:<filePath>");
                builder.AppendLine();
                builder.AppendLine();
                builder.AppendLine(
                    "/pattern:<fileSearch>\tThe file search pattern used to identify nuget specification to synchronize.");
                builder.AppendLine("\t\t\tMust be an absolute path and may contain * wildcard characters.");
                builder.AppendLine("/source:<fileSearch>\tThe binary file to get the version number from.");
                builder.AppendLine("\t\t\tMust be an absolute path. Wildcard (*) characters are not supported.");
                return builder.ToString();
            }
        }

        /// <inheritdoc />
        public override string Description
        {
            get
            {
                return "Synchronizes the product version in a Wix project to the version of a binary file.";
            }
        }

        /// <inheritdoc />
        public override IEnumerable<string> Names
        {
            get
            {
                return new[]
                {
                    "SyncNuGetVersionTask", "snv"
                };
            }
        }

        /// <inheritdoc />
        protected override string PatternArgument
        {
            get
            {
                return "/pattern:";
            }
        }

        /// <summary>
        ///     Gets or sets the binary version reader.
        /// </summary>
        private IVersionManager BinaryVersionReader
        {
            get;
            set;
        }
    }
}