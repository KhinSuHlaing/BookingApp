using BookingApp.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;


namespace BookingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly MyDbContext dbContext;
        public ScheduleController(MyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetSchedule")]
        public IActionResult GetSchedule(int Userid)
        {
            var user = dbContext.Users.FirstOrDefault(x => x.UserId == Userid);
            var schedule = dbContext.Schedules.FirstOrDefault(x => x.Country == user.Country && x.isAvailable == true);
            if (schedule != null)
            {
                return Ok(schedule);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPut]
        [Route("Booking")]
        public IActionResult Booking([FromBody] Schedule schedule)
        {
            var ObjUser = dbContext.Users.FirstOrDefault(x => x.UserId == schedule.UserId);
            var ObjSchedule = dbContext.Schedules.FirstOrDefault(x => x.Id == schedule.Id && x.Country == ObjUser.Country && x.isAvailable == true);
            if (ObjSchedule != null)
            {
                if (ObjSchedule.isInWaitList != true)
                {

                    dbContext.Schedules.Where(x => x.Id == schedule.Id).ExecuteUpdate(x => x.SetProperty(u => u.UserId, schedule.UserId));
                    dbContext.Schedules.Where(x => x.Id == schedule.Id).ExecuteUpdate(x => x.SetProperty(u => u.isAvailable, false));

                    return Ok("Booking Successfully");
                }
                else
                {
                    dbContext.Schedules.Add(new Schedule
                    {
                        Id = schedule.Id,
                        ClassName = schedule.ClassName,
                        UserId = schedule.UserId,
                        isInWaitList = true,
                        WaitingTime = DateTime.Now,
                        Country = schedule.Country,
                        WaitingUserId = schedule.UserId,

                    });

                    dbContext.SaveChanges();
                    return Ok("Booking is In Waiting List");
                }
            }
            else
            {
                dbContext.Schedules.Where(x => x.Id == schedule.Id).ExecuteUpdate(x => x.SetProperty(u => u.WaitingUserId, schedule.WaitingUserId));
                dbContext.Schedules.Where(x => x.Id == schedule.Id).ExecuteUpdate(x => x.SetProperty(u => u.isInWaitList, true));
                dbContext.Schedules.Where(x => x.Id == schedule.Id).ExecuteUpdate(x => x.SetProperty(u => u.WaitingTime, DateTime.Now));
                return Ok("Booking is In Waiting List");
               
                
            }
              
           
            
        }

        [HttpPut]
        [Route("BookingCancel")]
        public IActionResult BookingCancel([FromBody] Schedule schedule)
        {
            var ObjUser = dbContext.Users.FirstOrDefault(x => x.UserId == schedule.UserId);
            var ObjSchedule = dbContext.Schedules.FirstOrDefault(x => x.UserId == schedule.UserId && x.Id == schedule.Id);

            if(ObjSchedule.isInWaitList == true )
            {
                dbContext.Schedules.Where(x => x.UserId == schedule.UserId).ExecuteUpdate(x => x.SetProperty(u => u.isCancel, true));
            }
            DateTime? startTime = ObjSchedule.WaitingTime;
            DateTime? endTime = DateTime.Now; 

            TimeSpan? duration = endTime - startTime;

            if (duration < TimeSpan.FromHours(4))
            {
                dbContext.Schedules.Where(x => x.UserId == schedule.UserId).ExecuteUpdate(x => x.SetProperty(u => u.isRefunded, true));
                return Ok("Refund Successfully");
            }
            else
            {
                return Ok("Sorry Refund Failed!");
            }

        }

    }
}
