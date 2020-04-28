namespace Schoolar.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Schoolar.Web.Data.Entities;
    using Schoolar.Web.Data.Repositories;
    using Schoolar.Web.Helpers;
    using Schoolar.Web.Models;
    using System;
    using System.Threading.Tasks;
    public class CoordinatorsController : Controller
    {
        private readonly IUserHelper userHelper;
        private readonly IImageHelper imageHelper;
        private readonly ICoordinatorRepository coordinatorRepository;

        public CoordinatorsController(IUserHelper userHelper,
            IImageHelper imageHelper,ICoordinatorRepository coordinatorRepository)
        {
            this.userHelper = userHelper;
            this.imageHelper = imageHelper;
            this.coordinatorRepository = coordinatorRepository;
        }
        public IActionResult Index()
        {
            return View(this.coordinatorRepository.GetCoordinatorWithUser());
        }

        public IActionResult Create()
        {
            var model = new CoordinatorViewModel
            {
                HireDate = DateTime.Now,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CoordinatorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmailAsync(model.User.UserName);
                if (user == null)
                {
                    user = new User
                    {
                        FirstName = model.User.FirstName,
                        LastName = model.User.LastName,
                        PhoneNumber = model.User.PhoneNumber,
                        UserName = model.User.UserName,
                        Email = model.User.UserName,
                        Enrollment = model.User.Enrollment
                    };
                }
                var result = await this.userHelper.AddUserAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await this.userHelper.AddUserToRoleAsync(user, "Coordinator");
                    var coordinator = new Coordinator
                    {
                        HireDate = model.HireDate,
                        User = user
                    };

                    if (model.ImageFile != null)
                    {
                        coordinator.ImageUrl = await imageHelper.UploadImageAsync(
                            model.ImageFile, model.User.UserName, "Coordinator");
                    }
                    await coordinatorRepository.CreateAsync(coordinator);
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordinator = await coordinatorRepository.GetCoordinatorByIDWithUser(id.Value);
            if (coordinator == null)
            {
                return NotFound();
            }

            var model = new CoordinatorViewModel
            {
                HireDate = coordinator.HireDate,
                Id = coordinator.Id,
                ImageUrl = coordinator.ImageUrl,
                User = coordinator.User
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CoordinatorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await this.userHelper.GetUserByEmailAsync(model.User.UserName);
                if (user == null)
                {
                    return NotFound(":(");
                }
                user.FirstName = model.User.FirstName;
                user.LastName = model.User.LastName;
                user.PhoneNumber = model.User.PhoneNumber;
                user.Enrollment = model.User.Enrollment;

                var coordinator = await coordinatorRepository.GetCoordinatorByIDWithUser(model.Id);
                if (coordinator == null)
                {
                    return NotFound(":(");
                }

                coordinator.HireDate = model.HireDate;
                coordinator.User = user;

                if (model.ImageFile != null)
                {
                    coordinator.ImageUrl = await imageHelper.UploadImageAsync(
                        model.ImageFile, model.User.UserName, "Teacher");
                }
                await coordinatorRepository.UpdateAsync(coordinator);
                return RedirectToAction(nameof(Index));

            }
            return View(model);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coordinator = await coordinatorRepository.GetCoordinatorByIDWithUser(id.Value);
            if (coordinator == null)
            {
                return NotFound();
            }
            return View(coordinator);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coordinator = await coordinatorRepository.GetByIdAsync(id);
            try
            {
                await coordinatorRepository.DeleteAsync(coordinator);
                return RedirectToAction(nameof(Index));
            }

            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("conflicted"))
                {
                    ModelState.AddModelError(string.Empty, "El registro no puede ser eliminado porque esta relacionado con otro registro");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(coordinator);
        }

        public async Task<IActionResult> Details(int? Id)        {            if (Id == null)            {                return NotFound();            }            var coordinator = await this.coordinatorRepository.GetCoordinatorByIDWithUser(Id.Value);            if (coordinator == null)            {                return NotFound();            }            return View(coordinator);        }


    }
}