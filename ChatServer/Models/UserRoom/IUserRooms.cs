using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Models.UserRoom
{
	public interface IUserRooms
	{

		Task AddRoom(string email,string room);
		Task<List<UserRooms>> GetUserRooms(string email);

	}
}
