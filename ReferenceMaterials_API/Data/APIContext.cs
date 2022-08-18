using Microsoft.EntityFrameworkCore;
using ReferenceMaterials_API.Models;


//to handle our data sets for our database
namespace ReferenceMaterials_API.Data
{
    public class APIContext: DbContext
    {
        //providing our data sets:
        public DbSet<Material> Materials { get; set; }
        //creating a constructor
       public APIContext(DbContextOptions<APIContext> options)//passing the option as paramters
        : base(options)//calling the base constructor from inheritance
        {
            
        }
    }
}
