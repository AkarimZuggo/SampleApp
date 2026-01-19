using AutoMapper;
using DataAccess.Interfaces;
using DTOs.Models;
using Entities.DBEntities;
using Services.Interfaces;

namespace Services.Implementation
{
    public class CompanyService : ICompanyService
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly IMapper _mapper;

        public CompanyService(IUnitOfWork unitofWork,IMapper mapper)
        {
            _unitofWork = unitofWork;
            _mapper = mapper;
        }
        public async Task<CompanyModel> AddOrUpdate(CompanyModel model, string userId)
        {
            var entity = _mapper.Map<Company>(model);
            if (entity.PhoneNumber != "" && entity.Name != "")
            {
                if(entity.Id>0)
                {
                    var existingEntity = await _unitofWork.CompanyRepository.GetByIdAsync(entity.Id);
                    //var existingEntity = await _unitofWork.CompanyRepository.GetCompanyProfileByUserId(userId);
                    if (existingEntity != null)
                    {
                        existingEntity.CompanyOwnerId = userId;
                        existingEntity.Name = entity.Name;
                        existingEntity.PhoneNumber = entity.PhoneNumber;
                        existingEntity.Description = entity.Description;
                        existingEntity.IsActive = entity.IsActive;
                        existingEntity.ModifiedBy = userId;
                        existingEntity.ModifiedOn = DateTime.UtcNow;
                        _unitofWork.CompanyRepository.Update(existingEntity);
                        await _unitofWork.CommitAsync();
                        return _mapper.Map<CompanyModel>(existingEntity);
                    }
                }
                
                else
                {
                    entity.CompanyOwnerId = userId;
                    entity.CreatedBy = userId;
                    entity.CreatedOn = DateTime.UtcNow;
                    await _unitofWork.CompanyRepository.AddAsync(entity);
                }
                await _unitofWork.CommitAsync();
                return _mapper.Map<CompanyModel>(entity);
            }
            else throw new Exception("Name and Phone Number are required.");
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _unitofWork.CompanyRepository.GetByIdAsync(id);
            if (result == null)
            {
                return false;
            }
            _unitofWork.CompanyRepository.Remove(result);
            await _unitofWork.CommitAsync();
            return true;
        }

        public async Task<List<CompanyModel>> Get()
        {
            var list = await _unitofWork.CompanyRepository.GetAllAsync();
            return _mapper.Map<List<CompanyModel>>(list);
        }

        public async Task<CompanyModel> GetById(int id)
        {
            var result = await _unitofWork.CompanyRepository.GetByIdAsync(id);
            return _mapper.Map<CompanyModel>(result);
        }

        public async Task<string> GetCompanyId(string ownerId)
        {
            return await _unitofWork.CompanyRepository.GetCompanyId(ownerId);
        }
        public async Task<CompanyModel> GetCompanyProfile(int id)
        {
            var company = await _unitofWork.CompanyRepository.GetCompanyProfile(id);
            return _mapper.Map<CompanyModel>(company);
        }
    }
}
