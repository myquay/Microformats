using Microformats.Grammar;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static Microformats.Definitions.Constants;

namespace Microformats.Tests.PHPMf2
{
    /// <summary>
    /// Test cases from the indieweb php-mf2 parser
    /// From: <see href="https://github.com/microformats/php-mf2/blob/main/tests/Mf2/ParseUTest.php" />
    /// </summary>
    [TestClass]
    public class ParseUTest
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
        public void ParseUHandlesA()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\"><a class=\"u-url\" href=\"http://example.com\">Awesome example website</a></div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("url"));
            Assert.AreEqual("http://example.com", result.Items[0].Get(Props.URL)[0]);
        }

        [TestMethod]
        public void ParseUHandlesEmptyHrefAttribute()
        {
            var parser = new Mf2().WithOptions(o => { o.BaseUri = new Uri("http://example.com/"); return o; });
            var html = "<div class=\"h-card\"><a class=\"u-url\" href=\"\">Awesome example website</a></div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("url"));
            Assert.AreEqual("http://example.com", result.Items[0].Get(Props.URL)[0]);
        }

        [TestMethod]
        public void ParseUHandlesMissingHrefAttribute()
        {
            var parser = new Mf2().WithOptions(o => { o.BaseUri = new Uri("http://example.com/"); return o; });
            var html = "<div class=\"h-card\"><a class=\"u-url\">Awesome example website</a></div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("url"));
            Assert.AreEqual("http://example.com/Awesome example website", result.Items[0].Get(Props.URL)[0]);
        }

        [TestMethod]
        public void ParseUHandlesImg()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\"><img class=\"u-photo\" src=\"http://example.com/someimage.png\"></div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("photo"));
            Assert.AreEqual("http://example.com/someimage.png", result.Items[0].Get(Props.PHOTO)[0]);
        }

        [TestMethod]
        public void ParseUHandlesImgwithAlt()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\"><img class=\"u-photo\" src=\"http://example.com/someimage.png\" alt=\"Test Alt\"></div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("photo"));
            Assert.AreEqual("http://example.com/someimage.png", result.Items[0].Get(Props.PHOTO)[0]);
            Assert.AreEqual("Test Alt", result.Items[0].Get<MfImage>(Props.PHOTO)[0].Alt);
        }

        [TestMethod]
        public void ParseUHandlesImgwithoutAlt()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\"><img class=\"u-photo\" src=\"http://example.com/someimage.png\" alt=\"Test Alt\"></div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("photo"));
            Assert.AreEqual("http://example.com/someimage.png", result.Items[0].Get(Props.PHOTO)[0]);
        }

        [TestMethod]
        public void ParseUHandlesArea()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\"><area class=\"u-photo\" href=\"http://example.com/someimage.png\"></area></div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("photo"));
            Assert.AreEqual("http://example.com/someimage.png", result.Items[0].Get(Props.PHOTO)[0]);
        }

        [TestMethod]
        public void ParseUHandlesObject()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\"><object class=\"u-photo\" data=\"http://example.com/someimage.png\"></object></div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("photo"));
            Assert.AreEqual("http://example.com/someimage.png", result.Items[0].Get(Props.PHOTO)[0]);
        }

        [TestMethod]
        public void ParseUHandlesAbbr()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\"><abbr class=\"u-photo\" title=\"http://example.com/someimage.png\"></abbr></div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("photo"));
            Assert.AreEqual("http://example.com/someimage.png", result.Items[0].Get(Props.PHOTO)[0]);
        }

        [TestMethod]
        public void ParseUHandlesAbbrNoTitle()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\"><abbr class=\"u-photo\">no title attribute</abbr></div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("photo"));
            Assert.AreEqual("no title attribute", result.Items[0].Get(Props.PHOTO)[0]);
        }

        [TestMethod]
        public void ParseUHandlesData()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\"><data class=\"u-photo\" value=\"http://example.com/someimage.png\"></data></div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("photo"));
            Assert.AreEqual("http://example.com/someimage.png", result.Items[0].Get(Props.PHOTO)[0]);
        }

        [TestMethod]
        public void ResolvesRelativeUrlsFromDocumentUrl()
        {
            var parser = new Mf2().WithOptions(o =>
            {
                o.BaseUri = new Uri("http://example.com/things/more/more.html");
                return o;
            });
            var html = "<div class=\"h-card\"><data class=\"u-photo\" value=\"http://example.com/someimage.png\"></data></div>";
            var result = parser.Parse(html);

            Assert.AreEqual("http://example.com/things/image.png", result.Items[0].Get(Props.PHOTO)[0]);
        }

        [TestMethod]
        public void ResolvesRelativeUrlsFromBaseUrl()
        {
            var parser = new Mf2().WithOptions(o =>
            {
                o.BaseUri = new Uri("http://example.com/things/more.html");
                return o;
            });
            var html = "<head><base href=\"http://example.com/things/more/andmore/\" /></head><body><div class=\"h-card\"><img class=\"u-photo\" src=\"../image.png\" /></div></body>";
            var result = parser.Parse(html);

            Assert.AreEqual("http://example.com/things/more/image.png", result.Items[0].Get(Props.PHOTO)[0]);
        }

        [TestMethod]
        public void ResolvesRelativeUrlsInImpliedMicroformats()
        {
            var parser = new Mf2().WithOptions(o =>
            {
                o.BaseUri = new Uri("http://example.com/things/more.html");
                return o;
            });
            var html = "<a class=\"h-card\"><img src=\"image.png\" /></a>";
            var result = parser.Parse(html);

            Assert.AreEqual("http://example.com/things/image.png", result.Items[0].Get(Props.PHOTO)[0]);
        }

        [TestMethod]
        public void ResolvesRelativeBaseRelativeUrlsInImpliedMicroformats()
        {
            var parser = new Mf2().WithOptions(o =>
            {
                o.BaseUri = new Uri("http://example.com/");
                return o;
            });
            var html = "<base href=\"things/\"/><a class=\"h-card\"><img src=\"image.png\" /></a>";
            var result = parser.Parse(html);

            Assert.AreEqual("http://example.com/things/image.png", result.Items[0].Get(Props.PHOTO)[0]);
        }

        ///<summary>
        /// <see href="https://github.com/indieweb/php-mf2/issues/33"/>
        ///</summary>  
        [TestMethod]
        public void ParsesHrefBeforeValueClass()
        {
            var parser = new Mf2();
            var html = "<span class=\"h-card\"><a class=\"u-url\" href=\"http://example.com/right\"><span class=\"value\">WRONG</span></a></span>";
            var result = parser.Parse(html);

            Assert.AreEqual("http://example.com/right", result.Items[0].Get(Props.URL)[0]);
        }

        [TestMethod]
        public void ParseUHandlesAudio()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-entry\"><audio class=\"u-audio\" src=\"http://example.com/audio.mp3\"></div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("audio"));
            Assert.AreEqual("http://example.com/audio.mp3", result.Items[0].Get(Props.AUDIO)[0]);
        }

        [TestMethod]
        public void ParseUHandlesVideo()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-entry\"><video class=\"u-video\" src=\"\"></video></div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("video"));
            Assert.AreEqual("http://example.com/video.mp4", result.Items[0].Get(Props.VIDEO)[0]);
        }

        [TestMethod]
        public void ParseUHandlesVideoNoSrc()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-entry\"><video class=\"u-video\">no video support</video></div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("video"));
            Assert.AreEqual("no video support", result.Items[0].Get(Props.VIDEO)[0]);
        }

        [TestMethod]
        public void ParseUHandlesSource()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-entry\"><video><source class=\"u-video\" src=\"http://example.com/video.mp4\" type=\"video/mp4\"><source class=\"u-video\" src=\"http://example.com/video.ogg\" type=\"video/ogg\"></video></div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("video"));
            Assert.AreEqual("http://example.com/video.mp4", result.Items[0].Get(Props.VIDEO)[0]);
            Assert.AreEqual("http://example.com/video.ogg", result.Items[0].Get(Props.VIDEO)[1]);
        }

        [TestMethod]
        public void ParseUHandlesVideoPoster()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-entry\"><video class=\"u-photo\" poster=\"http://example.com/posterimage.jpg\"><source class=\"u-video\" src=\"http://example.com/video.mp4\" type=\"video/mp4\"></video></div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("video"));
            Assert.AreEqual("http://example.com/video.mp4", result.Items[0].Get(Props.VIDEO)[0]);
            Assert.AreEqual("http://example.com/posterimage.jpg", result.Items[0].Get(Props.PHOTO)[0]);
        }

        [TestMethod]
        public void ParseUWithSpaces()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\"><a class=\"u-url\" href=\" http://example.com \">Awesome example website</a></div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("url"));
            Assert.AreEqual("http://example.com", result.Items[0].Get(Props.URL)[0]);
        }

        ///<summary>
        /// <see href="https://github.com/indieweb/php-mf2/issues/130"/>
        ///</summary>  
        [TestMethod]
        public void ImpliedUWithEmptyHref()
        {
            var parser = new Mf2().WithOptions(o =>
            {
                o.BaseUri = new Uri("http://example.com");
                return o;
            });
            var html = "<a class=\"h-card\" href=\"\">Jane Doe</a>\r\n<area class=\"h-card\" href=\"\" alt=\"Jane Doe\"/ >\r\n<div class=\"h-card\" ><a href=\"\">Jane Doe</a><p></p></div>\r\n<div class=\"h-card\" ><area href=\"\">Jane Doe</area><p></p></div>\r\n<div class=\"h-card\" ><a class=\"h-card\" href=\"\">Jane Doe</a><p></p></div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("url"));
            Assert.AreEqual("http://example.com/", result.Items[0].Get(Props.URL)[0]);

            Assert.IsTrue(result.Items[1].Properties.ContainsKey("url"));
            Assert.AreEqual("http://example.com/", result.Items[1].Get(Props.URL)[0]);

            Assert.IsTrue(result.Items[2].Properties.ContainsKey("url"));
            Assert.AreEqual("http://example.com/", result.Items[1].Get(Props.URL)[0]);

            Assert.IsTrue(result.Items[3].Properties.ContainsKey("url"));
            Assert.AreEqual("http://example.com/", result.Items[1].Get(Props.URL)[0]);

            Assert.IsTrue(result.Items[4].Properties.ContainsKey("url"));
            Assert.AreEqual("http://example.com/", result.Items[1].Get(Props.URL)[0]);
        }

        ///<summary>
        /// <see href="https://github.com/indieweb/php-mf2/issues/130"/>
        ///</summary>  
        [TestMethod]
        public void ValueFromLinkTag()
        {
            var parser = new Mf2().WithOptions(o =>
            {
                o.BaseUri = new Uri("https://example.com");
                return o;
            });
            var html = "<!doctype html>\r\n<html class=\"h-entry\">\r\n\t<head>\r\n\t\t<link rel=\"canonical\" class=\"u-url p-name\" href=\"https://example.com/\" title=\"Example.com homepage\">\r\n\t</head>\r\n\t<body></body>\r\n</html>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("url"));
            Assert.AreEqual("https://example.com/", result.Items[0].Get(Props.URL)[0]);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("name"));
            Assert.AreEqual("Example.com homepage", result.Items[0].Get(Props.NAME)[0]);
        }

        [TestMethod]
        public void ResolveFromDataElement()
        {
            var parser = new Mf2().WithOptions(o =>
            {
                o.BaseUri = new Uri("http://example.com");
                return o;
            });
            var html = "<div class=\"h-test\"><data class=\"u-url\" value=\"relative.html\"></data></div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("url"));
            Assert.AreEqual("https://example.com/relative.html", result.Items[0].Get(Props.URL)[0]);
        }

        ///<summary>
        /// <see href="https://github.com/microformats/php-mf2/issues/182"/>
        ///</summary> 
        [TestMethod]
        public void ResolveFromIframeElement()
        {
            var parser = new Mf2().WithOptions(o =>
            {
                o.BaseUri = new Uri("http://example.com");
                return o;
            });
            var html = "<div class=\"h-entry\">\r\n<h1 class=\"p-name\">Title</h1>\r\n<iframe src=\"https://example.com/index.html\" class=\"u-url\">\r\n  <p>Your browser does not support iframes.</p>\r\n</iframe>\r\n</div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("url"));
            Assert.AreEqual("https://example.com/index.html", result.Items[0].Get(Props.URL)[0]);
        }

    }
}
