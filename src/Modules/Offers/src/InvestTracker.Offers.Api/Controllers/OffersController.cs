using InvestTracker.Offers.Api.Controllers.Base;
using InvestTracker.Offers.Api.Permissions;
using InvestTracker.Offers.Core.Features.Offers.Commands.CreateOffer;
using InvestTracker.Offers.Core.Features.Offers.Commands.DeleteOffer;
using InvestTracker.Offers.Core.Features.Offers.Commands.UpdateOffer;
using InvestTracker.Offers.Core.Features.Offers.Queries.GetOfferDetails;
using InvestTracker.Offers.Core.Features.Offers.Queries.GetOfferDetails.Dtos;
using InvestTracker.Offers.Core.Features.Offers.Queries.GetOffers;
using InvestTracker.Shared.Abstractions.Commands;
using InvestTracker.Shared.Abstractions.Context;
using InvestTracker.Shared.Abstractions.Queries;
using InvestTracker.Shared.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InvestTracker.Offers.Api.Controllers;

internal class OffersController : ApiControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;
    private readonly IRequestContext _requestContext;

    public OffersController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher,
        IRequestContext requestContext)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
        _requestContext = requestContext;
    }
    
    [HttpGet("{id:guid}")]
    [Authorize]
    [SwaggerOperation("Returns selected offer details")]
    public async Task<ActionResult<OfferDetailsDto>> GetOffer(Guid id, CancellationToken token)
        => OkOrNotFound(await _queryDispatcher.QueryAsync(new GetOfferDetails(id), token));
    
    [HttpGet]
    [AllowAnonymous]
    [SwaggerOperation("Returns all offers")]
    public async Task<ActionResult<IEnumerable<OfferDto>>> GetOffers(CancellationToken token)
        => OkOrNotFound(await _queryDispatcher.QueryAsync(new GetOffers(), token));

    [HttpPost]
    [HasPermission(OffersPermission.CreateOffer)]
    [SwaggerOperation("Advisor can add his own offer")]
    public async Task<ActionResult> CreateOffer([FromBody] CreateOffer command, CancellationToken token)
    {
        await _commandDispatcher.SendAsync(command, token);
        return Ok();
    }

    [HttpPut]
    [HasPermission(OffersPermission.UpdateOffer)]
    [SwaggerOperation("Advisor can edit his own offer")]
    public async Task<ActionResult> UpdateOffer([FromBody] UpdateOffer command, CancellationToken token)
    {
        await _commandDispatcher.SendAsync(command, token);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    [HasPermission(OffersPermission.DeleteOffer)]
    [SwaggerOperation("Advisor can delete his own offer")]
    public async Task<ActionResult> DeleteOffer(Guid id, CancellationToken token)
    {
        await _commandDispatcher.SendAsync(new DeleteOffer(id), token);
        return NoContent();
    }
}