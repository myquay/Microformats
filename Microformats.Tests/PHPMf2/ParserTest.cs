//using Microformats;
//using Microformats.Definitions.Properties.DateTime;
//using Microformats.Definitions.Properties.Embedded;
//using Microformats.Definitions.Properties.Link;
//using Microformats.Definitions.Properties.Standard;
//using Microformats.Definitions.Vocabularies;
//using Microformats.Grammar;
//using Microsoft.VisualBasic;
//using Microsoft.VisualStudio.TestPlatform.Utilities;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Newtonsoft.Json.Linq;
//using System;
//using System.Collections.Generic;
//using System.Dynamic;
//using System.Linq;
//using System.Net.NetworkInformation;
//using System.Reflection;
//using System.Reflection.Metadata;
//using System.Runtime.Intrinsics.X86;
//using System.Text;
//using System.Threading.Tasks;
//using static System.Net.Mime.MediaTypeNames;
//using static System.Runtime.InteropServices.JavaScript.JSType;

//namespace Microformats.Tests.PHPMf2
//{
//    /// <summary>
//    /// Test cases from the indieweb php-mf2 parser
//    /// From: <see href="https://github.com/microformats/php-mf2/blob/main/tests/Mf2/ParserTest.php" />
//    /// </summary>
//    [TestClass]
//    public class ParserTest
//    {

//        [TestMethod]
//        public void ParsesImpliedPNameFromNodeValue()
//        {
//            var parser = new Mf2();
//            var html = "<span class=\"h-card\">The Name</span>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("name"));
//            Assert.AreEqual("The Name", result.Items[0].Get<PropertyName>()[0]);
//        }

//        [TestMethod]
//        public void ParseE()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-entry\"><div class=\"e-content\">Here is a load of <strong>embedded markup</strong></div></div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("content"));
//            Assert.AreEqual("Here is a load of <strong>embedded markup</strong>", result.Items[0].Get<Content, MfEmbedded>()[0].Html);
//            Assert.AreEqual("Here is a load of embedded markup", result.Items[0].Get<Content, MfEmbedded>()[0].Value);
//        }

//        [TestMethod]
//        public void ParseEmptyE()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-entry\"><div class=\"p-name\">Name</div> <div class=\"e-content\"></div></div>";
//            var result = parser.Parse(html);

//            Assert.AreEqual("Name", result.Items[0].Get<PropertyName>()[0]);
//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("content"));
//            Assert.AreEqual("", result.Items[0].Get<Content, MfEmbedded>()[0].Html);
//            Assert.AreEqual("", result.Items[0].Get<Content, MfEmbedded>()[0].Value);
//        }

//        [TestMethod]
//        public void ParseEResolvesRelativeLinks()
//        {
//            var parser = new Mf2().WithOptions(o =>
//            {
//                o.BaseUri = new Uri("http://example.com");
//                return o;
//            });
//            var html = "<div class=\"h-entry\"><p class=\"e-content\">Blah blah <a href=\"/a-url\">thing</a>. <object data=\"/object\"></object> <img src=\"/img\" /></p></div>";
//            var result = parser.Parse(html);

//            Assert.AreEqual("Blah blah <a href=\"http://example.com/a-url\">thing</a>. <object data=\"http://example.com/object\"></object> <img src=\"http://example.com/img\">", result.Items[0].Get<Content, MfEmbedded>()[0].Html);
//            Assert.AreEqual("Blah blah thing. http://example.com/img", result.Items[0].Get<Content, MfEmbedded>()[0].Value);
//        }

//        [TestMethod]
//        public void ParseEWithBR()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-entry\"><div class=\"e-content\">Here is content with two lines.<br>The br tag should not be converted to an XML br/br element.</div></div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("content"));
//            Assert.AreEqual("Here is content with two lines.<br>The br tag should not be converted to an XML br/br element.", result.Items[0].Get<Content, MfEmbedded>()[0].Html);
//            Assert.AreEqual("Here is content with two lines.'.\"\\n\".'The br tag should not be converted to an XML br/br element.", result.Items[0].Get<Content, MfEmbedded>()[0].Value);
//        }

//        [TestMethod]
//        public void ParseEWithWhitespace()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-entry\">\r\n\t<div class=\"e-content\">\r\n\t\tThis <strong>leading and trailing whitespace</strong> should be removed from the HTML and text values.\r\n\t</div>\r\n</div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("content"));
//            Assert.AreEqual("This <strong>leading and trailing whitespace</strong> should be removed from the HTML and text values.", result.Items[0].Get<Content, MfEmbedded>()[0].Html);
//            Assert.AreEqual("This leading and trailing whitespace should be removed from the HTML and text values.", result.Items[0].Get<Content, MfEmbedded>()[0].Value);
//        }

