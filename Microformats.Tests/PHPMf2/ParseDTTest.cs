//using Microformats.Definitions.Properties.DateTime;
//using Microformats.Definitions.Properties.Standard;
//using Microsoft.VisualStudio.TestPlatform.Utilities;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Runtime.Intrinsics.X86;
//using System.Text;
//using System.Threading.Tasks;
//using static System.Runtime.InteropServices.JavaScript.JSType;

//namespace Microformats.Tests.PHPMf2
//{
//    /// <summary>
//    /// Test cases from the indieweb php-mf2 parser
//    /// From: <see href="https://github.com/microformats/php-mf2/blob/main/tests/Mf2/ParseDTTest.php" />
//    /// </summary>
//    [TestClass]
//    public class ParseDTTest
//    {

//        [TestMethod]
//        public void ParseDTHandlesImg()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-event\"><img class=\"dt-start\" alt=\"2012-08-05T14:50\"></div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("start"));
//            Assert.AreEqual("2012-08-05T14:50", result.Items[0].Get<Start>()[0]);
//        }

//        [TestMethod]
//        public void ParseDTHandlesDataValueAttr()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-event\"><data class=\"dt-start\" value=\"2012-08-05T14:50\"></div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("start"));
//            Assert.AreEqual("2012-08-05T14:50", result.Items[0].Get<Start>()[0]);
//        }

//        [TestMethod]
//        public void ParseDTHandlesDataInnerHTML()
//        {

//            var parser = new Mf2();
//            var html = "<div class=\"h-event\"><data class=\"dt-start\">2012-08-05T14:50</data></div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("start"));
//            Assert.AreEqual("2012-08-05T14:50", result.Items[0].Get<Start>()[0]);
//        }

//        [TestMethod]
//        public void ParseDTHandlesAbbrValueAttr()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-event\"><abbr class=\"dt-start\" title=\"2012-08-05T14:50\"></div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("start"));
//            Assert.AreEqual("2012-08-05T14:50", result.Items[0].Get<Start>()[0]);
//        }

//        [TestMethod]
//        public void ParseDTHandlesAbbrInnerHTML()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-event\"><abbr class=\"dt-start\">2012-08-05T14:50</abbr></div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("start"));
//            Assert.AreEqual("2012-08-05T14:50", result.Items[0].Get<Start>()[0]);
//        }

//        [TestMethod]
//        public void ParseDTHandlesTimeDatetimeAttr()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-event\"><time class=\"dt-start\" datetime=\"2012-08-05T14:50\"></div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("start"));
//            Assert.AreEqual("2012-08-05T14:50", result.Items[0].Get<Start>()[0]);
//        }

//        [TestMethod]
//        public void ParseDTHandlesTimeDatetimeAttrWithZ()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-event\"><time class=\"dt-start\" datetime=\"2012-08-05T14:50:00Z\"></div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("start"));
//            Assert.AreEqual("2012-08-05T14:50:00Z", result.Items[0].Get<Start>()[0]);
//        }

//        [TestMethod]
//        public void ParseDTHandlesTimeDatetimeAttrWithTZOffset()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-event\"><time class=\"dt-start\" datetime=\"2012-08-05T14:50:00-0700\"></div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("start"));
//            Assert.AreEqual("2012-08-05T14:50:00-0700", result.Items[0].Get<Start>()[0]);
//        }

//        [TestMethod]
//        public void ParseDTHandlesTimeDatetimeAttrWithTZOffset2()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-event\"><time class=\"dt-start\" datetime=\"2012-08-05T14:50:00-07:00\"></div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("start"));
//            Assert.AreEqual("2012-08-05T14:50:00-07:00", result.Items[0].Get<Start>()[0]);
//        }

//        [TestMethod]
//        public void ParseDTHandlesTimeInnerHTML()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-event\"><time class=\"dt-start\">2012-08-05T14:50</time></div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("start"));
//            Assert.AreEqual("2012-08-05T14:50", result.Items[0].Get<Start>()[0]);
//        }

