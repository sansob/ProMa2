using System.Collections.Generic;
using DataAccess.Models;

namespace BusinessLogic.Service {
    public interface IStatusService {
        List<Status> Get();
    }
}