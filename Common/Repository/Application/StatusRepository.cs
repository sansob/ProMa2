using System.Collections.Generic;
using System.Linq;
using DataAccess.Context;
using DataAccess.Models;

namespace Common.Repository.Application {
    public class StatusRepository : IStatusRepository {
        private readonly ApplicationContext _applicationContext = new ApplicationContext();

        public List<Status> Get() {
            return _applicationContext.Statuses.Where(X => X.IsDelete == false).ToList();
        }
    }
}