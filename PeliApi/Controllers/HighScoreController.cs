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
        [HttpPost("record")]
        public ActionResult<HighScore> RecordScore([FromBody] HighScore score)
        {
            if (score == null)
            {
                _logger.LogInformation("No score data received.");
                return BadRequest("Score data is required.");
            }

            try
            {
                // Check the current count of scores
                int currentScoreCount = _context.HighScores.Count();

                // If there are 10 scores, we need to check if we should remove the lowest one
                if (currentScoreCount >= 3)
                {
                    // Get the lowest score
                    var lowestScore = _context.HighScores.OrderBy(s => s.Score).FirstOrDefault();

                    // If the new score is higher than the lowest score, remove the lowest score
                    if (score.Score > lowestScore.Score)
                    {
                        _context.HighScores.Remove(lowestScore);
                    }
                    else
                    {
                        // If the new score isn't higher than the lowest score among the top 10, 
                        // it doesn't qualify for addition
                        return BadRequest("Score not high enough for top 10.");
                    }
                }

                score.DateAdded = DateTime.UtcNow; // Setting the date when the score is added
                _context.HighScores.Add(score);
                _context.SaveChanges();

                _logger.LogInformation($"Score recorded. PlayerName: {score.PlayerName}, Score: {score.Score}, DateAdded: {score.DateAdded}");

                return Ok("Score added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error recording score: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
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
            var scores = _context.HighScores
                                 .OrderByDescending(s => s.Score)
                                 .Take(count)
                                 .ToList();

            _logger.LogInformation($"Retrieved {scores.Count} scores.");
            foreach (var score in scores)
            {
                _logger.LogInformation($"ID: {score.ID}, PlayerName: {score.PlayerName}, Score: {score.Score}, DateAdded: {score.DateAdded}");
            }

            return scores;
        }



    }


}
