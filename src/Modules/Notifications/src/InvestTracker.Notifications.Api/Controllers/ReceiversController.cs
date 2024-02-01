using InvestTracker.Notifications.Api.Controllers.Base;
using InvestTracker.Notifications.Api.Permissions;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Infrastructure.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace InvestTracker.Notifications.Api.Controllers;

internal class ReceiversController : ApiControllerBase
{
    private readonly IReceiverSynchronizer _receiverSynchronizer;

    public ReceiversController(IReceiverSynchronizer receiverSynchronizer)
    {
        _receiverSynchronizer = receiverSynchronizer;
    }

    [HttpPost("synchronize")]
    [HasPermission(NotificationsPermission.SynchronizeReceivers)]
    [SwaggerOperation("Synchronize receivers with users module")]
    public async Task<ActionResult> SynchronizeReceivers(CancellationToken token)
    {
        await _receiverSynchronizer.Synchronize(token);
        return Ok();
    }
}