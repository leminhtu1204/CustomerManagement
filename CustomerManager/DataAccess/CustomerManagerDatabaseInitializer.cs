using System.Data.Entity;
using CustomerManager.Repository;

namespace DataAccess
{
    ////DropCreateDatabaseIfModelChanges<TodosContext>
    public class CustomerManagerDatabaseInitializer : DropCreateDatabaseAlways<CustomerManagerContext> // re-creates every time the server starts
    {
        protected override void Seed(CustomerManagerContext context)
        {
            DataInitializer.Initialize(context);
            base.Seed(context);
        }
    }
}
