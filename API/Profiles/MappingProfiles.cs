using API.DTO;
using AutoMapper;
using Core.Entities;

namespace API.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Producto, ProductoDto>().ReverseMap();
            CreateMap<Marca, MarcaDto>().ReverseMap();
            CreateMap<Categoria, CategoriaDto>().ReverseMap();

            CreateMap<Producto, ProductoListDto>()
                .ForMember(des => des.Marca, orig => orig.MapFrom(orig => orig.Marca.Nombre))
                .ForMember(des => des.Categoria, orig => orig.MapFrom(orig => orig.Categoria.Nombre))
                .ReverseMap()
                .ForMember(origen => origen.Categoria, dest => dest.Ignore())
                .ForMember(origen => origen.Marca, dest => dest.Ignore());

            CreateMap<Producto, ProductoAddUpdateDto>().ReverseMap();
        }
    }
}
