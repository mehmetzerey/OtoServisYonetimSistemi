using FluentValidation;
using MediatR;
using ValidationException = OtoServisYonetim.Application.Common.Exceptions.ValidationException;

namespace OtoServisYonetim.Application.Common.Behaviors;

/// <summary>
/// MediatR pipeline'ında validasyon işlemlerini gerçekleştiren davranış
/// </summary>
/// <typeparam name="TRequest">İstek tipi</typeparam>
/// <typeparam name="TResponse">Yanıt tipi</typeparam>
public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    /// <summary>
    /// İstek işlenmeden önce validasyon kurallarını uygular
    /// </summary>
    /// <param name="request">İstek</param>
    /// <param name="next">Sonraki handler</param>
    /// <param name="cancellationToken">İptal token'ı</param>
    /// <returns>Yanıt</returns>
    /// <exception cref="ValidationException">Validasyon hatası durumunda fırlatılır</exception>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
                throw new ValidationException(failures);
        }

        return await next();
    }
}