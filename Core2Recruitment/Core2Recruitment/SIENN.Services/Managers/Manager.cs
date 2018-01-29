using System;
using SIENN.DbAccess;
using SIENN.DbAccess.Entities;
using SIENN.Services.Mapping;

namespace SIENN.Services.Managers
{
    public class Manager
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IMapper Mapper;

        public Manager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        public int Save<TEntity>(DTO.DTO dto) where TEntity : Entity
        {
            TEntity entity = Mapper.Map<TEntity>(dto);
            var repository = UnitOfWork.GetRepository(entity);
            if (dto.Id == 0)
            {
                repository.Add(entity);
            }
            else
            {
                entity = repository.Get(dto.Id);
                Mapper.Map(dto, entity);
            }
            UnitOfWork.SaveChanges();
            return entity.Id;
        }

        public void Delete<TEntity>(int id) where TEntity : Entity
        {
            var entity = Activator.CreateInstance<TEntity>();
            entity.Id = id;
            var repository = UnitOfWork.GetRepository(entity);
            repository.Remove(entity);
            UnitOfWork.SaveChanges();
        }
    }
}