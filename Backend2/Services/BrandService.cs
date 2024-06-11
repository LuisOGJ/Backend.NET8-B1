using AutoMapper;
using Backend2.DTOs.BrandDTOs;
using Backend2.Models;
using Backend2.Repository;

namespace Backend2.Services
{
    public class BrandService : ICommonService<BrandDTO, BrandInsertDTOs, BrandUpdateDto>
    {

        private IRepository<Brand> _brandRepository;
        private IMapper _mapper;

        public BrandService(IRepository<Brand> brandRepository, IMapper mapper) { 
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public async Task<BrandDTO> Add(BrandInsertDTOs brandInsertDto)
        {
            var brand = _mapper.Map<Brand>(brandInsertDto);

            await _brandRepository.Add(brand);
            await _brandRepository.Save();

            var brandDto = _mapper.Map<BrandDTO>(brand);

            return brandDto;
        }

        public async Task<BrandDTO> Delete(int id)
        {
            var brand = await _brandRepository.GetById(id);

            if (brand == null) {
                return null;
            }

            var brandDto = _mapper.Map<BrandDTO>(brand);

            _brandRepository.Delete(brand);
            await _brandRepository.Save();

            return brandDto;


        }

        public async Task<IEnumerable<BrandDTO>> Get()
        {
            var brands = await _brandRepository.Get();
            return brands.Select(brand => _mapper.Map<BrandDTO>(brand));
        }

        public async Task<BrandDTO> GetById(int id)
        {
            var brand = await _brandRepository.GetById(id);
            if (brand == null)
            {
                return null;
            }

            var brandDto = _mapper.Map<BrandDTO>(brand);
            return brandDto;
        }

        public async Task<BrandDTO> Update(int id, BrandUpdateDto brandUpdateDto)
        {
            var brand = await _brandRepository.GetById(id);
            if (brand == null)
            {
                return null;
            }

            //brand.BrandID = brandUpdateDto.Id;
            brand.Name = brandUpdateDto.Name;

            _brandRepository.Update(brand);
            await _brandRepository.Save();

            var brandDto = _mapper.Map<BrandDTO>(brand);
            return brandDto;

        }
    }
}
