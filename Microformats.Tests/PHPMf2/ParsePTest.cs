using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static Microformats.Definitions.Constants;

namespace Microformats.Tests.PHPMf2
{
    /// <summary>
    /// Test cases from the indieweb php-mf2 parser
    /// From: <see href="https://github.com/microformats/php-mf2/blob/main/tests/Mf2/ParsePTest.php" />
    /// </summary>
    [TestClass]
    public class ParsePTest
    {

        [TestMethod]
        public void ParsePHandlesInnerText()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\"><p class=\"p-name\">Example User</p></div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("name"));
            Assert.AreEqual("Example User", result.Items[0].Get(Props.NAME)[0]);
        }

        [TestMethod]
        public void ParsePHandlesImg()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\"><img class=\"p-name\" alt=\"Example User\"></div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("name"));
            Assert.AreEqual("Example User", result.Items[0].Get(Props.NAME)[0]);
        }

        [TestMethod]
        public void ParsePHandlesAbbr()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card h-person\"><abbr class=\"p-name\" title=\"Example User\">@example</abbr></div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("name"));
            Assert.AreEqual("Example User", result.Items[0].Get(Props.NAME)[0]);
        }

        [TestMethod]
        public void ParsePHandlesData()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\"><data class=\"p-name\" value=\"Example User\"></data></div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("name"));
            Assert.AreEqual("Example User", result.Items[0].Get(Props.NAME)[0]);
        }

        [TestMethod]
        public void ParsePHandlesDataWithBlankValueAttribute()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\"><data class=\"p-name\" value=\"\">Example User</data></div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("name"));
            Assert.AreEqual("", result.Items[0].Get(Props.NAME)[0]);
        }

        [TestMethod]
        public void ParsePReturnsEmptyStringForBrHr()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\"><br class=\"p-name\"/></div><div class=\"h-card\"><hr class=\"p-name\"/></div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("name"));
            Assert.AreEqual("", result.Items[0].Get(Props.NAME)[0]);
        }

        [TestMethod]
        public void ParsesInputValue()
        {
            var parser = new Mf2();
            var html = "<span class=\"h-card\"><input class=\"u-url\" value=\"http://example.com\" /></span>";
            var result = parser.Parse(html);

            Assert.AreEqual("http://example.com", result.Items[0].Get(Props.URL)[0]);
        }

        ///<summary>
        /// <see href="https://github.com/indieweb/php-mf2/issues/53"/>
        /// <see href="http://microformats.org/wiki/microformats2-parsing#parsing_an_e-_property"/>
        ///</summary>   
        [TestMethod]
        public void ConvertsNestedImgElementToAltOrSrc()
        {
            var parser = new Mf2().WithOptions((o) =>
            {
                o.BaseUri = new Uri("http://waterpigs.co.uk/articles/five-legged-elephant");
                return o;
            });

            var html = "<div class=\"h-entry\">\r\n\t<p class=\"p-name\">The day I saw a <img alt=\"five legged elephant\" src=\"/photos/five-legged-elephant.jpg\" /></p>\r\n\t<p class=\"p-summary\">Blah blah <img src=\"/photos/five-legged-elephant.jpg\" /></p>\r\n</div>";
            var result = parser.Parse(html);

            Assert.AreEqual("The day I saw a five legged elephant", result.Items[0].Get(Props.NAME)[0]);
            Assert.AreEqual("Blah blah http://waterpigs.co.uk/photos/five-legged-elephant.jpg", result.Items[0].Get(Props.SUMMARY)[0]);
        }

        ///<summary>
        /// <see href="https://github.com/indieweb/php-mf2/issues/69"/>
        ///</summary>  
        [TestMethod]
        public void BrWhitespaceIssue69()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\"><p class=\"p-adr\"><span class=\"p-street-address\">Street Name 9</span><br/><span class=\"p-locality\">12345 NY, USA</span></p></div>";
            var result = parser.Parse(html);

            Assert.AreEqual("Street Name 9\n12345 NY, USA", result.Items[0].Get(Props.ADDRESS)[0]);
            Assert.AreEqual("Street Name 9", result.Items[0].Get(Props.STREET_ADDRESS)[0]);
            Assert.AreEqual("12345 NY, USA", result.Items[0].Get(Props.LOCALITY)[0]);
            Assert.IsFalse(result.Items[0].Properties.ContainsKey("name"));
        }

        ///<summary>
        /// <see href="https://github.com/indieweb/php-mf2/issues/89"/>
        ///</summary> 
        [TestMethod]
        public void EmptyAlt()
        {
            var parser = new Mf2();
            var html = "<div class=\"p-author h-card\"><a href=\"/\" class=\"p-org p-name\"><img class=\"u-logo\" src=\"/static/logo.jpg\" alt=\"\" />mention.tech</a></div>";
            var result = parser.Parse(html);

            Assert.AreEqual("mention.tech", result.Items[0].Get(Props.ORGANIZATION)[0]);
            Assert.AreEqual("mention.tech", result.Items[0].Get(Props.NAME)[0]);
        }

    }
}