//        [TestMethod]
//        public void ParseDTHandlesInsDelDatetime()
//        {

//            var parser = new Mf2();
//            var html = "<div class=\"h-event\"><ins class=\"dt-start\" datetime=\"2012-08-05T14:50\"></ins><del class=\"dt-end\" datetime=\"2012-08-05T18:00\"></del></div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("start"));
//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("end"));
//            Assert.AreEqual("2012-08-05T14:50", result.Items[0].Get<Start>()[0]);
//            Assert.AreEqual("2012-08-05T18:00", result.Items[0].Get<End>()[0]);
//        }

//        [TestMethod]
//        public void YYYY_MM_DD__HH_MM()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-event\"><span class=\"dt-start\"><span class=\"value\">2012-10-07</span> at <span class=\"value\">21:18</span></span></div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("start"));
//            Assert.AreEqual("2012-10-07 21:18", result.Items[0].Get<Start>()[0]);
//        }

//        [TestMethod]
//        public void AbbrYYYY_MM_DD__HH_MM()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-event\"><span class=\"dt-start\"><abbr class=\"value\" title=\"2012-10-07\">some day</abbr> at <span class=\"value\">21:18</span></span></div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("start"));
//            Assert.AreEqual("2012-10-07 21:18", result.Items[0].Get<Start>()[0]);
//        }

//        [TestMethod]
//        public void YYYY_MM_DD__HHpm()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-event\"><span class=\"dt-start\"><span class=\"value\">2012-10-07</span> at <span class=\"value\">9pm</span></span></div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("start"));
//            Assert.AreEqual("2012-10-07 21:00", result.Items[0].Get<Start>()[0]);
//        }

//        [TestMethod]
//        public void YYYY_MM_DD__HH_MMpm()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-event\"><span class=\"dt-start\"><span class=\"value\">2012-10-07</span> at <span class=\"value\">9:00pm</span></span></div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("start"));
//            Assert.AreEqual("2012-10-07 21:00", result.Items[0].Get<Start>()[0]);
//        }

//        [TestMethod]
//        public void YYYY_MM_DD__HH_MM_SSpm()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-event\"><span class=\"dt-start\"><span class=\"value\">2012-10-07</span> at <span class=\"value\">9:00:00pm</span></span></div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("start"));
//            Assert.AreEqual("2012-10-07 21:00:00", result.Items[0].Get<Start>()[0]);
//        }

//        [TestMethod]
//        public void ImpliedDTEndWithValueClass()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-event\"> <span class=\"dt-start\"><span class=\"value\">2014-06-04</span> at <span class=\"value\">18:30</span> <span class=\"dt-end\"><span class=\"value\">19:30</span></span></span> </div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("start"));
//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("end"));
//            Assert.AreEqual("2014-06-04 18:30", result.Items[0].Get<Start>()[0]);
//            Assert.AreEqual("2014-06-04 19:30", result.Items[0].Get<End>()[0]);
//        }

//        [TestMethod]
//        public void ImpliedDTEndWithoutValueClass()
//        {

//            var parser = new Mf2();
//            var html = "<div class=\"h-event\"> <span class=\"dt-start\"><span class=\"value\">2014-06-05</span> at <span class=\"value\">18:31</span> <span class=\"dt-end\">19:31</span></span> </div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("start"));
//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("end"));
//            Assert.AreEqual("2014-06-05 18:31", result.Items[0].Get<Start>()[0]);
//            Assert.AreEqual("2014-06-05 19:31", result.Items[0].Get<End>()[0]);
//        }

//        [TestMethod]
//        public void ImpliedDTEndUsingNonValueClassDTStart()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-event\"> <time class=\"dt-start\">2014-06-05T18:31</time> until <span class=\"dt-end\">19:31</span></span> </div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("start"));
//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("end"));
//            Assert.AreEqual("2014-06-05T18:31", result.Items[0].Get<Start>()[0]);
//            Assert.AreEqual("2014-06-05 19:31", result.Items[0].Get<End>()[0]);
//        }

