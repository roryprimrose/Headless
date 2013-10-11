namespace Headless.UnitTests
{
    using System;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The <see cref="PageTests" />
    ///     class tests the <see cref="Page" /> class.
    /// </summary>
    [TestClass]
    public class PageTests
    {
        /// <summary>
        ///     Runs a test for is on returns false when matching location folder against target file.
        /// </summary>
        [TestMethod]
        public void IsOnReturnsFalseWhenMatchingLocationFolderAgainstTargetFileTest()
        {
            var target = new Uri("http://www.test.com/some/time/test.txt");
            var page = new TextPageWrapper(target);
            const UriComponents VerificationParts = UriComponents.SchemeAndServer | UriComponents.Path;
            var location = new Uri("http://www.test.com/some/time/");

            var actual = page.IsOn(location, VerificationParts);

            actual.Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for is on returns false when matching location folder without forward slash against target file.
        /// </summary>
        [TestMethod]
        public void IsOnReturnsFalseWhenMatchingLocationFolderWithoutForwardSlashAgainstTargetFileTest()
        {
            var target = new Uri("http://www.test.com/some/time/test.txt");
            var page = new TextPageWrapper(target);
            const UriComponents VerificationParts = UriComponents.SchemeAndServer | UriComponents.Path;
            var location = new Uri("http://www.test.com/some/time");

            var actual = page.IsOn(location, VerificationParts);

            actual.Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for is on returns false when matching target folder against location file.
        /// </summary>
        [TestMethod]
        public void IsOnReturnsFalseWhenMatchingTargetFolderAgainstLocationFileTest()
        {
            var target = new Uri("http://www.test.com/some/time/");
            var page = new TextPageWrapper(target);
            const UriComponents VerificationParts = UriComponents.SchemeAndServer | UriComponents.Path;
            var location = new Uri("http://www.test.com/some/time/test.txt");

            var actual = page.IsOn(location, VerificationParts);

            actual.Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for is on returns false when matching target folder without forward slash against location file.
        /// </summary>
        [TestMethod]
        public void IsOnReturnsFalseWhenMatchingTargetFolderWithoutForwardSlashAgainstLocationFileTest()
        {
            var target = new Uri("http://www.test.com/some/time");
            var page = new TextPageWrapper(target);
            const UriComponents VerificationParts = UriComponents.SchemeAndServer | UriComponents.Path;
            var location = new Uri("http://www.test.com/some/time/test.txt");

            var actual = page.IsOn(location, VerificationParts);

            actual.Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for is on returns true when ignoring query string and both target and location lacks forward slash.
        /// </summary>
        [TestMethod]
        public void IsOnReturnsTrueWhenIgnoringQueryStringAndBothTargetAndLocationLacksForwardSlashTest()
        {
            var target = new Uri("http://www.test.com/some/time");
            var page = new TextPageWrapper(target);
            const UriComponents VerificationParts = UriComponents.SchemeAndServer | UriComponents.Path;
            var location = new Uri("http://www.test.com/some/time?stuff=true");

            var actual = page.IsOn(location, VerificationParts);

            actual.Should().BeTrue();
        }

        /// <summary>
        ///     Runs a test for is on returns true when ignoring query string and location lacks forward slash.
        /// </summary>
        [TestMethod]
        public void IsOnReturnsTrueWhenIgnoringQueryStringAndLocationLacksForwardSlashTest()
        {
            var target = new Uri("http://www.test.com/some/time/");
            var page = new TextPageWrapper(target);
            const UriComponents VerificationParts = UriComponents.SchemeAndServer | UriComponents.Path;
            var location = new Uri("http://www.test.com/some/time?stuff=true");

            var actual = page.IsOn(location, VerificationParts);

            actual.Should().BeTrue();
        }

        /// <summary>
        ///     Runs a test for is on returns true when ignoring query string and target lacks forward slash.
        /// </summary>
        [TestMethod]
        public void IsOnReturnsTrueWhenIgnoringQueryStringAndTargetLacksForwardSlashTest()
        {
            var target = new Uri("http://www.test.com/some/time");
            var page = new TextPageWrapper(target);
            const UriComponents VerificationParts = UriComponents.SchemeAndServer | UriComponents.Path;
            var location = new Uri("http://www.test.com/some/time/?stuff=true");

            var actual = page.IsOn(location, VerificationParts);

            actual.Should().BeTrue();
        }

        /// <summary>
        ///     Runs a test for is on returns true when ignoring query string.
        /// </summary>
        [TestMethod]
        public void IsOnReturnsTrueWhenIgnoringQueryStringTest()
        {
            var target = new Uri("http://www.test.com/some/time/");
            var page = new TextPageWrapper(target);
            const UriComponents VerificationParts = UriComponents.SchemeAndServer | UriComponents.Path;
            var location = new Uri("http://www.test.com/some/time/?stuff=true");

            var actual = page.IsOn(location, VerificationParts);

            actual.Should().BeTrue();
        }
    }
}