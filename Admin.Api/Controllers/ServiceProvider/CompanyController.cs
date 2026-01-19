using Admin.Api.Controllers.Base;
using AutoMapper;
using Common.Constant;
using Common.Logging;
using DTOs.Models;
using DTOs.Request;
using DTOs.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Admin.Api.Controllers.ServiceProvider
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = IdentityConstant.SuperAdmin + "," + IdentityConstant.CompanyOwner)]
    public class CompanyController : AppBaseApiController
    {
        private readonly ICompanyService _companyService;
        private IMapper _mapper;
        public CompanyController(ICompanyService companyService,IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Post(CompanyRequest request)
        {
            var model = _mapper.Map<CompanyModel>(request);
            var result = await _companyService.AddOrUpdate(model,UserId);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            try
            {
                if (User.IsInRole(IdentityConstant.SuperAdmin))
                {
                    var list =_mapper.Map<List<CompanyResponse>>( await _companyService.Get()); 
                    return Ok(SuccessMessage(list));
                }
                else if (User.IsInRole(IdentityConstant.CompanyOwner))
                {
                    var result = _mapper.Map< CompanyResponse>( await _companyService.GetCompanyProfile(CompanyId));
                    return Ok(SuccessMessage(result));
                }
                return BadRequest(ErrorMessage("You are not Authorize to access"));
            }
            catch (Exception ex)
            {
                AppLogging.LogException(ex.Message); return BadRequest(ErrorMessage(ex.Message));
            }
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = _mapper.Map<CompanyResponse>(await _companyService.GetById(id));
            return Ok(result);
        }
    }
}
