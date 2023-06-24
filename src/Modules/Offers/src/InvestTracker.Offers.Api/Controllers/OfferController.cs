using InvestTracker.Offers.Core.Commands.CreateOffer;
using InvestTracker.Offers.Core.Commands.DeleteOffer;
using InvestTracker.Offers.Core.Commands.UpdateOffer;
using InvestTracker.Offers.Core.Queries.GetOfferDetails;
using InvestTracker.Offers.Core.Queries.GetOfferDetails.Dtos;
using InvestTracker.Offers.Core.Queries.GetOffers;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InvestTracker.Offers.Api.Controllers;

internal class OfferController : ApiControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public OfferController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }
    
    [HttpGet("{id:guid}")]
    [SwaggerOperation("Returns selected offer details")]
    public async Task<ActionResult<OfferDetailsDto>> GetOffer(Guid id, CancellationToken token)
        => OkOrNotFound(await _queryDispatcher.QueryAsync(new GetOfferDetails(id), token));
    
    [HttpGet]
    [SwaggerOperation("Returns all offers")]
    public async Task<ActionResult<IEnumerable<OfferDto>>> GetOffers(CancellationToken token)
        => OkOrNotFound(await _queryDispatcher.QueryAsync(new GetOffers(), token));

    [HttpPost]
    [SwaggerOperation("Advisor can add his own offer")]
    public async Task<ActionResult> CreateOffer([FromBody] CreateOffer command, CancellationToken token)
    {
        await _commandDispatcher.SendAsync(command with { Id = Guid.NewGuid() }, token);
        return Ok();
    }

    [HttpPut]
    [SwaggerOperation("Advisor can edit his own offer")]
    public async Task<ActionResult> UpdateOffer([FromBody] UpdateOffer command, CancellationToken token)
    {
        await _commandDispatcher.SendAsync(command, token);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    [SwaggerOperation("Advisor can delete his own offer")]
    public async Task<ActionResult> DeleteOffer(Guid id, CancellationToken token)
    {
        await _commandDispatcher.SendAsync(new DeleteOffer(id), token);
        return NoContent();
    }
}