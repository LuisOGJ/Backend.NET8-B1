using AutoMapper;
using Backend2.DTOs;
using Backend2.DTOs.BrandDTOs;
using Backend2.Models;

namespace Backend2.Automappers
{
    public class MappingProfile : Profile
    {

        public MappingProfile() {
            CreateMap<BeerInsertDto, Beer>();
            CreateMap<Beer, BeerDto>().ForMember(dto => dto.Id, m => m.MapFrom(b => b.BeerID));
            CreateMap<BeerUpdateDto, Beer>();


            CreateMap<BrandInsertDTOs, Brand>();
            CreateMap<Brand, BrandDTO>().ForMember(dto => dto.Id, m => m.MapFrom(b => b.BrandID));
        }

    }
}
