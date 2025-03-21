using IdentityRegistration.Application.Features.Users.Commands.CreateUser;
using IdentityRegistration.Application.Features.Users.Commands.Login;
using Microsoft.AspNetCore.Mvc;

namespace IdentityRegistration.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UserController : BaseAPIController
{
    [HttpPost(nameof(CreateUser))]
    public async Task CreateUser(CreateUserCommand request) => await Mediator.Send(request);

    [HttpPost(nameof(Login))]
    public async Task Login(LoginCommand request) => await Mediator.Send(request);
}