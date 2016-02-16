using System.Collections.Generic;

namespace DataAccess.Entity
{
    public class Camera
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public ICollection<Customer> Customers { get; set; }
    }
}