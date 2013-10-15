namespace Headless.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
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
        ///     Runs a test for matches expressions throws exception.
        /// </summary>
        [TestMethod]
        public void MatchesExpressionsThrowsExceptionTest()
        {
            var actualLocation = new Uri("http://www.test.com/some/time/");

            var target = new RelaxedFolderLocationValidator();

            Action action = () => target.Matches(actualLocation, new List<Regex>());

            action.ShouldThrow<NotImplementedException>();
        }

        /// <summary>
        ///     Runs a test for matches location returns false when matching actual location folder against expected location file.
        /// </summary>
        [TestMethod]
        public void MatchesLocationReturnsFalseWhenMatchingActualLocationFolderAgainstExpectedLocationFileTest()
        {
            var expectedLocation = new Uri("http://www.test.com/some/time/test.txt");
            var actualLocation = new Uri("http://www.test.com/some/time/");

            var target = new RelaxedFolderLocationValidator();

            var actual = target.Matches(actualLocation, expectedLocation);

            actual.Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for matches location returns false when matching actual location folder without forward slash against
        ///     expected location file.
        /// </summary>
        [TestMethod]
        public void
            MatchesLocationReturnsFalseWhenMatchingActualLocationFolderWithoutForwardSlashAgainstExpectedLocationFileTest
            ()
        {
            var expectedLocation = new Uri("http://www.test.com/some/time/test.txt");
            var target = new RelaxedFolderLocationValidator();
            var actualLocation = new Uri("http://www.test.com/some/time");

            var actual = target.Matches(actualLocation, expectedLocation);

            actual.Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for matches location returns false when matching expected location folder against actual location file.
        /// </summary>
        [TestMethod]
        public void MatchesLocationReturnsFalseWhenMatchingExpectedLocationFolderAgainstActualLocationFileTest()
        {
            var expectedLocation = new Uri("http://www.test.com/some/time/");
            var target = new RelaxedFolderLocationValidator();
            var actualLocation = new Uri("http://www.test.com/some/time/test.txt");

            var actual = target.Matches(actualLocation, expectedLocation);

            actual.Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for matches location returns false when matching expected location folder without forward slash against
        ///     actual location file.
        /// </summary>
        [TestMethod]
        public void
            MatchesLocationReturnsFalseWhenMatchingExpectedLocationFolderWithoutForwardSlashAgainstActualLocationFileTest
            ()
        {
            var expectedLocation = new Uri("http://www.test.com/some/time");
            var target = new RelaxedFolderLocationValidator();
            var actualLocation = new Uri("http://www.test.com/some/time/test.txt");

            var actual = target.Matches(actualLocation, expectedLocation);

            actual.Should().BeFalse();
        }

        /// <summary>
        ///     Runs a test for matches location returns true when actual location lacks forward slash.
        /// </summary>
        [TestMethod]
        public void MatchesLocationReturnsTrueWhenActualLocationLacksForwardSlashTest()
        {
            var expectedLocation = new Uri("http://www.test.com/some/time/");
            var target = new RelaxedFolderLocationValidator();
            var actualLocation = new Uri("http://www.test.com/some/time?stuff=true");

            var actual = target.Matches(actualLocation, expectedLocation);

            actual.Should().BeTrue();
        }

        /// <summary>
        ///     Runs a test for matches location returns true when both location lacks forward slash.
        /// </summary>
        [TestMethod]
        public void MatchesLocationReturnsTrueWhenBothLocationLacksForwardSlashTest()
        {
            var expectedLocation = new Uri("http://www.test.com/some/time");
            var target = new RelaxedFolderLocationValidator();
            var actualLocation = new Uri("http://www.test.com/some/time?stuff=true");

            var actual = target.Matches(actualLocation, expectedLocation);

            actual.Should().BeTrue();
        }

        /// <summary>
        ///     Runs a test for matches location returns true when expected location lacks forward slash.
        /// </summary>
        [TestMethod]
        public void MatchesLocationReturnsTrueWhenExpectedLocationLacksForwardSlashTest()
        {
            var expectedLocation = new Uri("http://www.test.com/some/time");
            var target = new RelaxedFolderLocationValidator();
            var actualLocation = new Uri("http://www.test.com/some/time/?stuff=true");

            var actual = target.Matches(actualLocation, expectedLocation);

            actual.Should().BeTrue();
        }

        /// <summary>
        ///     Runs a test for matches location returns true when ignoring query string.
        /// </summary>
        [TestMethod]
        public void MatchesLocationReturnsTrueWhenIgnoringQueryStringTest()
        {
            var expectedLocation = new Uri("http://www.test.com/some/time/");
            var target = new RelaxedFolderLocationValidator();
            var actualLocation = new Uri("http://www.test.com/some/time/?stuff=true");

            var actual = target.Matches(actualLocation, expectedLocation);

            actual.Should().BeTrue();
        }

        /// <summary>
        ///     Runs a test for matches location returns true with case insensitive host matching.
        /// </summary>
        [TestMethod]
        public void MatchesLocationReturnsTrueWithCaseInsensitiveHostMatchingTest()
        {
            var expectedLocation = new Uri("http://www.TEST.com/some/time/");
            var target = new RelaxedFolderLocationValidator();
            var actualLocation = new Uri("http://www.test.com/some/time/?stuff=true");

            var actual = target.Matches(actualLocation, expectedLocation);

            actual.Should().BeTrue();
        }

        /// <summary>
        ///     Runs a test for matches location returns true with case insensitive path matching.
        /// </summary>
        [TestMethod]
        public void MatchesLocationReturnsTrueWithCaseInsensitivePathMatchingTest()
        {
            var expectedLocation = new Uri("http://www.test.com/some/Time");
            var actualLocation = new Uri("http://www.test.com/some/time");

            var target = new RelaxedFolderLocationValidator();

            var actual = target.Matches(actualLocation, expectedLocation);

            actual.Should().BeTrue();
        }

        /// <summary>
        ///     Runs a test for matches location throws exception with null actual location.
        /// </summary>
        [TestMethod]
        public void MatchesLocationThrowsExceptionWithNullActualLocationTest()
        {
            var actualLocation = new Uri("http://www.test.com/some/time");

            var target = new RelaxedFolderLocationValidator();

            Action action = () => target.Matches(actualLocation, (Uri)null);

            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        ///     Runs a test for matches location throws exception with null expected location.
        /// </summary>
        [TestMethod]
        public void MatchesLocationThrowsExceptionWithNullExpectedLocationTest()
        {
            var expectedLocation = new Uri("http://www.test.com/some/Time");

            var target = new RelaxedFolderLocationValidator();

            Action action = () => target.Matches(null, expectedLocation);

            action.ShouldThrow<ArgumentNullException>();
        }

        /// <summary>
        ///     Runs a test for matches location throws exception with relative actual location.
        /// </summary>
        [TestMethod]
        public void MatchesLocationThrowsExceptionWithRelativeActualLocationTest()
        {
            var actualLocation = new Uri("/some/time", UriKind.Relative);
            var expectedLocation = new Uri("http://www.test.com/some/Time");

            var target = new RelaxedFolderLocationValidator();

            Action action = () => target.Matches(actualLocation, expectedLocation);

            action.ShouldThrow<ArgumentException>();
        }

        /// <summary>
        ///     Runs a test for matches location throws exception with relative expected location.
        /// </summary>
        [TestMethod]
        public void MatchesLocationThrowsExceptionWithRelativeExpectedLocationTest()
        {
            var actualLocation = new Uri("http://www.test.com/some/time");
            var expectedLocation = new Uri("/some/Time", UriKind.Relative);

            var target = new RelaxedFolderLocationValidator();

            Action action = () => target.Matches(actualLocation, expectedLocation);

            action.ShouldThrow<ArgumentException>();
        }

        /// <summary>
        ///     Runs a test for validation type identifies URI only.
        /// </summary>
        [TestMethod]
        public void ValidationTypeIdentifiesUriOnlyTest()
        {
            var target = new RelaxedFolderLocationValidator();

            target.ValidationType.Should().Be(LocationValidationType.UriOnly);
        }
    }
}