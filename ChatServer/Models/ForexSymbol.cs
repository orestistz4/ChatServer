using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Models
{
	public class ForexSymbol
	{

		public string Name { get; set; }
		public string Bid { get; set; }
		public string Ask { get; set; }

		public string High { get; set; }
		public string Low { get; set; }
		public string Chg { get; set; }

	}
}
