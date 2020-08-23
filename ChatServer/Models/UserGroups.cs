using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Models
{
	public class UserGroups
	{
		[Key]
		public int Id { get; set; }
		public string Username { get; set; }
		public List<string> Groups { get; set; }
	}
}
