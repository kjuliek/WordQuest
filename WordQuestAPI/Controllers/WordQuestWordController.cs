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
    public class WordQuestWordController : ControllerBase
    {
        private readonly WordQuestContext _context;

        public WordQuestWordController(WordQuestContext context)
        {
            _context = context;
        }

        // GET: api/WordQuestWord
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Word>>> GetWords()
        {
            return await _context.Words.ToListAsync();
        }

        // GET: api/WordQuestWord/5
        [HttpGet("{word_id}")]
        public async Task<ActionResult<Word>> GetWord(int word_id)
        {
            var word = await _context.Words.FindAsync(word_id);

            if (word == null)
            {
                return NotFound();
            }

            return word;
        }

        // GET: api/WordQuestWord/5/courses
        [HttpGet("{word_id}/courses")]
        public async Task<ActionResult<Course>> GetWordCourses(int word_id)
        {
            var coursesWords = await _context.CoursesWords
                .Where(cw => cw.WordId == word_id)
                .ToListAsync();

            var courses = new List<Course>();
            foreach (var courseWord in coursesWords) {
                var course = await _context.Courses
                    .FindAsync(courseWord.CourseId);
                if (course == null) { return NotFound(); }
                courses.Add(course);
                }
            //if (courses.Count == 0) { return NotFound(); }

            return Ok(courses);
        }

        // PUT: api/WordQuestWord/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{word_id}")]
        public async Task<IActionResult> PutWord(int word_id, Word word)
        {
            if (word_id != word.WordId)
            {
                return BadRequest();
            }

            _context.Entry(word).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WordExists(word_id))
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

        // POST: api/WordQuestWord
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Word>> PostWord(Word word)
        {
            _context.Words.Add(word);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWord), new { word_id = word.WordId }, word);
        }

        // DELETE: api/WordQuestWord/5
        [HttpDelete("{word_id}")]
        public async Task<IActionResult> DeleteWord(int word_id)
        {
            var word = await _context.Words.FindAsync(word_id);

            if (word == null) { return NotFound(); }

            _context.Words.Remove(word);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WordExists(int word_id)
        {
            return _context.Words.Any(e => e.WordId == word_id);
        }
    }
}
