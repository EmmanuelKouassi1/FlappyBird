using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using FlappyC_.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FlappyC_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoresController : ControllerBase
    {
        private readonly _Context _context;
        private UserManager<User> UserManager;

        public ScoresController(_Context context, UserManager<User> userManager)
        {
            _context = context;
            UserManager = userManager;
        }

        // GET: api/Scores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Score>>> GetScore()
        {
            if(_context.Score == null)
            {
                return NotFound();
            }
            User? user = await UserManager.FindByIdAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (user != null)
            {
                return user.Scores;
            }
            return StatusCode(StatusCodes.Status400BadRequest, new { Message = "Utilisateur non trouvé." });
   
        }

        // GET: api/Scores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Score>> GetScore(int id)
        {
            var score = await _context.Score.FindAsync(id);

            if (score == null)
            {
                return NotFound();
            }

            return score;
        }

        // PUT: api/Scores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPut("{id}")]
        public async Task<IActionResult> PutScore(int id, Score score)
        {

            User? user = await _context.Users.FindAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (id == score.Id) 
            {
                return BadRequest();
            }

            if (_context.Score == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Veuillez réessayer plus tard." }); // Problème avec la BD ?

           
            Score? oldScore = await _context.Score.FindAsync(id);

      
            if (oldScore == null)
            {
                return NotFound();
            }

            if (user == null || !user.Scores.Contains(oldScore))
            {
                return Unauthorized(new { Message = "Hey touche pas, c'est pas à toi !" });
            }

            _context.ChangeTracker.Clear();
            _context.Entry(score).State = EntityState.Modified;

            try
            {
                oldScor
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if ((await _context.Score.FindAsync(id)) == null)
                    return NotFound();
                else
                    throw;
            }

            return Ok(new { Message = "Commentaire modifié", Comment = score }); 

        }*/

        // POST: api/Scores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Score>> PostScore(Score score)
        {
            User user = await _context.Users.FindAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (user == null)
                return Unauthorized(); 

            if (_context.Score == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Veuillez réessayer plus tard." }); // Problème avec la BD ?

            score.User = user;
            _context.Score.Add(score); 
            await _context.SaveChangesAsync();

            return Ok(score);
        }

            // DELETE: api/Scores/5
            [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScore(int id)
        {
          
            User? user = await _context.Users.FindAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (_context.Score == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Veuillez réessayer plus tard." }); // Problème avec la BD ?

      
            var comment = await _context.Score.FindAsync(id);

           
            if (comment == null)
                return NotFound();

          
            if (user == null || !user.Scores.Contains(comment))
            {
                return Unauthorized(new { Message = "Hey touche pas, c'est pas à toi !" });
            }

            _context.Remove(comment);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Commentaire supprimé." });

        }

        private bool ScoreExists(int id)
        {
            return _context.Score.Any(e => e.Id == id);

        }

        

    }
}
