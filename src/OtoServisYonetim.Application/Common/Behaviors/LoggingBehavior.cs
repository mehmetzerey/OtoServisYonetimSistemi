using MediatR;
using Microsoft.Extensions.Logging;
using OtoServisYonetim.Application.Common.Interfaces;

namespace OtoServisYonetim.Application.Common.Behaviors;

/// <summary>
/// MediatR pipeline'ında loglama işlemlerini gerçekleştiren davranış
/// </summary>
/// <typeparam name="TRequest">İstek tipi</typeparam>
/// <typeparam name="TResponse">Yanıt tipi</typeparam>
public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
    private readonly ICurrentUserService _currentUserService;

    public LoggingBehavior(
        ILogger<LoggingBehavior<TRequest, TResponse>> logger,
        ICurrentUserService currentUserService)
    {
        _logger = logger;
        _currentUserService = currentUserService;
    }

    /// <summary>
    /// İstek işlenmeden önce ve sonra loglama yapar
    /// </summary>
    /// <param name="request">İstek</param>
    /// <param name="next">Sonraki handler</param>
    /// <param name="cancellationToken">İptal token'ı</param>
    /// <returns>Yanıt</returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _currentUserService.UserId ?? "Anonim";
        var userName = _currentUserService.UserName ?? "Anonim";

        _logger.LogInformation(
            "OtoServisYonetim İstek: {RequestName} {@UserId} {@UserName} {@Request}",
            requestName, userId, userName, request);

        try
        {
            var result = await next();

            _logger.LogInformation(
                "OtoServisYonetim Yanıt: {RequestName} {@UserId} {@UserName} {@Response}",
                requestName, userId, userName, result);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex, "OtoServisYonetim Hata: {RequestName} {@UserId} {@UserName} {@Request}",
                requestName, userId, userName, request);

            throw;
        }
    }
}