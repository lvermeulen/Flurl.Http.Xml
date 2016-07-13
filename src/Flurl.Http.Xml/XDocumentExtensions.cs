﻿using System.IO;
using System.Text;
using System.Xml.Linq;

namespace Flurl.Http.Xml
{
	/// <summary>
	/// XDocumentExtensions
	/// </summary>
	public static class XDocumentExtensions
	{
		/// <summary>
		/// To the string with declaration.
		/// </summary>
		/// <param name="doc">The document.</param>
		public static string ToStringWithDeclaration(this XDocument doc)
		{
			var sb = new StringBuilder();
			using (TextWriter writer = new Utf8StringWriter(sb))
			{
				doc.Save(writer);
			}
			return sb.ToString();
		}
	}
}