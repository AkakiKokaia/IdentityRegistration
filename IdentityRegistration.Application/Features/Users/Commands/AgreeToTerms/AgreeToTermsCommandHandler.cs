using IdentityRegistration.Domain.Interfaces;
using MediatR;

namespace IdentityRegistration.Application.Features.Users.Commands.AgreeToTerms;

public class AgreeToTermsCommandHandler : IRequestHandler<AgreeToTermsCommand, Unit>
{
    private readonly IUserRepository _userRepository;

    public AgreeToTermsCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(AgreeToTermsCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId) ?? throw new Exception("User not found");

        if (request.HasAgreedToTerms)
        {
            user.AgreeToTerms();
            await _userRepository.UpdateAsync(user);
        }
        else
        {
            throw new Exception("You haven't agreed to Terms & Conditions");
        }

        return Unit.Value;
    }
}