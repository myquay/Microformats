using Microformats.Definitions;
using Microformats.Grammar;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Concurrent;
using System.Linq;
using static Microformats.Definitions.Constants;

namespace Microformats.Tests
{
    /// <summary>
    /// Tests from the microformats2 wiki specification.
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
            
            Assert.IsTrue(result.Items[0].Type.Contains("h-card"));
            Assert.AreEqual(1, result.Items[0].Properties.Count);
            Assert.AreEqual("Frances Berriman", result.Items[0].Get(Props.NAME)[0]);
        }

        /// <summary>
        /// From: <see href="https://microformats.org/wiki/microformats2#hyperlinked_person"/>
        /// </summary>
        [TestMethod]
        public void PersonHyperlinkedExample()
        {
            var parser = new Mf2();
            var html = "<a class=\"h-card\" href=\"http://benward.me\">Ben Ward</a>";
            var result = parser.Parse(html);

            Assert.AreEqual(1, result.Items.Length);
            Assert.IsTrue(result.Items[0].Type.Contains("h-card"));
            Assert.AreEqual(2, result.Items[0].Properties.Count);
            Assert.AreEqual("Ben Ward", result.Items[0].Get(Props.NAME)[0]);
            Assert.AreEqual("http://benward.me", result.Items[0].Get(Props.URL)[0]);
        }

        /// <summary>
        /// From: <see href="https://microformats.org/wiki/microformats2#hyperlinked_person_image"/>
        /// </summary>
        [TestMethod]
        public void PersonHyperlinkedPersonImageExample()
        {
            var parser = new Mf2();
            var html = "<a class=\"h-card\" href=\"http://rohit.khare.org/\">\r\n <img alt=\"Rohit Khare\"\r\n      src=\"https://s3.amazonaws.com/twitter_production/profile_images/53307499/180px-Rohit-sq_bigger.jpg\" />\r\n</a>";
            var result = parser.Parse(html);

            Assert.AreEqual(1, result.Items.Length);
            Assert.AreEqual(1, result.Items[0].Type.Length);
            Assert.IsTrue(result.Items[0].Type.Contains("h-card"));
            Assert.AreEqual(3, result.Items[0].Properties.Count);
            Assert.AreEqual("Rohit Khare", result.Items[0].Get(Props.NAME)[0]);
            Assert.AreEqual("http://rohit.khare.org/", result.Items[0].Get(Props.URL)[0]);
            Assert.AreEqual("https://s3.amazonaws.com/twitter_production/profile_images/53307499/180px-Rohit-sq_bigger.jpg", result.Items[0].Get(Props.PHOTO)[0]);
        }

        /// <summary>
        /// From: <see href="https://microformats.org/wiki/microformats2#detailed_person_example"/>
        /// </summary>
        [TestMethod]
        public void PersonDefailedImageExample()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\">\r\n  <img class=\"u-photo\" alt=\"photo of Mitchell\"\r\n       src=\"https://webfwd.org/content/about-experts/300.mitchellbaker/mentor_mbaker.jpg\"/>\r\n  <a class=\"p-name u-url\"\r\n     href=\"http://blog.lizardwrangler.com/\" \r\n    >Mitchell Baker</a>\r\n (<a class=\"u-url\" \r\n     href=\"https://twitter.com/MitchellBaker\"\r\n    >@MitchellBaker</a>)\r\n  <span class=\"p-org\">Mozilla Foundation</span>\r\n  <p class=\"p-note\">\r\n    Mitchell is responsible for setting the direction and scope of the Mozilla Foundation and its activities.\r\n  </p>\r\n  <span class=\"p-category\">Strategy</span>\r\n  <span class=\"p-category\">Leadership</span>\r\n</div>";
            var result = parser.Parse(html);

            Assert.AreEqual(1, result.Items.Length);
            Assert.AreEqual(1, result.Items[0].Type.Length);
            Assert.IsTrue(result.Items[0].Type.Contains("h-card"));
            Assert.AreEqual(6, result.Items[0].Properties.Count);
            Assert.AreEqual("Mitchell Baker", result.Items[0].Get(Props.NAME)[0]);
            Assert.AreEqual("Mozilla Foundation", result.Items[0].Get(Props.ORGANIZATION)[0]);
            Assert.AreEqual("Mitchell is responsible for setting the direction and scope of the Mozilla Foundation and its activities.", result.Items[0].Get(Props.NOTE)[0]);
            Assert.AreEqual("Strategy", result.Items[0].Get(Props.CATEGORY)[0]);
            Assert.AreEqual("Leadership", result.Items[0].Get(Props.CATEGORY)[1]);
            Assert.AreEqual("https://webfwd.org/content/about-experts/300.mitchellbaker/mentor_mbaker.jpg", result.Items[0].Get(Props.PHOTO)[0]);
            Assert.AreEqual("http://blog.lizardwrangler.com/", result.Items[0].Get(Props.URL)[0]);
            Assert.AreEqual("https://twitter.com/MitchellBaker", result.Items[0].Get(Props.URL)[1]);
        }

        /// <summary>
        /// From: <see href="https://microformats.org/wiki/h-card"/>
        /// </summary>
        [TestMethod]
        public void PersonOrgMinimalExample()
        {
            var parser = new Mf2();
            var html = "<span class=\"h-card\">\r\n  <a class=\"p-name p-org u-url\" href=\"https://microformats.org/\">microformats.org</a>\r\n</span>";
            var result = parser.Parse(html);

            Assert.AreEqual(1, result.Items.Length);
            Assert.AreEqual(1, result.Items[0].Type.Length);
            Assert.IsTrue(result.Items[0].Type.Contains("h-card"));
            Assert.AreEqual(3, result.Items[0].Properties.Count);
            Assert.AreEqual("microformats.org", result.Items[0].Get(Props.NAME)[0]);
            Assert.AreEqual("https://microformats.org/", result.Items[0].Get(Props.URL)[0]);
            Assert.AreEqual("microformats.org", result.Items[0].Get(Props.ORGANIZATION)[0]);
        }

        /// <summary>
        /// From: <see href="https://microformats.org/wiki/h-card"/>
        /// </summary>
        [TestMethod]
        public void PersonNestedExample()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\">\r\n  <a class=\"p-name u-url\"\r\n     href=\"https://blog.lizardwrangler.com/\" \r\n    >Mitchell Baker</a> \r\n  (<a class=\"p-org h-card\" \r\n      href=\"https://mozilla.org/\"\r\n     >Mozilla Foundation</a>)\r\n</div>";
            var result = parser.Parse(html);

            Assert.AreEqual(1, result.Items.Length);
            Assert.AreEqual(1, result.Items[0].Type.Length);
            Assert.IsTrue(result.Items[0].Type[0].Contains("h-card"));
            Assert.AreEqual(3, result.Items[0].Properties.Count);
            Assert.AreEqual("Mitchell Baker", result.Items[0].Get(Props.NAME)[0]);
            Assert.AreEqual("https://blog.lizardwrangler.com/", result.Items[0].Get(Props.URL)[0]);
            Assert.AreEqual("Mozilla Foundation", result.Items[0].Get<MfSpec>(Props.ORGANIZATION)[0].Value);
            Assert.AreEqual("Mozilla Foundation", result.Items[0].Get<MfSpec>(Props.ORGANIZATION)[0].Get(Props.NAME)[0]);
            Assert.AreEqual("https://mozilla.org/", result.Items[0].Get<MfSpec>(Props.ORGANIZATION)[0].Get(Props.URL)[0]);
        }

        /// <summary>
        /// From: <see href="https://microformats.org/wiki/h-card"/>
        /// </summary>
        [TestMethod]
        public void PersonLotsOfPropertiesExample()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\">\r\n<span class=\"p-name\">Sally Ride</span>\r\n<span class=\"p-honorific-prefix\">Dr.</span>\r\n<span class=\"p-given-name\">Sally</span>\r\n<abbr class=\"p-additional-name\">K.</abbr>\r\n<span class=\"p-family-name\">Ride</span>\r\n<span class=\"p-honorific-suffix\">Ph.D.</span>,\r\n<span class=\"p-nickname\">sallykride</span> (IRC)\r\n<div class=\"p-org\">Sally Ride Science</div>\r\n<img class=\"u-photo\" src=\"http://example.com/sk.jpg\"/>\r\n<a class=\"u-url\" href=\"http://sally.example.com\">w</a>,\r\n<a class=\"u-email\" href=\"mailto:sally@example.com\">e</a>\r\n<div class=\"p-tel\">+1.818.555.1212</div>\r\n<div class=\"p-street-address\">123 Main st.</div>\r\n<span class=\"p-locality\">Los Angeles</span>,\r\n<abbr class=\"p-region\" title=\"California\">CA</abbr>,\r\n<span class=\"p-postal-code\">91316</span>\r\n<div class=\"p-country-name\">U.S.A</div>\r\n<time class=\"dt-bday\">1951-05-26</time> birthday\r\n<div class=\"p-category\">physicist</div>\r\n<div class=\"p-note\">First American woman in space.</div>\r\n</div>";
            var result = parser.Parse(html);

            Assert.AreEqual(1, result.Items.Length);
            Assert.AreEqual(1, result.Items[0].Type.Length);
            Assert.IsTrue(result.Items[0].Type.Contains("h-card"));
            Assert.AreEqual(20, result.Items[0].Properties.Count);
            Assert.AreEqual("Sally Ride", result.Items[0].Get(Props.NAME)[0]);
            Assert.AreEqual("Dr.", result.Items[0].Get(Props.HONORIFIC_PREFIX)[0]);
            Assert.AreEqual("Sally", result.Items[0].Get(Props.GIVEN_NAME)[0]);
            Assert.AreEqual("K.", result.Items[0].Get(Props.ADDITIONAL_NAME)[0]);
            Assert.AreEqual("Ride", result.Items[0].Get(Props.FAMILY_NAME)[0]);
            Assert.AreEqual("Ph.D.", result.Items[0].Get(Props.HONORIFIC_SUFFIX)[0]);
            Assert.AreEqual("sallykride", result.Items[0].Get(Props.NICKNAME)[0]);
            Assert.AreEqual("Sally Ride Science", result.Items[0].Get(Props.ORGANIZATION)[0]);
            Assert.AreEqual("http://example.com/sk.jpg", result.Items[0].Get(Props.PHOTO)[0]);
            Assert.AreEqual("http://sally.example.com", result.Items[0].Get(Props.URL)[0]);
            Assert.AreEqual("mailto:sally@example.com", result.Items[0].Get(Props.EMAIL)[0]);
            Assert.AreEqual("+1.818.555.1212", result.Items[0].Get(Props.TELEPHONE)[0]);
            Assert.AreEqual("123 Main st.", result.Items[0].Get(Props.STREET_ADDRESS)[0]);
            Assert.AreEqual("Los Angeles", result.Items[0].Get(Props.LOCALITY)[0]);
            Assert.AreEqual("California", result.Items[0].Get(Props.REGION)[0]);
            Assert.AreEqual("91316", result.Items[0].Get(Props.POSTAL_CODE)[0]);
            Assert.AreEqual("U.S.A", result.Items[0].Get(Props.COUNTRY_NAME)[0]);
            Assert.AreEqual("1951-05-26", result.Items[0].Get(Props.BIRTHDAY)[0]);
            Assert.AreEqual("physicist", result.Items[0].Get(Props.CATEGORY)[0]);
            Assert.AreEqual("First American woman in space.", result.Items[0].Get(Props.NOTE)[0]);
        }

        /// <summary>
        /// From: <see href="https://microformats.org/wiki/h-card"/>
        /// </summary>
        [TestMethod]
        public void AddressNestedExample()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-card\">\r\n  <p class=\"p-name\">Joe Bloggs</p>\r\n  <p class=\"p-adr h-adr\">\r\n    <span class=\"p-street-address\">17 Austerstræti</span>\r\n    <span class=\"p-locality\">Reykjavík</span>\r\n    <span class=\"p-country-name\">Iceland</span>\r\n  </p>\r\n</div>";
            var result = parser.Parse(html);

            Assert.AreEqual(1, result.Items.Length);
            Assert.IsTrue(result.Items[0].Type.Contains("h-card"));
            Assert.AreEqual(2, result.Items[0].Properties.Count);
            Assert.AreEqual("Joe Bloggs", result.Items[0].Get(Props.NAME)[0]);

            var address = result.Items[0].Get<MfSpec>(Props.ADDRESS)[0];

            Assert.AreEqual(1, address.Type.Length);
            Assert.IsTrue(address.Type.Contains("h-adr"));
            Assert.AreEqual(4, address.Properties.Count);
            Assert.AreEqual("17 Austerstræti", address.Get(Props.STREET_ADDRESS)[0]);
            Assert.AreEqual("Reykjavík", address.Get(Props.LOCALITY)[0]);
            Assert.AreEqual("Iceland", address.Get(Props.COUNTRY_NAME)[0]);
        }

        /// <summary>
        /// From: <see href="https://microformats.org/wiki/h-adr"/>
        /// </summary>
        [TestMethod]
        public void AddressExample()
        {
            var parser = new Mf2();
            var html = "<p class=\"h-adr\">\r\n  <span class=\"p-street-address\">17 Austerstræti</span>\r\n  <span class=\"p-locality\">Reykjavík</span>\r\n  <span class=\"p-country-name\">Iceland</span>\r\n  <span class=\"p-postal-code\">107</span>\r\n</p>";
            var result = parser.Parse(html);

            Assert.AreEqual(1, result.Items.Length);
            Assert.IsTrue(result.Items[0].Type.Contains("h-adr"));
            Assert.AreEqual(5, result.Items[0].Properties.Count);
            Assert.AreEqual("17 Austerstræti", result.Items[0].Get(Props.STREET_ADDRESS)[0]);
            Assert.AreEqual("Reykjavík", result.Items[0].Get(Props.LOCALITY)[0]);
            Assert.AreEqual("Iceland", result.Items[0].Get(Props.COUNTRY_NAME)[0]);
            Assert.AreEqual("107", result.Items[0].Get(Props.POSTAL_CODE)[0]);
            Assert.AreEqual("17 Austerstræti Reykjavík Iceland 107", result.Items[0].Get(Props.NAME)[0]);
        }

        /// <summary>
        /// From: <see href="https://microformats.org/wiki/h-entry"/>
        /// </summary>
        [TestMethod]
        public void EntryExample()
        {
            var parser = new Mf2();
            var html = "<article class=\"h-entry\">\r\n  <h1 class=\"p-name\">Microformats are amazing</h1>\r\n  <p>Published by <a class=\"p-author h-card\" href=\"http://example.com\">W. Developer</a>\r\n     on <time class=\"dt-published\" datetime=\"2013-06-13 12:00:00\">13<sup>th</sup> June 2013</time></p>\r\n  \r\n  <p class=\"p-summary\">In which I extoll the virtues of using microformats.</p>\r\n  \r\n  <div class=\"e-content\">\r\n    <p>Blah blah blah</p>\r\n  </div>\r\n</article>";
            var result = parser.Parse(html);

            Assert.AreEqual(1, result.Items.Length);
            Assert.IsTrue(result.Items[0].Type[0].Contains("h-entry"));
            Assert.AreEqual(5, result.Items[0].Properties.Count);
            Assert.AreEqual("Microformats are amazing", result.Items[0].Get(Props.NAME)[0]);
            Assert.AreEqual("2013-06-13 12:00:00", result.Items[0].Get(Props.PUBLISHED)[0]);
            Assert.AreEqual("In which I extoll the virtues of using microformats.", result.Items[0].Get(Props.SUMMARY)[0]);

            var embedded = result.Items[0].Get<MfEmbedded>(Props.CONTENT)[0];
            Assert.AreEqual("Blah blah blah", embedded.Value);
            Assert.AreEqual("<p>Blah blah blah</p>", embedded.Html);

            var author = result.Items[0].Get<MfSpec>(Props.AUTHOR)[0];
            Assert.AreEqual("W. Developer", author.Value);
            Assert.IsTrue(author.Type.Contains("h-card"));
            Assert.AreEqual(2, author.Properties.Count);
            Assert.AreEqual("W. Developer", author.Get(Props.NAME)[0]);
            Assert.AreEqual("http://example.com", author.Get(Props.URL)[0]);
        }

        /// <summary>
        /// From: <see href="https://microformats.org/wiki/h-event"/>
        /// </summary>
        [TestMethod]
        public void EventExample()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-event\">\r\n  <h1 class=\"p-name\">Microformats Meetup</h1>\r\n  <p>From \r\n    <time class=\"dt-start\" datetime=\"2013-06-30 12:00\">30<sup>th</sup> June 2013, 12:00</time>\r\n    to <time class=\"dt-end\" datetime=\"2013-06-30 18:00\">18:00</time>\r\n    at <span class=\"p-location\">Some bar in SF</span></p>\r\n  <p class=\"p-summary\">Get together and discuss all things microformats-related.</p>\r\n</div>";
            var result = parser.Parse(html);

            Assert.AreEqual(1, result.Items.Length);
            Assert.IsTrue(result.Items[0].Type.Contains("h-event"));
            Assert.AreEqual(5, result.Items[0].Properties.Count);
            Assert.AreEqual("Microformats Meetup", result.Items[0].Get(Props.NAME)[0]);
            Assert.AreEqual("2013-06-30 12:00:00", result.Items[0].Get(Props.START)[0]);
            Assert.AreEqual("2013-06-30 18:00:00", result.Items[0].Get(Props.END)[0]);
            Assert.AreEqual("Some bar in SF", result.Items[0].Get(Props.LOCATION)[0]);
            Assert.AreEqual("Get together and discuss all things microformats-related.", result.Items[0].Get(Props.SUMMARY)[0]);

        }

        /// <summary>
        /// From: <see href="http://microformats.org/wiki/h-feed"/>
        /// </summary>
        [TestMethod]
        public void FeedExample()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-feed hfeed\">\r\n  <h1 class=\"p-name site-title\">The Markup Blog</h1>\r\n  <p class=\"p-summary site-description\">Stories of elements of their attributes.</p>\r\n\r\n  <article class=\"h-entry hentry\">\r\n    <a class=\"u-url\" rel=\"bookmark\" href=\"2020/06/22/balanced-divisive-complementary\">\r\n      <h2 class=\"p-name entry-title\">A Tale Of Two Tags: Part 2</h2>\r\n    </a>\r\n    <address class=\"p-author author h-card vcard\">\r\n      <a href=\"https://chandra.example.com/\" class=\"u-url url p-name fn\" rel=\"author\">Chandra</a>\r\n    </address>\r\n    <time class=\"dt-published published\" datetime=\"2012-06-22T09:45:57-07:00\">June 21, 2012</time>\r\n    <div class=\"p-summary entry-summary\">\r\n      <p>From balanced harmony, to divisive misunderstandings, to complementary roles.</p>\r\n    </div>\r\n    <a href=\"/category/uncategorized/\" rel=\"category tag\" class=\"p-category\">General</a>\r\n  </article>\r\n\r\n  <article class=\"h-entry hentry\">\r\n    <a class=\"u-url\" rel=\"bookmark\" href=\"2020/06/20/best-visible-alternative-invisible\">\r\n      <h2 class=\"p-name entry-title\">A Tale Of Two Tags: Part 1</h2>\r\n    </a>\r\n    <address class=\"p-author author h-card vcard\">\r\n      <a href=\"https://chandra.example.com/\" class=\"u-url url p-name fn\" rel=\"author\">Chandra</a>\r\n    </address>\r\n    <time class=\"dt-published published\" datetime=\"2012-06-20T08:34:46-07:00\">June 20, 2012</time>\r\n    <div class=\"p-summary entry-summary\">\r\n      <p>It was the best of visible tags, it was the alternative invisible tags.</p>\r\n    </div>\r\n    <a href=\"/category/uncategorized/\" rel=\"category tag\" class=\"p-category\">General</a>\r\n  </article>\r\n\r\n</div>";
            var result = parser.Parse(html);

            Assert.AreEqual(1, result.Items.Length);
            Assert.IsTrue(result.Items[0].Type.Contains("h-feed"));
            Assert.AreEqual(3, result.Items[0].Properties.Count);
            Assert.AreEqual("The Markup Blog", result.Items[0].Get(Props.NAME)[0]);
            Assert.AreEqual("Stories of elements of their attributes.", result.Items[0].Get(Props.SUMMARY)[0]);
            Assert.AreEqual(2, result.Items[0].Get<MfSpec>(Props.ENTRY).Length);

            var entry = result.Items[0].Get<MfSpec>(Props.ENTRY)[0];
            Assert.AreEqual(6, entry.Properties.Count);

        }

        /// <summary>
        /// From: <see href="http://microformats.org/wiki/h-geo"/>
        /// </summary>
        [TestMethod]
        public void GeoExample()
        {
            var parser = new Mf2();
            var html = "<p class=\"h-geo\">\r\n  <data class=\"p-longitude\" value=\"-27.116667\">27° 7′ 0″ S</data>,\r\n  <data class=\"p-latitude\" value=\"-109.366667\">109° 22′ 0″ W</data>\r\n</p>";
            var result = parser.Parse(html);

            Assert.AreEqual(1, result.Items.Length);
            Assert.IsTrue(result.Items[0].Type.Contains("h-geo"));
            Assert.AreEqual(2, result.Items[0].Properties.Count);
            Assert.AreEqual("-27.116667", result.Items[0].Get(Props.LONGITUDE)[0]);
            Assert.AreEqual("-109.366667", result.Items[0].Get(Props.LATITUDE)[0]);
        }

        /// <summary>
        /// From: <see href="http://microformats.org/wiki/h-item"/>
        /// </summary>
        [TestMethod]
        public void ItemExample()
        {
            var parser = new Mf2();
            var html = "<a class=\"h-item\" href=\"http://example.org/items/1\">\r\n  <img src=\"http://example.org/items/1/photo.png\" alt=\"\" />\r\n  The Item Name\r\n</a>";
            var result = parser.Parse(html);

            Assert.AreEqual(1, result.Items.Length);
            Assert.IsTrue(result.Items[0].Type[0].Contains("h-item"));
            Assert.AreEqual(3, result.Items[0].Properties.Count);
            Assert.AreEqual("http://example.org/items/1", result.Items[0].Get(Props.URL)[0]);
            Assert.AreEqual("http://example.org/items/1/photo.png", result.Items[0].Get(Props.PHOTO)[0]);
            Assert.AreEqual("The Item Name", result.Items[0].Get(Props.NAME)[0]);
        }

        /// <summary>
        /// From: <see href="http://microformats.org/wiki/h-product"/>
        /// </summary>
        [TestMethod]
        public void ProductExample()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-product\">\r\n  <h1 class=\"p-name\">Microformats For Dummies</h1>\r\n  <img class=\"u-photo\" src=\"http://example.org/mfd.png\" alt=\"\" />\r\n  <div class=\"e-description\">\r\n    <p>Want to get started using microformats, but intimidated by hyphens and mediawiki? This book contains everything you need to know!</p>\r\n  </div>\r\n  <p>Yours today for only <data class=\"p-price\" value=\"20.00\">$20.00</data>\r\n     from <a class=\"p-brand h-card\" href=\"http://example.com/acme\">ACME Publishing inc.</a>\r\n  </p>\r\n</div>";
            var result = parser.Parse(html);

            Assert.AreEqual(1, result.Items.Length);
            Assert.IsTrue(result.Items[0].Type.Contains("h-product"));
            Assert.AreEqual(5, result.Items[0].Properties.Count);
            Assert.AreEqual("Microformats For Dummies", result.Items[0].Get(Props.NAME)[0]);
            Assert.AreEqual("http://example.org/mfd.png", result.Items[0].Get(Props.PHOTO)[0]);
            Assert.AreEqual("<p>Want to get started using microformats, but intimidated by hyphens and mediawiki? This book contains everything you need to know!</p>", result.Items[0].Get<MfEmbedded>(Props.DESCRIPTION_HTML)[0].Html);
            Assert.AreEqual("20.00", result.Items[0].Get(Props.PRICE)[0]);
        }

        /// <summary>
        /// From: <see href="https://microformats.org/wiki/h-recipe"/>
        /// </summary>
        [TestMethod]
        public void RecipeExample()
        {
            var parser = new Mf2();
            var html = "<article class=\"h-recipe\">\r\n  <h1 class=\"p-name\">Bagels</h1>\r\n  \r\n  <ul>\r\n    <li class=\"p-ingredient\">Flour</li>\r\n    <li class=\"p-ingredient\">Sugar</li>\r\n    <li class=\"p-ingredient\">Yeast</li>\r\n  </ul>\r\n \r\n  <p>Takes <time class=\"dt-duration\" datetime=\"1H\">1 hour</time>,\r\n     serves <data class=\"p-yield\" value=\"4\">four people</data>.</p>\r\n  \r\n  <div class=\"e-instructions\">\r\n    <ol>\r\n      <li>Start by mixing all the ingredients together.</li>\r\n    </ol>\r\n  </div>\r\n</article>";
            var result = parser.Parse(html);

            Assert.AreEqual(1, result.Items.Length);
            Assert.IsTrue(result.Items[0].Type.Contains("h-recipe"));
            Assert.AreEqual(5, result.Items[0].Properties.Count);
            Assert.AreEqual("Bagels", result.Items[0].Get(Props.NAME)[0]);
            Assert.AreEqual(3, result.Items[0].Get(Props.INGREDIENT).Length);
            Assert.AreEqual("Flour", result.Items[0].Get(Props.INGREDIENT)[0]);
            Assert.AreEqual("Sugar", result.Items[0].Get(Props.INGREDIENT)[1]);
            Assert.AreEqual("Yeast", result.Items[0].Get(Props.INGREDIENT)[2]);
            Assert.AreEqual("1H", result.Items[0].Get(Props.DURATION_DATETIME)[0]);
            Assert.AreEqual("4", result.Items[0].Get(Props.YIELD)[0]);
            Assert.AreEqual("<ol>\r\n      <li>Start by mixing all the ingredients together.</li>\r\n    </ol>", result.Items[0].Get<MfEmbedded>(Props.INSTRUCTIONS)[0].Html);
        }

        /// <summary>
        /// From: <see href="https://microformats.org/wiki/h-resume"/>
        /// </summary>
        [TestMethod]
        public void ResumeExample()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-resume\">\r\n  <span class=\"p-name\">\r\n   <a class=\"p-contact h-card\" href=\"http://example.org\">\r\n    <img src=\"http://example.org/photo.png\" alt=\"\" />\r\n    Joe Bloggs\r\n   </a>\r\n   resume\r\n  </span>\r\n  \r\n  <p class=\"p-summary\">Joe is a top-notch llama farmer with a degree in <span class=\"p-skill\">Llama husbandry</span> and a thirst to produce the finest wool known to man</p>\r\n</div>";
            var result = parser.Parse(html);

            Assert.AreEqual(1, result.Items.Length);
            Assert.IsTrue(result.Items[0].Type.Contains("h-resume"));
            Assert.AreEqual(4, result.Items[0].Properties.Count);
            Assert.AreEqual("Joe Bloggs resume", result.Items[0].Get(Props.NAME)[0]);

            var contactCard = result.Items[0].Get<MfSpec>(Props.CONTACT)[0];
            Assert.AreEqual("Joe Bloggs", contactCard.Value);
            Assert.AreEqual("Joe Bloggs", contactCard.Get(Props.NAME)[0]);
            Assert.AreEqual("http://example.org/photo.png", contactCard.Get(Props.PHOTO)[0]);
            Assert.AreEqual("http://example.org", contactCard.Get(Props.URL)[0]);

            Assert.AreEqual("Joe is a top-notch llama farmer with a degree in Llama husbandry and a thirst to produce the finest wool known to man", result.Items[0].Get(Props.SUMMARY)[0]);
            Assert.AreEqual("Llama husbandry", result.Items[0].Get(Props.SKILL)[0]);
        }

        /// <summary>
        /// From: <see href="https://microformats.org/wiki/h-review"/>
        /// </summary>
        [TestMethod]
        public void ReviewExample()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-review\">\r\n  <h1 class=\"p-name\">Microformats: is structured data worth it?</h1>\r\n  \r\n  <blockquote>\r\n    <a class=\"p-item h-item\" href=\"http://microformats.org\">Microformats</a> are the simplest way to publish structured data on the web.\r\n  </blockquote>\r\n  \r\n  <p>\r\n    <data class=\"p-rating\" value=\"5\">★★★★★</data>\r\n    Published <time class=\"dt-published\" datetime=\"2013-06-12 12:00:00\">12<sup>th</sup> June 2013</time>\r\n    by <a class=\"p-author h-card\" href=\"http://example.com\">Joe Bloggs</a>.\r\n  </p>\r\n  \r\n  <div class=\"e-content\">\r\n    <p>Yes, microformats are undoubtedly great. They are the simplest way to markup structured data in HTML and reap the benefits thereof, including using your web page as your API by automatic conversion to JSON. The alternatives of microdata/schema and RDFa are much more work, require more markup, and are more complicated (harder to get right, more likely to break).</p>\r\n  </div>\r\n</div>";
            var result = parser.Parse(html);

            Assert.AreEqual(1, result.Items.Length);
            Assert.IsTrue(result.Items[0].Type.Contains("h-review"));
            Assert.AreEqual(6, result.Items[0].Properties.Count);
            Assert.AreEqual("Microformats: is structured data worth it?", result.Items[0].Get(Props.NAME)[0]);

            var item = result.Items[0].Get<MfSpec>(Props.ITEM)[0];
            Assert.AreEqual("Microformats", item.Value);
            Assert.AreEqual("Microformats", item.Get(Props.NAME)[0]);
            Assert.AreEqual("http://microformats.org", item.Get(Props.URL)[0]);

            Assert.AreEqual("5", result.Items[0].Get(Props.RATING)[0]);
            Assert.AreEqual("2013-06-12 12:00:00", result.Items[0].Get(Props.PUBLISHED)[0]);

            var author = result.Items[0].Get<MfSpec>(Props.AUTHOR)[0];
            Assert.AreEqual("Joe Bloggs", author.Value);
            Assert.AreEqual("Joe Bloggs", author.Get(Props.NAME)[0]);
            Assert.AreEqual("http://example.com", author.Get(Props.URL)[0]);


            Assert.AreEqual("<p>Yes, microformats are undoubtedly great. They are the simplest way to markup structured data in HTML and reap the benefits thereof, including using your web page as your API by automatic conversion to JSON. The alternatives of microdata/schema and RDFa are much more work, require more markup, and are more complicated (harder to get right, more likely to break).</p>", result.Items[0].Get<MfEmbedded>(Props.CONTENT)[0].Html);
        }

        /// <summary>
        /// From: <see href="https://microformats.org/wiki/h-review-aggregate"/>
        /// </summary>
        [TestMethod]
        public void ReviewAggregateExample()
        {
            var parser = new Mf2();
            var html = "<article class=\"h-review-aggregate\">\r\n <h1 class=\"p-item\">Mediterranean Wraps</h1>\r\n <p class=\"p-name\">Customers flock to this small restaurant for their \r\n tasty falafel and shawerma wraps and welcoming staff.</p>\r\n <span class=\"p-average\">4.5</span> out of 5 \r\n based on <span class=\"p-count\">17</span> reviews\r\n</article>";
            var result = parser.Parse(html);

            Assert.AreEqual(1, result.Items.Length);
            Assert.IsTrue(result.Items[0].Type.Contains("h-review-aggregate"));
            Assert.AreEqual(4, result.Items[0].Properties.Count);

            Assert.AreEqual("Customers flock to this small restaurant for their tasty falafel and shawerma wraps and welcoming staff.", result.Items[0].Get(Props.NAME)[0]);
            Assert.AreEqual("Mediterranean Wraps", result.Items[0].Get(Props.ITEM)[0]);
            Assert.AreEqual("4.5", result.Items[0].Get(Props.AVERAGE)[0]);
            Assert.AreEqual("17", result.Items[0].Get(Props.COUNT)[0]);
        }
    }
}