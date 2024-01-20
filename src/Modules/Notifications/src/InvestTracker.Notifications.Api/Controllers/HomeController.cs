using InvestTracker.Notifications.Api.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace InvestTracker.Notifications.Api.Controllers;

[Route(NotificationsModule.BasePath)]
internal class HomeController : ApiControllerBase
{
    [HttpGet]
    public ActionResult<string> Get() => "InvestTracker.Notifications API";
}