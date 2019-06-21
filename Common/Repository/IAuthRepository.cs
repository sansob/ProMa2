
using DataAccess.ViewModels;

namespace Common.Repository {
    public interface IAuthRepository {
        EmployeeVM DoLogin(string email, string password);
    }
}