![Icon](http://i.imgur.com/llEKpRL.png?1) 
# Flurl.Http.Xml [![Build status](https://ci.appveyor.com/api/projects/status/16qwl13xsaylb450?svg=true)](https://ci.appveyor.com/project/lvermeulen/flurl-http-xml) [![license](https://img.shields.io/github/license/lvermeulen/Flurl.Http.Xml.svg?maxAge=2592000)](https://github.com/lvermeulen/Flurl.Http.Xml/blob/master/LICENSE) [![NuGet](https://img.shields.io/nuget/v/Flurl.Http.Xml.svg?maxAge=86400)](https://www.nuget.org/packages/Flurl.Http.Xml/) ![](https://img.shields.io/badge/.net-4.5-yellowgreen.svg) ![](https://img.shields.io/badge/netstandard-1.4-yellowgreen.svg)

[![Join the chat at https://gitter.im/lvermeulen/Flurl.Http.Xml](https://badges.gitter.im/lvermeulen/Flurl.Http.Xml.svg)](https://gitter.im/lvermeulen/Flurl.Http.Xml?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
XML extension to the excellent [Flurl](https://github.com/tmenier/Flurl) library

##Features:
* Get, post and receive XML models
* Receive XDocument
* Receive XElements with XPath

##Usage:

* Get an XDocument:
~~~~
    var result = await "https://query.yahooapis.com/v1/public/yql"
        .SetQueryParam("q", "select wind from weather.forecast where woeid=2460286")
        .SetQueryParam("format", "xml")
        .GetXDocumentAsync();

    string chill = result
        ?.Element("query")
        ?.Element("results")
        ?.Element("channel")
        ?.Element(XNamespace.Get("http://xml.weather.yahoo.com/ns/rss/1.0") + "wind")
        ?.Attribute("chill")
        ?.Value;
~~~~

* Post and receive a model:
~~~~
    var result = await "http://my_xml_endpoint"
        .PostXmlAsync(new TestModel { Number = 3, Text = "Test" })
        .ReceiveXml<TestModel>();

    Assert.AreEqual(3, result.Number);
    Assert.AreEqual("Test", result.Text);
~~~~

* Post a model and receive an XDocument:
~~~~
    var result = await "http://my_xml_endpoint"
        .PostXmlAsync(new TestModel {Number = 3, Text = "Test"})
        .ReceiveXDocument();

    Assert.AreEqual("3", result?.Element("TestModel")?.Element("Number")?.Value);
    Assert.AreEqual("Test", result?.Element("TestModel")?.Element("Text")?.Value);
~~~~

##Thanks
* [XML File](https://thenounproject.com/term/xml-file/320630/) icon by [Oliviu Stoian](https://thenounproject.com/smashicons/) from [The Noun Project](https://thenounproject.com)
