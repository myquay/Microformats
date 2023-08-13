using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class StringExtensions
    {
        public static string ToAbsoluteUri(this string value, Uri baseUri)
        {
            value = value?.Trim();

            if (value == null || baseUri == null)
                return value; //Nothing we can do

            if (!Uri.TryCreate(value, UriKind.RelativeOrAbsolute, out var _))
                return value; //Not a valid URI

            if (value.StartsWith("https://") || value.StartsWith("http://"))
                return value; //Already an absolute URI

            if (Uri.TryCreate(baseUri, value, out var uri))
                return uri.ToString();

            return value;
        }
    }
}
