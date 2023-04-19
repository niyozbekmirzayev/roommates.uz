﻿using Roommates.Domain.Models.Roommates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roommates.Data.IRepositories
{
    public interface IRoommateRepository : IBaseRepository<Roommate, RoommatesDbContext>
    {
    }
}