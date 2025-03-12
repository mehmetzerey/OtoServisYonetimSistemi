using FluentValidation.Results;

namespace OtoServisYonetim.Application.Common.Exceptions;

/// <summary>
/// Validasyon hatalarını temsil eden özel istisna
/// </summary>
public class ValidationException : Exception
{
    /// <summary>
    /// Validasyon hataları
    /// </summary>
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException()
        : base("Bir veya daha fazla validasyon hatası oluştu.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }
}