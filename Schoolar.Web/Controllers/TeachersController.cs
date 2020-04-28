namespace Schoolar.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Schoolar.Web.Data.Entities;
    using Schoolar.Web.Data.Repositories;
    using Schoolar.Web.Helpers;
    using Schoolar.Web.Models;
    using System;
    using System.Threading.Tasks;

    public class TeachersController : Controller
    {
        private readonly IUserHelper userHelper;
        private readonly IImageHelper imageHelper;
        private readonly ITeacherRepository teacherRepository;

        public TeachersController(IUserHelper userHelper,
            IImageHelper imageHelper,
            ITeacherRepository teacherRepository)
        {
            this.userHelper = userHelper;
            this.imageHelper = imageHelper;
            this.teacherRepository = teacherRepository;
        }

        public IActionResult Index()
        {
            return View(this.teacherRepository.GetTeachersWithUser());
        }

        public IActionResult Create()
        {
            var model = new TeacherViewModel
            {
                HireDate = DateTime.Now,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeacherViewModel model)
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
                    await this.userHelper.AddUserToRoleAsync(user, "Teacher");
                    var teacher = new Teacher
                    {
                        HireDate = model.HireDate,
                        User = user
                    };

                    if (model.ImageFile != null)
                    {
                        teacher.ImageUrl = await imageHelper.UploadImageAsync(
                            model.ImageFile, model.User.UserName, "Teacher");
                    }
                    await teacherRepository.CreateAsync(teacher);
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

            var teacher = await teacherRepository.GetTeacherByIDWithUser(id.Value);
            if (teacher == null)
            {
                return NotFound();
            }

            var model = new TeacherViewModel
            {
                HireDate = teacher.HireDate,
                Id = teacher.Id,
                ImageUrl = teacher.ImageUrl,
                User = teacher.User
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TeacherViewModel model)
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

                var teacher = await teacherRepository.GetTeacherByIDWithUser(model.Id);
                if (teacher == null)
                {
                    return NotFound(":(");
                }

                teacher.HireDate = model.HireDate;
                teacher.User = user;

                if (model.ImageFile != null)
                {
                    teacher.ImageUrl = await imageHelper.UploadImageAsync(
                        model.ImageFile, model.User.UserName, "Teacher");
                }
                await teacherRepository.UpdateAsync(teacher);
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

            var teacher = await teacherRepository.GetByIdAsync(id.Value);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacher = await teacherRepository.GetByIdAsync(id);
            try
            {
                await teacherRepository.DeleteAsync(teacher);
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
            return View(teacher);
        }

        public async Task<IActionResult> Details(int? Id)        {            if (Id == null)            {                return NotFound();            }            var teacher = await this.teacherRepository.GetTeacherByIDWithUser(Id.Value);            if (teacher == null)            {                return NotFound();            }            return View(teacher);        }


    }
}
