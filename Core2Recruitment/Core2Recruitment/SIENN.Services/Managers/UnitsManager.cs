using System.Collections.Generic;
using System.Linq;
using SIENN.DbAccess;
using SIENN.DbAccess.Entities;
using SIENN.Services.DTO;
using SIENN.Services.Mapping;

namespace SIENN.Services.Managers
{
    public interface IUnitsManager
    {
        IEnumerable<UnitDTO> GetUnits();
        int Save(UnitDTO unitDto);
        UnitDTO GetUnitById(int id);
        void DeleteUnitById(int id);
    }

    public class UnitsManager : Manager, IUnitsManager
    {
        public UnitsManager(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public IEnumerable<UnitDTO> GetUnits()
        {
            return UnitOfWork.UnitsRepository.GetAll()
                .Select(Mapper.Map<UnitDTO>)
                .ToList();
        }

        public int Save(UnitDTO unitDto)
        {
            return Save<Unit>(unitDto);
        }


        public UnitDTO GetUnitById(int id)
        {
            return Mapper.Map<UnitDTO>(UnitOfWork.UnitsRepository.Get(id));
        }

        public void DeleteUnitById(int id)
        {
            Delete<Unit>(id);
        }
    }
}