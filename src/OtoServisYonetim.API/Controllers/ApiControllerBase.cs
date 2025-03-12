using MediatR;
using Microsoft.AspNetCore.Mvc;
using OtoServisYonetim.API.Filters;

namespace OtoServisYonetim.API.Controllers
{
    /// <summary>
    /// API controller temel sınıfı
    /// </summary>
    [ApiController]
    [ApiExceptionFilter]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        private ISender _mediator = null!;

        /// <summary>
        /// MediatR sender
        /// </summary>
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}