//        [TestMethod]
//        public void InvalidClassnamesContainingHAreIgnored()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"asdfgh-jkl\"></div>";
//            var result = parser.Parse(html);

//            Assert.IsFalse(result.Items.Any());
//        }

//        [TestMethod]
//        public void HtmlEncodesNonEProperties()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-card\">\r\n\t\t\t<span class=\"p-name\">&lt;p&gt;</span>\r\n\t\t\t<span class=\"dt-published\">&lt;dt&gt;</span>\r\n\t\t\t<span class=\"u-url\">&lt;u&gt;</span>\r\n\t\t\t</div>";
//            var result = parser.Parse(html);

//            Assert.AreEqual("<p>", result.Items[0].Get<PropertyName>()[0]);
//            Assert.AreEqual("<dt>", result.Items[0].Get<Published>()[0]);
//            Assert.AreEqual("<u>", result.Items[0].Get<Url>()[0]);
//        }

//        [TestMethod]
//        public void HtmlEncodesImpliedProperties()
//        {
//            var parser = new Mf2();
//            var html = "<a class=\"h-card\" href=\"&lt;url&gt;\"><img src=\"&lt;img&gt;\" alt=\"\" />&lt;name&gt;</a>";
//            var result = parser.Parse(html);

//            Assert.AreEqual("<name>", result.Items[0].Get<PropertyName>()[0]);
//            Assert.AreEqual("<url>", result.Items[0].Get<Url>()[0]);
//            Assert.AreEqual("<img>", result.Items[0].Get<Photo, MfImage>()[0].Value);
//            Assert.AreEqual("", result.Items[0].Get<Photo, MfImage>()[0].Alt);
//        }

//        [TestMethod]
//        public void ParsesRelValues()
//        {
//            var parser = new Mf2();
//            var html = "<a rel=\"author\" href=\"http://example.com\">Mr. Author</a>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.RelUrls.ContainsKey("author"));
//            Assert.AreEqual("http://example.com", result.RelUrls["author"]);
//        }

//        //TODO: NOT SUPPORTED
//        //[TestMethod]
//        //public void ParsesRelAlternateValues()
//        //{
//        //		$input = '<a rel="alternate home" href="http://example.org" hreflang="de", media="screen" type="text/html" title="German Homepage Link">German Homepage</a>';
//        //		$parser = new Parser($input);
//        //		$parser->enableAlternates = true;
//        //		$output = $parser->parse();

//        //		$this->assertArrayHasKey('alternates', $output);
//        //		$this->assertEquals('http://example.org', $output['alternates'][0]['url']);
//        //		$this->assertEquals('home', $output['alternates'][0]['rel']);
//        //		$this->assertEquals('de', $output['alternates'][0]['hreflang']);
//        //		$this->assertEquals('screen', $output['alternates'][0]['media']);
//        //		$this->assertEquals('text/html', $output['alternates'][0]['type']);
//        //		$this->assertEquals('German Homepage Link', $output['alternates'][0]['title']);
//        //		$this->assertEquals('German Homepage', $output['alternates'][0]['text']);
//        //}

//        [TestMethod]
//        public void ParseFromIdOnlyReturnsMicroformatsWithinThatId()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-entry\"><span class=\"p-name\">Not Included</span></div>\r\n\r\n<div id=\"parse-here\">\r\n\t<span class=\"h-card\">Included</span>\r\n</div>\r\n\r\n<div class=\"h-entry\"><span class=\"p-name\">Not Included</span></div>";
//            var result = parser.Parse(html);

//            Assert.AreEqual(1, result.Items.Length);
//            Assert.AreEqual("Included", result.Items[0].Get<PropertyName>()[0]);

//        }

//        ///<summary>
//        /// <see href="https://github.com/indieweb/php-mf2/issues/21"/>
//        ///</summary> 
//        [TestMethod]
//        public void DoesntAddArraysWithOnlyValueForAlreadyParsedNestedMicroformats()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-entry\">\r\n\t<div class=\"p-in-reply-to h-entry\">\r\n\t\t<span class=\"p-author h-card\">Nested Author</span>\r\n\t</div>\r\n\r\n\t<span class=\"p-author h-card\">Real Author</span>\r\n</div>";
//            var result = parser.Parse(html);

//            Assert.AreEqual(1, result.Items[0].Get<Author>().Length);
//        }


//        [TestMethod]
//        public void ParsesNestedMicroformatsWithClassnamesInAnyOrder()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-entry\">\r\n\t<div class=\"note- p-in-reply-to h-entry\">Name</div>\r\n</div>";
//            var result = parser.Parse(html);

