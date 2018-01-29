using System.Collections.Generic;
using System.Linq;
using SIENN.DbAccess;
using SIENN.DbAccess.Entities;
using SIENN.Services.DTO;
using SIENN.Services.Mapping;

namespace SIENN.Services.Managers
{
    public interface IProductsManager
    {
        int Save(ProductTypeDTO productType);
        IEnumerable<ProductTypeDTO> GetProductTypes();
        ProductTypeDTO GetProductType(int id);
        PaginationResult<ProductDTO> GetAvailableProducts(int page, int pageSzie);
        void DeleteProductTypeById(int id);
        ProductDTO GetProductById(int id);
        int Save(Product product);
        void DeleteProductById(int id);
        IEnumerable<ProductDTO> SearchProducts(int? type, int? category);
        IEnumerable<ProductDTO> GetUnavailableProductsForDelivery();
    }
    public class ProductsManager : Manager, IProductsManager
    {
        public ProductsManager(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public int Save(ProductTypeDTO productType)
        {
            return Save<Type>(productType);
        }

        public IEnumerable<ProductTypeDTO> GetProductTypes()
        {
            return UnitOfWork.TypesRepository.GetAll().Select(Mapper.Map<ProductTypeDTO>).ToList();
        }

        public ProductTypeDTO GetProductType(int id)
        {
            var type = UnitOfWork.ProductsRepository.Get(id);
            return type != null ? Mapper.Map<ProductTypeDTO>(type) : null;
        }

        public PaginationResult<ProductDTO> GetAvailableProducts(int page, int size)
        {
            var skip = (page - 1) * size;
            return new PaginationResult<ProductDTO>()
            {
                TotalCount = UnitOfWork.ProductsRepository.CountOfAvailableProducts(),
                Results = UnitOfWork.ProductsRepository
                                    .GetAvailableProducts(size, skip)
                                    .Select(Mapper.Map<ProductDTO>)
            };
        }

        public void DeleteProductTypeById(int id)
        {
            Delete<Type>(id);
        }

        public ProductDTO GetProductById(int id)
        {
            var product = UnitOfWork.ProductsRepository.GetProductById(id);
            if (product == null) return null;
            return Mapper.Map<ProductDTO>(product);
        }

        public int Save(Product product)
        {
            if (product.Id == 0)
            {
                UnitOfWork.ProductsRepository.Add(product);
            }
            else
            {
                var existing = UnitOfWork.ProductsRepository.Get(product.Id);
                Mapper.Map(product, existing);
            }

            UnitOfWork.SaveChanges();
            return product.Id;
        }

        public void DeleteProductById(int id)
        {
            Delete<Product>(id);
        }

        public IEnumerable<ProductDTO> SearchProducts(int? type, int? category)
        {
            return UnitOfWork.ProductsRepository
                             .GetProducts(type, category)
                             .Select(Mapper.Map<ProductDTO>)
                             .ToList();
        }

        public IEnumerable<ProductDTO> GetUnavailableProductsForDelivery()
        {
            return UnitOfWork.ProductsRepository.GetUnavailableProductsInCurrentMonth().Select(Mapper.Map<ProductDTO>).ToList();
        }
    }
}