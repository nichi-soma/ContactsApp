using System.Collections.Generic;

namespace ContactsApp.Models
{
    public class ContactsViewModel
    {
        public List<ContactModel> Data { get; set; }
        public string Name { get; set; }
        public string PhNo { get; set; }
        public string Email { get; set; }

        public string LoggedInUserID { get; set; }
    }
}