//            Assert.AreEqual(1, result.Items[0].Get<InReplyTo>().Length);
//            Assert.AreEqual("Name", result.Items[0].Get<InReplyTo, MfType>()[0].Get<PropertyName>()[0]);

//        }

//        ///<summary>
//        /// <see href="https://github.com/indieweb/php-mf2/issues/48"/>
//        ///</summary> 
//        [TestMethod]
//        public void IgnoreClassesEndingInHyphen()
//        {
//            var parser = new Mf2();
//            var html = "<span class=\"h-entry\"> <span class=\"e-\">foo</span> </span>";
//            var result = parser.Parse(html);

//            Assert.IsFalse(result.Items[0].Properties.Any());
//        }

//        ///<summary>
//        /// <see href="https://github.com/indieweb/php-mf2/issues/52"/>
//        /// <see href="https://github.com/tommorris/mf2py/commit/92740deb7e19b8f1e7fbf6bec001cf52f2b07e99"/>
//        ///</summary> 
//        [TestMethod]
//        public void IgnoresTemplateElements()
//        {
//            var parser = new Mf2();
//            var html = "<<template class=\"h-card\"><span class=\"p-name\">Tom Morris</span></template>";
//            var result = parser.Parse(html);

//            Assert.IsFalse(result.Items.Any());
//        }

//        ///<summary>
//        /// <see href="https://github.com/indieweb/php-mf2/issues/53"/>
//        /// <see href="http://microformats.org/wiki/microformats2-parsing#parsing_an_e-_property"/>
//        ///</summary> 
//        [TestMethod]
//        public void ConvertsNestedImgElementToAltOrSrc()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-entry\">\r\n\t<p class=\"e-content\">It is a strange thing to see a <img alt=\"five legged elephant\" src=\"/photos/five-legged-elephant.jpg\" /></p>\r\n</div>";
//            var result = parser.Parse(html);

//            Assert.AreEqual("It is a strange thing to see a five legged elephant", result.Items[0].Get<Content, MfEmbedded>()[0].Value);
//        }

//        ///<summary>
//        /// parser not respecting not[h-*] in rule  "else if .h-x>a[href]:only-of-type:not[.h-*] then use that [href] for url"
//        ///</summary> 
//        [TestMethod]
//        public void NotImpliedUrlFromHCard()
//        {
//            var parser = new Mf2();
//            var html = "<span class=\"h-entry\">\r\n\t<a class=\"h-card\" href=\"http://test.com\">John Q</a>\r\n</span>";
//            var result = parser.Parse(html);

//            Assert.IsFalse(result.Items[0].Properties.ContainsKey("url"));
//        }

//        [TestMethod]
//        public void ParseHcardInCategory()
//        {
//            var parser = new Mf2();
//            var html = "<span class=\"h-entry\">\r\n\t<a class=\"p-author h-card\" href=\"http://a.example.com/\">Alice</a> tagged\r\n\t<a href=\"http://b.example.com/\" class=\"u-category h-card\">Bob Smith</a> in\r\n\t<a class=\"u-tag-of u-in-reply-to\" href=\"http://s.example.com/permalink47\">\r\n\t\t<img src=\"http://s.example.com/photo47.png\" alt=\"a photo of Bob and Cole\" />\r\n\t</a>\r\n</span>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Type.Contains("h-entry"));
//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("category"));

//            Assert.IsTrue(result.Items[0].Get<Category, MfType>()[0].Type.Contains("h-card"));
//            Assert.IsTrue(result.Items[0].Get<Category, MfType>()[0].Properties.ContainsKey("name"));
//            Assert.AreEqual("Bob Smith", result.Items[0].Get<Category, MfType>()[0].Get<PropertyName>());
//            Assert.IsTrue(result.Items[0].Get<Category, MfType>()[0].Properties.ContainsKey("url"));
//            Assert.AreEqual("http://b.example.com/", result.Items[0].Get<Category, MfType>()[0].Get<Url>());

//        }

//        [TestMethod]
//        public void ScriptTagContentsRemovedFromTextValue()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-entry\">\r\n\t<div class=\"p-content\">\r\n\t\t<b>Hello World</b>\r\n\t\t<script>alert(\"hi\");</script>\r\n\t</div>\r\n</div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Type.Contains("h-entry"));
//            Assert.IsTrue(result.Items[0].Get<Content>().Contains("Hello World"));
//            Assert.IsFalse(result.Items[0].Get<Content>().Contains("alert"));
//        }

//        [TestMethod]
//        public void ScriptElementContentsRemovedFromAllPlaintextValues()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-entry\">\r\n\t<span class=\"dt-published\">contained<script>not contained</script><style>not contained</style></span>\r\n\t<span class=\"u-url\">contained<script>not contained</script><style>not contained</style></span>\r\n</div>";
//            var result = parser.Parse(html);

