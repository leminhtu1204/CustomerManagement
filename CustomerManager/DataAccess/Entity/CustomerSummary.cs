using CustomerManager.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DataAccess.Entity
{
    public class CustomerSummary
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public State State { get; set; }
        public int OrderCount { get; set; }
        public int CameraCount { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Gender Gender { get; set; }
    }
}