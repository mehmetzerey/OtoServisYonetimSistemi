namespace OtoServisYonetim.Application.Common.Exceptions
{
    /// <summary>
    /// Kaynak bulunamadığında fırlatılan istisna
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// NotFoundException constructor
        /// </summary>
        public NotFoundException()
            : base()
        {
        }

        /// <summary>
        /// NotFoundException constructor
        /// </summary>
        /// <param name="message">Hata mesajı</param>
        public NotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// NotFoundException constructor
        /// </summary>
        /// <param name="message">Hata mesajı</param>
        /// <param name="innerException">İç istisna</param>
        public NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// NotFoundException constructor
        /// </summary>
        /// <param name="name">Varlık adı</param>
        /// <param name="key">Varlık anahtarı</param>
        public NotFoundException(string name, object key)
            : base($"{name} ({key}) bulunamadı.")
        {
        }
    }
}