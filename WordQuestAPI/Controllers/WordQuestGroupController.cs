using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg;
using WordQuestAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace WordQuestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordQuestGroupController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly WordQuestContext _context;

        public WordQuestGroupController(UserManager<User> userManager, WordQuestContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: api/WordQuestGroup
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroup()
        {
            return await _context.Groups.ToListAsync();
        }

        // GET: api/WordQuestGroup/5
        [HttpGet("{group_id}")]
        public async Task<ActionResult<Group>> GetGroup(int group_id)
        {
            var @group = await _context.Groups.FindAsync(group_id);      // '@' allows using 'group' despite it being a reserved keyword in C#.

            if (@group == null) { return NotFound(); }

            return @group;
        }

        // GET: api/WordQuestGroup/5/admin
        [HttpGet("{group_id}/admin")]
        public async Task<ActionResult<User>> GetGroupAdmin(int group_id)
        {
            var @group = await _context.Groups.FindAsync(group_id);      // '@' allows using 'group' despite it being a reserved keyword in C#.

            if (@group == null) { return NotFound(); }

            var admin = await _context.Users.FindAsync(@group.AdminId);
            if (admin == null) return NotFound();

            return admin;
        }

        // GET: api/WordQuestGroup/5/courses/
        [HttpGet("{group_id}/courses/")]
        public async Task<ActionResult<Course>> GetGroupCourses(int group_id)
        {
            var groupCourses = await _context.GroupsCourses
                .Where(gc => gc.GroupId == group_id)
                .ToListAsync();
            
            var courses = new List<Course>();
            foreach (var groupCourse in groupCourses) {
                var course = await _context.Courses
                    .FirstOrDefaultAsync(c => c.CourseId == groupCourse.CourseId);
                if (course == null) { return NotFound(); }
                courses.Add(course); 
            }

            return Ok(courses);
        }

        // GET: api/WordQuestGroup/5/courses/2
        [HttpGet("{group_id}/courses/{course_id}")]
        public async Task<ActionResult<Course>> GetGroupCourse(int group_id, int course_id)
        {
            var groupCourses = await _context.GroupsCourses
                .Where(gc => gc.GroupId == group_id && gc.CourseId == course_id)
                .FirstOrDefaultAsync();
            if (groupCourses == null) { return NotFound(); }

            var course = await _context.Courses
                .FirstOrDefaultAsync(c => c.CourseId == course_id);

            if (course == null) { return NotFound(); }

            return Ok(course);
        }

        // GET: api/WordQuestGroup/5/members/
        [HttpGet("{group_id}/members/")]
        public async Task<ActionResult<User>> GetGroupMembers(int group_id)
        {
            var groupUsers = await _context.GroupsUsers   // '@' allows using 'group' despite it being a reserved keyword in C#.
                .Where(gu => gu.GroupId == group_id)
                .ToListAsync();

            var members = new List<User>();
            foreach (var groupUser in groupUsers)
            {
                var member = await _userManager.FindByIdAsync(groupUser.UserId.ToString());
                if (member == null) { return NotFound(); }
                members.Add(member);
            }

            return Ok(members);
        }

        // GET: api/WordQuestGroup/5/members/2
        [HttpGet("{group_id}/members/{user_id}")]
        public async Task<ActionResult<User>> GetGroupMember(int group_id, string user_id)
        {
            var groupUser = await _context.GroupsUsers   // '@' allows using 'group' despite it being a reserved keyword in C#.
                .FirstOrDefaultAsync(g => g.GroupId == group_id && g.UserId == user_id);  
            if (groupUser == null) { return NotFound(); }

            var member = await _userManager.FindByIdAsync(user_id);
            if (member == null) { return NotFound(); }

            return Ok(member);
        }

        // PUT: api/WordQuestGroup/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{group_id}")]
        public async Task<IActionResult> PutGroup(int group_id, Group @group)
        {
            if (group_id != @group.GroupId)
            {
                return BadRequest();
            }

            _context.Entry(@group).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(group_id))
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

        // POST: api/WordQuestGroup
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Group>> PostGroup(Group @group)
        {
            _context.Groups.Add(@group);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGroup", new { group_id = @group.GroupId }, @group);
        }

        // POST: api/WordQuestGroup/5/courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{group_id}/courses/")]
        public async Task<ActionResult<Course>> PostGroupCourse(int group_id, Course newCourse)
        {
            var groupCourse = new GroupCourses { CourseId = newCourse.CourseId , GroupId = group_id } ;

            _context.GroupsCourses.Add(groupCourse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGroupCourse", new {group_id, course_id = newCourse.CourseId }, newCourse);
        }

        // POST: api/WordQuestGroup/5/members
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{group_id}/members/")]
        public async Task<ActionResult<User>> PostGroupMember(int group_id, User member)
        {
            var groupUser = new GroupUsers { UserId = member.Id , GroupId = group_id } ;
            _context.GroupsUsers.Add(groupUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGroupMember", new {group_id, user_id = member.Id }, member);
        }

        // DELETE: api/WordQuestGroup/5
        [HttpDelete("{group_id}")]
        public async Task<IActionResult> DeleteGroup(int group_id)
        {
            var @group = await _context.Groups.FindAsync(group_id);
            if (@group == null)
            {
                return NotFound();
            }

            _context.Groups.Remove(@group);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/WordQuestGroup/5/courses/2
        [HttpDelete("{group_id}/courses/{course_id}")]
        public async Task<IActionResult> DeleteGroupCourse(int group_id, int course_id)
        {
            var groupCourse = await _context.GroupsCourses
                .Where(gc => gc.GroupId == group_id && gc.CourseId == course_id)
                .FirstOrDefaultAsync();
            if (groupCourse == null) { return NotFound(); }

            _context.GroupsCourses.Remove(groupCourse);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/WordQuestGroup/5/members/2
        [HttpDelete("{group_id}/members/{user_id}")]
        public async Task<IActionResult> DeleteGroupMember(int group_id, string user_id)
        {
            var groupUser = await _context.GroupsUsers
                .Where(gu => gu.UserId == user_id && gu.GroupId == group_id)
                .FirstOrDefaultAsync();
            if (groupUser == null) { return NotFound(); }

            _context.GroupsUsers.Remove(groupUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GroupExists(int group_id)
        {
            return _context.Groups.Any(e => e.GroupId == group_id);
        }
    }
}
