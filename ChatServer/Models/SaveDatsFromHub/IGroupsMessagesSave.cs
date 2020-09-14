using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Models.SaveDatsFromHub
{
	public interface IGroupsMessagesSave
	{

		Task SaveMessage(MessageModel messageModel,string room);


	}
}
