using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Flurl.Http.Xml.Tests.Models;
using NUnit.Framework;

namespace Flurl.Http.Xml.Tests
{
    [TestFixture]
    public class MicrosoftXmlSerializerShould
    {
        const string XML_WITHOUT_DECLARATION =
            @"<TestModel xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
  <Number>3</Number>
  <Text>Test</Text>
</TestModel>";

        const string XML_WITH_DECLARATION = @"<?xml version=""1.0"" encoding=""utf-8""?>" + "\r\n" + XML_WITHOUT_DECLARATION;

        private MicrosoftXmlSerializer _serializer;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            var settings = new XmlWriterSettings { Encoding = new UTF8Encoding(false, false), Indent = true, OmitXmlDeclaration = false };
            _serializer = new MicrosoftXmlSerializer(settings);
        }

        [Test]
        public void Serialize()
        {
            var model = new TestModel { Number = 3, Text = "Test" };
            string s = _serializer.Serialize(model);
            model = _serializer.Deserialize<TestModel>(s);

            Assert.AreEqual(3, model.Number);
            Assert.AreEqual("Test", model.Text);
        }

        [Test]
        public void DeserializeWithDeclaration()
        {
            var model = _serializer.Deserialize<TestModel>(XML_WITH_DECLARATION);

            Assert.AreEqual(3, model.Number);
            Assert.AreEqual("Test", model.Text);
        }

        [Test]
        public void DeserializeWithoutDeclaration()
        {
            var model = _serializer.Deserialize<TestModel>(XML_WITHOUT_DECLARATION);

            Assert.AreEqual(3, model.Number);
            Assert.AreEqual("Test", model.Text);
        }
    }
}
