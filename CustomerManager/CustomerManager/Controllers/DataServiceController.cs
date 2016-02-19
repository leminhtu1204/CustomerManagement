using CustomerManager.Model;
using CustomerManager.Repository;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using DataAccess.Entity;
using DataAccess.Repository;

namespace CustomerManager.Controllers
{
    public class DataServiceController : ApiController
    {
        private CustomerRepository _CustomerRepository;

        private StateRepository _StateReporitory;

        public DataServiceController()
        {
            _CustomerRepository = new CustomerRepository();
            _StateReporitory = new StateRepository();
        }

        [HttpGet]
        [Queryable]
        public HttpResponseMessage Customers()
        {
            var customers = _CustomerRepository.GetCustomers();
            var totalRecords = customers.Count();
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, customers);
        }

        [HttpGet]
        public HttpResponseMessage States()
        {
            var states = _StateReporitory.GetEntities();
            return Request.CreateResponse(HttpStatusCode.OK, states);
        }

        [HttpGet]
        [Queryable]
        public HttpResponseMessage CustomersSummary()
        {
            int totalRecords;
            var custSummary = _CustomerRepository.GetCustomersSummary(out totalRecords).ToList();
            HttpContext.Current.Response.Headers.Add("X-InlineCount", totalRecords.ToString());
            return Request.CreateResponse(HttpStatusCode.OK, custSummary);
        }

        [HttpGet]
        public HttpResponseMessage CheckUnique(int id, string property, string value)
        {
            var opStatus = _CustomerRepository.CheckUnique(id, property, value);
            return Request.CreateResponse(HttpStatusCode.OK, opStatus);
        }

        [HttpPost]
        public Customer Login([FromBody]UserLogin userLogin)
        {
            var customer = _CustomerRepository.Login(userLogin.UserName, userLogin.Password);
            return customer;
        }

        [HttpPost]
        public HttpResponseMessage Logout()
        {
            //Simulated logout
            return Request.CreateResponse(HttpStatusCode.OK, new { status = true });
        }

        // GET api/<controller>/5
        [HttpGet]
        public HttpResponseMessage CustomerById(int id)
        {
            var customer = _CustomerRepository.GetCustomerById(id);
            return Request.CreateResponse(HttpStatusCode.OK, customer);
        }

        // POST api/<controller>
        public HttpResponseMessage PostCustomer([FromBody]Customer customer)
        {
            var opStatus = _CustomerRepository.InsertCustomer(customer);
            if (opStatus.Status)
            {
                var response = Request.CreateResponse(HttpStatusCode.Created, customer);
                string uri = Url.Link("DefaultApi", new { id = customer.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }

        // PUT api/<controller>/5
        public HttpResponseMessage PutCustomer(int id, [FromBody]Customer customer)
        {
            var opStatus = _CustomerRepository.UpdateCustomer(customer);
            if (opStatus.Status)
            {
                return Request.CreateResponse<Customer>(HttpStatusCode.Accepted, customer);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotModified, opStatus.ExceptionMessage);
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage DeleteCustomer(int id)
        {
            var opStatus = _CustomerRepository.DeleteCustomer(id);

            if (opStatus.Status)
            {
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, opStatus.ExceptionMessage);
        }
    }
}