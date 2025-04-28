using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GymManagment_server.Models;
using GymManagment_server.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Hosting.Server;
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
        [HttpPost("addComment")]
        public IActionResult AddComment([FromBody] DTO.CommentDTO commentDTO)
        {
            try
            {
                // Validate that the user exists
                var user = context.Users.FirstOrDefault(u => u.Id == commentDTO.UserId);
                if (user == null)
                {
                    return BadRequest("User does not exist.");
                }

                // Create a new comment
                Models.Comment comment = commentDTO.GetModel();

                // Add to database
                context.Comments.Add(comment);
                context.SaveChanges();

                // Return the created comment id
                return Ok(comment.CommentId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getGymComments/{gymId}")]
        public async Task<ActionResult<List<CommentDTO>>> GetGymComments(int gymId)
        {
            try
            {
                var comments = await context.Comments
                    .Include(c => c.User)
                    .Where(c => c.GymId == gymId)
                    .OrderByDescending(c => c.Date)
                    .ToListAsync();

                List<CommentDTO> result = new List<CommentDTO>();
                foreach (var comment in comments) 
                {
                    result.Add(new CommentDTO(comment));
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("getGymAverageRating/{gymId}")]
        public async Task<ActionResult<double>> GetGymAverageRating(int gymId)
        {
            try
            {
                var comments = await context.Comments
                    .Where(c => c.GymId == gymId)
                    .ToListAsync();

                if (comments.Count == 0)
                    return Ok(0.0);

                double averageRating = comments.Average(c => c.Rank);
                return Ok(averageRating);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //Upload user photo
        [HttpPost("UploadProfileImage")]
        public async Task<IActionResult> UploadProfileImageAsync(IFormFile file)
        {
            //Check if who is logged in
            string? userEmail = HttpContext.Session.GetString("loggedInUser");
            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized("User is not logged in");
            }

            //Get model user class from DB with matching email. 
            Models.User? user = context.GetUser(userEmail);
            //Clear the tracking of all objects to avoid double tracking
            context.ChangeTracker.Clear();

            if (user == null)
            {
                return Unauthorized("User is not found in the database");
            }


            //Read all files sent
            long imagesSize = 0;

            if (file.Length > 0)
            {
                //Check the file extention!
                string[] allowedExtentions = { ".jpg" };
                string extention = "";
                if (file.FileName.LastIndexOf(".") > 0)
                {
                    extention = file.FileName.Substring(file.FileName.LastIndexOf(".")).ToLower();
                }
                if (!allowedExtentions.Where(e => e == extention).Any())
                {
                    //Extention is not supported
                    return BadRequest("File sent with non supported extention");
                }

                //Build path in the web root (better to a specific folder under the web root
                string filePath = $"{this.webHostEnvironment.WebRootPath}\\profileImages\\{user.Id}{extention}";

                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);

                    if (IsImage(stream))
                    {
                        imagesSize += stream.Length;
                    }
                    else
                    {
                        //Delete the file if it is not supported!
                        System.IO.File.Delete(filePath);
                    }

                }

            }

            return Ok();
        }

        //this function get profile image from the server
        [HttpPost("GetImage")]
        public ActionResult GetImage([FromQuery] string userId)
        {
            string filePath = $"{this.webHostEnvironment.WebRootPath}\\profileImages\\{userId}.jpg";
            bool fileExist = System.IO.File.Exists(filePath);
            if (fileExist)
            {
                return Redirect($"~/ProfileImages/{userId}.jpg");
            }
            else
                return Redirect($"~/ProfileImages/default.jpg");
        }

        //this function gets a file stream and check if it is an image
        private static bool IsImage(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);

            List<string> jpg = new List<string> { "FF", "D8" };
            List<string> bmp = new List<string> { "42", "4D" };
            List<string> gif = new List<string> { "47", "49", "46" };
            List<string> png = new List<string> { "89", "50", "4E", "47", "0D", "0A", "1A", "0A" };
            List<List<string>> imgTypes = new List<List<string>> { jpg, bmp, gif, png };

            List<string> bytesIterated = new List<string>();

            for (int i = 0; i < 8; i++)
            {
                string bit = stream.ReadByte().ToString("X2");
                bytesIterated.Add(bit);

                bool isImage = imgTypes.Any(img => !img.Except(bytesIterated).Any());
                if (isImage)
                {
                    return true;
                }
            }

            return false;
        }
        //Helper functions
        #region Backup / Restore
        [HttpGet("Backup")]
        public async Task<IActionResult> Backup()
        {
            string path = $"{this.webHostEnvironment.WebRootPath}\\..\\DBScripts\\backup.bak";

            bool success = await BackupDatabaseAsync(path);
            if (success)
            {
                return Ok("Backup was successful");
            }
            else
            {
                return BadRequest("Backup failed");
            }
        }

        [HttpGet("Restore")]
        public async Task<IActionResult> Restore()
        {
            string path = $"{this.webHostEnvironment.WebRootPath}\\..\\DBScripts\\backup.bak";

            bool success = await RestoreDatabaseAsync(path);
            if (success)
            {
                return Ok("Restore was successful");
            }
            else
            {
                return BadRequest("Restore failed");
            }
        }
        //this function backup the database to a specified path
        private async Task<bool> BackupDatabaseAsync(string path)
        {
            try
            {

                //Get the connection string
                string? connectionString = context.Database.GetConnectionString();
                //Get the database name
                string databaseName = context.Database.GetDbConnection().Database;
                //Build the backup command
                string command = $"BACKUP DATABASE {databaseName} TO DISK = '{path}'";
                //Create a connection to the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //Open the connection
                    await connection.OpenAsync();
                    //Create a command
                    using (SqlCommand sqlCommand = new SqlCommand(command, connection))
                    {
                        //Execute the command
                        await sqlCommand.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        //THis function restore the database from a backup in a certain path
        private async Task<bool> RestoreDatabaseAsync(string path)
        {
            try
            {
                //Get the connection string
                string? connectionString = context.Database.GetConnectionString();
                //Get the database name
                string databaseName = context.Database.GetDbConnection().Database;
                //Build the restore command
                string command = $@"
                USE master;
                ALTER DATABASE {databaseName} SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                RESTORE DATABASE {databaseName} FROM DISK = '{path}' WITH REPLACE;
                ALTER DATABASE {databaseName} SET MULTI_USER;";

                //Create a connection to the database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //Open the connection
                    await connection.OpenAsync();
                    //Create a command
                    using (SqlCommand sqlCommand = new SqlCommand(command, connection))
                    {
                        //Execute the command
                        await sqlCommand.ExecuteNonQueryAsync();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion
    }
} 

