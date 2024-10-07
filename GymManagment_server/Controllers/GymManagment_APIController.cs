using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GymManagment_server.Models;
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
    }
}
