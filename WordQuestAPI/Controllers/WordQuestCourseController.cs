using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WordQuestAPI.Models;
using Microsoft.AspNetCore.Identity;
namespace WordQuestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordQuestCourseController : ControllerBase
    {
        private readonly WordQuestContext _context;

        public WordQuestCourseController(WordQuestContext context)
        {
            _context = context;
        }

        // GET: api/WordQuestCourse
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourse()
        {
            return await _context.Courses.ToListAsync();
        }

        // GET: api/WordQuestCourse/5
        [HttpGet("{course_id}")]
        public async Task<ActionResult<Course>> GetCourse(int course_id)
        {
            var course = await _context.Courses.FindAsync(course_id);

            if (course == null) { return NotFound(); }

            return course;
        }

        // GET: api/WordQuestCourse/5/creator
        [HttpGet("{course_id}/creator")]
        public async Task<ActionResult<IdentityUser>> GetCourseCreator(int course_id)
        {
            var course = await _context.Courses.FindAsync(course_id);

            if (course == null) return NotFound();

            var creator = await _context.Users.FindAsync(course.CreatorId);
            if (creator == null) return NotFound();

            return creator;
        }

        // GET: api/WordQuestCourse/5/groups
        [HttpGet("{course_id}/groups")]
        public async Task<ActionResult<Group>> GetCourseGroups(int course_id)
        {
            var groupsCourse = await _context.GroupsCourses
                .Where(gc => gc.CourseId == course_id)
                .ToListAsync();

            var groups = new List<Group>();
            foreach (var groupCourses in groupsCourse)
            {
                var @group = await _context.Groups.FindAsync(groupCourses.GroupId);
                if (@group == null) { return NotFound(); }

                groups.Add(@group);
            }

            return Ok(groups);
        }
        
        // GET: api/WordQuestCourse/5/words
        [HttpGet("{course_id}/words")]
        public async Task<ActionResult<Word>> GetCourseWords(int course_id)
        {
            var courseWords = await _context.CoursesWords
                .Where(cw => cw.CourseId == course_id)
                .ToListAsync();

            var words = new List<Word>();
            foreach (var courseword in courseWords) {
                var word = await _context.Words.FindAsync(courseword.WordId);
                if (word == null) { return NotFound(); }

                words.Add(word);
            }

            return Ok(words);
        }
        
        // GET: api/WordQuestCourse/5/words/2
        [HttpGet("{course_id}/words/{word_id}")]
        public async Task<ActionResult<Word>> GetCourseWord(int course_id, int word_id)
        {
            var courseWords = await _context.CoursesWords
                .Where(cw => cw.CourseId == course_id && cw.WordId == word_id)
                .FirstOrDefaultAsync();

            if (courseWords == null) { return NotFound(); }

            var word = await _context.Words
                .FirstOrDefaultAsync(w => w.WordId == word_id);
                
            if (word == null) { return NotFound(); }

            return Ok(word);
        }

        // PUT: api/WordQuestCourse/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{course_id}")]
        public async Task<IActionResult> PutCourse(int course_id, Course course)
        {
            if (course_id != course.CourseId) { return BadRequest(); }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(course_id))
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

        // POST: api/WordQuestCourse
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.CourseId }, course);
        }

        // POST: api/WordQuestCourse/5/words
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{course_id}/words")]
        public async Task<ActionResult<Word>> PostCourseWord(int course_id, Word word)
        {
            var courseWord = new CourseWords { CourseId = course_id, WordId = word.WordId };
            
            _context.CoursesWords.Add(courseWord);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourseWords", new { course_id, word_id = word.WordId }, word);
        }

        // DELETE: api/WordQuestCourse/5
        [HttpDelete("{course_id}")]
        public async Task<IActionResult> DeleteCourse(int course_id)
        {
            var course = await _context.Courses.FindAsync(course_id);
            if (course == null) { return NotFound(); }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/WordQuestCourse/5/words/2
        [HttpDelete("{course_id}/words/{word_id}")]
        public async Task<IActionResult> DeleteCourseWord(int course_id, int word_id)
        {
            var courseWord = await _context.CoursesWords
                .Where(cw => cw.CourseId == course_id && cw.WordId == word_id)
                .FirstOrDefaultAsync();

            if (courseWord == null) { return NotFound(); }

            _context.CoursesWords.Remove(courseWord);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int course_id)
        {
            return _context.Courses.Any(e => e.CourseId == course_id);
        }
    }
}
