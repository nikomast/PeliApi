using PeliApi.Models;
using PeliApi.Data;
namespace PeliApi.Services
{
    public class HighScoreService
    {
        private readonly PongDbContext _context;

        public HighScoreService(PongDbContext context)
        {
            _context = context;
        }

        public void RecordScore(string playerName, int score)
        {
            var highScore = new HighScore
            {
                PlayerName = playerName,
                Score = score,
                DateAdded = DateTime.UtcNow
            };

            _context.HighScores.Add(highScore);
            _context.SaveChanges();
        }

        public List<HighScore> GetHighScores()
        {
            return _context.HighScores
                .OrderByDescending(s => s.Score)
                .Take(10)
                .ToList();
        }

        // ... Other methods like removing extra scores, etc.
    }

}