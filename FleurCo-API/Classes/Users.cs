namespace FleurCo_API.Classes
{

    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MainPhoneNumber { get; set; }

        public User(string firstname, string lastname, string email, string mainphonenumber)
        {
            FirstName = firstname;
            LastName = lastname;
            Email = email;
            MainPhoneNumber = mainphonenumber;
        }
    }
    public class WHManager : User
    {
        public string WHManagerID { get; set; }

        public WHManager(string whmanagerid, string firstname, string lastname, string email, string mainphonenumber)
        : base(firstname, lastname, email, mainphonenumber)
        {
            WHManagerID = whmanagerid;
        }

    }
    public class WHWorker : User
    {
        public string WHWorkerID { get; set; }

        public WHWorker(string whworkerid, string firstname, string lastname, string email, string mainphonenumber)
        : base(firstname, lastname, email, mainphonenumber)
        {
            WHWorkerID = whworkerid;
        }


    }
    public class Customer : User
    {
        public string CustomerID { get; set; }
        public string FaxNumber { get; set; }

        public Customer(string customerid, string firstname, string lastname, string email, string mainphonenumber, string faxnumber)
        : base(firstname, lastname, email, mainphonenumber)
        {
            CustomerID = customerid;
            FaxNumber = faxnumber;
        }

    }
}