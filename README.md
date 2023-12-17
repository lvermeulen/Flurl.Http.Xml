![Icon](http://i.imgur.com/llEKpRL.png?1) 
# FlurlX.Http.Xml
[![license](https://img.shields.io/github/license/renanaragao/FlurlX.Http.Xml.svg?maxAge=2592000)](https://github.com/lvermeulen/Flurl.Http.Xml/blob/master/LICENSE) 
[![NuGet](https://img.shields.io/nuget/v/FlurlX.Http.Xml.svg?maxAge=86400)](https://www.nuget.org/packages/Flurl.Http.Xml/) 
![downloads](https://img.shields.io/nuget/dt/FlurlX.Http.Xml)
![](https://img.shields.io/badge/.net-8.0.100-yellowgreen.svg) 

XML extension to the excellent [Flurl 4](https://github.com/tmenier/Flurl) library

## Features:
* Get, post and receive XML models
* Receive XDocument
* Receive XElements with XPath

## Usage:

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

* Put a model and receive an XDocument:
~~~~
    var result = await "http://my_xml_endpoint"
        .PutXmlAsync(new TestModel {Number = 3, Text = "Test"})
        .ReceiveXDocument();

    Assert.AreEqual("3", result?.Element("TestModel")?.Element("Number")?.Value);
    Assert.AreEqual("Test", result?.Element("TestModel")?.Element("Text")?.Value);
~~~~

## Thanks
* [XML File](https://thenounproject.com/term/xml-file/320630/) icon by [Oliviu Stoian](https://thenounproject.com/smashicons/) from [The Noun Project](https://thenounproject.com)
