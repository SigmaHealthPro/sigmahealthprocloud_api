﻿using BAL.RequestModels;
using BAL.Constant;
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Repository
{
    public interface ISiteRepository : IGenericRepository<SiteModel>
    {
        public Task<IEnumerable<SiteModel>> GetAllAsync(SearchParams search);
    }
}
