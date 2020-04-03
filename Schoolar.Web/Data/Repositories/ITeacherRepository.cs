namespace Schoolar.Web.Data.Repositories
{
    using Web.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface ITeacherRepository : IGenericRepository<Teacher>
    {
        IEnumerable<SelectListItem> GetComboTeachers();

        IQueryable GetTeachersWithUser();

        Task<Teacher> GetTeacherByIDWithUser(int id);
    }
}
