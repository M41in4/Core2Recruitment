using SIENN.DbAccess.Entities;
using SIENN.Services.DTO;

namespace SIENN.Services.Mapping
{
    public interface IMapper
    {
        T Map<T>(object entity);
        void Map(object source, object destination);
    }
    public class Mapper : IMapper
    {
        public Mapper()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Unit, UnitDTO>();
                cfg.CreateMap<UnitDTO, Unit>();
                cfg.CreateMap<Category, CategoryDTO>();
                cfg.CreateMap<CategoryDTO, Category>();
                cfg.CreateMap<Type, ProductTypeDTO>();
                cfg.CreateMap<ProductTypeDTO, Type>();
                cfg.CreateMap<ProductDTO, Product>();
                cfg.CreateMap<Product, ProductDTO>()
                    .ForMember(m => m.IsAvailable, opt => opt.MapFrom(s => s.IsAvailable ? "Available" : "Unavailable"))
                    .ForMember(m => m.ProductDescription, opt => opt.MapFrom(s => $"({s.Code}) {s.Description}"))
                    .ForMember(x => x.DeliveryDate, opt => opt.MapFrom(x => x.DeliveryDate.HasValue ? x.DeliveryDate.Value.ToString("dd.MM.yyyy") : null))
                    .ForMember(x => x.Type, opt => opt.MapFrom(x => x.Type != null ? $"({x.Type.Code}) {x.Type.Description}" : null))
                    .ForMember(x => x.Unit, opt => opt.MapFrom(x => x.Unit != null ? $"({x.Unit.Code}) {x.Unit.Description}" : null))
                    .ForMember(x => x.CategoryCount, opt => opt.MapFrom(x => x.Categories.Count));
                cfg.CreateMap<Product, Product>();
            });
        }

        public T Map<T>(object source)
        {
            return AutoMapper.Mapper.Map<T>(source);
        }

        public void Map(object source, object destination)
        {
            AutoMapper.Mapper.Map(source, destination);
        }
    }
}