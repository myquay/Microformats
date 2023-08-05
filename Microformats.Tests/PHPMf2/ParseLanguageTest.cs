//using Microformats.Definitions.Properties.DateTime;
//using Microformats.Definitions.Properties.Link;
//using Microformats.Definitions.Properties.Standard;
//using Microformats.Definitions.Vocabularies;
//using Microformats.Grammar;
//using Microsoft.VisualBasic;
//using Microsoft.VisualStudio.TestPlatform.ObjectModel;
//using Microsoft.VisualStudio.TestPlatform.Utilities;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
//using Newtonsoft.Json.Linq;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Diagnostics.Metrics;
//using System.IO;
//using System.Linq;
//using System.Reflection.Metadata;
//using System.Reflection.PortableExecutable;
//using System.Reflection;
//using System.Runtime.ConstrainedExecution;
//using System.Runtime.Intrinsics.X86;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading.Tasks;
//using static System.Net.Mime.MediaTypeNames;
//using System.Xml.Linq;
//using static System.Runtime.InteropServices.JavaScript.JSType;

//namespace Microformats.Tests.PHPMf2
//{
//    /// <summary>
//    /// Test cases from the indieweb php-mf2 parser
//    /// From: <see href="https://github.com/microformats/php-mf2/blob/main/tests/Mf2/ParseLanguageTest.php" />
//    /// </summary>
//    [TestClass]
//    public class ParseLanguageTest
//    {

//        /// <summary>
//        /// Test with only <html lang>
//        /// </summary>
//        [TestMethod]
//        public void HtmlLangOnly()
//        {
//            var parser = new Mf2();
//            var html = "<html lang=\"en\"> <div class=\"h-entry\">This test is in English.</div> </html>";
//            var result = parser.Parse(html);
//;
//            Assert.AreEqual("en", result.Items[0].Lang);
//        }

//        /// <summary>
//        /// Test with only h-entry lang
//        /// </summary>
//        [TestMethod]
//        public void HEntryLangOnly()
//        {
//            var parser = new Mf2();
//            var html = "<html> <div class=\"h-entry\" lang=\"en\">This test is in English.</div> </html>";
//            var result = parser.Parse(html);

//            Assert.AreEqual("en", result.Items[0].Lang);
//        }

//        /// <summary>
//        /// Test with different <html lang> and h-entry lang
//        /// </summary>
//        [TestMethod]
//        public void HtmlAndHEntryLang()
//        {
//            var parser = new Mf2();
//            var html = "<html lang=\"en\"> <div class=\"h-entry\" lang=\"es\">Esta prueba está en español.</div> </html>";
//            var result = parser.Parse(html);

//            Assert.AreEqual("en", result.Items[0].Lang);
//        }

//        /// <summary>
//        /// Test HTML fragment with only h-entry lang
//        /// </summary>
//        [TestMethod]
//        public void FragmentHEntryLangOnly()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-entry\" lang=\"en\">This test is in English.</div>";
//            var result = parser.Parse(html);

//            Assert.AreEqual("en", result.Items[0].Lang);
//        }

//        /// <summary>
//        /// Test HTML fragment with no lang
//        /// </summary>
//        [TestMethod]
//        public void FragmentHEntryNoLang()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-entry\">This test is in English.</div>";
//            var result = parser.Parse(html);

//            Assert.IsNull(result.Items[0].Lang);
//        }

//        /// <summary>
//        /// Test with different <html lang>, h-entry lang, and h-entry without lang, which should inherit from the <html lang>
//        /// </summary>
//        [TestMethod]
//        public void MultiLanguageInheritance()
//        {
//            var parser = new Mf2();
//            var html = "<html lang=\"en\"> <div class=\"h-entry\">This test is in English.</div> <div class=\"h-entry\" lang=\"es\">Esta prueba está en español.</div> </html>";
//            var result = parser.Parse(html);

//            Assert.AreEqual("en", result.Items[0].Lang);
//            Assert.AreEqual("es", result.Items[1].Lang);
//        }

//        /// <summary>
//        /// Test feed with .h-feed lang which contains multiple h-entries of different languages (or none specified), which should inherit from the .h-feed lang.
//        /// </summary>
//        [TestMethod]
//        public void MultiLanguageFeed()
//        {
//            var parser = new Mf2();
//            var html = "<html> <div class=\"h-feed\" lang=\"en\"> <h1 class=\"p-name\">Test Feed</h1> <div class=\"h-entry\">This test is in English.</div> <div class=\"h-entry\" lang=\"es\">Esta prueba está en español.</div> <div class=\"h-entry\" lang=\"fr\">Ce test est en français.</div> </html>";
//            var result = parser.Parse(html);

//            Assert.AreEqual("en", result.Items[0].Lang);
//            Assert.AreEqual("en", result.Items[0].Get<ImpliedEntry, MfType>()[0].Lang);
//            Assert.AreEqual("es", result.Items[0].Get<ImpliedEntry, MfType>()[1].Lang);
//            Assert.AreEqual("fr", result.Items[0].Get<ImpliedEntry, MfType>()[2].Lang);
//        }

//        /// <summary>
//        /// Test with language specified in <meta> http-equiv Content-Language
//        /// </summary>
//        [TestMethod]
//        public void MetaContentLanguage()
//        {
//            var parser = new Mf2();
//            var html = "<html> <meta http-equiv=\"Content-Language\" content=\"es\"/> <div class=\"h-entry\">Esta prueba está en español.</div> </html>";
//            var result = parser.Parse(html);

//            Assert.AreEqual("es", result.Items[0].Lang);
//        }
//    }
//}
