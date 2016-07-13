using System;
using System.IO;
using System.Text;

namespace Flurl.Http.Xml
{
	/// <summary>
	/// Utf8StringWriter
	/// </summary>
	/// <seealso cref="System.IO.StringWriter" />
	public class Utf8StringWriter : StringWriter
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Utf8StringWriter"/> class.
		/// </summary>
		public Utf8StringWriter()
		{ }

		/// <summary>
		/// Initializes a new instance of the <see cref="Utf8StringWriter"/> class.
		/// </summary>
		/// <param name="formatProvider">The format provider.</param>
		public Utf8StringWriter(IFormatProvider formatProvider)
			: base(formatProvider)
		{ }

		/// <summary>
		/// Initializes a new instance of the <see cref="Utf8StringWriter"/> class.
		/// </summary>
		/// <param name="builder">The builder.</param>
		public Utf8StringWriter(StringBuilder builder)
			: base(builder)
		{ }

		/// <summary>
		/// Initializes a new instance of the <see cref="Utf8StringWriter" /> class.
		/// </summary>
		/// <param name="builder">The builder.</param>
		/// <param name="formatProvider">The format provider.</param>
		public Utf8StringWriter(StringBuilder builder, IFormatProvider formatProvider)
					: base(builder, formatProvider)
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