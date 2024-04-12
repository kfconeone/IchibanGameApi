using IChibanGameServer.Models;
using Microsoft.AspNetCore.Mvc;
using IChibanGameServer.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace IChibanGameServer.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class GameController(
        IchibanGameContext context,
        GameManager gameManager) : Controller
    {
        private readonly IchibanGameContext _context = context;
        private readonly GameManager _gameManager = gameManager;




        // 更新指定的Lots
        [HttpPut("LotBox/{boxId}/Lots")]
        public IActionResult UpdateLot(string boxId, [FromBody] UpdateLotModel model)
        {
            try {
                LotDrawResultModel? result = null;
                switch (model.Type)
                {
                    case GameType.Standard:
                        result = _gameManager.DrawStandard(boxId, model);
                        break;
                    case GameType.Arena:
                        result = _gameManager.DrawArena(boxId, model);
                        break;
                    case GameType.Collection:
                        result = _gameManager.DrawCollection(boxId, model);
                        break;
                }
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                // 处理特定异常
                return BadRequest(ex.Message);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // 处理并发异常
                return Conflict("衝突");
            }
            catch (Exception ex)
            {
                // 处理其他未知异常
                return StatusCode(500, $"未知錯誤: {ex.Message}");
            }

            
        }

    }
}
