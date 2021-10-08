using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NATS.POC.Chat.Application.Commands;
using NATS.POC.Chat.Application.Queries;
using NATS.POC.Chat.Domain.Models.MessageAggregate;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace NATS.POC.Chat.Client.Controllers
{
    [ApiController]
    [Route("api/messages")]
    public class MessageController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMessageQueries _messageQueries;
        private readonly ILogger<MessageController> _logger;

        public MessageController(IMediator mediator, IMessageQueries messageQueries, ILogger<MessageController> logger)
        {
            _mediator = mediator;
            _messageQueries = messageQueries;
            _logger = logger;
        }

        [Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Message>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public Task<List<Message>> GetAsync(CancellationToken cancellationToken = default)
        {
            return _messageQueries.GetMessagesFromCurrentUserAsync(cancellationToken);
        }

        [Route("")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PostAsync([FromBody] SendMessageCommand command, CancellationToken cancellationToken = default)
        {
            _logger.LogDebug(
                    "----- Sending command: {0}",
                    command.GetType().Name);

            bool commandResult = await _mediator.Send(command, cancellationToken).ConfigureAwait(false);

            if (!commandResult)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
