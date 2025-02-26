using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GymManagment_server.Models;
using GymManagment_server.DTO;
using Microsoft.EntityFrameworkCore;
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

                return Ok(dtoUser.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("GymRegister")]
        public IActionResult GymRegister([FromBody] DTO.GymDTO gymDTO)
        {
            try
            {
                HttpContext.Session.Clear();
                Models.Gym gymrgister = new Gym()
                {
                    Name = gymDTO.Name,
                    Level = gymDTO.Level,
                    Address = gymDTO.Address,
                    Price = gymDTO.Price,
                    PhoneNumber = gymDTO.PhoneNumber
                };
                context.Gyms.Add(gymrgister);
                context.SaveChanges();
                DTO.GymDTO dtoGym = new DTO.GymDTO(gymrgister);
                return Ok(dtoGym.GymId);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getAllGyms")]
        public async Task<ActionResult<List<Gym>>> GetAllGyms()
        {
            try
            {
                var gyms = await context.Gyms.ToListAsync();
                return Ok(gyms);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("registerTrainer")]
        public IActionResult RegisterTrainer([FromBody] DTO.TrainerDTO trainerDTO)
        {
            try
            {
                // Check if the referenced gym exists
                var gym = context.Gyms.FirstOrDefault(g => g.GymId == trainerDTO.GymId);
                if (gym == null)
                {
                    return BadRequest("The specified gym does not exist.");
                }

                // Create a new trainer entity
                Models.Trainer trainer = new Trainer()
                {
                    Name = trainerDTO.Name,
                    Description = trainerDTO.Description,
                    GymId = trainerDTO.GymId
                };

                // Add the trainer to the database
                context.Trainers.Add(trainer);
                context.SaveChanges();

                // Create a DTO to return
                DTO.TrainerDTO dtoTrainer = new DTO.TrainerDTO(trainer);

                // Return the newly created trainer's ID
                return Ok(dtoTrainer.TrainerId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getTrainersByGym/{gymId}")]
        public async Task<ActionResult<List<Trainer>>> GetTrainersByGym(int gymId)
        {
            try
            {
                var trainers = await context.Trainers
                    .Where(t => t.GymId == gymId)
                    .ToListAsync();

                return Ok(trainers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("getAllTrainers")]
        public async Task<ActionResult<List<Trainer>>> GetAllTrainers()
        {
            try
            {
                var trainers = await context.Trainers.ToListAsync();
                return Ok(trainers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
} 

