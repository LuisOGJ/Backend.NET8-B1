using AutoMapper;
using Backend2.DTOs;
using Backend2.Models;

namespace Backend2.Automappers
{
    public class MappingProfile : Profile
    {

        public MappingProfile() {
            CreateMap<BeerInsertDto, Beer>();
        }

    }
}
