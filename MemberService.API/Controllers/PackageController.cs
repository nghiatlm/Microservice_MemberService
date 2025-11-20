using MemberService.BO.Common;
using MemberService.BO.Entites;
using MemberService.BO.Enums;
using MemberService.BO.Requests;
using MemberService.BO.Responses;
using MemberService.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MemberService.API.Controllers
{
    [ApiController]
    [Route("api/v1/packages")]
    public class PackageController : ControllerBase
    {
        private readonly ILogger<PackageController> _logger;
        private readonly IPackageService _packageService;

        public PackageController(ILogger<PackageController> logger, IPackageService packageService)
        {
            _logger = logger;
            _packageService = packageService;
        }

        [HttpPost]
        [Authorize(Roles = "ROLE_ADMIN")]
        public async Task<IActionResult> CreatePackage([FromBody] PackageRequest request)
        {
            var result = await _packageService.Create(request);
            return result ? Ok(ApiResponse<string>.SuccessResponse(null, "Creation successful")) : BadRequest(ApiResponse<object>.BadRequest("Creation failed"));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ROLE_ADMIN")]
        public async Task<IActionResult> DeletePackage([FromRoute] int id)
        {
            var result = await _packageService.Delete(id);
            return result ? Ok(ApiResponse<string>.SuccessResponse(null, "Deletion successful")) : BadRequest(ApiResponse<object>.BadRequest("Deletion failed"));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ROLE_ADMIN")]
        public async Task<IActionResult> UpdatePackage([FromRoute] int id, [FromBody] PackageRequest request)
        {
            var result = await _packageService.Update(id, request);
            return result ? Ok(ApiResponse<string>.SuccessResponse(null, "Update successful")) : BadRequest(ApiResponse<object>.BadRequest("Update failed"));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "ROLE_ADMIN, ROLE_CUSTOMER")]
        public async Task<IActionResult> GetPackageById([FromRoute] int id)
        {
            var pkg = await _packageService.GetById(id);
            if (pkg == null) return NotFound(ApiResponse<object>.NotFound("Package not found"));
            return Ok(ApiResponse<Package>.SuccessResponse(pkg, "Fetch successful"));
        }

        [HttpGet]
        [Authorize(Roles = "ROLE_ADMIN, ROLE_CUSTOMER")]
        public async Task<IActionResult> GetAllPackages([
            FromQuery] string? query, [FromQuery] int? packageTypeId = default,
            [FromQuery] Status? IsActive = default, [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var packages = await _packageService.GetPackageTypes(query, packageTypeId, IsActive, pageNumber, pageSize);
            return Ok(ApiResponse<PageResult<Package>>.SuccessResponse(packages, "Fetch successful"));
        }
    }
}
