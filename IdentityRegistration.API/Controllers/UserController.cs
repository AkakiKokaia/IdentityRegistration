using IdentityRegistration.Application.Features.Users.Commands.ActivateBiometricLogin;
using IdentityRegistration.Application.Features.Users.Commands.AgreeToTerms;
using IdentityRegistration.Application.Features.Users.Commands.CreatePin;
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

    [HttpPost(nameof(CreatePin))]
    public async Task CreatePin(CreatePinCommand request) => await Mediator.Send(request);

    [HttpPost(nameof(AgreeToTerms))]
    public async Task AgreeToTerms(AgreeToTermsCommand request) => await Mediator.Send(request);

    [HttpPost(nameof(ActivateBiometricLogin))]
    public async Task ActivateBiometricLogin(ActivateBiometricLoginCommand request) => await Mediator.Send(request);
}