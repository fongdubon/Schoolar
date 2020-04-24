namespace Schoolar.Web.Controllers.API
{
    using Microsoft.AspNetCore.Mvc;
    using Schoolar.Web.Data.Repositories;
    using Schoolar.Web.Helpers;
    using Schoolar.Web.Data.Entities;
    using System.Threading.Tasks;
    using Schoolar.Web.Models;
    using System.Collections.Generic;

    [Route("api/[Controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [HttpGet]
        public IActionResult GetTeachers()
        {
            return Ok(this.teacherRepository.GetTeachersCommonToList());
        }

        [HttpPost]
        public async Task<IActionResult> PostTeacher([FromBody]Common.Models.Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }
            var user = await this.userHelper.GetUserByEmailAsync(teacher.UserName);
            if (user == null)
            {
                user = new User
                {
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    PhoneNumber = teacher.PhoneNumber,
                    UserName = teacher.UserName,
                    Email = teacher.UserName,
                    Enrollment = teacher.Enrollment
                };
                var result = await this.userHelper.AddUserAsync(user, teacher.Password);
                if (!result.Succeeded)
                {
                    return this.BadRequest(ModelState);
                }
            }          
            else
            {
                return this.BadRequest(ModelState);
            }
            await this.userHelper.AddUserToRoleAsync(user, "Teacher");
            var teacherEntity = new Teacher
            {
                HireDate = teacher.HireDate,
                //TODO: Imagen
                User = user
            };
            var teacherNew = await teacherRepository.CreateAsync(teacherEntity);

            return Ok(teacherNew);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher([FromRoute] int id, [FromBody]Common.Models.Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            if (id != teacher.Id)
            {
                return this.BadRequest("Error Id");
            }

            var user = await this.userHelper.GetUserByEmailAsync(teacher.UserName);
            if (user == null)
            {
                return this.BadRequest("Usuario No Existe");
            }

            user.FirstName = teacher.FirstName;
            user.LastName = teacher.LastName;
            user.PhoneNumber = teacher.PhoneNumber;
            user.Enrollment = teacher.Enrollment;

            Teacher teacherOld = await this.teacherRepository.GetTeacherByIDWithUser(id);
            if (teacherOld == null)
            {
                return this.BadRequest("teacher don't exist");
            }

            teacherOld.HireDate = teacher.HireDate;
            // TODO: Imagen teacherOld.ImageUrl
            teacherOld.User = user;


            //TODO: actualizar imagen
            Teacher teacherUpdate = await this.teacherRepository.UpdateAsync(teacherOld);
            return Ok(teacherUpdate);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }
            Teacher teacher = await this.teacherRepository.GetTeacherByIDWithUser(id);
            if (teacher == null)
            {
                return this.BadRequest("teacher don't exist");
            }
            await this.teacherRepository.DeleteAsync(teacher);
            return Ok(teacher);
        }
    }
}