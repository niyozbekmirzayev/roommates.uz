using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roommates.Data
{
    public class PrepDatabase
    {
        public PrepDatabase(IConfiguration configuration)
        {
            var seedDataInfo = configuration["AddSeedData"];
            if (!string.IsNullOrEmpty(seedDataInfo))
            {
                bool addSeedData = bool.Parse(seedDataInfo);
                if (addSeedData)
                {
                    // Need to add seed data
                }
            }
        }
    }
}
