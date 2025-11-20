
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
    [Route("api/v1/package-types")]
    public class PackageTypeController : ControllerBase
    {
        private readonly ILogger<PackageTypeController> _logger;
        private readonly IPackageTypeService _packageTypeService;

        public PackageTypeController(ILogger<PackageTypeController> logger, IPackageTypeService packageTypeService)
        {
            _logger = logger;
            _packageTypeService = packageTypeService;
        }

        [HttpPost]
        [Authorize(Roles = "ROLE_ADMIN")]
        public async Task<IActionResult> CreatePackageType([FromBody] PackageTypeRequest request)
        {
            var result = await _packageTypeService.Create(request);
            return result ? Ok(ApiResponse<string>.SuccessResponse(null, "Registration successful")) : BadRequest(ApiResponse<object>.BadRequest("Registration failed"));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ROLE_ADMIN")]
        public async Task<IActionResult> DeletePackageType([FromRoute] int id)
        {
            var result = await _packageTypeService.Delete(id);
            return result ? Ok(ApiResponse<string>.SuccessResponse(null, "Deletion successful")) : BadRequest(ApiResponse<object>.BadRequest("Deletion failed"));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ROLE_ADMIN")]
        public async Task<IActionResult> UpdatePackageType([FromRoute] int id, [FromBody] PackageTypeRequest request)
        {
            var result = await _packageTypeService.Update(id, request);
            return result ? Ok(ApiResponse<string>.SuccessResponse(null, "Update successful")) : BadRequest(ApiResponse<object>.BadRequest("Update failed"));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "ROLE_ADMIN")]
        public async Task<IActionResult> GetPackageTypeById([FromRoute] int id)
        {
            var packageType = await _packageTypeService.GetById(id);
            if (packageType == null)
            {
                return NotFound(ApiResponse<object>.NotFound("PackageType not found"));
            }
            return Ok(ApiResponse<PackageType>.SuccessResponse(packageType, "Fetch successful"));
        }

        [HttpGet]
        [Authorize(Roles = "ROLE_ADMIN")]
        public async Task<IActionResult> GetAllPackageTypes(
                                                            [FromQuery] string? query, [FromQuery] TypeLevel? Level = default,
                                                            [FromQuery] Status? Status = default, [FromQuery] int pageNumber = 1,
                                                            [FromQuery] int pageSize = 10
                                                            )
        {
            var packageTypes = await _packageTypeService.GetPackageTypes(query, Level, Status, pageNumber, pageSize);
            return Ok(ApiResponse<PageResult<PackageType>>.SuccessResponse(packageTypes, "Fetch successful"));
        }
    }
}