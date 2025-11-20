

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
    public class PackageTypeService : IPackageTypeService
    {
        private readonly IPackageTypeRepository _packageTypeRepository;
        private readonly ILogger<IPackageRepository> _logger;

        public PackageTypeService(IPackageTypeRepository packageTypeRepository, ILogger<IPackageRepository> logger)
        {
            _packageTypeRepository = packageTypeRepository;
            _logger = logger;
        }

        public async Task<bool> Create(PackageTypeRequest request)
        {
            try
            {
                if (await _packageTypeRepository.FindByName(request.Name) != null) throw new AppException("PackageType name already exists", HttpStatusCode.BadRequest);
                if (await _packageTypeRepository.FindByLevel(request.Level) != null) throw new AppException("PackageType level already exists", HttpStatusCode.BadRequest);
                var packageType = new PackageType
                {
                    Name = request.Name,
                    Description = request.Description,
                    Level = request.Level,
                    Status = request.Status
                };
                var result = await _packageTypeRepository.Add(packageType);
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
                var packageType = await _packageTypeRepository.FindById(id);
                if (packageType == null) throw new AppException("PackageType not found", HttpStatusCode.NotFound);
                var result = await _packageTypeRepository.Delete(packageType);
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

        public async Task<PackageType?> GetById(int id)
        {
            try
            {
                var packageType = await _packageTypeRepository.FindById(id);
                return packageType ?? throw new AppException("PackageType not found", HttpStatusCode.NotFound);
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

        public async Task<PageResult<PackageType>> GetPackageTypes(string? query, TypeLevel? Level = null, Status? Status = null, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var result = await _packageTypeRepository.FindQueryParams(query, Level, Status, pageNumber, pageSize);
                return result ?? new PageResult<PackageType>(new List<PackageType>(), 0, 0, pageNumber, pageSize);
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

        public async Task<bool> Update(int id, PackageTypeRequest request)
        {
            try
            {
                var existingPackageType = await _packageTypeRepository.FindById(id);
                if (existingPackageType == null) throw new AppException("PackageType not found", HttpStatusCode.NotFound);
                if (existingPackageType.Name != request.Name)
                {
                    if (await _packageTypeRepository.FindByName(request.Name) != null) throw new AppException("PackageType name already exists", HttpStatusCode.BadRequest);
                }
                if (existingPackageType.Level != request.Level)
                {
                    if (await _packageTypeRepository.FindByLevel(request.Level) != null) throw new AppException("PackageType level already exists", HttpStatusCode.BadRequest);
                }
                existingPackageType.Name = request.Name;
                existingPackageType.Description = request.Description;
                existingPackageType.Level = request.Level;
                existingPackageType.Status = request.Status;
                var result = await _packageTypeRepository.Update(existingPackageType);
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
    }
}