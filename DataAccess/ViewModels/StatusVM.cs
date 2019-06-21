
namespace DataAccess.ViewModels {
    public class StatusVM {
        public int Id { get; set; }
        public string Status_name { get; set; }
        public string Status_module { get; set; }

        public StatusVM() {
        }

        public StatusVM(string statusName, string statusModule) {
            this.Status_name = statusName;
            this.Status_module = statusModule;
        }

        public void Update(int Id, string statusName, string statusModule) {
            this.Id = Id;
            this.Status_name = statusName;
            this.Status_module = statusModule;
        }
    }
}