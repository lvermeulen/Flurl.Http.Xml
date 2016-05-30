# Flurl.Http.Xml
Extension for Flurl.Http to support XML

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
    var result = await "http://putsreq.com/JMG62khbf6IWRR8Bz9nU"
        .PostXmlAsync(new TestModel {Number = 3, Text = "Test"})
        .ReceiveXDocument();

    Assert.AreEqual("3", result?.Element("TestModel")?.Element("Number")?.Value);
    Assert.AreEqual("Test", result?.Element("TestModel")?.Element("Text")?.Value);
~~~~
