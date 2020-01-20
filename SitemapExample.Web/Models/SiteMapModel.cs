using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CookieUsage.Web
{

    [XmlRoot("urlset", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    public class SiteMapModel
    {
        private readonly List<SiteMapNodeModel> urls = new List<SiteMapNodeModel>();

        [XmlElement("url")]
        public List<SiteMapNodeModel> SiteMapUrls { get { return urls; } }
    }

    public class SiteMapNodeModel
    {
        [XmlElement("loc")]
        public string Url { get; set; }

        [XmlElement("changefreq")]
        public string Frequency { get; set; }

        [XmlElement("lastmod")]
        public string LastModified { get; set; }

        [XmlElement("priority")]
        public string Priority { get; set; }
    }

    [XmlRoot("sitemapindex", Namespace = "http://www.sitemaps.org/schemas/sitemap/0.9")]
    public class SiteMapIndexModel
    {
        private readonly List<SiteMapIndexNodeModel> urls = new List<SiteMapIndexNodeModel>();

        [XmlElement("sitemap")]
        public List<SiteMapIndexNodeModel> SiteMapIndexUrls { get { return urls; } }
    }

    public class SiteMapIndexNodeModel
    {
        [XmlElement("loc")]
        public string Url { get; set; }

        [XmlElement("lastmod")]
        public string LastModified { get; set; }
    }

    public enum SiteMapFrequency
    {
        Never,
        Yearly,
        Monthly,
        Weekly,
        Daily,
        Hourly,
        Always
    }

}
