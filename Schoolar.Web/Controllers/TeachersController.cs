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
        private readonly ITeacherRepository teacherRepository;

        public TeachersController(IUserHelper userHelper,
            ITeacherRepository teacherRepository)
        {
            this.userHelper = userHelper;
            this.teacherRepository = teacherRepository;
        }

        public IActionResult Index()
        {
            return View(this.teacherRepository.GetTeachersWithUser());
        }

        // GET: Teachers/Create
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
                    //TODO manejar la imagen
                    await teacherRepository.CreateAsync(teacher);
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Teacher teacher)
        {
            return View();
        }
        public async Task<IActionResult> Delete(int? id)
        {
            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            return View();
        }

    }
}
