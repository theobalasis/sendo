// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace sendo.Views
{
    public class Contacts
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Name => FirstName + " " + LastName;

        public Contacts(string firstName, string lastName, string company)
        {
            FirstName = firstName;
            LastName = lastName;
            Company = company;
        }
    }


}
