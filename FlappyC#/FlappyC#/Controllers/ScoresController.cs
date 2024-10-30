using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlappyC_.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScore(int id, Score score)
        {
            if (id != score.Id)
            {
                return BadRequest();
            }

            _context.Entry(score).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScoreExists(id))
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

        // POST: api/Scores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Score>> PostScore(Score score)
        {
            if (_context.Score == null)
            {
                return Problem("Entity set 'semaine9_v2_serveurContext.Comment' is null.");
            }

            // Trouver l'utilisateur via son token
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User? user = await _context.Users.FindAsync(userId);

            if (user != null)
            {
                // On remplit les références de relation
                score.User = user;
                user.Scores.Add(score);

                // On ajoute l'objet dans la BD
                _context.Score.Add(score);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetComment", new { id = score.Id }, score);
            }

            return StatusCode(StatusCodes.Status400BadRequest,
                new { Message = "Utilisateur non trouvé." });
        }

            // DELETE: api/Scores/5
            [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScore(int id)
        {
            var score = await _context.Score.FindAsync(id);
            if (score == null)
            {
                return NotFound();
            }

            _context.Score.Remove(score);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ScoreExists(int id)
        {
            return _context.Score.Any(e => e.Id == id);

        }

        

    }
}
