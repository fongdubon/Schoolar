namespace Schoolar.Web.Data.Repositories
{
    using Web.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    public interface ITeacherRepository : IGenericRepository<Teacher>
    {
        IEnumerable<SelectListItem> GetComboTeachers();
    }
}
