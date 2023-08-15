using Microformats.Grammar;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using static Microformats.Definitions.Constants;

namespace Microformats.Tests.PHPMf2
{
    /// <summary>
    /// Test cases from the indieweb php-mf2 parser
    /// From: <see href="https://github.com/microformats/php-mf2/blob/main/tests/Mf2/PlainTextTest.php" />
    /// </summary>
    [TestClass]
    public class PlainTextTest
    {

        [TestMethod]
        public void Test01()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-entry\">\n  <div class=\"e-content p-name\"><p>Hello World</p></div>\n</div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("name"));
            Assert.AreEqual("Hello World", result.Items[0].Get(Props.NAME)[0]);
            Assert.AreEqual("Hello World", result.Items[0].Get<MfEmbedded>(Props.CONTENT)[0].Value);
            Assert.AreEqual("<p>Hello World</p>", result.Items[0].Get<MfEmbedded>(Props.CONTENT)[0].Html);
        }

        [TestMethod]
        public void Test02()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-entry\">\n  <div class=\"e-content p-name\"><p>Hello<br>World</p></div>\n</div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("name"));
            Assert.AreEqual("Hello\nWorld", result.Items[0].Get(Props.NAME)[0]);
            Assert.AreEqual("Hello\nWorld", result.Items[0].Get<MfEmbedded>(Props.CONTENT)[0].Value);
            Assert.AreEqual("<p>Hello<br>World</p>", result.Items[0].Get<MfEmbedded>(Props.CONTENT)[0].Html);
        }

        [TestMethod]
        public void Test03()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-entry\">\n  <div class=\"e-content p-name\"><p>Hello<br>\nWorld</p></div>\n</div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("name"));
            Assert.AreEqual("Hello\nWorld", result.Items[0].Get(Props.NAME)[0]);
            Assert.AreEqual("Hello\nWorld", result.Items[0].Get<MfEmbedded>(Props.CONTENT)[0].Value);
            Assert.AreEqual("<p>Hello<br>\nWorld</p>", result.Items[0].Get<MfEmbedded>(Props.CONTENT)[0].Html);
        }

        [TestMethod]
        public void Test04()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-entry\">\n  <div class=\"e-content p-name\">\n    <p>Hello World</p>\n  </div>\n</div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("name"));
            Assert.AreEqual("Hello World", result.Items[0].Get(Props.NAME)[0]);
            Assert.AreEqual("Hello World", result.Items[0].Get<MfEmbedded>(Props.CONTENT)[0].Value);
            Assert.AreEqual("<p>Hello World</p>", result.Items[0].Get<MfEmbedded>(Props.CONTENT)[0].Html);
        }

        [TestMethod]
        public void Test05()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-entry\">\n  <div class=\"e-content p-name\">Hello\nWorld</div>\n</div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("name"));
            Assert.AreEqual("Hello World", result.Items[0].Get(Props.NAME)[0]);
            Assert.AreEqual("Hello World", result.Items[0].Get<MfEmbedded>(Props.CONTENT)[0].Value);
            Assert.AreEqual("Hello\nWorld", result.Items[0].Get<MfEmbedded>(Props.CONTENT)[0].Html);
        }

        [TestMethod]
        public void Test06()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-entry\">\n  <div class=\"e-content p-name\"><p>Hello</p><p>World</p></div>\n</div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("name"));
            Assert.AreEqual("Hello\nWorld", result.Items[0].Get(Props.NAME)[0]);
            Assert.AreEqual("Hello\nWorld", result.Items[0].Get<MfEmbedded>(Props.CONTENT)[0].Value);
            Assert.AreEqual("<p>Hello</p><p>World</p>", result.Items[0].Get<MfEmbedded>(Props.CONTENT)[0].Html);
        }

        [TestMethod]
        public void Test07()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-entry\">\n  <div class=\"e-content p-name\">Hello<br>\n    World</div>\n</div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("name"));
            Assert.AreEqual("Hello\nWorld", result.Items[0].Get(Props.NAME)[0]);
            Assert.AreEqual("Hello\nWorld", result.Items[0].Get<MfEmbedded>(Props.CONTENT)[0].Value);
            Assert.AreEqual("Hello<br>\n    World", result.Items[0].Get<MfEmbedded>(Props.CONTENT)[0].Html);
        }

        [TestMethod]
        public void Test08()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-entry\">\n  <div class=\"e-content p-name\"><br>Hello<br>World<br></div>\n</div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("name"));
            Assert.AreEqual("Hello\nWorld", result.Items[0].Get(Props.NAME)[0]);
            Assert.AreEqual("Hello\nWorld", result.Items[0].Get<MfEmbedded>(Props.CONTENT)[0].Value);
            Assert.AreEqual("<br>Hello<br>World<br>", result.Items[0].Get<MfEmbedded>(Props.CONTENT)[0].Html);
        }

        [TestMethod]
        public void Test09()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-entry\">\n  <div class=\"e-content p-name\">\n    <p>One</p>\n    <p>Two</p>\n    <p>Three</p>\n  </div>\n</div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("name"));
            Assert.AreEqual("One\nTwo\nThree", result.Items[0].Get(Props.NAME)[0]);
            Assert.AreEqual("One\nTwo\nThree", result.Items[0].Get<MfEmbedded>(Props.CONTENT)[0].Value);
            Assert.AreEqual("<p>One</p>\n    <p>Two</p>\n    <p>Three</p>", result.Items[0].Get<MfEmbedded>(Props.CONTENT)[0].Html);
        }
    }
}