//            Assert.IsFalse(result.Items[0].Get<Published>().Contains("not contained"));
//            Assert.IsFalse(result.Items[0].Get<Url>().Contains("not contained"));
//        }

//        [TestMethod]
//        public void ScriptTagContentsNotRemovedFromHTMLValue()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-entry\">\r\n\t<div class=\"e-content\">\r\n\t\t<b>Hello World</b>\r\n\t\t<script>alert(\"hi\");</script>\r\n\t\t<style>body{ visibility: hidden; }</style>\r\n\t\t<p>\r\n\t\t\t<script>alert(\"hi\");</script>\r\n\t\t\t<style>body{ visibility: hidden; }</style>\r\n\t\t</p>\r\n\t</div>\r\n</div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Type.Contains("h-entry"));
//            Assert.IsTrue(result.Items[0].Get<Content, MfEmbedded>()[0].Value.Contains("Hello World"));
//            Assert.IsTrue(result.Items[0].Get<Content, MfEmbedded>()[0].Html.Contains("<b>Hello World</b>"));

//            //The script and style tags should be removed from plaintext results but left in HTML results.
//            Assert.IsFalse(result.Items[0].Get<Content, MfEmbedded>()[0].Value.Contains("alert"));
//            Assert.IsTrue(result.Items[0].Get<Content, MfEmbedded>()[0].Html.Contains("alert"));
//            Assert.IsFalse(result.Items[0].Get<Content, MfEmbedded>()[0].Value.Contains("visibility"));
//            Assert.IsTrue(result.Items[0].Get<Content, MfEmbedded>()[0].Html.Contains("visibility"));
//        }

//        [TestMethod]
//        public void WhitespaceBetweenElements()
//        {

//            var parser = new Mf2();
//            var html = "<div class=\"h-entry\">\r\n\t<data class=\"p-rsvp\" value=\"yes\">I'm attending</data>\r\n\t<a class=\"u-in-reply-to\" href=\"https://snarfed.org/2014-06-16_homebrew-website-club-at-quip\">Homebrew Website Club at Quip</a>\r\n\t<div class=\"p-content\">Thanks for hosting!</div>\r\n</div>";
//            var result = parser.Parse(html);

//            Assert.IsFalse(result.Items[0].Get<Published>().Contains("not contained"));
//            Assert.IsFalse(result.Items[0].Get<Url>().Contains("not contained"));

//            Assert.IsTrue(result.Items[0].Type.Contains("h-entry"));
//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("name"));
//        }

//        ///<summary>
//        /// <see href="https://github.com/indieweb/php-mf2/issues/127"/>
//        ///</summary> 
//        [TestMethod]
//        public void CamelCaseRootClassNames()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-Entry\"> <a href=\"https://example.com\" class=\"u-url\">content</a> </div>";
//            var result = parser.Parse(html);

//            Assert.AreEqual(0, result.Items.Length);
//        }

//        ///<summary>
//        /// <see href="https://github.com/indieweb/php-mf2/issues/127"/>
//        ///</summary> 
//        [TestMethod]
//        public void MisTypedRootClassNames()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-entry>\"> <a href=\"https://example.com\" class=\"u-url\">content</a></div>\r\n\t\t<div class=\"h-entry1>\"> <a href=\"https://example.com\" class=\"u-url\">content</a></div>\r\n\t\t<div class=\"h-👍\"> <a href=\"https://example.com\" class=\"u-url\">content</a></div>\r\n\t\t<div class=\"h-hentry_\"> <a href=\"https://example.com\" class=\"u-url\">content</a></div>\r\n\t\t<div class=\"h-\"> <a href=\"https://example.com\" class=\"u-url\">content</a></div>";
//            var result = parser.Parse(html);

//            Assert.AreEqual(0, result.Items.Length);
//        }


//        [TestMethod]
//        public void ClassNameNumbers()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-entry\"> <div class=\"u-column1\"> <p class=\"p-title\">Test</p> </div> </div>";
//            var result = parser.Parse(html);

//            Assert.IsFalse(result.Items[0].Properties.ContainsKey("column1"));
//        }

//        ///<summary>
//        /// <see href="https://github.com/microformats/microformats2-parsing/issues/6#issuecomment-366473390"/>
//        ///</summary> 
//        [TestMethod]
//        public void NoImpliedNameWhenE()
//        {
//            var parser = new Mf2();
//            var html = "<article class=\"h-entry\">\r\n\t<div class=\"e-content\">\r\n\t\t<p>Wanted content.</p>\r\n\t</div>\r\n\t<footer>\r\n\t\t<p>Footer to be ignored.</p>\r\n\t</footer>\r\n</article>";
//            var result = parser.Parse(html);

