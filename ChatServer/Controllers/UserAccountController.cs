using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatServer.Models;
using ChatServer.Models.UserAccount;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ChatServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        public UserManager<IdentityUser> UserManager { get; }
        public SignInManager<IdentityUser> SignInManager { get; }
        public UserAccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        [Route("registeruser")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {

            try
            {
                var userByEmail = await UserManager.FindByEmailAsync(model.Email);
                if (userByEmail != default(IdentityUser))
                {
                    //shmainei oti yparxei o user ara dn xreiazetai na kanw kati 

                    return StatusCode(400,new ErrorModel { ErrorCode=400,ErrorMessage=JsonConvert.SerializeObject(new List<string> { "The email is already used."})});
                }


                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user != default(IdentityUser))
                {
                    //shmainei oti yparxei o user ara dn xreiazetai na kanw kati 

                    return StatusCode(400, new ErrorModel { ErrorCode = 400, ErrorMessage = JsonConvert.SerializeObject(new List<string> { "The username is already used." }) });
                }

                var userRegisterNew = new IdentityUser
                {
                    Email = model.Email,
                    UserName = model.Username,
                    EmailConfirmed=true
                   
                };

                var result = await UserManager.CreateAsync(userRegisterNew,model.Password);
                if (result.Succeeded)
                {
                    //o xrhsths apo8hkeuthke sthn vash mas
                    //meta sto mellon vale na tou stelnei email!!
                    return Ok("Registration is succesfully completed!");
                }
                else
                {
                    var errorList = new List<string>();
                    foreach(var error in result.Errors)
                    {
                        errorList.Add(error.Description);
                    }
                    return StatusCode(400, new ErrorModel { ErrorCode = 400, ErrorMessage = JsonConvert.SerializeObject(errorList) });
                }
            }
            catch(Exception ex)
            {
                return StatusCode(400, new ErrorModel { ErrorCode = 0, ErrorMessage = JsonConvert.SerializeObject(new List<string> { "Someting went wrong!" }) });
            }



        }


        [Route("loginuser")]
        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody]LoginUserModel user)
        {

            try
            {
                var userByEmail = await UserManager.FindByEmailAsync(user.Email);
                if (userByEmail == default(IdentityUser))
                {
                    return StatusCode(400, new ErrorModel {
                        ErrorCode=400,
                        ErrorMessage=JsonConvert.SerializeObject(new List<string> { "Invalid Email or Password"})
                    
                    });
                }

                var result = await SignInManager.PasswordSignInAsync(userByEmail, user.Password, user.RememberMe, false);
                if (result.Succeeded)
                {
                    var mobileUser = new MobileUserModel() {

                        Id = userByEmail.Id,
                        Email=userByEmail.Email,
                        UserName=userByEmail.UserName,
                        Password=user.Password

                    };
                    return Ok(mobileUser);
                }
                else
                {
                    if (result.IsNotAllowed)
                    {
                        return StatusCode(400, new ErrorModel { 
                        
                            ErrorCode=400,
                            ErrorMessage=JsonConvert.SerializeObject(new List<string> { "The email is not verified.Please go to your email provider and click.."})
                        
                        });
                    }
                    return StatusCode(400, new ErrorModel
                    {
                        ErrorCode = 400,
                        ErrorMessage = JsonConvert.SerializeObject(new List<string> { $"Invalid Email or Password" })
                    });

                }
            }
            catch (Exception ex)
            {
                
                return StatusCode(400, new ErrorModel
                {
                    ErrorCode = 0,
                    ErrorMessage = JsonConvert.SerializeObject(new List<string> { $"Something went wrong" })
                });
            }
        }


    
    }
}