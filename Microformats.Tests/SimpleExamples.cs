using Microformats.Definitions;
using Microformats.Definitions.Properties;
using Microformats.Definitions.Properties.DateTime;
using Microformats.Definitions.Properties.Embedded;
using Microformats.Definitions.Properties.Link;
using Microformats.Definitions.Properties.Standard;
using Microformats.Definitions.Vocabularies;
using Microformats.Grammar;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Concurrent;

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

            Assert.IsTrue(result.Items.Length == 1);
            Assert.IsTrue(result.Items[0].Type.Length == 1);
            Assert.IsTrue(result.Items[0].Type[0] == "h-card");
            Assert.IsTrue(result.Items[0].Properties.Count == 1);
            Assert.IsTrue(result.Items[0].Get<PropertyName>()[0] == "Frances Berriman");
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

            Assert.IsTrue(result.Items.Length == 1);
            Assert.IsTrue(result.Items[0].Type.Length == 1);
            Assert.IsTrue(result.Items[0].Type[0] == "h-card");
            Assert.IsTrue(result.Items[0].Properties.Count == 2);
            Assert.IsTrue(result.Items[0].Get<PropertyName>()[0] == "Ben Ward");
            Assert.IsTrue(result.Items[0].Get<Url>()[0] == "http://benward.me");
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

            Assert.IsTrue(result.Items.Length == 1);
            Assert.IsTrue(result.Items[0].Type.Length == 1);
            Assert.IsTrue(result.Items[0].Type[0] == "h-card");
            Assert.IsTrue(result.Items[0].Properties.Count == 3);
            Assert.IsTrue(result.Items[0].Get<PropertyName>()[0] == "Rohit Khare");
            Assert.IsTrue(result.Items[0].Get<Url>()[0] == "http://rohit.khare.org/");
            Assert.IsTrue(result.Items[0].Get<Photo>()[0] == "https://s3.amazonaws.com/twitter_production/profile_images/53307499/180px-Rohit-sq_bigger.jpg");
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

            Assert.IsTrue(result.Items.Length == 1);
            Assert.IsTrue(result.Items[0].Type.Length == 1);
            Assert.IsTrue(result.Items[0].Type[0] == "h-card");
            Assert.IsTrue(result.Items[0].Properties.Count == 6);
            Assert.IsTrue(result.Items[0].Get<PropertyName>()[0] == "Mitchell Baker");
            Assert.IsTrue(result.Items[0].Get<Org>()[0] == "Mozilla Foundation");
            Assert.IsTrue(result.Items[0].Get<Note>()[0] == "Mitchell is responsible for setting the direction and scope of the Mozilla Foundation and its activities.");
            Assert.IsTrue(result.Items[0].Get<Category>()[0] == "Strategy"); 
            Assert.IsTrue(result.Items[0].Get<Category>()[1] == "Leadership");
            Assert.IsTrue(result.Items[0].Get<Photo>()[0] == "https://webfwd.org/content/about-experts/300.mitchellbaker/mentor_mbaker.jpg");
            Assert.IsTrue(result.Items[0].Get<Url>()[0] == "http://blog.lizardwrangler.com/");
            Assert.IsTrue(result.Items[0].Get<Url>()[1] == "https://twitter.com/MitchellBaker");

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

            Assert.IsTrue(result.Items.Length == 1);
            Assert.IsTrue(result.Items[0].Type.Length == 1);
            Assert.IsTrue(result.Items[0].Type[0] == "h-card");
            Assert.IsTrue(result.Items[0].Properties.Count == 3);
            Assert.IsTrue(result.Items[0].Get<PropertyName>()[0] == "microformats.org");
            Assert.IsTrue(result.Items[0].Get<Url>()[0] == "https://microformats.org/");
            Assert.IsTrue(result.Items[0].Get<Org>()[0] == "microformats.org");
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

            Assert.IsTrue(result.Items.Length == 1);
            Assert.IsTrue(result.Items[0].Type.Length == 1);
            Assert.IsTrue(result.Items[0].Type[0] == "h-card");
            Assert.IsTrue(result.Items[0].Properties.Count == 3);
            Assert.IsTrue(result.Items[0].Get<PropertyName>()[0] == "Mitchell Baker");
            Assert.IsTrue(result.Items[0].Get<Url>()[0] == "https://blog.lizardwrangler.com/");
            Assert.IsTrue(result.Items[0].Get<Org,MfType>()[0].Value == "Mozilla Foundation");
            Assert.IsTrue(result.Items[0].Get<Org, MfType>()[0].Get<PropertyName>()[0] == "Mozilla Foundation");
            Assert.IsTrue(result.Items[0].Get<Org, MfType>()[0].Get<Url>()[0] == "https://mozilla.org/");
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

            Assert.IsTrue(result.Items.Length == 1);
            Assert.IsTrue(result.Items[0].Type.Length == 1);
            Assert.IsTrue(result.Items[0].Type[0] == "h-card");
            Assert.IsTrue(result.Items[0].Properties.Count == 20);
            Assert.IsTrue(result.Items[0].Get<PropertyName>()[0] == "Sally Ride");
            Assert.IsTrue(result.Items[0].Get<HonorificPrefix>()[0] == "Dr.");
            Assert.IsTrue(result.Items[0].Get<GivenName>()[0] == "Sally");
            Assert.IsTrue(result.Items[0].Get<AdditionalName>()[0] == "K.");
            Assert.IsTrue(result.Items[0].Get<FamilyName>()[0] == "Ride");
            Assert.IsTrue(result.Items[0].Get<HonorificSuffix>()[0] == "Ph.D.");
            Assert.IsTrue(result.Items[0].Get<Nickname>()[0] == "sallykride");
            Assert.IsTrue(result.Items[0].Get<Org>()[0] == "Sally Ride Science");
            Assert.IsTrue(result.Items[0].Get<Photo>()[0] == "http://example.com/sk.jpg");
            Assert.IsTrue(result.Items[0].Get<Url>()[0] == "http://sally.example.com");
            Assert.IsTrue(result.Items[0].Get<Email>()[0] == "mailto:sally@example.com");
            Assert.IsTrue(result.Items[0].Get<Tel>()[0] == "+1.818.555.1212");
            Assert.IsTrue(result.Items[0].Get<StreetAddress>()[0] == "123 Main st.");
            Assert.IsTrue(result.Items[0].Get<Locality>()[0] == "Los Angeles");
            Assert.IsTrue(result.Items[0].Get<Region>()[0] == "California");
            Assert.IsTrue(result.Items[0].Get<PostalCode>()[0] == "91316");
            Assert.IsTrue(result.Items[0].Get<CountryName>()[0] == "U.S.A");
            Assert.IsTrue(result.Items[0].Get<Birthday>()[0] == "1951-05-26");
            Assert.IsTrue(result.Items[0].Get<Category>()[0] == "physicist");
            Assert.IsTrue(result.Items[0].Get<Note>()[0] == "First American woman in space.");
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

            Assert.IsTrue(result.Items.Length == 1);
            Assert.IsTrue(result.Items[0].Type[0] == "h-card");
            Assert.IsTrue(result.Items[0].Properties.Count == 2);
            Assert.IsTrue(result.Items[0].Get<PropertyName>()[0] == "Joe Bloggs");

            var address = (result.Items[0].Properties["adr"][0]).Get<MfType>();

            Assert.IsTrue(address.Type.Length == 1);
            Assert.IsTrue(address.Type[0] == "h-adr");
            Assert.IsTrue(address.Properties.Count == 4);
            Assert.IsTrue(address.Get<StreetAddress>()[0] == "17 Austerstræti");
            Assert.IsTrue(address.Get<Locality>()[0] == "Reykjavík");
            Assert.IsTrue(address.Get<CountryName>()[0] == "Iceland");
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

            Assert.IsTrue(result.Items.Length == 1);
            Assert.IsTrue(result.Items[0].Type[0] == "h-adr");
            Assert.IsTrue(result.Items[0].Properties.Count == 5);
            Assert.IsTrue(result.Items[0].Get<StreetAddress>()[0] == "17 Austerstræti");
            Assert.IsTrue(result.Items[0].Get<Locality>()[0] == "Reykjavík");
            Assert.IsTrue(result.Items[0].Get<CountryName>()[0] == "Iceland");
            Assert.IsTrue(result.Items[0].Get<PostalCode>()[0] == "107");
            Assert.IsTrue(result.Items[0].Get<PropertyName>()[0] == "17 Austerstræti Reykjavík Iceland 107");
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

            Assert.IsTrue(result.Items.Length == 1);
            Assert.IsTrue(result.Items[0].Type[0] == "h-entry");
            Assert.IsTrue(result.Items[0].Properties.Count == 5);
            Assert.IsTrue(result.Items[0].Get<PropertyName>()[0] == "Microformats are amazing");
            Assert.IsTrue(result.Items[0].Get<Published>()[0] == "2013-06-13 12:00:00");
            Assert.IsTrue(result.Items[0].Get<Summary>()[0] == "In which I extoll the virtues of using microformats.");

            var embedded = result.Items[0].Get<Content, MfEmbedded>()[0];
            Assert.IsTrue(embedded.Value == "Blah blah blah");
            Assert.IsTrue(embedded.Html == "<p>Blah blah blah</p>");

            var author = result.Items[0].Get<Author, MfType>()[0];
            Assert.IsTrue(author.Value == "W. Developer");
            Assert.IsTrue(author.Type[0] == "h-card");
            Assert.IsTrue(author.Properties.Count == 2);
            Assert.IsTrue(author.Get<PropertyName>()[0] == "W. Developer");
            Assert.IsTrue(author.Get<Url>()[0] == "http://example.com");
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

            Assert.IsTrue(result.Items.Length == 1);
            Assert.IsTrue(result.Items[0].Type[0] == "h-event");
            Assert.IsTrue(result.Items[0].Properties.Count == 5);
            Assert.IsTrue(result.Items[0].Get<PropertyName>()[0] == "Microformats Meetup");
            Assert.IsTrue(result.Items[0].Get<Start>()[0] == "2013-06-30 12:00");
            Assert.IsTrue(result.Items[0].Get<End>()[0] == "2013-06-30 18:00");
            Assert.IsTrue(result.Items[0].Get<Location>()[0] == "Some bar in SF");
            Assert.IsTrue(result.Items[0].Get<Summary>()[0] == "Get together and discuss all things microformats-related.");

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

            Assert.IsTrue(result.Items.Length == 1);
            Assert.IsTrue(result.Items[0].Type[0] == "h-feed");
            Assert.IsTrue(result.Items[0].Properties.Count == 3);
            Assert.IsTrue(result.Items[0].Get<PropertyName>()[0] == "The Markup Blog");
            Assert.IsTrue(result.Items[0].Get<Summary>()[0] == "Stories of elements of their attributes.");
            Assert.IsTrue(result.Items[0].Get<Entry, MfType>().Length == 2);

            var entry = result.Items[0].Get<Entry, MfType>()[0];
            Assert.IsTrue(entry.Properties.Count == 6);

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

            Assert.IsTrue(result.Items.Length == 1);
            Assert.IsTrue(result.Items[0].Type[0] == "h-geo");
            Assert.IsTrue(result.Items[0].Properties.Count == 2);
            Assert.IsTrue(result.Items[0].Get<Longitude>()[0] == "-27.116667");
            Assert.IsTrue(result.Items[0].Get<Latitude>()[0] == "-109.366667");

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

            Assert.IsTrue(result.Items.Length == 1);
            Assert.IsTrue(result.Items[0].Type[0] == "h-item");
            Assert.IsTrue(result.Items[0].Properties.Count == 3);
            Assert.IsTrue(result.Items[0].Get<Url>()[0] == "http://example.org/items/1");
            Assert.IsTrue(result.Items[0].Get<Photo>()[0] == "http://example.org/items/1/photo.png");
            Assert.IsTrue(result.Items[0].Get<PropertyName>()[0] == "The Item Name");
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

            Assert.IsTrue(result.Items.Length == 1);
            Assert.IsTrue(result.Items[0].Type[0] == "h-product");
            Assert.IsTrue(result.Items[0].Properties.Count == 5);
            Assert.IsTrue(result.Items[0].Get<PropertyName>()[0] == "Microformats For Dummies");
            Assert.IsTrue(result.Items[0].Get<Photo>()[0] == "http://example.org/mfd.png");
            Assert.IsTrue(result.Items[0].Get<DescriptionEmbedded, MfEmbedded>()[0].Html == "<p>Want to get started using microformats, but intimidated by hyphens and mediawiki? This book contains everything you need to know!</p>");
            Assert.IsTrue(result.Items[0].Get<Price>()[0] == "20.00");
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

            Assert.IsTrue(result.Items.Length == 1);
            Assert.IsTrue(result.Items[0].Type[0] == "h-recipe");
            Assert.IsTrue(result.Items[0].Properties.Count == 5);
            Assert.IsTrue(result.Items[0].Get<PropertyName>()[0] == "Bagels");
            Assert.IsTrue(result.Items[0].Get<Ingredient>().Length == 3);
            Assert.IsTrue(result.Items[0].Get<Ingredient>()[0] == "Flour");
            Assert.IsTrue(result.Items[0].Get<Ingredient>()[1] == "Sugar");
            Assert.IsTrue(result.Items[0].Get<Ingredient>()[2] == "Yeast");
            Assert.IsTrue(result.Items[0].Get<Duration>()[0] == "1H");
            Assert.IsTrue(result.Items[0].Get<Yield>()[0] == "4");
            Assert.IsTrue(result.Items[0].Get<Instructions, MfEmbedded>()[0].Html == "<ol>\r\n      <li>Start by mixing all the ingredients together.</li>\r\n    </ol>");
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

            Assert.IsTrue(result.Items.Length == 1);
            Assert.IsTrue(result.Items[0].Type[0] == "h-resume");
            Assert.IsTrue(result.Items[0].Properties.Count == 4);
            Assert.IsTrue(result.Items[0].Get<PropertyName>()[0] == "Joe Bloggs resume");

            var contactCard = result.Items[0].Get<Contact, MfType>()[0];
            Assert.IsTrue(contactCard.Value == "Joe Bloggs");
            Assert.IsTrue(contactCard.Get<PropertyName>()[0] == "Joe Bloggs");
            Assert.IsTrue(contactCard.Get<Photo>()[0] == "http://example.org/photo.png");
            Assert.IsTrue(contactCard.Get<Url>()[0] == "http://example.org");

            Assert.IsTrue(result.Items[0].Get<Summary>()[0] == "Joe is a top-notch llama farmer with a degree in Llama husbandry and a thirst to produce the finest wool known to man");
            Assert.IsTrue(result.Items[0].Get<Skill>()[0] == "Llama husbandry");
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

            Assert.IsTrue(result.Items.Length == 1);
            Assert.IsTrue(result.Items[0].Type[0] == "h-review");
            Assert.IsTrue(result.Items[0].Properties.Count == 6);
            Assert.IsTrue(result.Items[0].Get<PropertyName>()[0] == "Microformats: is structured data worth it?");

            var item = result.Items[0].Get<Item, MfType>()[0];
            Assert.IsTrue(item.Value == "Microformats");
            Assert.IsTrue(item.Get<PropertyName>()[0] == "Microformats");
            Assert.IsTrue(item.Get<Url>()[0] == "http://microformats.org");

            Assert.IsTrue(result.Items[0].Get<Rating>()[0] == "5");
            Assert.IsTrue(result.Items[0].Get<Published>()[0] == "2013-06-12 12:00:00");

            var author = result.Items[0].Get<Author, MfType>()[0];
            Assert.IsTrue(author.Value == "Joe Bloggs");
            Assert.IsTrue(author.Get<PropertyName>()[0] == "Joe Bloggs");
            Assert.IsTrue(author.Get<Url>()[0] == "http://example.com");


            Assert.IsTrue(result.Items[0].Get<Content, MfEmbedded>()[0].Html == "<p>Yes, microformats are undoubtedly great. They are the simplest way to markup structured data in HTML and reap the benefits thereof, including using your web page as your API by automatic conversion to JSON. The alternatives of microdata/schema and RDFa are much more work, require more markup, and are more complicated (harder to get right, more likely to break).</p>");
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

            Assert.IsTrue(result.Items.Length == 1);
            Assert.IsTrue(result.Items[0].Type[0] == "h-review-aggregate");
            Assert.IsTrue(result.Items[0].Properties.Count == 4);
            
            Assert.IsTrue(result.Items[0].Get<PropertyName>()[0] == "Customers flock to this small restaurant for their tasty falafel and shawerma wraps and welcoming staff.");
            Assert.IsTrue(result.Items[0].Get<Item>()[0] == "Mediterranean Wraps");
            Assert.IsTrue(result.Items[0].Get<Average>()[0] == "4.5");
            Assert.IsTrue(result.Items[0].Get<Count>()[0] == "17");

        }
    }
}