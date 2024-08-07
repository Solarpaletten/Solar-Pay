﻿using Data.Context;
using Data.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DataContext dataContext;
        private readonly DbSet<T> table;
        public Repository(DataContext _dataContext)
        {
            dataContext = _dataContext;
            table = dataContext.Set<T>();
        }
        public T Add(T entity)
        {
           table.Add(entity);
            return entity;
        }

		public bool AddRange(List<T> entities)
		{
            table.AddRange(entities);
            return true;
		}

		public bool Delete(Guid id)
        {
            T entity = table.Find(id);
            table.Remove(entity);
            return true;
        }

        public bool Delete(List<T> Entities)
        {
            table.RemoveRange(Entities);
            return true;
        }

        public IQueryable<T> GetAll()
        {
            return table;
        }

        public T GetById(Guid id)
        {
            return table.Find(id);
        }

		public T GetById(string id)
		{
			return table.Find(id);
		}

		public T Update(T entity)
        {
            table.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }

		public List<T> GetDataFiltered(Expression<Func<T, bool>> condition)
        {
            return table.Where(condition).ToList();
        }


	}
}
