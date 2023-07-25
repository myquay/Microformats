using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microformats.Tests
{
    [TestClass]
    public class SimpleExamples
    {
        [TestMethod]
        public void PersonExample()
        {
            var parser = new Mf2();
            var html = "<span class=\"h-card\">Frances Berriman</span>";
            var result = parser.Parse(html);

            Assert.IsTrue(result.Items.Length == 1);
            Assert.IsTrue(result.Items[0].Type == Result.MType.Root);
            Assert.IsTrue(result.Items[0].Value.Length == 1);
            Assert.IsTrue(result.Items[0].Value[0] == "h-card");
            Assert.IsTrue(result.Items[0].Properties.Count == 1);
            Assert.IsTrue(result.Items[0].Properties.ContainsKey("name"));
            Assert.IsTrue(result.Items[0].Properties["name"].Value.Length == 1);
            Assert.IsTrue(result.Items[0].Properties["name"].Value[0] == "Frances Berriman");
        }
    }
}