public class Address
{
    public string Street {get;set;}
    public string Municipality {get;set;}
    public string Province {get;set;}
    public string Country {get;set;}
    public string PostalCode {get;set;}
    public string Type {get;set;}

    public Address (string street, string municipality, string province, string country, string postalCode, string type)
    {
        Street = street;
        Municipality = municipality;
        Province = province;
        Country = country;
        PostalCode = postalCode;
        Type = type;
    }

}
public class User
{
    public string FirstName {get;set;}
    public string LastName {get;set;}
    public string Email {get;set;}
    public string MainPhoneNumber {get;set;}

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
    public int WHManagerID {get;set;}

    public WHManager(int whmanagerid, string firstname, string lastname, string email, string mainphonenumber)
    :base ( firstname,  lastname,  email, mainphonenumber){
        WHManagerID = whmanagerid;
    }
    
}
public class WHWorker : User
{
    public int WHWorkerID {get;set;}

    public WHWorker(int whworkerid, string firstname, string lastname, string email, string mainphonenumber)
    :base ( firstname,  lastname,  email, mainphonenumber){
        WHWorkerID = whworkerid;
        }
   
    
}
public class Customer : User
{
    public int CustomerID {get;set;}
    public string FaxNumber{get;set;}

    public Customer(int customerid, string firstname, string lastname, string email, string mainphonenumber, string faxnumber)
    :base ( firstname,  lastname,  email, mainphonenumber){
        CustomerID = customerid;
        FaxNumber = faxnumber;
        }
   
}
