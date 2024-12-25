using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GymManagment_server.Models;
using GymManagment_server.DTO;
namespace GymManagment_server.Controllers
{
    [Route("api")]
    [ApiController]
    public class GymManagment_APIController : ControllerBase
    {
        //a variable to hold a reference to the db context!
        private BenDBContext context;
        //a variable that hold a reference to web hosting interface (that provide information like the folder on which the server runs etc...)
        private IWebHostEnvironment webHostEnvironment;
        //Use dependency injection to get the db context and web host into the constructor
        public GymManagment_APIController(BenDBContext context, IWebHostEnvironment env)
        {
            this.context = context;
            this.webHostEnvironment = env;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] DTO.Logininfo loginDto)
        {
            try
            {
                HttpContext.Session.Clear(); //Logout any previous login attempt

                //Get model user class from DB with matching email. 
                Models.User? modelsUser = context.GetUser(loginDto.Email);

                //Check if user exist for this email and if password match, if not return Access Denied (Error 403) 
                if (modelsUser == null || modelsUser.Password != loginDto.Password)
                {
                    return Unauthorized();
                }

                //Login suceed! now mark login in session memory!
                HttpContext.Session.SetString("loggedInUser", modelsUser.Email);

                DTO.UserDTO dtoUser = new DTO.UserDTO(modelsUser);

                return Ok(dtoUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("register")]
        public IActionResult Register([FromBody] DTO.UserDTO userDto)
        {
            try
            {
                HttpContext.Session.Clear(); //Logout any previous login attempt

                //Get model user class from DB with matching email. 
                Models.User modelsUser = new User()
                {
                    Email = userDto.Email,
                    Username = userDto.Username,
                    Password = userDto.Password,
                    BirthDate=userDto.BirthDate,
                    Address=userDto.Address,
                    Difficulty=userDto.Difficulty,
                    GenderId=userDto.GenderId,
                    IsManager = userDto.IsManager
                };

                context.Users.Add(modelsUser);
                context.SaveChanges();

                //User was added!
                DTO.UserDTO dtoUser = new DTO.UserDTO(modelsUser);

                return Ok(dtoUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
    } 

