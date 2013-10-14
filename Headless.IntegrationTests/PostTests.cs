namespace Headless.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using FluentAssertions;
    using Headless.IntegrationTests.Pages;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    ///     The <see cref="PostTests" />
    ///     class tests POST functionality in headless.
    /// </summary>
    [TestClass]
    public class PostTests
    {
        /// <summary>
        ///     Runs a test for can post file directly without preloading page.
        /// </summary>
        [TestMethod]
        public void CanPostFileDirectlyWithoutPreloadingPageTest()
        {
            var firstFileName = Guid.NewGuid().ToString("N") + ".txt";
            var firstFile = Path.Combine(Path.GetTempPath(), firstFileName);
            var firstFileContent = Guid.NewGuid().ToString();
            var secondFileName = Guid.NewGuid().ToString("N") + ".txt";
            var secondFile = Path.Combine(Path.GetTempPath(), secondFileName);
            var secondFileContent = Guid.NewGuid().ToString();

            try
            {
                File.WriteAllText(firstFile, firstFileContent);
                File.WriteAllText(secondFile, secondFileContent);

                using (var browser = new Browser())
                {
                    var textValue = Guid.NewGuid().ToString();

                    var parameters = new List<PostEntry>
                    {
                        new PostFileEntry("files", firstFile), 
                        new PostFileEntry("files", secondFile), 
                        new PostEntry("SomeData", textValue)
                    };

                    var postedPage = browser.PostTo<FormFilePage>(parameters);

                    postedPage.Result.TraceResults();

                    postedPage.SomeData.Value.Should().Be(textValue);
                    postedPage.FileCount.Text.Should().Be("2");

                    var firstFileCells =
                        postedPage.PostedFiles.Find<HtmlTableRow>()
                            .ByPredicate(x => x.Html.Contains(firstFileName))
                            .Cells.ToList();

                    firstFileCells[0].Text.Should().Be(firstFileName);
                    firstFileCells[1].Text.Should().Be("text/plain");
                    firstFileCells[2].Text.Should().Be(firstFileContent);

                    var secondFileCells =
                        postedPage.PostedFiles.Find<HtmlTableRow>()
                            .ByPredicate(x => x.Html.Contains(secondFileName))
                            .Cells.ToList();

                    secondFileCells[0].Text.Should().Be(secondFileName);
                    secondFileCells[1].Text.Should().Be("text/plain");
                    secondFileCells[2].Text.Should().Be(secondFileContent);
                }
            }
            finally
            {
                File.Delete(firstFile);
                File.Delete(secondFile);
            }
        }

        /// <summary>
        ///     Runs a test for can post stream directly without preloading page.
        /// </summary>
        [TestMethod]
        public void CanPostStreamDirectlyWithoutPreloadingPageTest()
        {
            var firstFileName = Guid.NewGuid().ToString("N") + ".txt";
            var secondFileName = Guid.NewGuid().ToString("N") + ".txt";

            using (var firstStream = new MemoryStream())
            {
                using (var secondStream = new MemoryStream())
                {
                    using (var browser = new Browser())
                    {
                        var firstFileContent = Guid.NewGuid().ToString();
                        var firstFileBuffer = Encoding.UTF8.GetBytes(firstFileContent);
                        var secondFileContent = Guid.NewGuid().ToString();
                        var secondFileBuffer = Encoding.UTF8.GetBytes(secondFileContent);

                        firstStream.Write(firstFileBuffer, 0, firstFileBuffer.Length);
                        secondStream.Write(secondFileBuffer, 0, secondFileBuffer.Length);

                        firstStream.Position = 0;
                        secondStream.Position = 0;

                        var textValue = Guid.NewGuid().ToString();

                        var parameters = new List<PostEntry>
                        {
                            new PostFileStreamEntry("files", firstFileName, firstStream), 
                            new PostFileStreamEntry("files", secondFileName, secondStream), 
                            new PostEntry("SomeData", textValue)
                        };

                        var postedPage = browser.PostTo<FormFilePage>(parameters);

                        postedPage.Result.TraceResults();

                        postedPage.SomeData.Value.Should().Be(textValue);
                        postedPage.FileCount.Text.Should().Be("2");

                        var firstFileCells =
                            postedPage.PostedFiles.Find<HtmlTableRow>()
                                .ByPredicate(x => x.Html.Contains(firstFileName))
                                .Cells.ToList();

                        firstFileCells[0].Text.Should().Be(firstFileName);
                        firstFileCells[1].Text.Should().Be("text/plain");
                        firstFileCells[2].Text.Should().Be(firstFileContent);

                        var secondFileCells =
                            postedPage.PostedFiles.Find<HtmlTableRow>()
                                .ByPredicate(x => x.Html.Contains(secondFileName))
                                .Cells.ToList();

                        secondFileCells[0].Text.Should().Be(secondFileName);
                        secondFileCells[1].Text.Should().Be("text/plain");
                        secondFileCells[2].Text.Should().Be(secondFileContent);
                    }
                }
            }
        }
    }
}