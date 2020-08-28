using ChatServer.Models.UserAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace ChatServer.Models.Rooms
{
	public class RoomHandler : IRoom
	{


		public RoomHandler(AppDbContext context)
		{
			Context = context;
		}

		public AppDbContext Context { get; }

		public async Task<bool> AddRoom(string roomName)
		{
			var exists = Context.Rooms.FirstOrDefault(c=>c.Room==roomName);
			if (exists == null)
			{

				await Context.Rooms.AddAsync(new GroupRoom() { Room=roomName});

				await Context.SaveChangesAsync();
				return true;
			}
			else
			{
				return false;
			}
		}

		//tsekarei an yparxei to room
		public async Task<bool> CheckRoom(string roomName)
		{
			var exists = Context.Rooms.FirstOrDefault(c => c.Room == roomName);
			if (exists != null)
			{
				//yparxei to room
				return true;
			}
			else
			{
				//den yparxei to room
				return false;
			}
		}

		public async Task DeleteRoom(string roomName)
		{
			var exists = Context.Rooms.FirstOrDefault(c => c.Room == roomName);
			if (exists != null)
			{

				Context.Rooms.Remove(exists);

				await Context.SaveChangesAsync();
			}
		}
	}
}
