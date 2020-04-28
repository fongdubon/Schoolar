namespace Schoolar.Web.Data.Repositories
{
    using Web.Data.Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;
    public class CoordinatorRepository : GenericRepository<Coordinator>, ICoordinatorRepository
    {
        private readonly DataContext _dataContext;
        public CoordinatorRepository(DataContext dataContext) : base(dataContext)
        {
            this._dataContext = dataContext;
        }

        public IEnumerable<SelectListItem> GetComboCoordinators()
        {
            var list = this._dataContext.Coordinators.Select(c => new SelectListItem
            {
                Text = c.User.FullName,
                Value = $"{c.Id}"
            }).ToList();
            list.Insert(0, new SelectListItem
            {
                Text = "(Select a Coordinator...)",
                Value = "0"
            });
            return list;
        }

        public IQueryable GetCoordinatorWithUser()
        {
            return this._dataContext.Coordinators
                .Include(t => t.User)
                .OrderBy(t => t.User.FullName);
        }

        public IEnumerable<Common.Models.Coordinator> GetCoordinatorsCommonToList()
        {
            var list = this._dataContext.Coordinators.Select(c => new Common.Models.Coordinator
            {
                Enrollment = c.User.Enrollment,
                FirstName = c.User.FirstName,
                FullImageUrl = new System.Uri(c.FullImageUrl),
                HireDate = c.HireDate,
                Id = c.Id,
                ImageUrl = c.ImageUrl,
                LastName = c.User.LastName,
                PhoneNumber = c.User.PhoneNumber,
                UserName = c.User.UserName
            }).ToList();
            return list;
        }

        public async Task<Coordinator> GetCoordinatorByIDWithUser(int id)
        {
            return await this._dataContext.Coordinators
               .Include(t => t.User)
               .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
