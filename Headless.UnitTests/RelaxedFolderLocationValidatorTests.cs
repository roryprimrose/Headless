namespace Headless.UnitTests
{
    using System;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The <see cref="RelaxedFolderLocationValidatorTests" />
    ///     class tests the <see cref="RelaxedFolderLocationValidator" /> class.
    /// </summary>
    [TestClass]
    public class RelaxedFolderLocationValidatorTests
    {
        /// <summary>
        ///     Runs a test for matches returns false when matching actual location folder against expected location file.
        /// </summary>
        [TestMethod]
        public void MatchesReturnsFalseWhenMatchingActualLocationFolderAgainstExpectedLocationFileTest()
        {
            var expectedLocation = new Uri("http://www.test.com/some/time/test.txt");
            var actualLocation = new Uri("http://www.test.com/some/time/");

            var target = new RelaxedFolderLocationValidator();

            var actual = target.Matches(expectedLocation, actualLocation);

            actual.Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for matches returns false when matching actual location folder without forward slash against expected
        ///     location file.
        /// </summary>
        [TestMethod]
        public void
            MatchesReturnsFalseWhenMatchingActualLocationFolderWithoutForwardSlashAgainstExpectedLocationFileTest()
        {
            var expectedLocation = new Uri("http://www.test.com/some/time/test.txt");
            var target = new RelaxedFolderLocationValidator();
            var actualLocation = new Uri("http://www.test.com/some/time");

            var actual = target.Matches(expectedLocation, actualLocation);

            actual.Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for matches returns false when matching expected location folder against actual location file.
        /// </summary>
        [TestMethod]
        public void MatchesReturnsFalseWhenMatchingExpectedLocationFolderAgainstActualLocationFileTest()
        {
            var expectedLocation = new Uri("http://www.test.com/some/time/");
            var target = new RelaxedFolderLocationValidator();
            var actualLocation = new Uri("http://www.test.com/some/time/test.txt");

            var actual = target.Matches(expectedLocation, actualLocation);

            actual.Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for matches returns false when matching expected location folder without forward slash against actual
        ///     location file.
        /// </summary>
        [TestMethod]
        public void
            MatchesReturnsFalseWhenMatchingExpectedLocationFolderWithoutForwardSlashAgainstActualLocationFileTest()
        {
            var expectedLocation = new Uri("http://www.test.com/some/time");
            var target = new RelaxedFolderLocationValidator();
            var actualLocation = new Uri("http://www.test.com/some/time/test.txt");

            var actual = target.Matches(expectedLocation, actualLocation);

            actual.Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for matches returns true when actual location lacks forward slash.
        /// </summary>
        [TestMethod]
        public void MatchesReturnsTrueWhenActualLocationLacksForwardSlashTest()
        {
            var expectedLocation = new Uri("http://www.test.com/some/time/");
            var target = new RelaxedFolderLocationValidator();
            var actualLocation = new Uri("http://www.test.com/some/time?stuff=true");

            var actual = target.Matches(expectedLocation, actualLocation);

            actual.Should().BeTrue();
        }

        /// <summary>
        ///     Runs a test for matches returns true when both location lacks forward slash.
        /// </summary>
        [TestMethod]
        public void MatchesReturnsTrueWhenBothLocationLacksForwardSlashTest()
        {
            var expectedLocation = new Uri("http://www.test.com/some/time");
            var target = new RelaxedFolderLocationValidator();
            var actualLocation = new Uri("http://www.test.com/some/time?stuff=true");

            var actual = target.Matches(expectedLocation, actualLocation);

            actual.Should().BeTrue();
        }

        /// <summary>
        ///     Runs a test for matches returns true when expected location lacks forward slash.
        /// </summary>
        [TestMethod]
        public void MatchesReturnsTrueWhenExpectedLocationLacksForwardSlashTest()
        {
            var expectedLocation = new Uri("http://www.test.com/some/time");
            var target = new RelaxedFolderLocationValidator();
            var actualLocation = new Uri("http://www.test.com/some/time/?stuff=true");

            var actual = target.Matches(expectedLocation, actualLocation);

            actual.Should().BeTrue();
        }

        /// <summary>
        ///     Runs a test for is on returns true when ignoring query string.
        /// </summary>
        [TestMethod]
        public void MatchesReturnsTrueWhenIgnoringQueryStringTest()
        {
            var expectedLocation = new Uri("http://www.test.com/some/time/");
            var target = new RelaxedFolderLocationValidator();
            var actualLocation = new Uri("http://www.test.com/some/time/?stuff=true");

            var actual = target.Matches(expectedLocation, actualLocation);

            actual.Should().BeTrue();
        }

        /// <summary>
        ///     Runs a test for matches returns true with case insensitive host matching.
        /// </summary>
        [TestMethod]
        public void MatchesReturnsTrueWithCaseInsensitiveHostMatchingTest()
        {
            var expectedLocation = new Uri("http://www.TEST.com/some/time/");
            var target = new RelaxedFolderLocationValidator();
            var actualLocation = new Uri("http://www.test.com/some/time/?stuff=true");

            var actual = target.Matches(expectedLocation, actualLocation);

            actual.Should().BeTrue();
        }

        /// <summary>
        ///     Runs a test for matches returns true with case insensitive path matching.
        /// </summary>
        [TestMethod]
        public void MatchesReturnsTrueWithCaseInsensitivePathMatchingTest()
        {
            var expectedLocation = new Uri("http://www.test.com/some/Time");
            var target = new RelaxedFolderLocationValidator();
            var actualLocation = new Uri("http://www.test.com/some/time");

            var actual = target.Matches(expectedLocation, actualLocation);

            actual.Should().BeTrue();
        }
    }
}