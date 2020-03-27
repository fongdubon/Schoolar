namespace Schoolar.Web.Data
{
    using Schoolar.Web.Helpers;
    using System.Threading.Tasks;

    public class SeedDb
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext dataContext, IUserHelper userHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
        }
        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();

            await _userHelper.CheckRoleAsync("Teacher");
        }
    }
}
