using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GymManagment_server.Models;
using GymManagment_server.DTO;
namespace GymManagment_server.Controllers
{
    [Route("api/[controller]")]
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
        [HttpPost("register")]
        public IActionResult Register([FromBody] DTO.UserDTO userDTO)
        {
            try
            {
                HttpContext.Session.Clear();
                Models.User modelUser = new User()
                {
                    Username = userDTO.Username,
                    Password = userDTO.Password,
                    Age = userDTO.Age,
                    Gender = userDTO.Gender,
                };
                context.Users.Add(modelUser);
                context.SaveChanges();
                DTO.UserDTO dtoUser = new DTO.UserDTO(modelUser);
                return Ok(dtoUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    } 
}
