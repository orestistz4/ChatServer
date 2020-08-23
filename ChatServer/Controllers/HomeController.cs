using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatServer.Hubs;
using ChatServer.Models;
using ChatServer.Models.Rooms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace ChatServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {

        private IHubContext<MessageHub> _hubContext;
        private IRoom _roomRepository;

        public HomeController(IHubContext<MessageHub> hubContext,IRoom roomRepository)
        {
            _hubContext = hubContext;
            _roomRepository = roomRepository;
        }

       

        [Route("getsth")]
        public ActionResult Get()
        {
            //messageHub = new MessageHub();
            _hubContext.Clients.All.SendAsync("ReceiveMessage","sup");



            return Ok("hey");
        }


        [Route("post_data")]
        [HttpPost]
        public ActionResult PostData([FromBody]Customer customer)
        {

            var name = customer.Name;
            var id = customer.Id;
            var surname = customer.Surname;
            return Ok($"hello there {name} {surname}");


        }



        [Route("post_dataa")]
        [HttpPost]
        public ActionResult PostDataa([FromBody]List<ForexSymbol> obj)
        {

            Console.WriteLine("");
            _hubContext.Clients.All.SendAsync("ReceiveObject",obj);
            return Ok();


        }




        [Route("sendmessage")]
        [HttpPost]
        public ActionResult SendMessage([FromBody]MessageModel obj)
        {

            Console.WriteLine("");
            try
            {



                

                _hubContext.Clients.Group(obj.Group).SendAsync(obj.Group,obj);
               
                return Ok();
            }catch(Exception ex)
            {
                return NotFound();
            }

            //_hubContext.Clients.All.SendAsync("ReceiveObject", obj);
            


        }

        [Route("createroom")]
        [HttpPost]
        public async Task<ActionResult> CreateRoom([FromBody]string roomName)
        {
            try
            {
                await _roomRepository.AddRoom(roomName);
                return Ok();
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }

        [Route("deleteroom")]
        [HttpPost]
        public async Task<ActionResult> DeleteRoom(string roomName)
        {
            try
            {
                await _roomRepository.DeleteRoom(roomName);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }





    }
}