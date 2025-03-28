﻿using IdentityRegistration.Infrastructure.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IdentityRegistration.Application.Configuration.Behaviors;

public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;
    private readonly IdentityRegistrationDbContext _context;

    public TransactionBehavior(
        ILogger<TransactionBehavior<TRequest, TResponse>> logger,
        IdentityRegistrationDbContext context)
    {
        _logger = logger ?? throw new ArgumentException(nameof(ILogger));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        TResponse? response = default(TResponse);

        try
        {
            Microsoft.EntityFrameworkCore.Storage.IExecutionStrategy strategy = _context.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                _logger.LogInformation($"Begin transaction {typeof(TRequest).Name}");

                await _context.BeginTransactionAsync(cancellationToken);

                response = await next();

                await _context.CommitTransactionAsync(cancellationToken);

                _logger.LogInformation($"Committed transaction {typeof(TRequest).Name}");
            });

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogInformation($"Rollback transaction executed {typeof(TRequest).Name}; \n {ex.ToString()}");

            await _context.RollbackTransaction();
            throw;
        }
    }
}