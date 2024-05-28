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

        // GET: api/WordQuestCourse/5/groups
        [HttpGet("{course_id}/groups")]
        public async Task<ActionResult<Group>> GetCourseGroups(int course_id)
        {
            var course = await _context.Courses
                .Include(c => c.Groups)
                .FirstOrDefaultAsync(c => c.CourseId == course_id);

            if (course == null) { return NotFound(); }

            return Ok(course.Groups);
        }
        
        // GET: api/WordQuestCourse/5/words
        [HttpGet("{course_id}/words")]
        public async Task<ActionResult<Word>> GetCourseWords(int course_id)
        {
            var course = await _context.Courses
                .Include(c => c.Words)
                .FirstOrDefaultAsync(c => c.CourseId == course_id);

            if (course == null) { return NotFound(); }

            return Ok(course.Words);
        }
        
        // GET: api/WordQuestCourse/5/words/2
        [HttpGet("{course_id}/groups/{word_id}")]
        public async Task<ActionResult<Word>> GetCourseWord(int course_id, int word_id)
        {
            var course = await _context.Courses
                .Include(c => c.Words)
                .FirstOrDefaultAsync(c => c.CourseId == course_id);

            if (course == null) { return NotFound(); }

            var word = course.Words
                .FirstOrDefault(w => w.WordId == word_id);
                
            if (word == null) { return NotFound(); }

            return Ok(word);
        }

        // PUT: api/WordQuestCourse/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{course_id}")]
        public async Task<IActionResult> PutCourse(int course_id, Course course)
        {
            if (course_id != course.CourseId)
            {
                return BadRequest();
            }

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
            var course = await _context.Courses
                .Include(c => c.Words)
                .FirstOrDefaultAsync(c => c.CourseId == course_id);
            if (course == null) { return NotFound(); }

            course.Words.Add(word);
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
            var course = await _context.Courses
                .Include(c => c.Words)
                .FirstOrDefaultAsync(c => c.CourseId == course_id);
            if (course == null) { return NotFound(); }

            var word = course.Words
                .FirstOrDefault(w => w.WordId == word_id);
            if (word == null) { return NotFound(); }

            course.Words.Remove(word);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int course_id)
        {
            return _context.Courses.Any(e => e.CourseId == course_id);
        }
    }
}