//        [TestMethod]
//        public void DTStartOnly()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-event\"> <span class=\"dt-start\"><span class=\"value\">2014-06-06</span> at <span class=\"value\">18:32</span> </span> </div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("start"));
//            Assert.IsFalse(result.Items[0].Properties.ContainsKey("end"));
//            Assert.AreEqual("2014-06-06 18:32", result.Items[0].Get<Start>()[0]);
//        }

//        [TestMethod]
//        public void DTStartDateOnly()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-event\"> <span class=\"dt-start\"><span class=\"value\">2014-06-07</span> </span> </div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("start"));
//            Assert.AreEqual("2014-06-07", result.Items[0].Get<Start>()[0]);
//        }

//        /// <summary>
//        /// TZ offsets normalized only for VCP.
//        /// This behavior is implied from "However the colons ":" separating the hours and minutes of any timezone offset are optional and discouraged in order to make it
//        /// less likely that a timezone offset will be confused for a time.
//        /// See: <see href="http://microformats.org/wiki/index.php?title=value-class-pattern&oldid=66473##However+the+colons"/>
//        /// </summary>

//        [TestMethod]
//        public void NormalizeTZOffsetVCP()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-event\">\r\n            <span class=\"dt-start\"> <time class=\"value\" datetime=\"2017-05-27\">May 27</time>, from\r\n            <time class=\"value\">20:57-07:00</time> </span>\r\n        </div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("start"));
//            Assert.AreEqual("2017-05-27 20:57-0700", result.Items[0].Get<Start>()[0]);
//        }

//        /// <summary>
//        /// TZ offsets *not* normalized for non-VCP dates
//        /// </summary>

//        [TestMethod]
//        public void NoNormalizeTZOffset()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-entry\"> <time class=\"dt-start\" datetime=\"2018-03-13 15:30-07:00\">March 13, 2018 3:30PM</time> </div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("start"));
//            Assert.AreEqual("2018-03-13 15:30-07:00", result.Items[0].Get<Start>()[0]);
//        }

//        /// <summary>
//        /// <see href="https://github.com/indieweb/php-mf2/issues/115" />
//        /// </summary>
//        [TestMethod]
//        public void DoNotAddT()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-entry\">\r\n  <span class=\"dt-start\">\r\n    <time class=\"value\" datetime=\"2009-06-26\">26 July</time>, from\r\n    <time class=\"value\">19:00:00-08:00</time>\r\n  </span>\r\n</div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("start"));
//            Assert.AreEqual("2009-06-26 19:00:00-0800", result.Items[0].Get<Start>()[0]);
//        }

//        /// <summary>
//        /// <see href="https://github.com/indieweb/php-mf2/issues/115" />
//        /// </summary>
//        [TestMethod]
//        public void PreserrveTIfAuthored()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-entry\"> <time class=\"dt-start\" datetime=\"2009-06-26T19:01-08:00\">26 July, 7:01pm</time> </div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("start"));
//            Assert.AreEqual("2009-06-26T19:01-08:00", result.Items[0].Get<Start>()[0]);
//        }

//        /// <summary>
//        /// <see href="https://github.com/indieweb/php-mf2/issues/126" />
//        /// </summary>
//        [TestMethod]
//        public void DtVCPTimezone()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-event\">\r\n <span class=\"e-summary\">HomebrewWebsiteClub Berlin</span> will be next on\r\n <span class=\"dt-start\">\r\n  <span class=\"value\">2017-05-31</span>, from\r\n  <span class=\"value\">19:00</span> (UTC<span class=\"value\">+02:00</span>)\r\n</span> to  <span class=\"dt-end\">21:00</span>.</div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("start"));
//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("end"));
//            Assert.AreEqual("2017-05-31 19:00+0200", result.Items[0].Get<Start>()[0]);
//            Assert.AreEqual("2017-05-31 21:00+0200", result.Items[0].Get<End>()[0]);
//        }

