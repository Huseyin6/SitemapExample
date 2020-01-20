using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CookieUsage.Web.Controllers
{
    public class SitemapController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SitemapController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        [Route("sitemap_index.xml")]
        public void Get()
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(GetSitemapIndexXMLString());
            }
            catch (Exception)
            {
            }
            var result = doc.InnerXml;
            if (String.IsNullOrEmpty(result)) return;
            var response = _httpContextAccessor.HttpContext.Response;
            response.Clear();
            response.ContentType = "application/xml";
            response.WriteAsync(result);
            response.CompleteAsync();
        }

        [Route("userpages.xml")]
        public void GetCVPage()
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.LoadXml(GetCvPageXMLString());
            }
            catch (Exception)
            {
            }
            var result = doc.InnerXml;
            if (String.IsNullOrEmpty(result)) return;
            var response = _httpContextAccessor.HttpContext.Response;
            response.Clear();
            response.ContentType = "application/xml";
            response.WriteAsync(result);
            response.CompleteAsync();
        }
    
        private string GetCvPageXMLString()
        {
            using (StringWriter str = new Utf8StringWriter())
            using (XmlWriter writer = XmlWriter.Create(str))
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "http://www.sitemaps.org/schemas/sitemap/0.9");
                XmlSerializer xser = new XmlSerializer(typeof(SiteMapModel));
                var obj = new SiteMapModel();
                List<string> users = new List<string> { "AliPage.com", "VeliPage.com", "AysePage.com" };// Veritabanından bir kaynaktan gelebilir.
                foreach (var user in users)
                {
                    if (!String.IsNullOrEmpty(user))
                        obj.SiteMapUrls.Add(new SiteMapNodeModel() { Url = user , LastModified=DateTime.Now.ToString(), Priority="0.8", Frequency= SiteMapFrequency.Monthly.ToString()});
                }
                xser.Serialize(writer, obj, ns);
                return str.ToString();
            }
        }

        private string GetSitemapIndexXMLString()
        {
            using (StringWriter str = new Utf8StringWriter())
            using (XmlWriter writer = XmlWriter.Create(str))
            {
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "http://www.sitemaps.org/schemas/sitemap/0.9");
                XmlSerializer xser = new XmlSerializer(typeof(SiteMapIndexModel));
                var obj = new SiteMapIndexModel
                {
                    SiteMapIndexUrls = {
                        new SiteMapIndexNodeModel { Url = "userpages.xml"},
                        new SiteMapIndexNodeModel { Url = "testpage.xml"}
                    }
                };
                xser.Serialize(writer, obj, ns);
                return str.ToString();
            }
        }

        private class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;// utf16 formatını utf8 olarak ezmesi için.  
        }
    }
}