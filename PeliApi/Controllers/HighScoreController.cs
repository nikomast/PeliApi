using Microsoft.AspNetCore.Mvc;
using PeliApi.Models;
using PeliApi.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;


namespace PeliApi.Controllers
{

    [ApiController]
    [Route("api/highscores")]
    public class HighScoreController : ControllerBase
    {
        private readonly PongDbContext _context;
        private readonly ILogger<HighScoreController> _logger;

        public HighScoreController(PongDbContext context, ILogger<HighScoreController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Endpoint to record a new score
        [HttpPost]
        public ActionResult<HighScore> RecordScore(HighScore newScore)
        {
            newScore.DateAdded = DateTime.UtcNow; // Set the current time
            _context.HighScores.Add(newScore);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = newScore.ID }, newScore);
        }

        // Endpoint to get a specific score by ID
        [HttpGet("{id}")]
        public ActionResult<HighScore> GetById(int id)
        {
            var score = _context.HighScores.FirstOrDefault(s => s.ID == id);
            if (score == null)
                return NotFound();

            return score;
        }

        [HttpGet("top/{count}")]
        public ActionResult<IEnumerable<HighScore>> GetTopScores(int count)
        {
            var scores = _context.HighScores.ToList();
            _logger.LogInformation($"Retrieved {scores.Count} scores.");
                foreach (var score in scores)
                {
                    _logger.LogInformation("Tuleeko se tänne?");
                    _logger.LogInformation($"ID: {score.ID}, PlayerName: {score.PlayerName}, Score: {score.Score}, DateAdded: {score.DateAdded}");
                }
            
            return scores;
        }


    }


}
