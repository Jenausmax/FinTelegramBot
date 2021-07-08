using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FinBot.Domain.Interfaces;
using Telegram.Bot.Types;

namespace FinBot.WebApi.Controllers
{
    [Route("")]
    [ApiController]
    public class UpdateController : ControllerBase
    {
        private readonly ICommandBot _commandBot;

        public UpdateController(ICommandBot commandServiceBot)
        {
            _commandBot = commandServiceBot;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Update update)
        {
            await _commandBot.SetUpdateBot(update);
            await _commandBot.SetCommandBot(update.Type);
            return Ok();
        }
    }
}
