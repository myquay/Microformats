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

        /// <summary>
        /// From: <see href="https://microformats.org/wiki/h-card"/>
        /// </summary>
        [TestMethod]
        public void LotsOfPropertiesExample()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\">\r\n<span class=\"p-name\">Sally Ride</span>\r\n<span class=\"p-honorific-prefix\">Dr.</span>\r\n<span class=\"p-given-name\">Sally</span>\r\n<abbr class=\"p-additional-name\">K.</abbr>\r\n<span class=\"p-family-name\">Ride</span>\r\n<span class=\"p-honorific-suffix\">Ph.D.</span>,\r\n<span class=\"p-nickname\">sallykride</span> (IRC)\r\n<div class=\"p-org\">Sally Ride Science</div>\r\n<img class=\"u-photo\" src=\"http://example.com/sk.jpg\"/>\r\n<a class=\"u-url\" href=\"http://sally.example.com\">w</a>,\r\n<a class=\"u-email\" href=\"mailto:sally@example.com\">e</a>\r\n<div class=\"p-tel\">+1.818.555.1212</div>\r\n<div class=\"p-street-address\">123 Main st.</div>\r\n<span class=\"p-locality\">Los Angeles</span>,\r\n<abbr class=\"p-region\" title=\"California\">CA</abbr>,\r\n<span class=\"p-postal-code\">91316</span>\r\n<div class=\"p-country-name\">U.S.A</div>\r\n<time class=\"dt-bday\">1951-05-26</time> birthday\r\n<div class=\"p-category\">physicist</div>\r\n<div class=\"p-note\">First American woman in space.</div>\r\n</div>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items.Length == 1);
            Assert.IsTrue(result.Items[0].Type.Length == 1);
            Assert.IsTrue(result.Items[0].Type[0] == "h-card");
            Assert.IsTrue(result.Items[0].Properties.Count == 20);
            Assert.IsTrue(result.Items[0].GetProperty(Props.Name)[0] == "Sally Ride");
            Assert.IsTrue(result.Items[0].GetProperty(Props.HonorificPrefix)[0] == "Dr.");
            Assert.IsTrue(result.Items[0].GetProperty(Props.GivenName)[0] == "Sally");
            Assert.IsTrue(result.Items[0].GetProperty(Props.AdditionalName)[0] == "K.");
            Assert.IsTrue(result.Items[0].GetProperty(Props.FamilyName)[0] == "Ride");
            Assert.IsTrue(result.Items[0].GetProperty(Props.HonorificSuffix)[0] == "Ph.D.");
            Assert.IsTrue(result.Items[0].GetProperty(Props.Nickname)[0] == "sallykride");
            Assert.IsTrue(result.Items[0].GetProperty(Props.Org)[0] == "Sally Ride Science");
            Assert.IsTrue(result.Items[0].GetProperty(Props.Photo)[0] == "http://example.com/sk.jpg");
            Assert.IsTrue(result.Items[0].GetProperty(Props.Url)[0] == "http://sally.example.com");
            Assert.IsTrue(result.Items[0].GetProperty(Props.Email)[0] == "mailto:sally@example.com");
            Assert.IsTrue(result.Items[0].GetProperty(Props.Telephone)[0] == "+1.818.555.1212");
            Assert.IsTrue(result.Items[0].GetProperty(Props.StreetAddress)[0] == "123 Main st.");
            Assert.IsTrue(result.Items[0].GetProperty(Props.Locality)[0] == "Los Angeles");
            Assert.IsTrue(result.Items[0].GetProperty(Props.Region)[0] == "California");
            Assert.IsTrue(result.Items[0].GetProperty(Props.PostalCode)[0] == "91316");
            Assert.IsTrue(result.Items[0].GetProperty(Props.CountryName)[0] == "U.S.A");
            Assert.IsTrue(result.Items[0].GetProperty(Props.Birthday)[0] == "1951-05-26");
            Assert.IsTrue(result.Items[0].GetProperty(Props.Category)[0] == "physicist");
            Assert.IsTrue(result.Items[0].GetProperty(Props.Note)[0] == "First American woman in space.");
        }

    }
}