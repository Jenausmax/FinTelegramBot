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
            //_responderService.SetUpdateBot(_update);



            return Ok();
        }
    }
}
