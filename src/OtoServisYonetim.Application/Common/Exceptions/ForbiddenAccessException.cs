namespace OtoServisYonetim.Application.Common.Exceptions
{
    /// <summary>
    /// Erişim reddedildiğinde fırlatılan istisna
    /// </summary>
    public class ForbiddenAccessException : Exception
    {
        /// <summary>
        /// ForbiddenAccessException constructor
        /// </summary>
        public ForbiddenAccessException()
            : base("Bu işlemi gerçekleştirmek için yetkiniz bulunmamaktadır.")
        {
        }

        /// <summary>
        /// ForbiddenAccessException constructor
        /// </summary>
        /// <param name="message">Hata mesajı</param>
        public ForbiddenAccessException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// ForbiddenAccessException constructor
        /// </summary>
        /// <param name="message">Hata mesajı</param>
        /// <param name="innerException">İç istisna</param>
        public ForbiddenAccessException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}