using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Models.Rooms
{
	public interface IRoom
	{

		Task AddRoom(string roomName);
		Task DeleteRoom(string roomName);

		Task<bool> CheckRoom(string roomName);

	}
}
