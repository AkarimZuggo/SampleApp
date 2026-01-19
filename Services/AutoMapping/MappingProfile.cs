using AutoMapper;
using DTOs.Models;
using DTOs.Request;
using DTOs.Response;
using Entities.DBEntities;

namespace Services.AutoMapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Company, CompanyModel>().ReverseMap();
            CreateMap<CompanyModel, Company>().ReverseMap();
            CreateMap<CompanyModel, CompanyRequest>().ReverseMap();
            CreateMap<CompanyRequest, CompanyModel>().ReverseMap();
            CreateMap<CompanyModel, CompanyResponse>().ReverseMap();
            CreateMap<CompanyResponse, CompanyModel>().ReverseMap();
        }
    }
}
