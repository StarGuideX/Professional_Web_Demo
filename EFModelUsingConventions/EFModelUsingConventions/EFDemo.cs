using EFModelUsingConventions.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFModelUsingConventions
{
    public class EFDemo
    {
        private async Task CreateTheDatabaseAsync()
        {
            using (var context = new PensContext())
            {
                bool created = await context.Database.EnsureCreatedAsync();
                string creationInfo = created ? "created" : "exists";
                Console.WriteLine($"database {creationInfo}");
            }
        }

    }
}
