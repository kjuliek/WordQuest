using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WordQuestAPI.Models;

namespace WordQuestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordQuestGroupController : ControllerBase
    {
        private readonly WordQuestContext _context;

        public WordQuestGroupController(WordQuestContext context)
        {
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

        // GET: api/WordQuestGroup/5/members/
        [HttpGet("{group_id}/members/")]
        public async Task<ActionResult<User>> GetGroupMembers(int group_id)
        {
            var @group = await _context.Groups   // '@' allows using 'group' despite it being a reserved keyword in C#.
                .Include(g => g.Members)
                .FirstOrDefaultAsync(g => g.GroupId == group_id);  
            if (@group == null) { return NotFound(); }

            return Ok(@group.Members);
        }

        // GET: api/WordQuestGroup/5/members/2
        [HttpGet("{group_id}/members/{user_id}")]
        public async Task<ActionResult<User>> GetGroupMember(int group_id, int user_id)
        {
            var @group = await _context.Groups   // '@' allows using 'group' despite it being a reserved keyword in C#.
                .Include(g => g.Members)
                .FirstOrDefaultAsync(g => g.GroupId == group_id);  
            if (@group == null) { return NotFound(); }

            var member = @group.Members
                .FirstOrDefault(m => m.UserId == user_id);
            if (member == null) { return NotFound(); }

            return Ok(member);
        }

                // GET: api/WordQuestGroup/5/courses/
        [HttpGet("{group_id}/courses/")]
        public async Task<ActionResult<Course>> GetGroupCourses(int group_id)
        {
            var @group = await _context.Groups   // '@' allows using 'group' despite it being a reserved keyword in C#.
                .Include(g => g.Courses)
                .FirstOrDefaultAsync(g => g.GroupId == group_id);  
            if (@group == null) { return NotFound(); }

            return Ok(@group.Courses);
        }

        // GET: api/WordQuestGroup/5/courses/2
        [HttpGet("{group_id}/courses/{course_id}")]
        public async Task<ActionResult<Course>> GetGroupCourse(int group_id, int course_id)
        {
            var @group = await _context.Groups   // '@' allows using 'group' despite it being a reserved keyword in C#.
                .Include(g => g.Courses)
                .FirstOrDefaultAsync(g => g.GroupId == group_id);  
            if (@group == null) { return NotFound(); }

            var course = @group.Courses
                .FirstOrDefault(c => c.CourseId == course_id);
            if (course == null) { return NotFound(); }

            return Ok(course);
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

        // POST: api/WordQuestGroup/5/members
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{group_id}/members/")]
        public async Task<ActionResult<User>> PostGroupMember(int group_id, User member)
        {
            var @group = await _context.Groups
                .Include(g => g.Members)
                .FirstOrDefaultAsync(g => g.GroupId == group_id);
            if (@group == null) { return NotFound(); }

            @group.Members.Add(member);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGroupMember", new {group_id, user_id = member.UserId }, member);
        }

        // POST: api/WordQuestGroup/5/courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{group_id}/courses/")]
        public async Task<ActionResult<Course>> PostGroupCourse(int group_id, Course newCourse)
        {
            var @group = await _context.Groups
                .Include(g => g.Courses)
                .FirstOrDefaultAsync(g => g.GroupId == group_id);
            if (@group == null) { return NotFound(); }

            @group.Courses.Add(newCourse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGroupCourse", new {group_id, course_id = newCourse.CourseId }, newCourse);
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

        // DELETE: api/WordQuestGroup/5/members/2
        [HttpDelete("{group_id}/members/{user_id}")]
        public async Task<IActionResult> DeleteGroupMember(int group_id, int user_id)
        {
            var @group = await _context.Groups
                .Include(g => g.Members)
                .FirstOrDefaultAsync(g => g.GroupId == group_id);
            if (@group == null) { return NotFound(); }
            
            var member = @group.Members
                .FirstOrDefault(m => m.UserId == user_id);
            if (member == null) { return NotFound(); }

            @group.Members.Remove(member);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/WordQuestGroup/5/courses/2
        [HttpDelete("{group_id}/courses/{course_id}")]
        public async Task<IActionResult> DeleteGroupCourse(int group_id, int course_id)
        {
            var @group = await _context.Groups
                .Include(g => g.Courses)
                .FirstOrDefaultAsync(g => g.GroupId == group_id);
            if (@group == null) { return NotFound(); }
            
            var course = @group.Courses
                .FirstOrDefault(c => c.CourseId == course_id);
            if (course == null) { return NotFound(); }
            
            @group.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GroupExists(int group_id)
        {
            return _context.Groups.Any(e => e.GroupId == group_id);
        }
    }
}
