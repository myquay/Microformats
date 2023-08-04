using Microformats.Definitions.Properties.DateTime;
using Microformats.Definitions.Properties.Standard;
using Microformats.Definitions.Vocabularies;
using Microformats.Grammar;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Microformats.Tests.PHPMf2
{
    /// <summary>
    /// Test cases from the indieweb php-mf2 parser
    /// From: <see href="https://github.com/microformats/php-mf2/blob/main/tests/Mf2/ParseHtmlIdTest.php" />
    /// </summary>
    [TestClass]
    public class ParseHtmlIdTest
    {

        [TestMethod]
        public void ParserIdAttribute()
        {
            var parser = new Mf2();
            var html = "<div class=\"h-feed\" id=\"recentArticles\"><h2 class=\"p-name\">Recent Articles</h2><div class=\"hentry\" id=\"article\">Lorem Ipsum</div>\r\n\t\t<div class=\"p-author h-card\" id=\"theAuthor\">Max Mustermann</div>\r\n\t\t<div class=\"h-entry\" id=\"\">empty id should not be parsed</div>\r\n\t\t<div class=\"h-entry\" id=\"0\">id=0 should work and not be treated false-y</div>\r\n\t\t</div>";
            var result = parser.Parse(html);

            Assert.IsNotNull(result.Items[0].Id);
            Assert.AreEqual("recentArticles", result.Items[0].Id);
            Assert.IsNotNull(result.Items[0].Get<Author, MfType>()[0].Id);
            Assert.AreEqual("theAuthor", result.Items[0].Get<Author, MfType>()[0].Id);
        }

    }
}
