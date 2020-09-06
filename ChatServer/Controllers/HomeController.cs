using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatServer.Hubs;
using ChatServer.Models;
using ChatServer.Models.Rooms;
using ChatServer.Models.UserRoom;
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
        private IUserRooms _userRoomsRepository;

        public HomeController(IHubContext<MessageHub> hubContext,IRoom roomRepository,IUserRooms userRoomRepository)
        {
            _hubContext = hubContext;
            _roomRepository = roomRepository;
            _userRoomsRepository = userRoomRepository;
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
               var result=  await _roomRepository.AddRoom(roomName);
                if (result)
                {
                    //tote to room ftiaxthke!!!!
                    return Ok("Room");
                }
                else
                {
                    //to room yparxei hdh den mporei na ftia3ei allo
                    return NotFound("Room Already exxists");
                }
                
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
                return Ok("Room deleted");
            }
            catch (Exception ex)
            {
                return NotFound("Please Check your Internet conneciton...");
            }
        }


        [Route("joinroom")]
        [HttpPost]
        public async Task<ActionResult> JoinRoom([FromBody]string roomName)
        {
            try
            {
                var result = await _roomRepository.CheckRoom(roomName);
                if (result)
                {
                    //o xrhsths ekane join
                    return Ok(true);
                }
                else
                {
                    //den yparxei to room
                    return Ok(false);
                }
                //return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(null);
            }
        }


        [Route("createuserroom")]
        [HttpPost]
        public async Task<ActionResult> CreateUserRoom([FromBody]UserRooms room) {


            try
            {

             await _userRoomsRepository.AddRoom(room.Email,room.Room);
                return Ok(new ResponseModel() { Response="Room Created"});

            }
            catch (Exception ex) {


                throw new Exception(ex.Message);

            
            }

        
        }

        [Route("getuserrooms")]
        [HttpPost]
        public async Task<ActionResult> GetUserRooms([FromBody] string email) {



            try {

                var list1 = await _userRoomsRepository.GetUserRooms(email);
                return Ok(list1);
            
            }
            catch(Exception ex)
            {

                throw new Exception("sth happend...");

            }

        
        }



    }
}