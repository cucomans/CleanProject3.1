namespace Miniblog.Core.Controllers
{
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using Microsoft.SyndicationFeed;
    using Microsoft.SyndicationFeed.Atom;
    using Microsoft.SyndicationFeed.Rss;

    using Miniblog.Core.Services;

    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;

    using WebEssentials.AspNetCore.Pwa;

    public class RobotsController : Controller
    {
        //private readonly IBlogService blog;

        private readonly WebManifest manifest;

        private readonly IOptionsSnapshot<BlogSettings> settings;

        public RobotsController( IOptionsSnapshot<BlogSettings> settings, WebManifest manifest)
        {
           
            this.settings = settings;
            this.manifest = manifest;
        }

        [Route("/robots.txt")]
        [OutputCache(Profile = "default")]
        public string RobotsTxt()
        {
            var sb = new StringBuilder();
            sb.AppendLine("User-agent: *")
                .AppendLine("Disallow:")
                .Append("sitemap: ")
                .Append(this.Request.Scheme)
                .Append("://")
                .Append(this.Request.Host)
                .AppendLine("/sitemap.xml");

            return sb.ToString();
        }

        [Route("/rsd.xml")]
        public void RsdXml()
        {
            EnableHttpBodySyncIO();

            var host = $"{this.Request.Scheme}://{this.Request.Host}";

            this.Response.ContentType = "application/xml";
            this.Response.Headers["cache-control"] = "no-cache, no-store, must-revalidate";

            using var xml = XmlWriter.Create(this.Response.Body, new XmlWriterSettings { Indent = true });
            xml.WriteStartDocument();
            xml.WriteStartElement("rsd");
            xml.WriteAttributeString("version", "1.0");

            xml.WriteStartElement("service");

            xml.WriteElementString("enginename", "Miniblog.Core");
            xml.WriteElementString("enginelink", "http://github.com/madskristensen/Miniblog.Core/");
            xml.WriteElementString("homepagelink", host);

            xml.WriteStartElement("apis");
            xml.WriteStartElement("api");
            xml.WriteAttributeString("name", "MetaWeblog");
            xml.WriteAttributeString("preferred", "true");
            xml.WriteAttributeString("apilink", $"{host}/metaweblog");
            xml.WriteAttributeString("blogid", "1");

            xml.WriteEndElement(); // api
            xml.WriteEndElement(); // apis
            xml.WriteEndElement(); // service
            xml.WriteEndElement(); // rsd
        }

      

        [Route("/sitemap.xml")]
        public async Task SitemapXml()
        {
            EnableHttpBodySyncIO();

            var host = $"{this.Request.Scheme}://{this.Request.Host}";

            this.Response.ContentType = "application/xml";

            using var xml = XmlWriter.Create(this.Response.Body, new XmlWriterSettings { Indent = true });
            xml.WriteStartDocument();
            xml.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");


            xml.WriteEndElement();
        }

        private async Task<ISyndicationFeedWriter> GetWriter(string? type, XmlWriter xmlWriter, DateTime updated)
        {
            var host = $"{this.Request.Scheme}://{this.Request.Host}/";

            if (type?.Equals("rss", StringComparison.OrdinalIgnoreCase) ?? false)
            {
                var rss = new RssFeedWriter(xmlWriter);
                await rss.WriteTitle(this.manifest.Name).ConfigureAwait(false);
                await rss.WriteDescription(this.manifest.Description).ConfigureAwait(false);
                await rss.WriteGenerator("Miniblog.Core").ConfigureAwait(false);
                await rss.WriteValue("link", host).ConfigureAwait(false);
                return rss;
            }

            var atom = new AtomFeedWriter(xmlWriter);
            await atom.WriteTitle(this.manifest.Name).ConfigureAwait(false);
            await atom.WriteId(host).ConfigureAwait(false);
            await atom.WriteSubtitle(this.manifest.Description).ConfigureAwait(false);
            await atom.WriteGenerator("Miniblog.Core", "https://github.com/madskristensen/Miniblog.Core", "1.0").ConfigureAwait(false);
            await atom.WriteValue("updated", updated.ToString("yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture)).ConfigureAwait(false);
            return atom;
        }

        private void EnableHttpBodySyncIO()
        {
            var body = HttpContext.Features.Get<IHttpBodyControlFeature>();
            body.AllowSynchronousIO = true;
        }
    }
}