//            Assert.IsFalse(result.Items[0].Properties.ContainsKey("name"));
//        }

//        ///<summary>
//        /// <see href="https://github.com/microformats/microformats2-parsing/issues/6#issuecomment-366473390"/>
//        ///</summary> 
//        [TestMethod]
//        public void NoImpliedNameWhenP()
//        {
//            var parser = new Mf2();
//            var html = "<article class=\"h-entry\">\r\n\t<div class=\"p-content\">\r\n\t\t<p>Wanted content.</p>\r\n\t</div>\r\n\t<footer>\r\n\t\t<p>Footer to be ignored.</p>\r\n\t</footer>\r\n</article>";
//            var result = parser.Parse(html);

//            Assert.IsFalse(result.Items[0].Properties.ContainsKey("name"));
//        }

//        ///<summary>
//        /// <see href="https://github.com/microformats/microformats2-parsing/issues/6#issuecomment-366473390"/>
//        ///</summary> 
//        [TestMethod]
//        public void NoImpliedNameWhenNestedMicroformat()
//        {
//            var parser = new Mf2();
//            var html = "<article class=\"h-entry\">\r\n\t<div class=\"u-like-of h-cite\">\r\n\t\t<p>I really like <a class=\"p-name u-url\" href=\"http://microformats.org/\">Microformats</a></p>\r\n\t</div>\r\n\t<footer>\r\n\t\t<p>Footer to be ignored.</p>\r\n\t</footer>\r\n</article>";
//            var result = parser.Parse(html);

//            Assert.IsFalse(result.Items[0].Properties.ContainsKey("name"));
//        }

//        ///<summary>
//        /// <see href="https://github.com/indieweb/php-mf2/issues/143"/>
//        ///</summary> 
//        [TestMethod]
//        public void ChildObjects()
//        {
//            var parser = new Mf2();
//            var html = "<html>\r\n\t<head>\r\n\t\t<title>Test</title>\r\n\t</head>\r\n\t<body>\r\n\r\n\t\t<div class=\"h-feed\">\r\n\t\t\t<a href=\"/author\" class=\"p-author h-card\">Author Name</a>\r\n\r\n\t\t\t<ul>\r\n\t\t\t\t<li class=\"h-entry\">\r\n\t\t\t\t\t<a href=\"/1\" class=\"u-url p-name\">One</a>\r\n\t\t\t\t</li>\r\n\t\t\t\t<li class=\"h-entry\">\r\n\t\t\t\t\t<a href=\"/2\" class=\"u-url p-name\">Two</a>\r\n\t\t\t\t</li>\r\n\t\t\t\t<li class=\"h-entry\">\r\n\t\t\t\t\t<a href=\"/3\" class=\"u-url p-name\">Three</a>\r\n\t\t\t\t</li>\r\n\t\t\t\t<li class=\"h-entry\">\r\n\t\t\t\t\t<a href=\"/4\" class=\"u-url p-name\">Four</a>\r\n\t\t\t\t</li>\r\n\t\t\t</ul>\r\n\t\t</div>\r\n\r\n\t</body>\r\n</html>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("author"));
//            Assert.AreEqual("Author Name", result.Items[0].Get<Author, MfType>()[0].Get<PropertyName>());

//            Assert.AreEqual(4, result.Items[0].Get<ImpliedEntry, MfType>().Length);
//            Assert.AreEqual("One", result.Items[0].Get<ImpliedEntry, MfType>()[0].Get<PropertyName>()[0]);
//            Assert.AreEqual("Two", result.Items[0].Get<ImpliedEntry, MfType>()[1].Get<PropertyName>()[0]);
//            Assert.AreEqual("Three", result.Items[0].Get<ImpliedEntry, MfType>()[2].Get<PropertyName>()[0]);
//            Assert.AreEqual("Four", result.Items[0].Get<ImpliedEntry, MfType>()[3].Get<PropertyName>()[0]);
//        }

