using IChibanGameServer.Models;
using IChibanGameServer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Web;


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

        [HttpGet("ECPay/{amount}")]
        public IActionResult GetCheckMacValue(string amount)
        {

            var orderId = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 20);

            var order = new Dictionary<string, string>
            {
                //綠界需要的參數
                { "MerchantTradeNo",  orderId},
                { "MerchantTradeDate",  DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") },
                { "TotalAmount",  amount},
                { "TradeDesc",  "享玩點數儲值"},
                { "ItemName",  $"{amount}點數"},
                { "ReturnURL",  $"http://localhost:53600/api/Ecpay/AddPayInfo"}, //後端
                { "OrderResultURL", $"http://localhost:4200/Home/PayInfo/{orderId}"}, //前端
                { "PaymentInfoURL",  $"http://localhost:53600/api/Ecpay/AddAccountInfo"}, //後端
                { "ClientRedirectURL",  $"http://localhost:4200/Home/AccountInfo/{orderId}"}, //前端
                { "MerchantID",  "3002599"},
                { "PaymentType",  "aio"},
                { "ChoosePayment",  "ALL"},
                { "EncryptType",  "1"},
            };

            //檢查碼
            order["CheckMacValue"] = GetCheckMacValue(order);


            return Ok(order);

        }



        private string GetCheckMacValue(Dictionary<string, string> order)
        {
            var param = order.Keys.OrderBy(x => x).Select(key => key + "=" + order[key]).ToList();
            
            var checkValue = string.Join("&", param);
            //測試用的 HashKey
            var hashKey = "spPjZn66i0OhqJsQ";
            //測試用的 HashIV
            var HashIV = "hT5OJckN45isQTTs";
            checkValue = $"HashKey={hashKey}" + "&" + checkValue + $"&HashIV={HashIV}";
            checkValue = HttpUtility.UrlEncode(checkValue).ToLower();
            checkValue = GetSHA256(checkValue);
            return checkValue.ToUpper();
        }
        private string GetSHA256(string value)
        {
            var result = new StringBuilder();
            var sha256 = SHA256.Create();
            var bts = Encoding.UTF8.GetBytes(value);
            var hash = sha256.ComputeHash(bts);
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }



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
