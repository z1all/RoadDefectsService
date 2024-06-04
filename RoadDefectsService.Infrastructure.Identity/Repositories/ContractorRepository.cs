﻿using Microsoft.EntityFrameworkCore;
using RoadDefectsService.Core.Application.DTOs.ContractorService;
using RoadDefectsService.Core.Application.Interfaces.Repositories;
using RoadDefectsService.Core.Domain.Models;
using RoadDefectsService.Infrastructure.Identity.Contexts;
using RoadDefectsService.Infrastructure.Identity.Repositories.Base;

namespace RoadDefectsService.Infrastructure.Identity.Repositories
{
    public class ContractorRepository : BaseWithBaseEntityRepository<Contractor, AppDbContext>, IContractorRepository
    {
        public ContractorRepository(AppDbContext dbContext) : base(dbContext) { }

        public async Task<List<Contractor>> GetContractorsByFilterAsync(ContractorFilterDTO contractorFilter)
        {
            var contractors = _dbContext.Contractors.AsQueryable();

            if (contractorFilter.ContractorFullName is not null)
            {
                contractors = contractors
                    .Where(contractor => contractor.ContractorFullName.ToLower().Contains(contractorFilter.ContractorFullName.ToLower()));
            }

            if (contractorFilter.OrganizationName is not null)
            {
                contractors = contractors
                    .Where(contractor => contractor.OrganizationName.ToLower().Contains(contractorFilter.OrganizationName.ToLower()));
            }

            return await contractors
                .Skip((contractorFilter.Page - 1) * contractorFilter.Size)
                .Take(contractorFilter.Size)
                .ToListAsync();
        }

        public async Task<int> CountContractorsByFilterAsync(ContractorFilterDTO contractorFilter)
        {
            var contractors = _dbContext.Contractors.AsQueryable();

            if (contractorFilter.ContractorFullName is not null)
            {
                contractors = contractors
                    .Where(contractor => contractor.ContractorFullName.ToLower().Contains(contractorFilter.ContractorFullName.ToLower()));
            }

            if (contractorFilter.OrganizationName is not null)
            {
                contractors = contractors
                    .Where(contractor => contractor.OrganizationName.ToLower().Contains(contractorFilter.OrganizationName.ToLower()));
            }

            return await contractors
                .CountAsync();
        }

        public Task<bool> AnyByEmailAsync(string email)
        {
            return _dbContext.Contractors
                .AnyAsync(contractor => contractor.Email == email);
        }
    }
}
