using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using CustomerManager.Model;
using DataAccess.Entity;

namespace DataAccess.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public IQueryable<Customer> GetCustomers()
        {
            var query = dbSet
                        .Include("Orders")
                        .Include("State")
                        .OrderBy(c => c.LastName);
            return query.AsQueryable();
        }

        public List<State> GetStates()
        {
            return null; //_Context.States.OrderBy(s => s.Name).ToList();
        }

        public IQueryable<CustomerSummary> GetCustomersSummary(out int totalRecords)
        {
            var query = dbSet
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
                    var uniqueEmail = !dbSet.Any(c => c.Id != id && c.Email == value);
                    return new OperationStatus { Status = uniqueEmail };
                case "username":
                    var uniqueUserName = !dbSet.Any(c => c.Id != id && c.UserName == value);
                    return new OperationStatus { Status = uniqueUserName };
                default:
                    return new OperationStatus();
            }
        }

        public Customer GetCustomerById(int id)
        {
            return GetEntityById(id);
        }

        public Customer Login(string username, string password)
        {
            var encrypt = Encrypt(password);

            var customer = dbSet
                    .FirstOrDefault(c => String.Equals(c.UserName.Trim(), username) && c.Password == encrypt);

            return customer;
        }

        public OperationStatus InsertCustomer(Customer customer)
        {
            return InsertEntity(customer);
        }

        public OperationStatus UpdateCustomer(Customer customer)
        {
            return UpdateEntity(customer);
        }

        public OperationStatus DeleteCustomer(int id)
        {
            return DeleteEntity(id);
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