//        [TestMethod]
//        public void MultiLevelRecursion()
//        {
//            var parser = new Mf2();
//            var html = "<html>\r\n\t<head>\r\n\t\t<title>Test</title>\r\n\t</head>\r\n\t<body>\r\n\r\n\t\t<div class=\"h-feed\">\r\n\t\t\t<a href=\"/author\" class=\"p-author h-card\">Author Name</a>\r\n\r\n\t\t\t<ul>\r\n\t\t\t\t<li class=\"h-entry\">\r\n\t\t\t\t\t<a href=\"/1\" class=\"u-url p-name\">One</a>\r\n\t\t\t\t</li>\r\n\t\t\t\t<li class=\"h-entry\">\r\n\t\t\t\t\t<a href=\"/2\" class=\"u-url p-name\">Two</a>\r\n\t\t\t\t\t<ul>\r\n\t\t\t\t\t\t<li class=\"p-comment h-entry\"><a href=\"/a\" class=\"u-url p-name\">Comment A</a></li>\r\n\t\t\t\t\t\t<li class=\"p-comment h-entry\"><a href=\"/b\" class=\"u-url p-name\">Comment B</a></li>\r\n\t\t\t\t\t</ul>\r\n\t\t\t\t</li>\r\n\t\t\t\t<li class=\"h-entry\">\r\n\t\t\t\t\t<a href=\"/3\" class=\"u-url p-name\">Three</a>\r\n\t\t\t\t\t<ul>\r\n\t\t\t\t\t\t<li class=\"h-entry\"><a href=\"/c\" class=\"u-url p-name\">Comment C</a></li>\r\n\t\t\t\t\t\t<li class=\"h-entry\"><a href=\"/d\" class=\"u-url p-name\">Comment D</a></li>\r\n\t\t\t\t\t</ul>\r\n\t\t\t\t</li>\r\n\t\t\t\t<li class=\"h-entry\">\r\n\t\t\t\t\t<a href=\"/4\" class=\"u-url p-name\">Four</a>\r\n\t\t\t\t</li>\r\n\t\t\t</ul>\r\n\t\t</div>\r\n\r\n\t</body>\r\n</html>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("author"));
//            Assert.AreEqual("Author Name", result.Items[0].Get<Author, MfType>()[0].Get<PropertyName>());

//            Assert.AreEqual(4, result.Items[0].Get<ImpliedEntry, MfType>().Length);
//            Assert.AreEqual("One", result.Items[0].Get<ImpliedEntry, MfType>()[0].Get<PropertyName>()[0]);
//            Assert.AreEqual("Two", result.Items[0].Get<ImpliedEntry, MfType>()[1].Get<PropertyName>()[0]);
//            Assert.AreEqual("Comment A", result.Items[0].Get<ImpliedEntry, MfType>()[1].Get<Comment, MfType>()[0].Get<PropertyName>());
//            Assert.AreEqual("Comment B", result.Items[0].Get<ImpliedEntry, MfType>()[1].Get<Comment, MfType>()[1].Get<PropertyName>());

//            Assert.AreEqual("Three", result.Items[0].Get<ImpliedEntry, MfType>()[2].Get<PropertyName>()[0]);
//            Assert.AreEqual("Comment C", result.Items[0].Get<ImpliedEntry, MfType>()[2].Get<Comment, MfType>()[0].Get<PropertyName>());
//            Assert.AreEqual("Comment D", result.Items[0].Get<ImpliedEntry, MfType>()[2].Get<Comment, MfType>()[1].Get<PropertyName>());

//            Assert.AreEqual("Four", result.Items[0].Get<ImpliedEntry, MfType>()[3].Get<PropertyName>()[0]);

//        }

//        [TestMethod]
//        public void NoErrantWhitespaceOnEHtml()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-entry\"><div class=\"e-content\"><p>1</p><p>2</p></div></div>";
//            var result = parser.Parse(html);

//            Assert.AreEqual("<p>1</p><p>2</p>", result.Items[0].Get<Content, MfEmbedded>()[3].Html);
//        }

//        //    ///<summary>
//        //    /// <see href="https://github.com/indieweb/php-mf2/issues/158"/>
//        //    ///</summary> 
//        //    [TestMethod]
//        //    public void PrefixWithNumbers()
//        //    {
//        //        var parser = new Mf2();
//        //        var html = "<li class=\"h-entry\">\r\n\t<data class=\"p-name\" value=\"Coffee\"></data>\r\n\t<div class=\"p-p3k-drank h-p3k-food\">\r\n\t\t<span class=\"value\">Coffee</span>\r\n\t</div>\r\n</li>";
//        //        var result = parser.Parse(html);

//        //    		$this->assertArrayHasKey('p3k-drank', $output['items'][0]['properties']);
//        //    		$this->assertCount(1, $output['items']
//        //[0]
//        //['properties']
//        //['p3k-drank']);
//        //    		$this->assertEquals('h-p3k-food', $output['items']
//        //[0]
//        //['properties']
//        //['p3k-drank']
//        //[0]
//        //['type']
//        //[0]);
//        //    }

