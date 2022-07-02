using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationTest
{
    public class DatabaseBuilder
    {
        public DatabaseContext SqlDatabase()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>().UseSqlServer("Data Source= .; Initial Catalog = OnlineShopDb; Integrated Security  = true ;").Options;
            return new DatabaseContext(options);
        }
        public DatabaseContext InMemoryDatabase()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            DatabaseContext context = new DatabaseContext(options);
            return new DatabaseContext(options);
        }
    }
}
