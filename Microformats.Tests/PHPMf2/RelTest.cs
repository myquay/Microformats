using Microformats.Grammar;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System.Linq;
using static Microformats.Definitions.Constants;

namespace Microformats.Tests.PHPMf2
{
    /// <summary>
    /// Test cases from the indieweb php-mf2 parser
    /// From: <see href="https://github.com/microformats/php-mf2/blob/main/tests/Mf2/RelTest.php" />
    /// </summary>
    [TestClass]
    public class RelTest
    {
        [TestMethod]
        public void RelValueOnLinkTag()
        {
            var parser = new Mf2();
            var html = "<link rel=\"webmention\" href=\"http://example.com/webmention\">";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Rels.ContainsKey("webmention"));
            Assert.AreEqual("http://example.com/webmention", result.Rels["webmention"][0]);
        }

        [TestMethod]
        public void RelValueOnATag()
        {
            var parser = new Mf2();
            var html = "<a rel=\"webmention\" href=\"http://example.com/webmention\">webmention me</a>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Rels.ContainsKey("webmention"));
            Assert.AreEqual("http://example.com/webmention", result.Rels["webmention"][0]);
        }

        [TestMethod]
        public void RelValueOnAreaTag()
        {
            var parser = new Mf2();
            var html = "<map><area rel=\"webmention\" href=\"http://example.com/webmention\"/></map>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Rels.ContainsKey("webmention"));
            Assert.AreEqual("http://example.com/webmention", result.Rels["webmention"][0]);
        }

