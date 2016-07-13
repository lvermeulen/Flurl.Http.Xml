﻿using System.Text;
using Flurl.Http.Content;

namespace Flurl.Http.Xml
{
	/// <summary>
	/// Provides HTTP content based on a serialized XML object, with the XML string captured to a property
	/// so it can be read without affecting the read-once content stream.
	/// </summary>
	public class CapturedXmlContent : CapturedStringContent
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CapturedXmlContent"/> class.
		/// </summary>
		/// <param name="xml">The XML.</param>
		public CapturedXmlContent(string xml) : base(xml, Encoding.UTF8, "application/xml") { }
	}
}