//        //    ///<summary>
//        //    /// <see href="https://github.com/indieweb/php-mf2/issues/160"/>
//        //    ///</summary> 
//        //    [TestMethod]
//        //    public void ConsecutiveDashes()
//        //    {
//        //        var parser = new Mf2();
//        //        var html = "<div class=\"h-entry h-----\">\r\n<p> <a href=\"http://example.com/post\" class=\"u-in-reply--to\">http://example.com/post posted:</a> </p>\r\n<span class=\"p-name\">Too many dashes</span>\r\n<span class=\"p--acme-leading\">leading dash</span>\r\n<span class=\"p-acme--middle\">middle dash</span>\r\n<span class=\"p-acme-trailing-\">trailing dash</span>\r\n</div>";
//        //        var result = parser.Parse(html);

//        //    		$this->assertCount(1, $output['items'][0]['type']);
//        //    		$this->assertEquals('h-entry', $output['items']
//        //[0]
//        //['type']
//        //[0]);
//        //    		$this->assertCount(1, $output['items']
//        //[0]
//        //['properties']);
//        //    		$this->assertArrayHasKey('name', $output['items']
//        //[0]
//        //['properties']);
//        //    }

//        //    ///<summary>
//        //    /// Additional test from mf2py. Covers consecutive dashes, numbers in vendor prefix, and capital letters.
//        //    /// Added markup for numbers-only prefix and capital letter in prefix
//        //    /// <see href="https://github.com/kartikprabhu/mf2py/blob/experimental/test/examples/class_names_format.html"/>
//        //    /// <see href="https://github.com/indieweb/php-mf2/issues/160"/>
//        //    /// <see href="https://github.com/indieweb/php-mf2/issues/158"/>
//        //    ///</summary> 
//        //    [TestMethod]
//        //    public void MfClassRegex()
//        //    {
//        //    		$input = '<article class="h-x-test h-p3k-entry h-feed h-Entry h-p3k-fEed h--d h-test-">
//        //            < a class="u-url u-Url u-p3k-url u--url u-url- u-123-url u-123A-url" href="example.com" >URL</a>
//        //    		<span class="p-name p-nAme p-p3k-name p--name p-name-" >name</span>
//        //    </article>';
//        //    		$output = Mf2\parse($input);

//        //    		$this->assertCount(3, $output['items'][0]['type']);
//        //    		$this->assertContains('h-feed', $output['items']
//        //[0]
//        //['type']);
//        //    		$this->assertContains('h-p3k-entry', $output['items']
//        //[0]
//        //['type']);
//        //    		$this->assertContains('h-x-test', $output['items']
//        //[0]
//        //['type']);
//        //    		$this->assertCount(5, $output['items']
//        //[0]
//        //['properties']);
//        //    		$this->assertArrayHasKey('url', $output['items']
//        //[0]
//        //['properties']);
//        //    		$this->assertArrayHasKey('p3k-url', $output['items']
//        //[0]
//        //['properties']);
//        //    		$this->assertArrayHasKey('name', $output['items']
//        //[0]
//        //['properties']);
//        //    		$this->assertArrayHasKey('p3k-name', $output['items']
//        //[0]
//        //['properties']);
//        //    		$this->assertArrayHasKey('123-url', $output['items']
//        //[0]
//        //['properties']);
//        //}

//        /////<summary>
//        ///// <see href="https://github.com/microformats/microformats2-parsing/issues/30"/>
//        /////</summary> 
//        //[TestMethod]
//        //public void UniqueAndAlphabeticalMfClasses()
//        //{
//        //    		$input = '<div class="h-entry h-cite h-entry"></div>';
//        //    		$output = Mf2\parse($input);

//        //    		$this->assertEquals(array('h-cite', 'h-entry'), $output['items'][0]['type']);
//        //}

//        /////<summary>
//        ///// The default DOMDocument parser will trip here because it does not know HTML5 elements.
//        ///// <see href="https://html.spec.whatwg.org/#optional-tags:the-p-element"/>
//        /////</summary> 
//        //[TestMethod]
//        //public void Html5OptionalPEndTag()
//        //{
//        //    if (!class_exists('Masterminds\\HTML5'))
//        //    {
//        //    			$this->markTestSkipped('masterminds/html5 is required for this test.');
//        //    }
//        //    		$input = '<div class="h-entry"><p class="p-name">Name<article>Not Name</article></div>';
//        //    		$output = Mf2\parse($input);

//        //    		$this->assertEquals('Name', $output['items'][0]['properties']['name'][0]);
//        //}

//        /////<summary>
//        ///// Make sure the parser does not mutate any DOMDocument instances passed to the constructor.
//        ///// <see href="https://github.com/indieweb/php-mf2/issues/174"/>
//        ///// <see href="https://github.com/microformats/mf2py/issues/104"/>
//        /////</summary> 
//        //[TestMethod]
//        //public void NotMutatingPassedInDOM()
//        //{
//        //    		$input = file_get_contents(__DIR__. '/snarfed.org.html');

