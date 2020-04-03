namespace Schoolar.Web.Data.Repositories
{
    using Web.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public class TeacherRepository : GenericRepository<Teacher>, ITeacherRepository
    {
        private readonly DataContext _dataContext;

        public TeacherRepository(DataContext dataContext) : base(dataContext)
        {
            this._dataContext = dataContext;
        }

        public IEnumerable<SelectListItem> GetComboTeachers()
        {
            var list = _dataContext.Teachers.Select(c => new SelectListItem
            {
                Text = c.User.FullName,
                Value = $"{c.Id}"
            }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "(Selecciona un maestro...)",
                Value = "0"
            });
            return list;
        }

        public IQueryable GetTeachersWithUser()
        {
            return this._dataContext.Teachers
                .Include(t => t.User)
                .OrderBy(t => t.User.FullName);
        }

        public async Task<Teacher> GetTeacherByIDWithUser(int id)
        {
            return await this._dataContext.Teachers
               .Include(t => t.User)
               .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
