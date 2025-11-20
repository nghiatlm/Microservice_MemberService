using System.Net;
using MemberService.BO.Common;
using MemberService.BO.Entites;
using MemberService.BO.Exceptions;
using MemberService.Repository;
using Microsoft.Extensions.Logging;

namespace MemberService.Service.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly ILogger<MemberService> _logger;

        public MemberService(IMembershipRepository membershipRepository, ILogger<MemberService> logger)
        {
            _membershipRepository = membershipRepository;
            _logger = logger;
        }

        public async Task<Membership> GetById(int id)
        {
            try
            {
                var member = await _membershipRepository.FindById(id);
                if (member == null) throw new AppException("Member not found", HttpStatusCode.NotFound);
                return member;
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching member: {Message}", ex.Message);
                throw new AppException("Internal Server Error", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<PageResult<Membership>> GetMembers(int? accountId = default, int? packageId = default, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var result = await _membershipRepository.FindQueryParams(accountId, packageId, null, pageNumber, pageSize);
                return result ?? new PageResult<Membership>(new List<Membership>(), 0, 0, pageNumber, pageSize);
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching members: {Message}", ex.Message);
                throw new AppException("Internal Server Error", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<int> Update(Membership request)
        {
            try
            {
                var existing = await _membershipRepository.FindById(request.Id);
                if (existing == null) throw new AppException("Member not found", HttpStatusCode.NotFound);

                // Map allowed updatable fields
                existing.PackageId = request.PackageId;
                existing.UpdatedAt = DateTime.UtcNow;
                existing.Status = request.Status;

                var updated = await _membershipRepository.Update(existing);
                if (updated < 0) throw new AppException("Update failed", HttpStatusCode.InternalServerError);
                return updated;
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating member: {Message}", ex.Message);
                throw new AppException("Internal Server Error", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<int> Delete(int id)
        {
            try
            {
                var existing = await _membershipRepository.FindById(id);
                if (existing == null) throw new AppException("Member not found", HttpStatusCode.NotFound);
                var deleted = await _membershipRepository.Delete(existing);
                if (deleted < 0) throw new AppException("Delete failed", HttpStatusCode.InternalServerError);
                return deleted;
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting member: {Message}", ex.Message);
                throw new AppException("Internal Server Error", HttpStatusCode.InternalServerError);
            }
        }
    }
}
