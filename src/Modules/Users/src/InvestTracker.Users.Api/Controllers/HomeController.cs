using InvestTracker.Users.Api.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace InvestTracker.Users.Api.Controllers;

[Route(UsersModule.BasePath)]
internal class HomeController : ApiControllerBase
{
    [HttpGet]
    public ActionResult<string> Get() => "InvestTracker.Users API";
}