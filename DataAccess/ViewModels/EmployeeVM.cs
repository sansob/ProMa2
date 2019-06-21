namespace DataAccess.ViewModels {
    public class EmployeeVM {
        public int Id;
        public string Email;
        public string Password;
        public int Role;

        public EmployeeVM()
        {

        }

        public EmployeeVM(int id, string email, string password, int role) {
            this.Id = id;
            this.Email = email;
            this.Password = password;
            this.Role = role;
        }
    }
}