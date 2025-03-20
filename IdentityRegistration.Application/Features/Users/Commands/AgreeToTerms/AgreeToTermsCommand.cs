using MediatR;

namespace IdentityRegistration.Application.Features.Users.Commands.AgreeToTerms;

public sealed record AgreeToTermsCommand(Guid UserId,
                                         bool HasAgreedToTerms) : IRequest<Unit>;