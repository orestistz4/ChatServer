﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Models
{
	public class Customer
	{

		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }

	}
}