//        //    // Use same parsing as Parser::__construct(), twice to have a comparison object.
//        //    libxml_use_internal_errors(true);
//        //    		$refDoc = new \DOMDocument();
//        //    @$refDoc->loadHTML(Mf2\unicodeToHtmlEntities($input), \LIBXML_NOWARNING);
//        //    		$inputDoc = new \DOMDocument();
//        //    @$inputDoc->loadHTML(Mf2\unicodeToHtmlEntities($input), \LIBXML_NOWARNING);

//        //    		// For completion sake, test PHP itself.
//        //    		$this->assertEquals($refDoc, $inputDoc, 'PHP could not create identical DOMDocument instances.');

//        //    // Parse one DOMDocument instance, and test if it is still the same as the other.
//        //    Mf2\parse($inputDoc, 'http://snarfed.org/2013-10-23_oauth-dropins');
//        //    		$this->assertEquals($refDoc, $inputDoc, 'Parsing mutated the DOMDocument.');
//        //}

//        //[TestMethod]
//        //public void NoImpliedURLForEmptyProperties()
//        //{
//        //    		// In the 0.4.5 release, this caused an error
//        //    		// https://github.com/microformats/php-mf2/issues/196

//        //    		$input = <<< EOD
//        //    < div class="h-entry">
//        //      <li class="h-cite u-comment">
//        //        <div class="vcard"></div>
//        //      </li>
//        //    </div>
//        //    EOD;

//        //    		$output = Mf2\parse($input);
//        //    		$this->assertIsArray($output['items'][0]['properties']['comment'][0]['properties']);
//        //    		$this->assertIsArray($output ['items']
//        //[0]
//        //['properties']
//        //['comment']
//        //[0]
//        //['children']
//        //[0]
//        //['properties']);
//        //    		$this->assertEmpty($output ['items']
//        //[0]
//        //['properties']
//        //['comment']
//        //[0]
//        //['properties']);
//        //    		$this->assertEmpty($output ['items']
//        //[0]
//        //['properties']
//        //['comment']
//        //[0]
//        //['children']
//        //[0]
//        //['properties']);
//        //}

//        /////<summary>
//        ///// Make sure day of year passed to normalizeOrdinalDate() is valid
//        ///// <see href="https://github.com/indieweb/php-mf2/issues/167"/>
//        /////</summary> 
//        //[TestMethod]
//        //public void InvalidOrdinalDate()
//        //{
//        //    		# 365 days in non-leap years
//        //    		$this->assertEquals('2018-12-31', Mf2\normalizeOrdinalDate('2018-365'));
//        //    		$this->assertEquals('', Mf2\normalizeOrdinalDate('2018-366'));
//        //    		# 366 days in leap years
//        //    		$this->assertEquals('2016-12-31', Mf2\normalizeOrdinalDate('2016-366'));
//        //    		$this->assertEquals('', Mf2\normalizeOrdinalDate('2016-367'));
//        //}

//        /////<summary>
//        ///// <see href="https://github.com/microformats/php-mf2/issues/230"/>
//        /////</summary> 
//        //[TestMethod]
//        //public void PropertyWithInvalidHPrefixedRootClassParsed()
//        //{
//        //    		$input = <<< EOF
//        //    < div class="h-card">
//        //    	<img class="u-photo w-32 h-32" alt="Jon Doe" src="/image.jpg"/>
//        //    </div>
//        //    EOF;

//        //    		$output = Mf2\parse($input);
//        //    		$this->assertEquals(array('value' => '/image.jpg', 'alt' => 'Jon Doe'), $output ['items']
//        //[0]
//        //['properties']
//        //['photo']
//        //[0]);
//        //}

//        //[TestMethod]
//        //public void GetRootMfOnlyFindsValidElements()
//        //{
//        //    		$input = <<< EOF
//        //    < div class="h-entry>"> <a href = "https://example.com" class="u-url">content</a></div>
//        //    <div class="h-entry1>"> <a href = "https://example.com" class="u-url">content</a></div>
//        //    <div class="h-👍"> <a href = "https://example.com" class="u-url">content</a></div>
//        //    <div class="h-hentry_"> <a href = "https://example.com" class="u-url">content</a></div>
//        //    <div class="h-"> <a href = "https://example.com" class="u-url">content</a></div>
//        //    <div class="h-vendor123-name"><a href = "https://example.com" class="u-url">content</a></div>
//        //    EOF;

//        //    		$p = new Mf2\Parser($input);
//        //    		$rootEls = $p->getRootMF();

//        //    		$this->assertEquals(1, count($rootEls));
//        //    		$this->assertEquals('h-vendor123-name', $rootEls->item(0)->getAttribute('class'));
//        //}

//    }
//}
