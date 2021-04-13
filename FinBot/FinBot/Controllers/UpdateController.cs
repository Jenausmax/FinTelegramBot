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
    [Route("api/Telegram")]
    [ApiController]
    public class UpdateController : ControllerBase
    {
        private readonly IUpdateService _updateService;

        private Update _update;

        public UpdateController(IUpdateService updateService)
        {
            _updateService = updateService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            if (update.Message == null || update.Message.Text == null)
            {
                return Ok();
            }

            _update = update;
            update.Message.Text = update.Message.Text.Replace("", "");

            return Ok();
        }

        private async void Responder(string newMessage)
        {
            _update.Message.Text = newMessage;
            await _updateService.EchoAsync(_update);
        }

    }
}
