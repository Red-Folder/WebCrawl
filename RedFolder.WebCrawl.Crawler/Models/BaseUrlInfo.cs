using System;
using System.Collections.Generic;

namespace RedFolder.WebCrawl.Crawler.Models
{
    public partial class BaseUrlInfo : IUrlInfo
    {
        protected string _url;
        protected string _invalidationMessage = "";
        protected IList<IUrlInfo> _links;

        public bool HasLinks
        {
            get
            {
                return _links != null && _links.Count > 0;
            }
        }

        public IList<IUrlInfo> Links
        {
            get
            {
                return _links;
            }
        }

        public string Url
        {
            get
            {
                return _url;
            }
        }

        public bool Valid
        {
            get
            {
                return _invalidationMessage.Length == 0;
            }
        }

        public string InvalidationMessage
        {
            get
            {
                return _invalidationMessage;
            }
        }

        public override string ToString()
        {
            if (Valid)
            {
                return String.Format("{0} - Valid", _url);
            }
            else
            {
                return String.Format("{0} - {1}", _url, _invalidationMessage);
            }
        }
    }
}
