using PeliApi.Models;

namespace PeliApi.Services
{
    public class GameService
    {
        private const int GameWidth = 600;
        private const int GameHeight = 300;
        private const int PlayerSpeed = 15;

        private readonly ILogger<GameService> _logger;
        private readonly HighScoreService _highScoreService;

        public GameService(ILogger<GameService> logger, HighScoreService highScoreService)
        {
            _logger = logger;
            _highScoreService = highScoreService;
        }

        public static GameState CurrentState { get; set; } = new GameState();

        public void MovePlayer1(bool up, bool down)
        {
            //_logger.LogInformation("Player movement" + CurrentState.Player1.YPosition);
            if (up && CurrentState.Player1.YPosition > 0)
                CurrentState.Player1.YPosition -= PlayerSpeed;
            if (down && CurrentState.Player1.YPosition + 35 < GameHeight) // assuming paddle height is 50
                CurrentState.Player1.YPosition += PlayerSpeed;
            //_logger.LogInformation("Player movement" + CurrentState.Player1.YPosition);
        }

        public void UpdateBallPosition()
        {
            if (CurrentState.state.value == true)
            {
                _logger.LogInformation("true");
            }
            else _logger.LogInformation("false");

            if (CurrentState.state.value == true)
            {
                // Update ball position based on velocities
                CurrentState.Ball.XPosition += CurrentState.Ball.XVelocity;
                CurrentState.Ball.YPosition += CurrentState.Ball.YVelocity;
                //_logger.LogInformation("Update ball position" + CurrentState.Ball.YPosition);
                //_logger.LogInformation("Update ball position" + CurrentState.Ball.XPosition);

                // Ball collision with top and bottom
                if (CurrentState.Ball.YPosition <= 0 || CurrentState.Ball.YPosition + 40 >= GameHeight) // assuming ball height is 20
                {
                    CurrentState.Ball.YVelocity = -CurrentState.Ball.YVelocity;
                    //_logger.LogInformation("Update ball position" + CurrentState.Ball.YVelocity);
                }

                if (CurrentState.Ball.XPosition <= 40 &&
                    CurrentState.Ball.YPosition + 10 >= CurrentState.Player1.YPosition &&
                    CurrentState.Ball.YPosition <= CurrentState.Player1.YPosition + 40)
                {
                    CurrentState.Ball.XVelocity = -CurrentState.Ball.XVelocity;
                    // _logger.LogInformation("Update ball position" + CurrentState.Ball.XVelocity);
                }

                // Collision with Player 2's "wall"
                if (CurrentState.Ball.XPosition >= GameWidth - 20)
                {
                    CurrentState.Ball.XVelocity = -CurrentState.Ball.XVelocity;
                    CurrentState.Player1.Score += 1;
                    _logger.LogInformation("+1" + CurrentState.Player1.Score);
                }

                // Check if ball goes behind Player 1's paddle
                if (CurrentState.Ball.XPosition <= 0)
                {
                    EndGame();
                    _logger.LogInformation("Player1" + CurrentState.Player1.Score);
                    return; // Stops updating the ball's position after game ends
                }
            }
        }

        public void ResetGame()
        {
            // Reset to initial state
            CurrentState = new GameState();
            CurrentState.state.value = true;
        }

        public void EndGame()
        {
            // Other game ending logic...
            _logger.LogInformation("ENDGAME");
            CurrentState.state.value = false;
            //_highScoreService.RecordScore(playerName, score);
        }

        public class GameState
        {
            public Player Player1 { get; set; } = new Player();
            public Player Player2 { get; set; } = new Player();
            public Ball Ball { get; set; } = new Ball();
            public IsActive state { get; set; } = new IsActive();  
        }
    }
}
