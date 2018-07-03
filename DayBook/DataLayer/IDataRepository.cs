using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DayBook.DataLayer
{
    public interface IDataRepository<TEntity> where TEntity:class,IDisposable
    {
        Task CreateRecord(TEntity record);
        Task DeleteRecord(int id);
        Task<IEnumerable<TEntity>> GetRecords();
    }
}
