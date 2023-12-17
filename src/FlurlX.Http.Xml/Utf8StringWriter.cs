using System.IO;
using System.Text;

namespace FlurlX.Http.Xml
{
    /// <summary>
    /// Utf8StringWriter
    /// </summary>
    /// <seealso cref="System.IO.StringWriter" />
    internal class Utf8StringWriter : StringWriter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Utf8StringWriter"/> class.
        /// </summary>
        public Utf8StringWriter()
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Utf8StringWriter"/> class.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public Utf8StringWriter(StringBuilder builder)
            : base(builder)
        { }

        /// <summary>
        /// Gets the encoding.
        /// </summary>
        /// <value>
        /// The encoding.
        /// </value>
        public override Encoding Encoding => Encoding.UTF8;
    }
}