using Microformats.Definitions.Properties.Standard;
using Microformats.Definitions.Properties.Link;
using System;
using System.Collections.Generic;
using System.Text;
using Microformats.Definitions.Properties.Standard.Card;
using Microformats.Definitions.Properties.Standard.Address;

namespace Microformats.Definitions
{
    /// <summary>
    /// Properties
    /// </summary>
    public class Props
    {

        public static HonorificPrefix HonorificPrefix { get; } = new HonorificPrefix();
        public static GivenName GivenName { get; } = new GivenName();
        public static AdditionalName AdditionalName { get; } = new AdditionalName();
        public static FamilyName FamilyName { get; } = new FamilyName();
        public static HonorificSuffix HonorificSuffix { get; } = new HonorificSuffix();
        public static Nickname Nickname { get; } = new Nickname();
        public static Email Email { get; } = new Email();
        public static Tel Telephone { get; } = new Tel();
        public static StreetAddress StreetAddress { get; } = new StreetAddress();
        public static Locality Locality { get; } = new Locality();
        public static Region Region { get; } = new Region();
        public static PostalCode PostalCode { get; } = new PostalCode();
        public static CountryName CountryName { get; } = new CountryName();
        public static Birthday Birthday { get; } = new Birthday();

        public static Category Category { get; } = new Category();
        public static PName Name { get; } = new PName();
        public static Note Note { get; } = new Note();
        public static Org Org { get; } = new Org();
        public static Photo Photo { get; } = new Photo();
        public static Url Url { get; } = new Url();
    }
}
