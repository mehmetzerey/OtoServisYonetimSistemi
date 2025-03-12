using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OtoServisYonetim.Application.Common.Interfaces;

namespace OtoServisYonetim.Application.Common.Behaviors;

/// <summary>
/// MediatR pipeline'ında transaction işlemlerini gerçekleştiren davranış
/// </summary>
/// <typeparam name="TRequest">İstek tipi</typeparam>
/// <typeparam name="TResponse">Yanıt tipi</typeparam>
public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;
    private readonly IApplicationDbContext _dbContext;

    public TransactionBehavior(
        ILogger<TransactionBehavior<TRequest, TResponse>> logger,
        IApplicationDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    /// <summary>
    /// İstek işlenmeden önce transaction başlatır ve işlem sonunda commit yapar
    /// </summary>
    /// <param name="request">İstek</param>
    /// <param name="next">Sonraki handler</param>
    /// <param name="cancellationToken">İptal token'ı</param>
    /// <returns>Yanıt</returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var isTransactional = request.GetType().GetInterfaces()
            .Any(i => i.Name == "ITransactionalRequest");

        if (!isTransactional)
        {
            return await next();
        }

        _logger.LogInformation("Transaction başlatılıyor: {RequestName}", requestName);

        var strategy = ((DbContext)_dbContext).Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            using var transaction = await ((DbContext)_dbContext).Database.BeginTransactionAsync(cancellationToken);

            try
            {
                var response = await next();

                await ((DbContext)_dbContext).SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                _logger.LogInformation("Transaction commit edildi: {RequestName}", requestName);

                return response;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);

                _logger.LogError(ex, "Transaction rollback edildi: {RequestName}", requestName);
                throw;
            }
        });
    }
}