//        /// <summary>
//        /// <see href="https://github.com/indieweb/php-mf2/issues/126" />
//        /// </summary>
//        [TestMethod]
//        public void DtVCPTimezoneShort()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-event\">\r\n <span class=\"e-summary\">HomebrewWebsiteClub Berlin</span> will be next on\r\n <span class=\"dt-start\">\r\n  <span class=\"value\">2017-05-31</span>, from\r\n  <span class=\"value\">19:00</span> (UTC<span class=\"value\">+2</span>)\r\n</span> to  <span class=\"dt-end\">21:00</span>.</div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("start"));
//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("end"));
//            Assert.AreEqual("2017-05-31 19:00+0200", result.Items[0].Get<Start>()[0]);
//            Assert.AreEqual("2017-05-31 21:00+0200", result.Items[0].Get<End>()[0]);
//        }

//        /// <summary>
//        /// <see href="https://github.com/indieweb/php-mf2/issues/126" />
//        /// </summary>
//        [TestMethod]
//        public void DtVCPTimezoneNoLeadingZero()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-event\">\r\n\t<span class=\"dt-start\">\r\n\t\t<span class=\"value\">2017-06-17</span>\r\n\t\t<span class=\"value\">22:00-700</span>\r\n\t</span>\r\n\t<span class=\"dt-end\">\r\n\t\t<span class=\"value\">2017-06-17</span>\r\n\t\t<span class=\"value\">23:00-700</span>\r\n\t</span>\r\n</div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("start"));
//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("end"));
//            Assert.AreEqual("2017-06-17 22:00-0700", result.Items[0].Get<Start>()[0]);
//            Assert.AreEqual("2017-06-17 23:00-0700", result.Items[0].Get<End>()[0]);
//        }

//        /// <summary>
//        /// <see href="https://github.com/microformats/microformats2-parsing/issues/4" />
//        /// </summary>
//        [TestMethod]
//        public void ImplyTimezoneFromStart()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-event\"> <time class=\"dt-start\" datetime=\"2014-09-11 13:30-0700\">13:30</time> to <time class=\"dt-end\">15:30</time> </div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("start"));
//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("end"));
//            Assert.AreEqual("2014-09-11 13:30-0700", result.Items[0].Get<Start>()[0]);
//            Assert.AreEqual("2014-09-11 15:30-0700", result.Items[0].Get<End>()[0]);
//        }

//        /// <summary>
//        /// <see href="https://github.com/microformats/microformats2-parsing/issues/4" />
//        /// </summary>
//        [TestMethod]
//        public void ImplyTimezoneFromEnd()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-event\"> <time class=\"dt-start\" datetime=\"2014-09-11 13:30\">13:30</time> to <time class=\"dt-end\">15:30-0700</time> </div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("start"));
//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("end"));
//            Assert.AreEqual("2014-09-11 13:30-0700", result.Items[0].Get<Start>()[0]);
//            Assert.AreEqual("2014-09-11 15:30-0700", result.Items[0].Get<End>()[0]);
//        }

//        [TestMethod]
//        public void AMPMWithPeriods()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-event\">\r\n\t<span class=\"dt-start\">\r\n\t\t<span class=\"value\">2017-06-11</span>\r\n\t\t<span class=\"value\">10:00P.M.</span>\r\n\t</span>\r\n\t<span class=\"dt-end\">\r\n\t\t<span class=\"value\">2017-06-12</span>\r\n\t\t<span class=\"value\">02:00a.m.</span>\r\n\t</span>\r\n</div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("start"));
//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("end"));
//            Assert.AreEqual("2017-06-11 22:00", result.Items[0].Get<Start>()[0]);
//            Assert.AreEqual("2017-06-12 02:00", result.Items[0].Get<End>()[0]);
//        }

