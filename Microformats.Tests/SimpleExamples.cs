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
            Assert.IsTrue(result.Items[0].Properties.ContainsKey("name"));
            Assert.IsTrue(result.Items[0].Properties["name"].Length == 1);
            Assert.IsTrue(result.Items[0].GetProperty("p-name")[0] == "Frances Berriman");
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
            Assert.IsTrue(result.Items[0].Properties.ContainsKey("name"));
            Assert.IsTrue(result.Items[0].Properties.ContainsKey("url"));
            Assert.IsTrue(result.Items[0].Properties["name"].Length == 1);
            Assert.IsTrue(result.Items[0].Properties["url"].Length == 1);
            Assert.IsTrue(result.Items[0].GetProperty("p-name")[0] == "Ben Ward");
            Assert.IsTrue(result.Items[0].GetProperty("u-url")[0] == "http://benward.me");
        }
    }
}