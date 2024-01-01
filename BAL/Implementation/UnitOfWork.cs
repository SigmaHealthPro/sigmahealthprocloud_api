﻿using BAL.Repository;
using BAL.Implementation;
using Data.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SigmaproIisContext _context;
        private readonly ILogger<UnitOfWork> _logger;

        public ICitiesRepository Cities { get; private set; }
        public ICountiesRepository Counties { get; private set; }
        public ICountriesRepository Countries { get; private set; }
        public IStatesRepository States { get; private set; }
        public IFacilitiesRepository Facilities { get; private set; }
        public IJuridictionsRepository Juridictions { get; private set; }
        public IOrganizationsRepository Organizations { get; private set; }
        public IUsersRepository Users { get; private set; }
        public IContactsRepository Contacts { get; private set; }
        public ILOVTypeMasterRepository lOVTypeMaster { get; private set; }
        public IAddressesRepository Addresss { get; private set; }
        public IBusinessConfigurationRepository BusinessConfiguration { get; private set; }
        public ISiteRepository Sites { get; private set; }

        public UnitOfWork(SigmaproIisContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
            Cities = new CitiesRepository(_context, _logger);
            Counties = new CountiesRepository(_context, _logger);
            Countries = new CountriesRepository(_context, _logger);
            States = new StatesRepository(_context, _logger);
            Facilities = new FacilitiesRepository(_context, _logger);
            Juridictions = new JuridictionsRepository(_context, _logger);
            Organizations = new OrganizationsRepository(_context, _logger);
            Users = new UsersRepository(_context, _logger);
            Contacts = new ContactsRepository(_context, _logger);
            lOVTypeMaster = new LovTypeMasterRepository(_context, _logger);
            Addresss = new AddressesRepository(_context, _logger);
            BusinessConfiguration = new BusinessConfigurationRepository(_context, _logger);
            Sites = new SiteRepository(_context, _logger);
        }


        public int Save()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
