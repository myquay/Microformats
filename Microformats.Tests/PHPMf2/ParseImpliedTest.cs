using Microformats;
using Microformats.Definitions.Properties.DateTime;
using Microformats.Definitions.Properties.Link;
using Microformats.Definitions.Properties.Standard;
using Microformats.Definitions.Vocabularies;
using Microformats.Grammar;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Microformats.Tests.PHPMf2
{
    /// <summary>
    /// Test cases from the indieweb php-mf2 parser
    /// From: <see href="https://github.com/microformats/php-mf2/blob/main/tests/Mf2/ParseImpliedTest.php" />
    /// </summary>
    [TestClass]
    public class ParseImpliedTest
    {

        [TestMethod]
        public void ParsesImpliedPNameFromNodeValue()
        {
            var parser = new Mf2();
            var html = "<span class=\"h-card\">The Name</span>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("name"));
            Assert.AreEqual("The Name", result.Items[0].Get<PropertyName>()[0]);
        }

        [TestMethod]
        public void ParsesImpliedPNameFromImgAlt()
        {
            var parser = new Mf2();
            var html = "<img class=\"h-card\" src=\"\" alt=\"The Name\" />";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("name"));
            Assert.AreEqual("The Name", result.Items[0].Get<PropertyName>()[0]);
        }

        [TestMethod]
        public void ParsesImpliedPNameFromNestedImgAlt()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\"><img src=\"\" alt=\"The Name\" /></div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("name"));
            Assert.AreEqual("The Name", result.Items[0].Get<PropertyName>()[0]);
        }

        [TestMethod]
        public void ParsesImpliedPNameFromDoublyNestedImgAlt()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\"><span><img src=\"\" alt=\"The Name\" /></span></div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("name"));
            Assert.AreEqual("The Name", result.Items[0].Get<PropertyName>()[0]);
        }

        [TestMethod]
        public void ParsesImpliedUPhotoFromImgSrcWithoutAlt()
        {
            var parser = new Mf2();
            var html = "<img class=\"h-card\" src=\"http://example.com/img.png\" />";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("photo"));
            Assert.AreEqual("http://example.com/img.png", result.Items[0].Get<Photo>()[0]);
        }

        [TestMethod]
        public void ParsesImpliedUPhotoFromImgSrcWithEmptyAlt()
        {
            var parser = new Mf2();
            var html = "<img class=\"h-card\" src=\"http://example.com/img.png\" alt=\"\" />";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("photo"));
            Assert.AreEqual("http://example.com/img.png", result.Items[0].Get<Photo, MfImage>()[0].Value);
            Assert.AreEqual("", result.Items[0].Get<Photo, MfImage>()[0].Alt);
        }

        [TestMethod]
        public void ParsesImpliedUPhotoFromImgSrcWithAlt()
        {
            var parser = new Mf2();
            var html = "<img class=\"h-card\" src=\"http://example.com/img.png\" alt=\"Example\" />";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("photo"));
            Assert.AreEqual("http://example.com/img.png", result.Items[0].Get<Photo, MfImage>()[0].Value);
            Assert.AreEqual("Example", result.Items[0].Get<Photo, MfImage>()[0].Alt);
        }

        [TestMethod]
        public void ParsesImpliedUPhotoFromNestedImgSrc()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\"><img src=\"http://example.com/img.png\" alt=\"\" /></div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("photo"));
            Assert.AreEqual("http://example.com/img.png", result.Items[0].Get<Photo, MfImage>()[0].Value);
            Assert.AreEqual("", result.Items[0].Get<Photo, MfImage>()[0].Alt);
        }

        [TestMethod]
        public void ParsesImpliedUPhotoFromDoublyNestedImgSrc()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\"><span><img src=\"http://example.com/img.png\" alt=\"\" /></span></div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("photo"));
            Assert.AreEqual("http://example.com/img.png", result.Items[0].Get<Photo, MfImage>()[0].Value);
            Assert.AreEqual("", result.Items[0].Get<Photo, MfImage>()[0].Alt);
        }

        [TestMethod]
        public void ParsesImpliedUUrlFromAHref()
        {
            var parser = new Mf2();
            var html = "<a class=\"h-card\" href=\"http://example.com/\">Some Name</a>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("url"));
            Assert.AreEqual("http://example.com/", result.Items[0].Get<Url>()[0]);
        }

        [TestMethod]
        public void ParsesImpliedUUrlFromNestedAHref()
        {
            var parser = new Mf2();
            var html = "<span class=\"h-card\"><a href=\"http://example.com/\">Some Name</a></span>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("url"));
            Assert.AreEqual("http://example.com/", result.Items[0].Get<Url>()[0]);
        }

        [TestMethod]
        public void ParsesImpliedUUrlWithExplicitName()
        {
            var parser = new Mf2();
            var html = "<span class=\"h-card\"><a href=\"http://example.com/\" class=\"p-name\">Some Name</a></span>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("url"));
            Assert.AreEqual("http://example.com/", result.Items[0].Get<Url>()[0]);
        }

        [TestMethod]
        public void ParsesImpliedNameWithExplicitURL()
        {
            var parser = new Mf2();
            var html = "<span class=\"h-card\"><a href=\"http://example.com/\" class=\"u-url\">Some Name</a></span>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("url"));
            Assert.AreEqual("http://example.com/", result.Items[0].Get<Url>()[0]);
            Assert.AreEqual("Some Name", result.Items[0].Get<PropertyName>()[0]);
        }

        [TestMethod]
        public void MultipleImpliedHCards()
        {

            var parser = new Mf2();
            var html = "<span class=\"h-card\">Frances Berriman</span>\r\n\r\n<a class=\"h-card\" href=\"http://benward.me\">Ben Ward</a>\r\n\r\n<img class=\"h-card\" alt=\"Sally Ride\"\r\n\t src=\"http://upload.wikimedia.org/wikipedia/commons/a/a4/Ride-s.jpg\"/>\r\n\r\n<a class=\"h-card\" href=\"http://tantek.com\">\r\n <img alt=\"Tantek Çelik\" src=\"http://ttk.me/logo.jpg\"/>\r\n</a>";
            var result = parser.Parse(html);


            Assert.AreEqual(4, result.Items.Length);

            Assert.AreEqual("Frances Berriman", result.Items[0].Get<PropertyName>()[0]);

            Assert.AreEqual("Ben Ward", result.Items[1].Get<PropertyName>()[0]);
            Assert.AreEqual("http://benward.me", result.Items[1].Get<Url>()[0]);

            Assert.AreEqual("Sally Ride", result.Items[2].Get<PropertyName>()[0]);
            Assert.AreEqual("http://upload.wikimedia.org/wikipedia/commons/a/a4/Ride-s.jpg", result.Items[2].Get<Photo, MfImage>()[0].Value);
            Assert.AreEqual("Sally Ride", result.Items[2].Get<Photo, MfImage>()[0].Alt);

            Assert.AreEqual("Tantek Çelik", result.Items[3].Get<PropertyName>()[0]);
            Assert.AreEqual("http://tantek.com", result.Items[3].Get<Url>()[0]);
            Assert.AreEqual("http://ttk.me/logo.jpg", result.Items[3].Get<Photo, MfImage>()[0].Value);
            Assert.AreEqual("Tantek Çelik", result.Items[3].Get<Photo, MfImage>()[0].Alt);
        }

        /// <summary>
        /// <see href="https://github.com/indieweb/php-mf2/issues/37"/>
        /// </summary>
        [TestMethod]
        public void ParsesImpliedNameConsistentWithPName()
        {
            var parser = new Mf2();

            var inner = "Name	\nand more";
            var html = $"<span class=\"h-card\"> {inner} </span><span class=\"h-card\"><span class=\"p-name\"> {inner} </span></span>";
            var result = parser.Parse(html);

            Assert.AreEqual(2, result.Items.Length);
            Assert.AreEqual("Name and more", result.Items[0].Get<PropertyName>()[0]);
            Assert.AreEqual("Name and more", result.Items[1].Get<PropertyName>()[0]);
        }

        /// <summary>
        /// <see href="https://github.com/indieweb/php-mf2/issues/6"/>
        /// </summary>
        [TestMethod]
        public void ParsesImpliedNameFromAbbrTitle()
        {
            var parser = new Mf2();
            var html = "<abbr class=\"h-card\" title=\"Barnaby Walters\">BJW</abbr>";
            var result = parser.Parse(html);
            Assert.AreEqual("Barnaby Walters", result.Items[0].Get<PropertyName>()[0]);
        }

        [TestMethod]
        public void ImpliedPhotoFromObject()
        {
            var parser = new Mf2();
            var html = "<object class=\"h-card\" data=\"http://example/photo1.jpg\">John Doe</object>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("photo"));
            Assert.AreEqual("http://example/photo1.jpg", result.Items[0].Get<Photo>()[0]);
        }

        [TestMethod]
        public void ImpliedPhotoFromNestedImg()
        {
            var parser = new Mf2();
            var html = "<span class=\"h-card\"><a href=\"http://tantek.com/\" class=\"external text\" style=\"border: 0px none;\"><img src=\"https://pbs.twimg.com/profile_images/423350922408767488/nlA_m2WH.jpeg\" style=\"width:128px;float:right;margin-left:1em\"><b><span style=\"font-size:2em\">Tantek Çelik</span></b></a></span>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("photo"));
            Assert.AreEqual("https://pbs.twimg.com/profile_images/423350922408767488/nlA_m2WH.jpeg", result.Items[0].Get<Photo>()[0]);
        }

        [TestMethod]
        public void IgnoredPhotoIfMultipleImg()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\"><img src=\"http://example/photo2.jpg\" /> <img src=\"http://example/photo3.jpg\" /> </div>";
            var result = parser.Parse(html);

            Assert.IsFalse(result.Items[0].Properties.ContainsKey("photo"));
        }

        [TestMethod]
        public void ImpliedPhotoFromNestedObject()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\"><object data=\"http://example/photo3.jpg\">John Doe</object> <p>Moar text</p></div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("photo"));
            Assert.AreEqual("http://example/photo3.jpg", result.Items[0].Get<Photo>()[0]);
        }

        [TestMethod]
        public void IgnoredPhotoIfMultipleObject()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\"><object data=\"http://example/photo3.jpg\">John Doe</object> <object data=\"http://example/photo4.jpg\"></object> </div>";
            var result = parser.Parse(html);

            Assert.IsFalse(result.Items[0].Properties.ContainsKey("photo"));
        }

        [TestMethod]
        public void IgnoredPhotoIfNestedImgH()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-entry\"> <img src=\"http://example/photo2.jpg\" class=\"h-card\" /> </div>";
            var result = parser.Parse(html);

            Assert.IsFalse(result.Items[0].Properties.ContainsKey("photo"));
        }

        [TestMethod]
        public void IgnoredPhotoIfNestedImgHasHClass()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-entry\"> <img src=\"http://example/photo2.jpg\" alt=\"John Doe\" class=\"h-card\" /> </div>";
            var result = parser.Parse(html);

            Assert.IsFalse(result.Items[0].Properties.ContainsKey("photo"));
        }

        [TestMethod]
        public void IgnoredPhotoIfNestedObjectHasHClass()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-entry\"> <object data=\"http://example/photo3.jpg\" class=\"h-card\">John Doe</object> </div>";
            var result = parser.Parse(html);

            Assert.IsFalse(result.Items[0].Properties.ContainsKey("photo"));
        }

        /// <summary>
        /// <see href="https://github.com/indieweb/php-mf2/issues/176"/>
        /// </summary>
        [TestMethod]
        public void IgnoredPhotoImgInNestedH()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-entry\"> <div class=\"u-comment h-cite\"> <img src=\"/image.jpg\"> </div> </div>";
            var result = parser.Parse(html);

            Assert.IsFalse(result.Items[0].Properties.ContainsKey("photo"));
        }

        /// <summary>
        /// <see href="https://github.com/indieweb/php-mf2/issues/176"/>
        /// </summary>
        [TestMethod]
        public void IgnoredPhotoObjectInNestedH()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-entry\"> <div class=\"u-comment h-cite\"> <object data=\"/image2.jpg\">John Doe</object> </div> </div>";
            var result = parser.Parse(html);

            Assert.IsFalse(result.Items[0].Properties.ContainsKey("photo"));
        }

        /// <summary>
        /// <see href="https://github.com/indieweb/php-mf2/issues/190"/>
        /// </summary>
        [TestMethod]
        public void IgnoredMultiChildrenWithNestedPhotoImg()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\"> <a href=\"https://example.com\"><img src=\"https://example.com/photo.jpg\"></a> <span class=\"p-name\"><a href=\"/User:Example.com\">Max Mustermann</a></span> </div>";
            var result = parser.Parse(html);

            Assert.IsFalse(result.Items[0].Properties.ContainsKey("photo"));
        }

        /// <summary>
        /// <see href="https://github.com/indieweb/php-mf2/issues/190"/>
        /// </summary>
        [TestMethod]
        public void IgnoredMultiChildrenWithNestedPhotoObject()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\"> <a href=\"https://example.com\"><object data=\"https://example.com/photo.jpg\"></object></a> <span class=\"p-name\"><a href=\"/User:Example.com\">Max Mustermann</a></span> </div>";
            var result = parser.Parse(html);

            Assert.IsFalse(result.Items[0].Properties.ContainsKey("photo"));
        }


        /// <summary>
        /// Imply properties only on explicit h-x class name root microformat element (no backcompat roots)
        /// <see href="http://microformats.org/wiki/microformats2-parsing#parsing_for_implied_properties"/>
        /// </summary>
        [TestMethod]
        public void BackcompatNoImpliedName()
        {
            var parser = new Mf2();
            var html = "<div class=\"hentry\"> <div class=\"entry-content\"> <p> blah blah blah </p> </div> </div>";
            var result = parser.Parse(html);

            Assert.IsFalse(result.Items[0].Properties.ContainsKey("name"));
            Assert.IsFalse(result.Items[0].Properties.ContainsKey("content"));
        }

        /// <summary>
        /// Imply properties only on explicit h-x class name root microformat element (no backcompat roots)
        /// <see href="http://microformats.org/wiki/microformats2-parsing#parsing_for_implied_properties"/>
        /// </summary>
        [TestMethod]
        public void BackcompatNoImpliedPhoto()
        {
            var parser = new Mf2();
            var html = "<div class=\"hentry\"> <img src=\"https://example.com/photo.jpg\" alt=\"photo\" /> </div>";
            var result = parser.Parse(html);

            Assert.AreEqual(0, result.Items[0].Properties.Count);
        }

        /// <summary>
        /// Imply properties only on explicit h-x class name root microformat element (no backcompat roots)
        /// <see href="http://microformats.org/wiki/microformats2-parsing#parsing_for_implied_properties"/>
        /// </summary>
        [TestMethod]
        public void BackcompatNoImpliedUrl()
        {
            var parser = new Mf2();
            var html = "<div class=\"hentry\"> <a href=\"https://example.com/this-post\" class=\"entry-title\">Title</a> <div class=\"entry-content\"> <p> blah blah blah </p> </div> </div>";
            var result = parser.Parse(html);

            Assert.IsFalse(result.Items[0].Properties.ContainsKey("url"));
            Assert.IsTrue(result.Items[0].Properties.ContainsKey("name"));
            Assert.IsTrue(result.Items[0].Properties.ContainsKey("content"));
        }

        /// <summary>
        /// Don't imply u-url if there are other u-*
        /// <see href="http://microformats.org/wiki/microformats2-parsing#parsing_for_implied_properties"/>
        /// <see href="https://github.com/microformats/php-mf2/issues/183"/>
        /// </summary>
        [TestMethod]
        public void NoImpliedUrl()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-entry\"> <h1 class=\"p-name\"><a href=\"https://example.com/this-post\">Title</a></h1> <div class=\"e-content\"> <p> blah blah blah </p> </div> <a href=\"https://example.org/syndicate\" class=\"u-syndication\"></a> </div>";
            var result = parser.Parse(html);

            Assert.IsFalse(result.Items[0].Properties.ContainsKey("url"));
        }

        /// <summary>
        /// Don't imply u-url if there are other u-*
        /// <see href="https://github.com/microformats/php-mf2/issues/180"/>
        /// </summary>
        [TestMethod]
        public void NoImgSrcImpliedName()
        {
            var parser = new Mf2();
            var html = "<p class=\"h-card\">My Name <img src=\"http://xyz\" /></p>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items[0].Properties.ContainsKey("name"));
            Assert.AreEqual("My Name", result.Items[0].Get<PropertyName>()[0]);
        }

        /// <summary>
        /// <see href="https://github.com/microformats/php-mf2/issues/198"/>
        /// </summary>
        [TestMethod]
        public void NoImpliedPhotoWhenExplicitUProperty()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\"> <span class=\"p-org\">Organization Name</span> <img src=\"/logo.png\" class=\"u-logo\" alt=\"\"> </div>";
            var result = parser.Parse(html);

            Assert.IsFalse(result.Items[0].Properties.ContainsKey("photo"));
        }

        /// <summary>
        /// <see href="https://github.com/microformats/php-mf2/issues/198"/>
        /// </summary>
        [TestMethod]
        public void NoImpliedPhotoWhenNestedMicroformat()
        {

            var parser = new Mf2();
            var html = "<div class=\"h-entry\"> <img src=\"/photo.jpg\" alt=\"\"> <div class=\"p-author h-card\"> <span class=\"p-name\">Alice</span> <span class=\"p-org\">Organization Name</span> <img src=\"/logo.png\" class=\"u-logo\" alt=\"\"> </div> </div>";
            var result = parser.Parse(html);

            Assert.IsFalse(result.Items[0].Properties.ContainsKey("photo"));
            Assert.IsTrue(result.Items[0].Properties.ContainsKey("author"));
            Assert.IsFalse(result.Items[0].Get<Author, MfType>()[0].Properties.ContainsKey("photo"));
        }

    }
}
