using Microsoft.Extensions.Options;
using TimeTracker.Core.Data;
using TimeTracker.Core.Models;
using TimeTracker.Core.Services;
using TimeTracker.Core.Services.Implements;
using TimeTracker.Core.Utils;

namespace TimeTracker.Web.Test.Utils
{
    public abstract class ABaseServiceTest<TService>
    {
        protected NHibernateHelper GetConnectionHelper()
        {
            IOptions<ApplicationSettings> config = Options.Create<ApplicationSettings>(new ApplicationSettings() { ConnectionString = "Server=localhost,1433;Database=TimeTracker;User Id=TimeTrackerManagerLogin;Password=TimeTrackerManagerPassword123;MultipleActiveResultSets=True;Integrated Security=False" });
            var dbHelper = new NHibernateHelper(config);
            return dbHelper;
        }

        public abstract TService GetService();
    }
}