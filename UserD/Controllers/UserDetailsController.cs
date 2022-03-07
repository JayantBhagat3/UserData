#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserD.Models;

namespace UserD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly UserDContext _context;

        public UserDetailsController(UserDContext context)
        {
            _context = context;
        }

        [Route("Login")]
        [HttpPost]
        public async Task<ActionResult<string>> Login(User login)
        {
            var log = _context.UserDetails.Where(x => x.Email.Equals(login.Email) && x.Password.Equals(login.Password)).FirstOrDefault();

            if (log == null)
            {
                return Ok(new { status = 401, isSuccess = false, message = "Invalid User", });
            }
            else

                return Ok(new { status = 200, isSuccess = true, message = "User Login successfully", UserDetails = log });
        }
        UserDetail EL = new UserDetail();

       // public static Master ad = new Master();

        [Route("Register")]
        [HttpPost]
        public object InsertEmployee(UserDetail Reg)
        {
            try
            {
                string[] rl = { "User", "SuperUser", "Admin" };
                //int ind = rnd.Next(rl.Length);

                if (EL.UserId == 0)
                {
                    EL.FirstName = Reg.FirstName;
                    EL.LastName = Reg.LastName;
                    EL.Email = Reg.Email;
                    EL.Password = Reg.Password;
                    EL.Username = Reg.Username;
                    //EL.RIdNavigation = Reg.RIdNavigation;
                    //ad.type = rl[ind];
                    //ad.CreatedOn = DateTime.Now.ToLocalTime();
                    //ad.Createdby = EL.Username;
                    //_context.Masters.Add(ad);

                    _context.UserDetails.Add(EL);
                    //audit(a);
                    _context.SaveChanges();
                    return Ok(new { Status = "Success", Message = "Record SuccessFully Saved." });
                }

            }
            catch (Exception)
            {
                throw;
            }
            return Ok(new { Status = "Error", Message = "Invalid Data." });
        }

        // GET: api/UserDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDetail>>> GetUserDetails()
        {
            return await _context.UserDetails.ToListAsync();
        }

        // GET: api/UserDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetail>> GetUserDetail(int id)
        {
            var userDetail = await _context.UserDetails.FindAsync(id);

            if (userDetail == null)
            {
                return NotFound();
            }

            return userDetail;
        }

        // PUT: api/UserDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserDetail(int id, UserDetail userDetail)
        {
            if (id != userDetail.UserId)
            {
                return BadRequest();
            }

            _context.Entry(userDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserDetail>> PostUserDetail(UserDetail userDetail)
        {
            _context.UserDetails.Add(userDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserDetail", new { id = userDetail.UserId }, userDetail);
        }

        // DELETE: api/UserDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserDetail(int id)
        {
            var userDetail = await _context.UserDetails.FindAsync(id);
            if (userDetail == null)
            {
                return NotFound();
            }

            _context.UserDetails.Remove(userDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserDetailExists(int id)
        {
            return _context.UserDetails.Any(e => e.UserId == id);
        }
    }
}
