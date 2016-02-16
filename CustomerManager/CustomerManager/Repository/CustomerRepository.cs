using System.Security.Cryptography;
using System.Text;
using CustomerManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerManager.Repository
{
    public class CustomerRepository
    {
        CustomerManagerContext _Context;

        public CustomerRepository()
        {
            _Context = new CustomerManagerContext();
            //System.Threading.Thread.Sleep(5000); 
        }

        public IQueryable<Customer> GetCustomers()
        {
            var query = _Context.Customers
                        .Include("Orders")
                        .Include("State")
                        .OrderBy(c => c.LastName);
            return query.AsQueryable();
        }

        public List<State> GetStates()
        {
            return _Context.States.OrderBy(s => s.Name).ToList();
        }

        public IQueryable<CustomerSummary> GetCustomersSummary(out int totalRecords)
        {
            var query = _Context.Customers
               .Include("States")
               .OrderBy(c => c.LastName);

            totalRecords = query.Count();

            return query.Select(c => new CustomerSummary
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                City = c.City,
                State = c.State,
                CameraCount = c.Cameras.Count(),
                Gender = c.Gender
            }).AsQueryable();
        }

        public OperationStatus CheckUnique(int id, string property, string value)
        {
            switch (property.ToLower())
            {
                case "email":
                    var uniqueEmail = !_Context.Customers.Any(c => c.Id != id && c.Email == value);
                    return new OperationStatus { Status = uniqueEmail };
                case "username":
                    var uniqueUserName = !_Context.Customers.Any(c => c.Id != id && c.UserName == value);
                    return new OperationStatus { Status = uniqueUserName };
                default:
                    return new OperationStatus();
            }
        }

        public Customer GetCustomerById(int id)
        {
            return _Context.Customers.SingleOrDefault(c => c.Id == id);
        }

        public Customer Login(string username, string password)
        {
            var encrypt = Encrypt(password);

            var customer = _Context.Customers
                    .FirstOrDefault(c => String.Equals(c.UserName.Trim(), username) && c.Password == encrypt);

            return customer;
        }

        public OperationStatus InsertCustomer(Customer customer)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                customer.Password = Encrypt(customer.Password);
                _Context.Customers.Add(customer);
                _Context.SaveChanges();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus UpdateCustomer(Customer customer) 
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                customer.State.Id = customer.StateId;
                _Context.Customers.Attach(customer);
                _Context.Entry<Customer>(customer).State = System.Data.Entity.EntityState.Modified;
                _Context.SaveChanges();
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        public OperationStatus DeleteCustomer(int id)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                var cust = _Context.Customers.SingleOrDefault(c => c.Id == id);
                if (cust != null)
                {
                    _Context.Customers.Remove(cust);
                    _Context.SaveChanges();
                }
                else
                {
                    opStatus.Status = false;
                    opStatus.ExceptionMessage = "Customer not found";
                }
            }
            catch (Exception exp)
            {
                opStatus.Status = false;
                opStatus.ExceptionMessage = exp.Message;
            }
            return opStatus;
        }

        private static string Encrypt(string input)
        {
            //create new instance of md5
            MD5 md5 = MD5.Create();

            //convert the input text to array of bytes
            byte[] hashData = md5.ComputeHash(Encoding.Default.GetBytes(input));

            //create new instance of StringBuilder to save hashed data
            StringBuilder returnValue = new StringBuilder();

            //loop for each byte and add it to StringBuilder
            for (int i = 0; i < hashData.Length; i++)
            {
                returnValue.Append(hashData[i].ToString());
            }

            // return hexadecimal string
            return returnValue.ToString();
        }
    }
}