using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats
{
    /// <summary>
    /// Mapping for classic microformats
    /// </summary>
    public class ClassicMapping
    {
        public string ClassicType { get; set; }

        public string Type { get; set; }

        public (string classic, string modern)[] Properties { get; set; }
    }

    public static class ClassicMappings
    {
        public static string DISABLE_IMPLIED_PROPS = "disable-implied-properties";

        public static List<ClassicMapping> Mapping = new List<ClassicMapping>
        {
            //hentry
            new ClassicMapping
            {
                 ClassicType = "hentry",
                 Type = "h-entry",
                 Properties = new (string classic, string modern)[]{
                    ("category", "p-category"),
                    ("entry-title", "p-name"),
                    ("published", "dt-published"),
                    ("entry-content", "e-content"),
                    ("entry-summary", "p-summary"),
                    ("author", "p-author"),
                    ("author", "h-card"),
                    ("geo", "p-geo"),
                    ("geo", "h-geo"),
                    ("updated", "dt-updated"),
                 }
            }
        };
    }
}
