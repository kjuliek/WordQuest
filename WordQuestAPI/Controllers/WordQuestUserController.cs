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
    public class WordQuestUserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly WordQuestContext _context;

        public WordQuestUserController(UserManager<User> userManager, WordQuestContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: api/WordQuestUser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _userManager.Users
                .ToListAsync();
        }

        // GET: api/WordQuestUser/5
        [HttpGet("{user_id}")]
        public async Task<ActionResult<User>> GetUser(string user_id)
        {
            var user = await _userManager.FindByIdAsync(user_id);
            if (user == null) return NotFound();
            Console.WriteLine(user);
            return user;
        }

        // GET: api/WordQuestUser/get_by_name/georgette
        [HttpGet("/api/WordQuestUser/get_by_name/{user_name}")]
        public async Task<ActionResult<User>> GetUserByName(string user_name)
        {
            var user = await _userManager.FindByNameAsync(user_name);
            if (user == null) return NotFound();
            Console.WriteLine(user);
            return user;
        }

        // GET: api/WordQuestUser/5/learnedwords
        [HttpGet("{user_id}/learnedwords")]
        public async Task<ActionResult<IEnumerable<Word>>> GetUserLearnedWords(string user_id)
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
        public async Task<ActionResult<IEnumerable<int>>> GetUserLearnedWord(string user_id, int word_id)
        {
            var learnedWord = await _context.LearnedWords
                .FirstOrDefaultAsync(lw => lw.UserId == user_id && lw.WordId == word_id);

            if (learnedWord == null) {  return Ok(null); }

            return Ok(learnedWord.LearningStage);
        }

        // GET: api/WordQuestUser/5/administeredgroups/
        [HttpGet("{user_id}/administeredgroups/")]
        public async Task<ActionResult<IEnumerable<Group>>> GetUserAdministeredGroups(string user_id)
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
        public async Task<ActionResult<IEnumerable<Course>>> GetUserCreatedCourses(string user_id)
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
        public async Task<ActionResult<IEnumerable<Group>>> GetUserGroups(string user_id)
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
        public async Task<ActionResult<IEnumerable<Group>>> GetUserGroup(string user_id, int group_id)
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
        public async Task<IActionResult> PutUser(string user_id, User user)
        {
            if (user_id != user.Id) return BadRequest();

            var existingUser = await _userManager.FindByIdAsync(user_id);
            if (existingUser == null) return NotFound();

            existingUser.PhoneNumber = user.PhoneNumber;
            
            var result = await _userManager.UpdateAsync(existingUser);
            if (result.Succeeded)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        // PUT: api/WordQuestUser/5/learnedwords/2
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{user_id}/learnedwords/{word_id}")]
        public async Task<IActionResult> UpdateLearnedWord(string user_id, int word_id, [FromBody] int newLearningStage)
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
        public async Task<ActionResult<User>> PostUser(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(GetUser), new { userId = user.Id }, user);
            }

            return BadRequest(result.Errors);
        }

        // POST: api/WordQuestUser/5/learnedwords
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{user_id}/learnedwords")]
        public async Task<ActionResult<User>> PostLearnedWord(string user_id, [FromBody] int word_id)
        {  
            var user = await _userManager.FindByIdAsync(user_id);
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
        public async Task<IActionResult> DeleteUser(string user_id)
        {
            var user = await _userManager.FindByIdAsync(user_id);
            if (user == null)  return NotFound();

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return NoContent();
            }
            else
            {
                // Gérer les erreurs de suppression de l'utilisateur
                return StatusCode(500);
            }
        }

        // DELETE: api/WordQuestUser/5/learnedwords/2
        [HttpDelete("{user_id}/learnedwords/{word_id}")]
        public async Task<IActionResult> DeleteUserLearnedWord(string user_id, int word_id)
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
        public async Task<IActionResult> DeleteUserGroup(string user_id, int group_id)
        {
            var groupUser = await _context.GroupsUsers
                .Where(gu => gu.UserId == user_id && gu.GroupId == group_id)
                .FirstOrDefaultAsync();
            if (groupUser == null) { return NotFound(); }
            
            _context.GroupsUsers.Remove(groupUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
