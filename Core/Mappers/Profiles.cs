using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Enums;

namespace Core.Mappers
{
    public class Profiles : Profile
    {
        public Profiles() 
        {
            CreateMap<Catalog, CatalogContentItemDTO>()
                .ForMember(x => x.Type, x => x.MapFrom(y => CategoryContentEnum.Category));

            CreateMap<Note, CatalogContentItemDTO>()
                .ForMember(x => x.Type, x => x.MapFrom(y => CategoryContentEnum.Note));
        }
    }
}
