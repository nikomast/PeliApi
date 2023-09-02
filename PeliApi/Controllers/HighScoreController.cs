using Microsoft.AspNetCore.Mvc;
using PeliApi.Models;
using PeliApi.Data;
using System.Linq;

namespace PeliApi.Controllers
{
    [ApiController]
    [Route("api/highscores")]
    public class HighScoreController : ControllerBase
    {
        private readonly PongDbContext _context;

        public HighScoreController(PongDbContext context)
        {
            _context = context;
        }

        // Endpoint to record a new score
        [HttpPost]
        public ActionResult<HighScore> RecordScore(HighScore newScore)
        {
            newScore.DateAdded = DateTime.UtcNow; // Set the current time
            _context.Scores.Add(newScore);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = newScore.ID }, newScore);
        }

        // Endpoint to get a specific score by ID
        [HttpGet("{id}")]
        public ActionResult<HighScore> GetById(int id)
        {
            var score = _context.Scores.FirstOrDefault(s => s.ID == id);
            if (score == null)
                return NotFound();

            return score;
        }

        // Endpoint to get top N scores
        [HttpGet("top/{count}")]
        public ActionResult<IEnumerable<HighScore>> GetTopScores(int count)
        {
            return _context.Scores.OrderByDescending(s => s.Score).Take(count).ToList();
        }
    }
}
