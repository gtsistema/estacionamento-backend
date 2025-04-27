using Gp.Domain.Interface.Repositories;
using Gp.Domain.Models;
using Gp.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Gp.Infra.Repository
{
    public class BaseRepositories<T> : IBaseRepositories<T> where T : Base
    {
        protected readonly GpContext _context;
        public BaseRepositories(GpContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteAsync(long id)
        {
            try
            {
                var result = await _context.Set<T>().SingleOrDefaultAsync(p => p.Id.Equals(id));
                if (result == null)
                {
                    return false;
                }

                _context.Remove(result);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ExistAsync(long id)
        {
            return await _context.Set<T>().AsNoTracking().AnyAsync(p => p.Id.Equals(id));
        }

        public async Task<T> InsertAsync(T item)
        {
            try
            {
                if (item.Id == 0)
                {
                    item.DataCriacao = DateTime.Now;
                    _context.Add(item);
                }

                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }

            return item;
        }

        public async Task<T> SelectAsync(long id)
        {
            try
            {
                return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(p => p.Id.Equals(id));
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }

        public Task<IQueryable<T>> SelectAllAsync()
        {
            try
            {
                return Task.FromResult(_context.Set<T>().AsNoTracking().AsQueryable());
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }

        public async Task<T> UpdateAsync(T item)
        {
            try
            {
                var result = await _context.Set<T>().SingleOrDefaultAsync(p => p.Id.Equals(item.Id));
                if (result == null)
                {
                    return null;
                }

                item.DataAtualizacao = DateTime.Now;
                item.DataCriacao = result.DataCriacao;

                _context.Entry(result).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }

            return item;
        }


        public async Task<T> InsertNoSaveChangesAsync(T item)
        {
            try
            {
                if (item.Id == 0)
                {
                    item.DataCriacao = DateTime.Now;
                    await _context.AddAsync(item);
                }
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }

            return item;
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }

        public async Task<long?> GetIdByDescricaoAsync(string descricao)
        {
            return await _context.Set<T>().AsNoTracking()
                .Where(p => descricao.ToLower().Trim().Contains(p.Descricao.ToLower()))
                .Select(p => (long?)p.Id)
                .FirstOrDefaultAsync();
        }

        public long LastCodeTable()
        {
            long? codigo = _context.Set<T>().OrderByDescending(s => s.Id).FirstOrDefault()?.Id;

            return codigo == null ? 1 : codigo.Value + 1;
        }
    }
}