//        [TestMethod]
//        public void AMPMWithoutPeriods()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-event\">\r\n\t<span class=\"dt-start\">\r\n\t\t<span class=\"value\">2017-06-17</span>\r\n\t\t<span class=\"value\">10:30pm</span>\r\n\t</span>\r\n\t<span class=\"dt-end\">\r\n\t\t<span class=\"value\">2017-06-18</span>\r\n\t\t<span class=\"value\">02:30AM</span>\r\n\t</span>\r\n</div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("start"));
//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("end"));
//            Assert.AreEqual("2017-06-17 22:30", result.Items[0].Get<Start>()[0]);
//            Assert.AreEqual("2017-06-18 02:30", result.Items[0].Get<End>()[0]);
//        }

//        [TestMethod]
//        public void DtVCPTimeAndTimezone()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-event\">\r\n\t<span class=\"dt-start\">\r\n\t\t<span class=\"value\">2017-06-17</span>\r\n\t\t<span class=\"value\">13:30-07:00</span>\r\n\t</span>\r\n\t<span class=\"dt-end\">\r\n\t\t<span class=\"value\">2017-06-17</span>\r\n\t\t<span class=\"value\">15:30-0700</span>\r\n\t</span>\r\n</div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("start"));
//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("end"));
//            Assert.AreEqual("2017-06-17 13:30-0700", result.Items[0].Get<Start>()[0]);
//            Assert.AreEqual("2017-06-17 15:30-0700", result.Items[0].Get<End>()[0]);
//        }

//        /// <summary>
//        /// <see href="https://github.com/indieweb/php-mf2/issues/147" />
//        /// </summary>
//        [TestMethod]
//        public void DtVCPMultipleDatesAndTimezones()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-event\">\r\n  <h1 class=\"p-name\">Multiple date and time values</h1>\r\n\r\n  <p> When:\r\n    <span class=\"dt-start\">\r\n    <span class=\"value\" title=\"June 1, 2014\">2014-06-01</span>\r\n    <span class=\"value\" title=\"June 1, 3014\">3014-06-01</span>\r\n    <span class=\"value\" title=\"12:30\">12:30</span>\r\n    (UTC<span class=\"value\">-06:00</span>)\r\n    <span class=\"value\" title=\"23:00\">23:00</span>\r\n    (UTC<span class=\"value\">+01:00</span>)\r\n    –\r\n    <span class=\"dt-end\">19:30</span>\r\n  </p>\r\n\r\n</div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("start"));
//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("end"));
//            Assert.AreEqual("2014-06-01 12:30-0600", result.Items[0].Get<Start>()[0]);
//            Assert.AreEqual("2014-06-01 19:30-0600", result.Items[0].Get<End>()[0]);
//        }

//        /// <summary>
//        /// <see href="https://github.com/indieweb/php-mf2/issues/149" />
//        /// </summary>
//        [TestMethod]
//        public void DtWithoutYear()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-card\"> <time class=\"dt-bday\" datetime=\"--12-28\"></time> </div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("bday"));
//            Assert.AreEqual("--12-28", result.Items[0].Get<Birthday>()[0]);
//        }

//        /// <summary>
//        /// <see href="https://github.com/indieweb/php-mf2/issues/167" />
//        /// <see href="https://github.com/microformats/mf2py/blob/master/test/examples/datetimes.html" />
//        /// </summary>
//        [TestMethod]
//        public void NormalizeOrdinalDate()
//        {
//            var parser = new Mf2();
//            var html = "<div class=\"h-event\">\r\n\t<h1 class=\"p-name\">Ordinal date</h1>\r\n\t<p> When:\r\n\t\t<span class=\"dt-start\">\r\n\t\t\t<span class=\"value\">2016-062</span>\r\n\t\t\t<span class=\"value\">12:30AM</span>\r\n\t\t\t(UTC<span class=\"value\">-06:00</span>)\r\n\t</p>\r\n</div>";
//            var result = parser.Parse(html);

//            Assert.IsTrue(result.Items[0].Properties.ContainsKey("bday"));
//            Assert.AreEqual("2016-03-02 12:30-0600", result.Items[0].Get<Start>()[0]);
//        }

//    }
//}
