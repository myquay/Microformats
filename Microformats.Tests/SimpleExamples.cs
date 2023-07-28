using Microformats.Definitions;
using Microformats.Definitions.Properties;
using Microformats.Result;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microformats.Tests
{
    /// <summary>
    /// From: <see href="http://microformats.org/wiki/microformats2#simple_microformats2_examples" />
    /// </summary>
    [TestClass]
    public class SimpleExamples
    {
        /// <summary>
        /// From: <see href="https://microformats.org/wiki/microformats2#person_example"/>
        /// </summary>
        [TestMethod]
        public void PersonExample()
        {
            var parser = new Mf2();
            var html = "<span class=\"h-card\">Frances Berriman</span>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items.Length == 1);
            Assert.IsTrue(result.Items[0].Type.Length == 1);
            Assert.IsTrue(result.Items[0].Type[0] == "h-card");
            Assert.IsTrue(result.Items[0].Properties.Count == 1);
            Assert.IsTrue(result.Items[0].GetProperty(Props.Name)[0] == "Frances Berriman");
        }

        /// <summary>
        /// From: <see href="https://microformats.org/wiki/microformats2#hyperlinked_person"/>
        /// </summary>
        [TestMethod]
        public void HyperlinkedPersonExample()
        {
            var parser = new Mf2();
            var html = "<a class=\"h-card\" href=\"http://benward.me\">Ben Ward</a>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items.Length == 1);
            Assert.IsTrue(result.Items[0].Type.Length == 1);
            Assert.IsTrue(result.Items[0].Type[0] == "h-card");
            Assert.IsTrue(result.Items[0].Properties.Count == 2);
            Assert.IsTrue(result.Items[0].GetProperty(Props.Name)[0] == "Ben Ward");
            Assert.IsTrue(result.Items[0].GetProperty(Props.Url)[0] == "http://benward.me");
        }

        /// <summary>
        /// From: <see href="https://microformats.org/wiki/microformats2#hyperlinked_person_image"/>
        /// </summary>
        [TestMethod]
        public void HyperlinkedPersonImageExample()
        {
            var parser = new Mf2();
            var html = "<a class=\"h-card\" href=\"http://rohit.khare.org/\">\r\n <img alt=\"Rohit Khare\"\r\n      src=\"https://s3.amazonaws.com/twitter_production/profile_images/53307499/180px-Rohit-sq_bigger.jpg\" />\r\n</a>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items.Length == 1);
            Assert.IsTrue(result.Items[0].Type.Length == 1);
            Assert.IsTrue(result.Items[0].Type[0] == "h-card");
            Assert.IsTrue(result.Items[0].Properties.Count == 3);
            Assert.IsTrue(result.Items[0].GetProperty(Props.Name)[0] == "Rohit Khare");
            Assert.IsTrue(result.Items[0].GetProperty(Props.Url)[0] == "http://rohit.khare.org/");
            Assert.IsTrue(result.Items[0].GetProperty(Props.Photo)[0] == "https://s3.amazonaws.com/twitter_production/profile_images/53307499/180px-Rohit-sq_bigger.jpg");
        }

        /// <summary>
        /// From: <see href="https://microformats.org/wiki/microformats2#detailed_person_example"/>
        /// </summary>
        [TestMethod]
        public void DefailedPersonImageExample()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\">\r\n  <img class=\"u-photo\" alt=\"photo of Mitchell\"\r\n       src=\"https://webfwd.org/content/about-experts/300.mitchellbaker/mentor_mbaker.jpg\"/>\r\n  <a class=\"p-name u-url\"\r\n     href=\"http://blog.lizardwrangler.com/\" \r\n    >Mitchell Baker</a>\r\n (<a class=\"u-url\" \r\n     href=\"https://twitter.com/MitchellBaker\"\r\n    >@MitchellBaker</a>)\r\n  <span class=\"p-org\">Mozilla Foundation</span>\r\n  <p class=\"p-note\">\r\n    Mitchell is responsible for setting the direction and scope of the Mozilla Foundation and its activities.\r\n  </p>\r\n  <span class=\"p-category\">Strategy</span>\r\n  <span class=\"p-category\">Leadership</span>\r\n</div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items.Length == 1);
            Assert.IsTrue(result.Items[0].Type.Length == 1);
            Assert.IsTrue(result.Items[0].Type[0] == "h-card");
            Assert.IsTrue(result.Items[0].Properties.Count == 6);
            Assert.IsTrue(result.Items[0].GetProperty(Props.Name)[0] == "Mitchell Baker");
            Assert.IsTrue(result.Items[0].GetProperty(Props.Org)[0] == "Mozilla Foundation");
            Assert.IsTrue(result.Items[0].GetProperty(Props.Note)[0] == "Mitchell is responsible for setting the direction and scope of the Mozilla Foundation and its activities.");
            Assert.IsTrue(result.Items[0].GetProperty(Props.Category)[0] == "Strategy"); 
            Assert.IsTrue(result.Items[0].GetProperty(Props.Category)[1] == "Leadership");
            Assert.IsTrue(result.Items[0].GetProperty(Props.Photo)[0] == "https://webfwd.org/content/about-experts/300.mitchellbaker/mentor_mbaker.jpg");
            Assert.IsTrue(result.Items[0].GetProperty(Props.Url)[0] == "http://blog.lizardwrangler.com/");
            Assert.IsTrue(result.Items[0].GetProperty(Props.Url)[1] == "https://twitter.com/MitchellBaker");

        }

        /// <summary>
        /// From: <see href="https://microformats.org/wiki/h-card"/>
        /// </summary>
        [TestMethod]
        public void MinimalOrgExample()
        {
            var parser = new Mf2();
            var html = "<span class=\"h-card\">\r\n  <a class=\"p-name p-org u-url\" href=\"https://microformats.org/\">microformats.org</a>\r\n</span>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items.Length == 1);
            Assert.IsTrue(result.Items[0].Type.Length == 1);
            Assert.IsTrue(result.Items[0].Type[0] == "h-card");
            Assert.IsTrue(result.Items[0].Properties.Count == 3);
            Assert.IsTrue(result.Items[0].GetProperty(Props.Name)[0] == "microformats.org");
            Assert.IsTrue(result.Items[0].GetProperty(Props.Url)[0] == "https://microformats.org/");
            Assert.IsTrue(result.Items[0].GetProperty(Props.Org)[0] == "microformats.org");
        }

        /// <summary>
        /// From: <see href="https://microformats.org/wiki/h-card"/>
        /// </summary>
        [TestMethod]
        public void NestedExample()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\">\r\n  <a class=\"p-name u-url\"\r\n     href=\"https://blog.lizardwrangler.com/\" \r\n    >Mitchell Baker</a> \r\n  (<a class=\"p-org h-card\" \r\n      href=\"https://mozilla.org/\"\r\n     >Mozilla Foundation</a>)\r\n</div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items.Length == 1);
            Assert.IsTrue(result.Items[0].Type.Length == 1);
            Assert.IsTrue(result.Items[0].Type[0] == "h-card");
            Assert.IsTrue(result.Items[0].Properties.Count == 3);
            Assert.IsTrue(result.Items[0].GetProperty(Props.Name)[0] == "Mitchell Baker");
            Assert.IsTrue(result.Items[0].GetProperty(Props.Url)[0] == "https://blog.lizardwrangler.com/");
            Assert.IsTrue(result.Items[0].Properties[Props.Org.Key][0].GetValue() == "Mozilla Foundation");
            Assert.IsTrue(result.Items[0].Properties[Props.Org.Key][0].GetValueMfType().GetProperty(Props.Name)[0] == "Mozilla Foundation");
            Assert.IsTrue(result.Items[0].Properties[Props.Org.Key][0].GetValueMfType().GetProperty(Props.Url)[0] == "https://mozilla.org/");
        }
    }
}