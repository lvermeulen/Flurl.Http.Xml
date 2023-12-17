using System.Text;
using System.Xml;
using FlurlX.Http.Xml.Tests.Models;
using Xunit;

namespace FlurlX.Http.Xml.Tests
{
    public class MicrosoftXmlSerializerShould
    {
        const string XML_WITHOUT_DECLARATION =
            @"<TestModel xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
              <Number>3</Number>
              <Text>Test</Text>
            </TestModel>";

        const string XML_WITH_DECLARATION = @"<?xml version=""1.0"" encoding=""utf-8""?>" + "\r\n" + XML_WITHOUT_DECLARATION;

        private readonly MicrosoftXmlSerializer _serializer;

        public MicrosoftXmlSerializerShould()
        {
            var settings = new XmlWriterSettings { Encoding = new UTF8Encoding(false, false), Indent = true, OmitXmlDeclaration = false };
            _serializer = new MicrosoftXmlSerializer(settings);
        }

        [Fact]
        public void Serialize()
        {
            var model = new TestModel { Number = 3, Text = "Test" };
            model = _serializer.Deserialize<TestModel>(_serializer.Serialize(model));

            Assert.Equal(3, model.Number);
            Assert.Equal("Test", model.Text);
        }

        [Fact]
        public void DeserializeWithDeclaration()
        {
            var model = _serializer.Deserialize<TestModel>(XML_WITH_DECLARATION);

            Assert.Equal(3, model.Number);
            Assert.Equal("Test", model.Text);
        }

        [Fact]
        public void DeserializeWithoutDeclaration()
        {
            var model = _serializer.Deserialize<TestModel>(XML_WITHOUT_DECLARATION);

            Assert.Equal(3, model.Number);
            Assert.Equal("Test", model.Text);
        }
    }
}
