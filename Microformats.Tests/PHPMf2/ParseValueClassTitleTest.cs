using Microformats.Grammar;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microformats.Definitions.Constants;

namespace Microformats.Tests.PHPMf2
{
    /// <summary>
    /// Test cases from the indieweb php-mf2 parser
    /// From: <see href="https://github.com/microformats/php-mf2/blob/main/tests/Mf2/ParseValueClassTitleTest.php" />
    /// </summary>
    [TestClass]
    public class ParseValueClassTitleTest
    {

        [TestMethod]
        public void ParsesImpliedPNameFromNodeValue()
        {
            var parser = new Mf2();
            var html = "<span class=\"h-card\">The Name</span>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("name"));
            Assert.AreEqual("The Name", result.Items[0].Get(Props.NAME)[0]);
        }

        [TestMethod]
        public void ValueClassTitleHandlesSingleValueClass()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\"><p class=\"p-name\"><span class=\"value\">Name</span> (this should not be included)</p></div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("name"));
            Assert.AreEqual("Name", result.Items[0].Get(Props.NAME)[0]);
        }

        [TestMethod]
        public void ValueClassTitleHandlesMultipleValueClass()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\"><p class=\"p-name\"><span class=\"value\">Name</span> (this should not be included) <span class=\"value\">Endname</span></p></div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("name"));
            Assert.AreEqual("NameEndname", result.Items[0].Get(Props.NAME)[0]);
        }

        [TestMethod]
        public void ValueClassTitleHandlesSingleValueTitle()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\"><p class=\"p-name\"><span class=\"value-title\" title=\"Real Name\">Wrong Name</span> (this should not be included)</p></div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("name"));
            Assert.AreEqual("Real Name", result.Items[0].Get(Props.NAME)[0]);
        }

        [TestMethod]
        public void ValueClassTitleHandlesMultipleValueTitle()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\"><p class=\"p-name\"><span class=\"value-title\" title=\"Real \">Wrong Name</span> <span class=\"value-title\" title=\"Name\">(this should not be included)</span></p></div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("name"));
            Assert.AreEqual("Real Name", result.Items[0].Get(Props.NAME)[0]);
        }

        ///<summary>
        /// <see href="https://github.com/indieweb/php-mf2/issues/25"/>
        ///</summary> 
        [TestMethod]
        public void ValueClassDatetimeWorksWithUrlProperties()
        {

            var parser = new Mf2();
            var html = "<div class=\"h-entry\">\r\n\t<a href=\"2013/178/t1/surreal-meeting-dpdpdp-trondisc\"\r\n\t\trel=\"bookmark\"\r\n\t\tclass=\"dt-published published dt-updated updated u-url u-uid\">\r\n\t\t\t<time class=\"value\">10:17</time>\r\n\t\t\ton <time class=\"value\">2013-06-27</time>\r\n\t</a>\r\n</div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("published"));
            Assert.AreEqual("2013-06-27 10:17", result.Items[0].Get(Props.PUBLISHED)[0]);
        }

        ///<summary>
        /// <see href="https://github.com/indieweb/php-mf2/issues/27"/>
        ///</summary> 
        [TestMethod]
        public void ParsesValueTitleDatetimes()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-entry\">\r\n <h1 class=\"p-name\">test</h1>\r\n <span class=\"dt-published\"><span class=\"value-title\" title=\"2012-02-16T16:14:47+00:00\"> </span>16.02.2012</span>\r\n</div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("published"));
            Assert.AreEqual("2012-02-16T16:14:47+00:00", result.Items[0].Get(Props.PUBLISHED)[0]);
        }

        ///<summary>
        /// <see href="https://github.com/indieweb/php-mf2/issues/34"/>
        ///</summary> 
        [TestMethod]
        public void IgnoresValueClassNestedFurtherThanChild()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\"><span class=\"p-tel\"><span class=\"value\">1234</span><span class=\"h-card\"><span class=\"p-tel\"><span class=\"value\">5678</span>";
            var result = parser.Parse(html);

            Assert.AreEqual("1234", result.Items[0].Get(Props.TELEPHONE)[0]);
            //TODO: CHILDREN SUPPORT REQUIRED
            Assert.AreEqual("5678", result.Items[0].Get<MfSpec>(Props.TELEPHONE)[0].Get(Props.TELEPHONE)[0]);
        }

        ///<summary>
        /// <see href="https://github.com/indieweb/php-mf2/issues/38"/>
        ///</summary> 
        [TestMethod]
        public void ValueClassDtMatchesSingleDigitTimeComponent()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-entry\"><span class=\"dt-published\"><time class=\"value\">6:01</time>, <time class=\"value\">2013-02-01</time></span></div>";
            var result = parser.Parse(html);

            Assert.AreEqual("2013-02-01 6:01", result.Items[0].Get(Props.PUBLISHED)[0]);
        }
    }
}
