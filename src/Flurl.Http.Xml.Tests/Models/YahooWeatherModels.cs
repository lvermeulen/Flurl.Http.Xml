using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Flurl.Http.Xml.Tests.Models
{
    [XmlRoot(ElementName = "wind", Namespace = "http://xml.weather.yahoo.com/ns/rss/1.0")]
    public class Wind
    {
        [XmlAttribute(AttributeName = "yweather", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Yweather { get; set; }
        [XmlAttribute(AttributeName = "chill")]
        public string Chill { get; set; }
        [XmlAttribute(AttributeName = "direction")]
        public string Direction { get; set; }
        [XmlAttribute(AttributeName = "speed")]
        public string Speed { get; set; }
    }

    [XmlRoot(ElementName = "channel")]
    public class Channel
    {
        [XmlElement(ElementName = "wind", Namespace = "http://xml.weather.yahoo.com/ns/rss/1.0")]
        public Wind Wind { get; set; }
    }

    [XmlRoot(ElementName = "results")]
    public class Results
    {
        [XmlElement(ElementName = "channel")]
        public Channel Channel { get; set; }
    }

    [XmlRoot(ElementName = "query")]
    public class Query
    {
        [XmlElement(ElementName = "results")]
        public Results Results { get; set; }
        [XmlAttribute(AttributeName = "yahoo", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Yahoo { get; set; }
        [XmlAttribute(AttributeName = "count", Namespace = "http://www.yahooapis.com/v1/base.rng")]
        public string Count { get; set; }
        [XmlAttribute(AttributeName = "created", Namespace = "http://www.yahooapis.com/v1/base.rng")]
        public string Created { get; set; }
        [XmlAttribute(AttributeName = "lang", Namespace = "http://www.yahooapis.com/v1/base.rng")]
        public string Lang { get; set; }
    }
}
