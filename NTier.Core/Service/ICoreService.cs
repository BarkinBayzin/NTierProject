using NTier.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NTier.Core.Service
{
    //Temel metotlarımı ekliyoruz
    public interface ICoreService<T> where T : CoreEntity
    {
        void Add(T item);
        void Add(List<T> items);
        void Update(T item);
        void Remove(T item);
        void Remove(Guid id);
        void RemoveAll(Expression<Func<T, bool>> expression);
        T GetById(Guid id);
        T GetByDefault(Expression<Func<T, bool>> expression);
        List<T> GetActives();
        List<T> GetDefaults(Expression<Func<T, bool>> expression);
        List<T> GetAll();
        bool Any(Expression<Func<T, bool>> expression);
        int Save();
    }
}
