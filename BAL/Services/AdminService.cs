using BAL.Interfaces;
using Data.Models;
using Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class AdminService : DataAccessProvider<BusinessConfiguration>, IAdminService
    {
        private readonly SigmaproIisContext _dbContext;
        private readonly SigmaproIisContextUdf _dbContextudf;
        private readonly ILogger<BusinessConfiguration> _logger;
        private readonly string _corelationId = string.Empty;
        public AdminService(SigmaproIisContext dbContext, ILogger<BusinessConfiguration> logger, SigmaproIisContextUdf dbContextudf) : base(dbContext)
        {
            _dbContext = dbContext;
            _logger = logger;
            _dbContextudf = dbContextudf;
        }

    }
}
