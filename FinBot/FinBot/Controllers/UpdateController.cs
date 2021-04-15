using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FinBot.Domain.Interfaces;
using Telegram.Bot.Types;

namespace FinBot.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateController : ControllerBase
    {
        private readonly IResponderBot _responderService;

        private Update _update;

        public UpdateController(IResponderBot responderService)
        {
            _responderService = responderService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            _update = update;
            _responderService.SetUpdateBot(_update);


            if (update.Message == null)
            {
                if (update.CallbackQuery.Data == "Lucky")
                {
                    _responderService.ResponderAsync("Я понял!");
                }
            }

            if (update.Message.Text == "/start")
            {
                _responderService.ResponderAsync("Я БотЁ");
            }







            //if (update.Message == null || update.Message.Text == null)
            //{
            //    return NotFound();
            //}

            //if (update.CallbackQuery.Data != null)
            //{
            //    _responderService.ResponderAsync("aaff");
            //}

           
            ////update.Message.Text = update.Message.Text.Replace("@BotMy", "");

            //if (update.Message.Text == "/start" || update.CallbackQuery.Data == "Lucky")
            //{
            //    _responderService.ResponderAsync("Я БотЁ");
            //}


            return Ok();
        }
    }
}
