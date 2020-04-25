namespace Schoolar.Web.Data.Repositories
{
    using Web.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public interface ICoordinatorRepository : IGenericRepository<Coordinator>
    {
        IEnumerable<SelectListItem> GetComboCoordinators();

        IQueryable GetCoordinatorWithUser();

        //Pendiente Common
        //IEnumerable<Common.Models.Teacher> GetCoordinatorsCommonToList();

        Task<Coordinator> GetCoordinatorByIDWithUser(int id);
    }
}
