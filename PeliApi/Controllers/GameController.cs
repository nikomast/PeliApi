using Microsoft.AspNetCore.Mvc;
using PeliApi.Models;
using PeliApi.Services;

namespace PeliApi.Controllers
{
    [ApiController]
    [Route("api/game")]
    public class GameController : ControllerBase
    {
        private readonly GameService _gameService;

        public GameController(GameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost("movePlayer1")]
        public IActionResult MovePlayer1([FromBody] MovementInput input)
        {
            _gameService.MovePlayer1(input.Up, input.Down);
            return Ok(GameService.CurrentState);


        }
        [HttpGet("state")]
        public IActionResult GetState()
        {
            return Ok(GameService.CurrentState);

        }

        [HttpPost("updateBall")]
        public IActionResult UpdateBallPosition()
        {
            _gameService.UpdateBallPosition();
            return Ok(GameService.CurrentState);
        }

        [HttpPost("reset")]
        public IActionResult ResetGame()
        {
            _gameService.ResetGame();
            return Ok("Game reset.");
        }
    }

 }


