using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (update.Message == null || update.Message.Text == null)
            {
                return Ok();
            }

            _update = update;
            _responderService.SetUpdateBot(_update);
            update.Message.Text = update.Message.Text.Replace("@BotMy", "");

            if (update.Message.Text == "Привет!")
            {

            }


            return Ok();
        }
    }
}
