using ChatServer.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ChatServer.Hubs
{
	public class MessageHub:Hub
	{


		public event EventHandler<string> fireEvent1;

		public  Task SendMessageToAll(string message)
		{

			Console.WriteLine(Context.ConnectionId);
			return  Clients.All.SendAsync("ReceiveMessage", message);

		}
		public async Task Join(string roomName)
		{
			//edw vazw ton user sto group
			await Groups.AddToGroupAsync(Context.ConnectionId,roomName);
			//await Clients.Group(roomName).SendAsync(roomName, $"{Context.ConnectionId} has joined the group {roomName}.");

		}

		public async Task SendToGroup(string roomName,MessageModel message)
		{
			await Clients.Group(roomName).SendAsync(roomName, message);
		}




	}
}
