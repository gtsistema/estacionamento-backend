using Estac.Domain.Interface.Repositories;
using Estac.Domain.Models.Base;
using Estac.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Estac.Infra.Repository
{
    public class BaseRepositoriesNone<T> : IBaseRepositoriesNone<T> where T : BaseIntDataNull
    {
        protected readonly GtsContext _context;
        public BaseRepositoriesNone(GtsContext context)
        {
            _context = context;
        }
        public async Task<bool> Excluir(long id)
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
            catch (DbUpdateException)
            {
                throw;                  
            }
        }

        public async Task<bool> Existe(long id)
        {
            return await _context.Set<T>().AsNoTracking().AnyAsync(p => p.Id.Equals(id));
        }

        public async Task<T> Gravar(T item)
        {
            try
            {
                if (item.Id == 0)
                {
                    _context.Add(item);
                }

                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException)
            {
                throw;
            }

            return item;
        }

        public async Task<T> Selecionar(long id)
        {
            try
            {
                return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(p => p.Id.Equals(id));
            }
            catch (DbUpdateException )
            {
                throw;
            }
        }

        public Task<IQueryable<T>> SelectAllAsync()
        {
            try
            {
                return Task.FromResult(_context.Set<T>().AsNoTracking().AsQueryable());
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        public async Task<T> Alterar(T item)
        {
            try
            {
                var result = await _context.Set<T>().SingleOrDefaultAsync(p => p.Id.Equals(item.Id));
                if (result == null)
                {
                    return null;
                }

                _context.Entry(result).CurrentValues.SetValues(item);
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateException)
            {
                throw;
            }

            return item;
        }


        public async Task<T> InsertNoSaveChangesAsync(T item)
        {
            try
            {
                if (item.Id == 0)
                {
                    await _context.AddAsync(item);
                }
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return item;
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw;
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
