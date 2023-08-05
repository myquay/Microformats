using System;
using System.Collections.Generic;
using System.Text;

namespace Microformats.Definitions
{
    /// <summary>
    /// Well known values
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Well known property names
        /// </summary>
        public static class Props
        {
            public const string ANNIVERSARY = "dt-anniversary";
            public const string BIRTHDAY = "dt-bday";
            public const string DURATION_DATETIME = "dt-duration";
            public const string END = "dt-end";
            public const string EXPIRED = "dt-expired";
            public const string LISTED = "dt-listed";
            public const string PUBLISHED = "dt-published";
            public const string START = "dt-start";
            public const string UPDATED = "dt-updated";

            public const string ACTION = "p-action";
            public const string ADDITIONAL_NAME = "p-additional-name";
            public const string ADDRESS = "p-adr";
            public const string AFFILIATION = "p-affiliation";
            public const string ALTIUDE = "p-altitude";
            public const string ATTENDEE = "p-attendee";
            public const string AUTHOR = "p-author";
            public const string AVERAGE = "p-average";
            public const string BEST = "p-best";
            public const string BRAND = "p-brand";
            public const string CATEGORY = "p-category";
            public const string CONTACT = "p-contact";
            public const string COUNT = "p-count";
            public const string COUNTRY_NAME = "p-country-name";
            public const string DESCRIPTION = "p-description";
            public const string DURATION = "p-duration";
            public const string EDUCATION = "p-education";
            public const string ENTRY = "p-entry";
            public const string EXTENDED_ADDRESS = "p-extended-address";
            public const string FAMILY_NAME = "p-family-name";
            public const string GENDER_IDENTITY = "p-gender-identity";
            public const string GEO = "p-geo";
            public const string GIVEN_NAME = "p-given-name";
            public const string HONORIFIC_PREFIX = "p-honorific-prefix";
            public const string HONORIFIC_SUFFIX = "p-honorific-suffix";
            public const string INGREDIENT = "p-ingredient";
            public const string ITEM = "p-item";
            public const string JOB_TITLE = "p-job-title";
            public const string LABEL = "p-label";
            public const string LATITUDE = "p-latitude";
            public const string LISTER = "p-lister";
            public const string LOCALITY = "p-locality";
            public const string LOCATION = "p-location";
            public const string LONGITUDE = "p-longitude";
            public const string NAME = "p-name";
            public const string NICKNAME = "p-nickname";
            public const string NOTE = "p-note";
            public const string NUTRITION = "p-nutrition";
            public const string ORGANIZATION = "p-org";
            public const string ORGANIZER = "p-organizer";
            public const string POST_OFFICE_BOX = "p-post-office-box";
            public const string POSTAL_CODE = "p-postal-code";
            public const string PRICE = "p-price";
            public const string RATING = "p-rating";
            public const string READ_OF = "p-read-of";
            public const string REGION = "p-region";
            public const string REVIEW = "p-review";
            public const string ROLE = "p-role";
            public const string RSVP = "p-rsvp";
            public const string SEX = "p-sex";
            public const string SIZE = "p-size";
            public const string SKILL = "p-skill";
            public const string SORT_STRING = "p-sort-string";
            public const string STREET_ADDRESS = "p-street-address";
            public const string SUMMARY = "p-summary";
            public const string TELEPHONE = "p-tel";
            public const string VOTES = "p-votes";
            public const string WORST = "p-worst";
            public const string YIELD = "p-yield";

            public const string AUDIO = "u-audio";
            public const string BOOKMARK_OF = "u-bookmark-of";
            public const string CHECKIN = "u-checkin";
            public const string EMAIL = "u-email";
            public const string FEATURED = "u-featured";
            public const string GEO_URI = "u-geo";
            public const string IDENTIFIER = "u-identifier";
            public const string IMPP = "u-impp";
            public const string IN_REPLY_TO = "u-in-reply-to";
            public const string KEY = "u-key";
            public const string LIKE = "u-like";
            public const string LIKE_OF = "u-like-of";
            public const string LISTEN_OF = "u-listen-of";
            public const string LOGO = "u-logo";
            public const string PHOTO = "u-photo";
            public const string REMIX_OF = "u-remix-of";
            public const string SOUND = "u-sound";
            public const string SYNDICATION = "u-syndication";
            public const string TRANSLATION_OF = "u-translation-of";
            public const string REPOST = "u-repost";
            public const string REPOST_OF = "u-repost-of";
            public const string RSVP_URI = "u-rsvp";
            public const string URL = "u-url";
            public const string UID = "u-uid";
            public const string VIDEO = "u-video";
            public const string WATCH_OF = "u-watch-of";

            public const string CONTENT = "e-content";
            public const string DESCRIPTION_HTML = "e-description";
            public const string INSTRUCTIONS = "e-instructions";
        }

        /// <summary>
        /// Well known specification names
        /// </summary>
        public static class Specs
        {
            /// <summary>
            /// The h-adr microformat is for marking up structured locations such as addresses, physical and/or postal. 
            /// This is an update to adr.
            /// </summary>
            public const string ADDRESS = "h-adr";

            /// <summary>
            /// The h-card microformat is for marking up people and organizations. 
            /// This is an update to hCard.
            /// </summary>
            public const string CARD = "h-card";

            /// <summary>
            /// The h-entry microformat is for marking up syndicatable content such as blog posts, notes, articles, comments, photos and similar. 
            /// This is an update to hAtom.
            /// </summary>
            public const string ENTRY = "h-entry";

            /// <summary>
            /// The h-event microformat is for marking up events. 
            /// This is an update to hCalendar.
            /// </summary>
            public const string EVENT = "h-event";

            /// <summary>
            /// h-feed is a simple, open format for publishing a stream or feed of h-entry posts, like complete posts on a home page or archive pages, or summaries or other brief lists of posts. h-feed is one of several open microformat draft standards suitable for embedding data in HTML.
            /// </summary>
            public const string FEED = "h-feed";

            /// <summary>
            /// The h-geo microformat is for marking up WGS84 geophysical coordinates. 
            /// This is an update to geo.
            /// </summary>
            public const string GEO = "h-geo";

            /// <summary>
            /// The h-item microformat is for marking up the item of an h-review or h-product. 
            /// This is an update to part of hReview.
            /// </summary>
            public const string ITEM = "h-item";

            /// <summary>
            /// h-listing is a simple, open format for publishing product data on the web. h-listing is one of several open microformat draft standards suitable for embedding data in HTML.
            /// </summary>
            public const string LISTING = "h-listing";

            /// <summary>
            /// The h-product microformat is for marking up products. 
            /// This is an update to hProduct.
            /// </summary>
            public const string PRODUCT = "h-product";

            /// <summary>
            /// The h-recipe microformat is for marking up food recipes. 
            /// This is an update to hRecipe.
            /// </summary>
            public const string RECIPE = "h-recipe";

            /// <summary>
            /// The h-resume microformat is for marking up resumes. 
            /// This is an update to hResume.
            /// </summary>
            public const string RESUME = "h-resume";

            /// <summary>
            /// The h-review microformat is for marking up reviews. 
            /// This is an update to hReview. See also h-item.
            /// </summary>
            public const string REVIEW = "h-review";

            /// <summary>
            /// The h-review-aggregate microformat is for marking up aggregate reviews of a single item. 
            /// This is an update to hreview-aggregate. See also h-item.
            /// </summary>
            public const string REVIEW_AGGREGATE = "h-review-aggregate";
        }
    }
}
