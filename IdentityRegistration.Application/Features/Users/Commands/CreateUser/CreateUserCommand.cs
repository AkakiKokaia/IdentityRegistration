using MediatR;

namespace IdentityRegistration.Application.Features.Users.Commands.CreateUser;

public sealed record CreateUserCommand(string CustomerName, 
                                       string IcNumber,
                                       string MobileNumber, 
                                       string Email) : IRequest<Unit>;