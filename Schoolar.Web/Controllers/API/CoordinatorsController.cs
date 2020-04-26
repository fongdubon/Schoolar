

namespace Schoolar.Web.Controllers.API
{
    using Microsoft.AspNetCore.Mvc;
    using Schoolar.Web.Data.Repositories;
    using Schoolar.Web.Helpers;
    using Schoolar.Web.Data.Entities;
    using System.Threading.Tasks;

    [Route("api/[Controller]")]
    public class CoordinatorsController : Controller
    {
        private readonly IUserHelper userHelper;
        private readonly ICoordinatorRepository coordinatorRepository;

        public CoordinatorsController(IUserHelper userHelper, ICoordinatorRepository coordinatorRepository)
        {
            this.userHelper = userHelper;
            this.coordinatorRepository = coordinatorRepository;
        }

        [HttpGet]
        public IActionResult GetCoordinators()
        {
            return Ok(this.coordinatorRepository.GetCoordinatorsCommonToList());
        }

        [HttpPost]
        public async Task<IActionResult> PostCoordinator([FromBody]Common.Models.Coordinator coordinator)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }
            var user = await this.userHelper.GetUserByEmailAsync(coordinator.UserName);
            if (user == null)
            {
                user = new User
                {
                    FirstName = coordinator.FirstName,
                    LastName = coordinator.LastName,
                    PhoneNumber = coordinator.PhoneNumber,
                    UserName = coordinator.UserName,
                    Email = coordinator.UserName,
                    Enrollment = coordinator.Enrollment
                };
                var result = await this.userHelper.AddUserAsync(user, coordinator.Password);
                if (!result.Succeeded)
                {
                    return this.BadRequest(ModelState);
                }
            }
            else
            {
                return this.BadRequest(ModelState);
            }
            await this.userHelper.AddUserToRoleAsync(user, "Coordinator");
            var coordinatorEntity = new Coordinator
            {
                HireDate = coordinator.HireDate,
                //TODO: Imagen
                User = user
            };
            var coordinatorNew = await coordinatorRepository.CreateAsync(coordinatorEntity);

            return Ok(coordinatorNew);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoordinator([FromRoute] int id, [FromBody]Common.Models.Coordinator coordinator)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            if (id != coordinator.Id)
            {
                return this.BadRequest("Error Id");
            }

            var user = await this.userHelper.GetUserByEmailAsync(coordinator.UserName);
            if (user == null)
            {
                return this.BadRequest("Usuario No Existe");
            }

            user.FirstName = coordinator.FirstName;
            user.LastName = coordinator.LastName;
            user.PhoneNumber = coordinator.PhoneNumber;
            user.Enrollment = coordinator.Enrollment;

            Coordinator coordinatorOld = await this.coordinatorRepository.GetCoordinatorByIDWithUser(id);

            if (coordinatorOld == null)
            {
                return this.BadRequest("coordinator don't exist");
            }

            coordinatorOld.HireDate = coordinator.HireDate;
            // TODO: Imagen teacherOld.ImageUrl
            coordinatorOld.User = user;


            //TODO: actualizar imagen
            Coordinator coordinatorUpdate = await this.coordinatorRepository.UpdateAsync(coordinatorOld);
            return Ok(coordinatorUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoordinator([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            Coordinator coordinator = await this.coordinatorRepository.GetCoordinatorByIDWithUser(id);
            if (coordinator == null)
            {
                return this.BadRequest("coordinator don't exist");
            }

            await this.coordinatorRepository.DeleteAsync(coordinator);
            return Ok(coordinator);
        }
    }
}
