using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WordQuestAPI.Migrations;
using WordQuestAPI.Models;

namespace WordQuestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordQuestUserController : ControllerBase
    {
        private readonly WordQuestContext _context;

        public WordQuestUserController(WordQuestContext context)
        {
            _context = context;
        }

        // GET: api/WordQuestUser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users
            .ToListAsync();
        }

        // GET: api/WordQuestUser/5
        [HttpGet("{user_id}")]
        public async Task<ActionResult<User>> GetUser(int user_id)
        {
            
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == user_id);

            if (user == null) { return NotFound(); }

            return user;
        }

        // GET: api/WordQuestUser/5/learnedwords
        [HttpGet("{user_id}/learnedwords")]
        public async Task<ActionResult<IEnumerable<Word>>> GetUserLearnedWords(int user_id)
        {
            var learnedWords = await _context.LearnedWords
                .Where(lw => lw.UserId == user_id)
                .ToListAsync();
            var words = new List<Word>();
            foreach (var learnedWord in learnedWords){
                var word = await _context.Words.FindAsync(learnedWord.WordId);
                if (word == null) { return NotFound(); }
                words.Add(word);
            }

            return Ok(words);
        }

        // GET: api/WordQuestUser/5/learnedwords
        [HttpGet("{user_id}/learnedwords/{word_id}")]
        public async Task<ActionResult<IEnumerable<Word>>> GetUserLearnedWord(int user_id, int word_id)
        {
            var learnedWord = await _context.LearnedWords
                .FirstOrDefaultAsync(lw => lw.UserId == user_id && lw.WordId == word_id);

            if (learnedWord == null) {  return BadRequest(); }

            var word = await _context.Words.FindAsync(word_id);

            return Ok(word);
        }

        // GET: api/WordQuestUser/5/administeredgroups/
        [HttpGet("{user_id}/administeredgroups/")]
        public async Task<ActionResult<IEnumerable<Group>>> GetUserAdministeredGroups(int user_id)
        {
            var groups = await _context.Groups
                .ToListAsync();
            
            foreach (Group @group in groups) {
                if (@group.AdminId != user_id) {
                    groups.Remove(@group);
                }
            }
            //if (groups.Count == 0) { return NotFound(); }

            return Ok(groups);
        }

        // GET: api/WordQuestUser/5/createdcourses/
        [HttpGet("{user_id}/createdcourses/")]
        public async Task<ActionResult<IEnumerable<Course>>> GetUserCreatedCourses(int user_id)
        {
            var courses = await _context.Courses
                .ToListAsync();
            
            foreach (Course course in courses) {
                if (course.CreatorId != user_id) {
                    courses.Remove(course);
                }
            }
            //if (courses.Count == 0) { return NotFound(); }

            return Ok(courses);
        }

        // GET: api/WordQuestUser/5/groups/
        [HttpGet("{user_id}/groups/")]
        public async Task<ActionResult<IEnumerable<Group>>> GetUserGroups(int user_id)
        {
            var groupUsers = await _context.GroupsUsers
                .Where(gu => gu.UserId == user_id)
                .ToListAsync();
            
            var groups = new List<Group>();
            foreach (var groupUser in groupUsers) {
                var @group = await _context.Groups
                    .FirstOrDefaultAsync(g => g.GroupId == groupUser.GroupId);
                if (@group == null) { return NotFound(); }
                
                groups.Add(@group);
            }
            return Ok(groups);
        }

        // GET: api/WordQuestUser/5/groups/2
        [HttpGet("{user_id}/groups/{group_id}")]
        public async Task<ActionResult<IEnumerable<Group>>> GetUserGroup(int user_id, int group_id)
        {
            var groupUser = await _context.GroupsUsers
                .Where(gu => gu.UserId == user_id && gu.GroupId == group_id)
                .FirstOrDefaultAsync();
            
            if (groupUser == null) { return NotFound(); }

            var @group = await _context.Groups
                .FirstOrDefaultAsync(g => g.GroupId == group_id );
            if (@group == null) { return NotFound(); }

            return Ok(@group);
        }

        // PUT: api/WordQuestUser/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{user_id}")]
        public async Task<IActionResult> PutUser(int user_id, User user)
        {
            if (user_id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user_id))
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

        // PUT: api/WordQuestUser/5/learnedwords/2
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{user_id}/learnedwords/{word_id}")]
        public async Task<IActionResult> UpdateLearnedWord(int user_id, int word_id, [FromBody] int newLearningStage)
        {
            var learnedWord = await _context.LearnedWords
                .FirstOrDefaultAsync(lw => lw.UserId == user_id && lw.WordId == word_id);

            if (learnedWord == null) {  return NotFound(); }

            learnedWord.LearningStage = newLearningStage;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent(); // Mise à jour réussie
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500); // Erreur de mise à jour
            }

        }

        // POST: api/WordQuestUser
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { user_id = user.UserId }, user);
        }

        // POST: api/WordQuestUser/5/learnedwords
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{user_id}/learnedwords")]
        public async Task<ActionResult<User>> PostLearnedWord(int user_id, [FromBody] int word_id)
        {  
            var user = await _context.Users.FindAsync(user_id);
            if (user == null) { return NotFound(); }

            var word = await _context.Words.FindAsync(word_id);
            if (word == null) { return NotFound(); }

            var learnedWord = new LearnedWord { UserId = user_id, WordId = word_id, LearningStage = 0 };
            _context.LearnedWords.Add(learnedWord);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserLearnedWord), new { user_id, word_id }, learnedWord);
        }

        // DELETE: api/WordQuestUser/5
        [HttpDelete("{user_id}")]
        public async Task<IActionResult> DeleteUser(int user_id)
        {
            var user = await _context.Users.FindAsync(user_id);
            if (user == null)  { return NotFound(); }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/WordQuestUser/5/learnedwords/2
        [HttpDelete("{user_id}/learnedwords/{word_id}")]
        public async Task<IActionResult> DeleteUserLearnedWord(int user_id, int word_id)
        {
            var learnedWord = await _context.LearnedWords
                .FirstOrDefaultAsync(lw => lw.UserId == user_id && lw.WordId == word_id);

            if (learnedWord == null) { return NotFound(); }

            _context.LearnedWords.Remove(learnedWord);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/WordQuestUser/5/groups/2
        [HttpDelete("{user_id}/groups/{group_id}")]
        public async Task<IActionResult> DeleteUserGroup(int user_id, int group_id)
        {
            var groupUser = await _context.GroupsUsers
                .Where(gu => gu.UserId == user_id && gu.GroupId == group_id)
                .FirstOrDefaultAsync();
            if (groupUser == null) { return NotFound(); }
            
            _context.GroupsUsers.Remove(groupUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int user_id)
        {
            return _context.Users.Any(e => e.UserId == user_id);
        }
    }
}
