using BookingApp.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly MyDbContext dbContext;
        public PackageController(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetPackage")]
        public IActionResult GetUser(int Userid)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.UserId == Userid);
            var Package = dbContext.Packages.FirstOrDefault(x => x.Country == user.Country && x.Enable == true);
            if (user != null)
            {
                return Ok(Package);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet]
        [Route("GetPurchasePackage")]
        public IActionResult GetPurchasePackage(int Userid)
        {

            var ObjUser = dbContext.UserPackages.FirstOrDefault(x => x.Id == Userid);
            if (ObjUser != null)
            {
                return Ok(ObjUser);
            }
            else
            {
                return NoContent();
            }
        }
    }
}
