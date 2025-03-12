using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtoServisYonetim.API.Controllers;
using OtoServisYonetim.Application.Mechanics.Commands.CreateMechanic;
using OtoServisYonetim.Application.Mechanics.Commands.DeleteMechanic;
using OtoServisYonetim.Application.Mechanics.Commands.UpdateMechanic;
using OtoServisYonetim.Application.Mechanics.Queries.GetMechanicById;
using OtoServisYonetim.Application.Mechanics.Queries.GetMechanicsList;

namespace OtoServisYonetim.API.Controllers
{
    /// <summary>
    /// Teknisyen işlemleri için API controller
    /// </summary>
    [Authorize]
    public class MechanicsController : ApiControllerBase
    {
        /// <summary>
        /// Tüm teknisyenleri listeler
        /// </summary>
        /// <param name="query">Sorgu parametreleri</param>
        /// <returns>Teknisyen listesi</returns>
        [HttpGet]
        public async Task<ActionResult<MechanicsListVm>> GetAll([FromQuery] GetMechanicsListQuery query)
        {
            return await Mediator.Send(query);
        }

        /// <summary>
        /// Belirtilen ID'ye sahip teknisyeni getirir
        /// </summary>
        /// <param name="id">Teknisyen ID</param>
        /// <returns>Teknisyen detayları</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<MechanicVm>> Get(Guid id)
        {
            return await Mediator.Send(new GetMechanicByIdQuery { Id = id });
        }

        /// <summary>
        /// Yeni bir teknisyen oluşturur
        /// </summary>
        /// <param name="command">Teknisyen oluşturma komutu</param>
        /// <returns>Oluşturulan teknisyenin ID'si</returns>
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult<Guid>> Create(CreateMechanicCommand command)
        {
            return await Mediator.Send(command);
        }

        /// <summary>
        /// Mevcut bir teknisyeni günceller
        /// </summary>
        /// <param name="id">Teknisyen ID</param>
        /// <param name="command">Teknisyen güncelleme komutu</param>
        /// <returns>İşlem sonucu</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> Update(Guid id, UpdateMechanicCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Belirtilen ID'ye sahip teknisyeni siler
        /// </summary>
        /// <param name="id">Teknisyen ID</param>
        /// <returns>İşlem sonucu</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await Mediator.Send(new DeleteMechanicCommand { Id = id });

            return NoContent();
        }
    }
}