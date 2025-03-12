namespace OtoServisYonetim.Application.Common.Exceptions
{
    /// <summary>
    /// Geçersiz istek durumunda fırlatılan istisna
    /// </summary>
    public class BadRequestException : Exception
    {
        /// <summary>
        /// BadRequestException constructor
        /// </summary>
        public BadRequestException()
            : base("Geçersiz istek.")
        {
        }

        /// <summary>
        /// BadRequestException constructor
        /// </summary>
        /// <param name="message">Hata mesajı</param>
        public BadRequestException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// BadRequestException constructor
        /// </summary>
        /// <param name="message">Hata mesajı</param>
        /// <param name="innerException">İç istisna</param>
        public BadRequestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// BadRequestException constructor
        /// </summary>
        /// <param name="errors">Hata listesi</param>
        public BadRequestException(IDictionary<string, string[]> errors)
            : base("Birden fazla doğrulama hatası oluştu.")
        {
            Errors = errors;
        }

        /// <summary>
        /// Hata listesi
        /// </summary>
        public IDictionary<string, string[]>? Errors { get; }
    }
}