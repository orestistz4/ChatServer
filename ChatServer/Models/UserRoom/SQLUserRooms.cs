using ChatServer.Models.UserAccount;
using Microsoft.AspNetCore.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Models.UserRoom { 

	public class SQLUserRooms : IUserRooms
{

	private AppDbContext context;


	public SQLUserRooms(AppDbContext Contex)
	{

		context = Contex;

	}


	public async Task AddRoom(string email, string room)
	{

			var exist = await context.UserRooms.Where(c => c.Email == email && c.Room == room).FirstOrDefaultAsync();

			if (exist == null)
			{

				var newRoom = new UserRooms() { Email = email, Room = room };
				try
				{
					await context.UserRooms.AddAsync(newRoom);
					await context.Rooms.AddAsync(new GroupRoom() { Room=newRoom.Room});
					

					await context.SaveChangesAsync();


				}
				catch (Exception ex)
				{

					throw new Exception("Failed to add room.");

				}



			}
			else {

				throw new Exception("Room already in the user's rooms.");
			}
	}

		public async Task<List<UserRooms>> GetUserRooms(string email)
		{

			try
			{

				var list = await context.UserRooms.Where(c => c.Email == email).ToListAsync();
				return list;
				

			}
			catch(Exception ex)
			{
				throw new Exception("Something happend please try again later...");
			}

		}
	}
}