        [TestMethod]
        public void RelValueOrder()
        {
            var parser = new Mf2();
            var html = "<map><area rel=\"webmention\" href=\"http://example.com/area\"/></map>\r\n      <a rel=\"webmention\" href=\"http://example.com/a\">webmention me</a>\r\n      <link rel=\"webmention\" href=\"http://example.com/link\">";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Rels.ContainsKey("webmention"));
            Assert.AreEqual("http://example.com/area", result.Rels["webmention"][0]);
            Assert.AreEqual("http://example.com/a", result.Rels["webmention"][1]);
            Assert.AreEqual("http://example.com/link", result.Rels["webmention"][2]);
        }

        [TestMethod]
        public void RelValueOrder2()
        {
            var parser = new Mf2();
            var html = "<map><area rel=\"webmention\" href=\"http://example.com/area\"/></map>\r\n      <link rel=\"webmention\" href=\"http://example.com/link\">\r\n      <a rel=\"webmention\" href=\"http://example.com/a\">webmention me</a>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Rels.ContainsKey("webmention"));
            Assert.AreEqual("http://example.com/area", result.Rels["webmention"][0]);
            Assert.AreEqual("http://example.com/link", result.Rels["webmention"][1]);
            Assert.AreEqual("http://example.com/a", result.Rels["webmention"][2]);
        }

        [TestMethod]
        public void RelValueOrder3()
        {
            var parser = new Mf2();
            var html = "<html>\r\n      <head>\r\n        <link rel=\"webmention\" href=\"http://example.com/link\">\r\n      </head>\r\n      <body>\r\n        <a rel=\"webmention\" href=\"http://example.com/a\">webmention me</a>\r\n        <map><area rel=\"webmention\" href=\"http://example.com/area\"/></map>\r\n      </body>\r\n    </html>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Rels.ContainsKey("webmention"));
            Assert.AreEqual("http://example.com/link", result.Rels["webmention"][0]);
            Assert.AreEqual("http://example.com/a", result.Rels["webmention"][1]);
            Assert.AreEqual("http://example.com/area", result.Rels["webmention"][2]);
        }

        [TestMethod]
        public void RelValueOnBTag()
        {
            var parser = new Mf2();
            var html = "<b rel=\"webmention\" href=\"http://example.com/webmention\">this makes no sense</b>";
            var result = parser.Parse(html);

            Assert.IsFalse(result.Rels.ContainsKey("webmention"));
        }

        //TODO: ALTERNATIVES NOT SUPPORT YET
        //[TestMethod]
        //public void EnableAlternatesFlagTrue()
        //{
        //    var parser = new Mf2();
        //    var html = "<a rel=\"author\" href=\"http://example.com/a\">author a</a>\r\n<a rel=\"author\" href=\"http://example.com/b\">author b</a>\r\n<a rel=\"in-reply-to\" href=\"http://example.com/1\">post 1</a>\r\n<a rel=\"in-reply-to\" href=\"http://example.com/2\">post 2</a>\r\n<a rel=\"alternate home\"\r\n   href=\"http://example.com/fr\"\r\n   media=\"handheld\"\r\n   hreflang=\"fr\">French mobile homepage</a>";
        //    var result = parser.Parse(html);
        //}

        //TODO: ALTERNATIVES NOT SUPPORT YET
        //[TestMethod]
        //public void EnableAlternatesFlagFalse()
        //{

        //    var parser = new Mf2();
        //    var html = "<a rel=\"author\" href=\"http://example.com/a\">author a</a>\r\n<a rel=\"author\" href=\"http://example.com/b\">author b</a>\r\n<a rel=\"in-reply-to\" href=\"http://example.com/1\">post 1</a>\r\n<a rel=\"in-reply-to\" href=\"http://example.com/2\">post 2</a>\r\n<a rel=\"alternate home\"\r\n   href=\"http://example.com/fr\"\r\n   media=\"handheld\"\r\n   hreflang=\"fr\">French mobile homepage</a>";
        //    var result = parser.Parse(html);
        //}

        ///<summary>
        /// <see href="https://github.com/indieweb/php-mf2/issues/112"/>
        /// <see href="http://microformats.org/wiki/microformats2-parsing#rel_parse_examples"/>
        ///</summary>
        [TestMethod]
        public void RelURLs()
        {
            var parser = new Mf2();
            var html = "<a rel=\"author\" href=\"http://example.com/a\">author a</a>\r\n<a rel=\"author\" href=\"http://example.com/b\">author b</a>\r\n<a rel=\"in-reply-to\" href=\"http://example.com/1\">post 1</a>\r\n<a rel=\"in-reply-to\" href=\"http://example.com/2\">post 2</a>\r\n<a rel=\"alternate home\"\r\n   href=\"http://example.com/fr\"\r\n   media=\"handheld\"\r\n   hreflang=\"fr\">French mobile homepage</a>\r\n<link rel=\"alternate\" type=\"application/atom+xml\" href=\"http://example.com/articles.atom\" title=\"Atom Feed\" />";
            var result = parser.Parse(html);

            Assert.AreEqual(4, result.Rels.Count);
            Assert.IsTrue(result.Rels.ContainsKey("author"));
            Assert.IsTrue(result.Rels.ContainsKey("in-reply-to"));
            Assert.IsTrue(result.Rels.ContainsKey("alternate"));
            Assert.IsTrue(result.Rels.ContainsKey("home"));

            Assert.AreEqual(6, result.RelUrls.Count);

            Assert.IsTrue(result.RelUrls.ContainsKey("http://example.com/a"));
            Assert.IsTrue(result.RelUrls.ContainsKey("http://example.com/b"));
            Assert.IsTrue(result.RelUrls.ContainsKey("http://example.com/1"));
            Assert.IsTrue(result.RelUrls.ContainsKey("http://example.com/2"));
            Assert.IsTrue(result.RelUrls.ContainsKey("http://example.com/fr"));
            Assert.IsTrue(result.RelUrls.ContainsKey("http://example.com/articles.atom"));

            Assert.IsTrue(result.RelUrls["http://example.com/a"].Rels.Any());
            Assert.IsNotNull(result.RelUrls["http://example.com/a"].Text);
            Assert.IsTrue(result.RelUrls["http://example.com/b"].Rels.Any());
            Assert.IsNotNull(result.RelUrls["http://example.com/b"].Text);
            Assert.IsTrue(result.RelUrls["http://example.com/1"].Rels.Any());
            Assert.IsNotNull(result.RelUrls["http://example.com/1"].Text);
            Assert.IsTrue(result.RelUrls["http://example.com/2"].Rels.Any());
            Assert.IsNotNull(result.RelUrls["http://example.com/2"].Text);


            Assert.IsTrue(result.RelUrls["http://example.com/fr"].Rels.Any());
            Assert.IsNotNull(result.RelUrls["http://example.com/fr"].Text);
            Assert.IsNotNull(result.RelUrls["http://example.com/fr"].Media);
            Assert.IsNotNull(result.RelUrls["http://example.com/fr"].HrefLang);

            Assert.IsTrue(result.RelUrls["http://example.com/articles.atom"].Rels.Any());
            Assert.IsNotNull(result.RelUrls["http://example.com/articles.atom"].Title);
            Assert.IsNotNull(result.RelUrls["http://example.com/articles.atom"].Type);
        }

        ///<summary>
        /// <see href="https://github.com/microformats/microformats2-parsing/issues/29"/>
        /// <see href="ttps://github.com/microformats/microformats2-parsing/issues/30"/>
        ///</summary>
        [TestMethod]
        public void RelURLsRelsUniqueAndSorted()
        {
            var parser = new Mf2();
            var html = "<a href=\"#\" rel=\"me bookmark\"></a>\r\n<a href=\"#\" rel=\"bookmark archived\"></a>";
            var result = parser.Parse(html);

            Assert.AreEqual("archived", result.RelUrls["#"].Rels[0]);
            Assert.AreEqual("bookmark", result.RelUrls["#"].Rels[1]);
            Assert.AreEqual("me", result.RelUrls["#"].Rels[2]);
        }

        [TestMethod]
        public void RelURLsInfoMergesCorrectly()
        {
            var parser = new Mf2();
            var html = "<a href=\"#\" rel=\"a\">This nodeValue</a>\r\n<a href=\"#\" rel=\"a\" hreflang=\"en\">Not this nodeValue</a>";
            var result = parser.Parse(html);

            Assert.AreEqual("en", result.RelUrls["#"].HrefLang);
            Assert.IsNull(result.RelUrls["#"].Media);
            Assert.IsNull(result.RelUrls["#"].Title);
            Assert.IsNull(result.RelUrls["#"].Type);
            Assert.AreEqual("This nodeValue", result.RelUrls["#"].Text);
        }

        [TestMethod]
        public void RelURLsNoDuplicates()
        {
            var parser = new Mf2();
            var html = "<a href=\"#a\" rel=\"a\"></a>\r\n<a href=\"#b\" rel=\"a\"></a>\r\n<a href=\"#a\" rel=\"a\"></a>";
            var result = parser.Parse(html);

            Assert.AreEqual(2, result.Rels["a"].Length);
            Assert.AreEqual("#a", result.Rels["a"][0]);
            Assert.AreEqual("#b", result.Rels["a"][1]);
        }

        [TestMethod]
        public void RelURLsFalsyTextVSEmpty()
        {
            var parser = new Mf2();
            var html = "<a href=\"#a\" rel=\"a\">0</a>\r\n<a href=\"#b\" rel=\"b\"></a>";
            var result = parser.Parse(html);

            Assert.IsNotNull(result.RelUrls["#a"].Text);
            Assert.AreEqual("0", result.RelUrls["#a"].Text);
            Assert.IsNull(result.RelUrls["#b"].Text);
        }
    }
}
