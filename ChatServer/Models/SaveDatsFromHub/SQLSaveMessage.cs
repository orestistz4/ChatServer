using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Models.SaveDatsFromHub
{
	public class SQLSaveMessage : IGroupsMessagesSave
	{




		private AppDbContext context;
		public SQLSaveMessage(AppDbContext Context)
		{
			context = Context;
		}

		public async Task SaveMessage(MessageModel messageModel,string room)
		{

			var newMessage = new Groups() { GroupName = room, Username = "aasdfasdf", Date = messageModel.Date, Message = messageModel.Message };
			await context.Groups.AddAsync(new Groups() { GroupName = room, Username = "aasdfasdf", Date = messageModel.Date, Message = messageModel.Message });
			//await context.Groups.AddAsync(newMessage);
			await context.SaveChangesAsync();


		}
	}
}
