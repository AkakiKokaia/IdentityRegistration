using IdentityRegistration.Application.Features.Otps.Commands.Resend;
using IdentityRegistration.Application.Features.Otps.Commands.Verify;
using Microsoft.AspNetCore.Mvc;

namespace IdentityRegistration.API.Controllers;


[Route("api/v1/[controller]")]
[ApiController]
public class OtpController : BaseAPIController
{
    [HttpPost(nameof(ResendOtpCommand))]
    public async Task ResendOtpCommand(ResendOtpCommand request) => await Mediator.Send(request);

    [HttpPost(nameof(VerifyOtp))]
    public async Task VerifyOtp(VerifyOtpCommand request) => await Mediator.Send(request);
}