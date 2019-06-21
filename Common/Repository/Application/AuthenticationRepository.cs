//using DataAccess.Context;
//using DataAccess.ViewModels;
//
//namespace Common.Repository.Application {
//    public class AuthenticationRepository: IAuthRepository {
//        ApplicationContext applicationContext = new ApplicationContext();
//        public EmployeeVM DoLogin(string email, string password) {
//            var temp = applicationContext.
//                .SingleOrDefault(x => x.Email == email && x.Password == password);
//        }
//    }
//}