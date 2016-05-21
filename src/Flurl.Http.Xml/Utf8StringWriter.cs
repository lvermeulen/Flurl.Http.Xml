using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Flurl.Http.Xml
{
    public class Utf8StringWriter : StringWriter
    {
        public Utf8StringWriter()
        { }

        public Utf8StringWriter(IFormatProvider formatProvider)
            : base(formatProvider)
        { }

        public Utf8StringWriter(StringBuilder builder)
            : base(builder)
        { }

        public Utf8StringWriter(StringBuilder builder, IFormatProvider formatProvider)
            : base(builder, formatProvider)
        { }

        public override Encoding Encoding => Encoding.UTF8;
    }
}
