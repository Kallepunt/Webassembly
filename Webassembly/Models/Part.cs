using System;
using System.Web;
using System.Xml.Linq;

namespace Webassembly.Models
{
	public class Part
	{
		public int Id { get; set; }
		public string Category { get; set; } = string.Empty;
		public string Subcategory { get; set; } = string.Empty;

		public string Name { get; set; } = string.Empty;
		public string Location { get; set; } = string.Empty;
		public int Stock { get; set; } 
		public long  PriceCents { get; set; }
		public string Url => $"https://letmebingthatforyou.com/BingThis/{Name}";
	}
}
