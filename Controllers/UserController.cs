using BookingApp.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MyDbContext dbContext;
        public UserController(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        [Route("Registration")]
        public IActionResult Registration(UserDTO userDTO)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ObjUser = dbContext.Users.FirstOrDefault(x => x.Email == userDTO.Email);
            if (ObjUser == null)
            {
                dbContext.Users.Add(new User
                {
                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName,
                    Email = userDTO.Email,
                    Password = userDTO.Password,
                    Country = userDTO.Country,
                   
                });

                dbContext.SaveChanges();
                return Ok("Registration Successfully");
            }
            else
            {
                return BadRequest("User Already Exit.");
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login (LoginDTO loginDTO)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.Email == loginDTO.Email && x.Password == loginDTO.Password);
            if (user != null)
            {
                return Ok(user);

            }
            else {

                return NoContent();
            }
        }

        [HttpGet]
        [Route("GetUsers")]
        public IActionResult GetUsers()
        {
            return Ok(dbContext.Users.ToList());
        }

        [HttpGet]       
        [Route("GetProfile")]
        public IActionResult GetProfile(int id)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.UserId == id);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPut]
        [Route("ChangePassword")]
        public IActionResult ChangePassword(int id,string OldPassword,string NewPassword)
        {
            var ObjUser = dbContext.Users.FirstOrDefault(x => x.UserId == id && x.Password == OldPassword);

            if (ObjUser != null)
            {
                dbContext.Users.Where(x => x.UserId == id).ExecuteUpdate(x => x.SetProperty(u => u.Password, NewPassword));
                return Ok("Password Changed.");
            }
            else
            {
                return Ok("Last Password is Wrong.");
            }

        }

        [HttpPut]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(int id, string OldPassword)
        {
            var ObjUser = dbContext.Users.FirstOrDefault(x => x.UserId == id && x.Password == OldPassword);

            if (ObjUser != null)
            {
                dbContext.Users.Where(x => x.UserId == id).ExecuteUpdate(x => x.SetProperty(u => u.Password, ""));
                return Ok("Reset Password.");
            }
            else
            {
                return Ok("Last Password is Wrong");
            }

        }
    }
}
