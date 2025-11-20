

using System.Net;
using MemberService.BO.Common;
using MemberService.BO.Entites;
using MemberService.BO.Enums;
using MemberService.BO.Exceptions;
using MemberService.BO.Requests;
using MemberService.Repository;
using Microsoft.Extensions.Logging;

namespace MemberService.Service.Services
{
    public class PackageService : IPackageService
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IPackageTypeRepository _packageTypeRepository;
        private readonly ILogger<IPackageRepository> _logger;

        public PackageService(IPackageRepository packageRepository, IPackageTypeRepository packageTypeRepository, ILogger<IPackageRepository> logger)
        {
            _packageRepository = packageRepository;
            _packageTypeRepository = packageTypeRepository;
            _logger = logger;
        }

        public async Task<bool> Create(PackageRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Name))
                    throw new AppException("Name is required", HttpStatusCode.BadRequest);
                if (await _packageRepository.FindByName(request.Name) != null)
                    throw new AppException("Package name already exists", HttpStatusCode.BadRequest);
                if (request.PackageTypeId <= 0) throw new AppException("PackageTypeId is required", HttpStatusCode.BadRequest);
                var pkgType = await _packageTypeRepository.FindById(request.PackageTypeId);
                if (pkgType == null) throw new AppException("PackageType not found", HttpStatusCode.BadRequest);
                string code;
                int attempts = 0;
                do
                {
                    if (++attempts > 50) throw new AppException("Unable to generate unique package code", HttpStatusCode.InternalServerError);
                    code = Utils.GenerateCode("P");
                }
                while (await _packageRepository.FindByCode(code) != null);

                var entity = new Package
                {
                    Code = code,
                    Name = request.Name ?? string.Empty,
                    Description = request.Description,
                    Price = request.Price,
                    DurationInDays = request.DurationInDays,
                    PackageTypeId = request.PackageTypeId,
                    IsActive = request.IsActive
                };
                var result = await _packageRepository.Add(entity);
                return result > 0 ? true : false;
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {Message}", ex.Message);
                throw new AppException("Internal Server Error", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var entity = await _packageRepository.FindById(id);
                if (entity == null) throw new AppException("Package not found", HttpStatusCode.NotFound);
                var result = await _packageRepository.Delete(entity);
                return result > 0 ? true : false;
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {Message}", ex.Message);
                throw new AppException("Internal Server Error", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Package?> GetById(int id)
        {
            try
            {
                var entity = await _packageRepository.FindById(id);
                return entity ?? throw new AppException("Package not found", HttpStatusCode.NotFound);
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {Message}", ex.Message);
                throw new AppException("Internal Server Error", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<PageResult<Package>> GetPackageTypes(string? query, int? packageTypeId = null, Status? IsActive = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var result = await _packageRepository.FindQueryParams(query, packageTypeId, IsActive, pageNumber, pageSize);
                return result ?? new PageResult<Package>(new List<Package>(), 0, 0, pageNumber, pageSize);
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {Message}", ex.Message);
                throw new AppException("Internal Server Error", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<bool> Update(int id, PackageRequest request)
        {
            try
            {
                var existing = await _packageRepository.FindById(id);
                if (existing == null) throw new AppException("Package not found", HttpStatusCode.NotFound);

                existing.Name = request.Name;
                existing.Description = request.Description;
                existing.Price = request.Price;
                existing.DurationInDays = request.DurationInDays;
                existing.PackageTypeId = request.PackageTypeId;
                existing.IsActive = request.IsActive;

                var result = await _packageRepository.Update(existing);
                return result > 0;
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {Message}", ex.Message);
                throw new AppException("Internal Server Error", HttpStatusCode.InternalServerError);
            }
        }
    }
}