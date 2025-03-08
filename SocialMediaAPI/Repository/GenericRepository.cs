using Microsoft.EntityFrameworkCore;
using SocialMediaAPI.Abstraction.Repository;
using SocialMediaAPI.Presistence;

namespace SocialMediaAPI.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly SocialMediaDbContext _dbContext;

        public GenericRepository(SocialMediaDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(T entity) => await _dbContext.Set<T>().AddAsync(entity);

        public void Delete(T entity) => _dbContext.Set<T>().Remove(entity);

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbContext.Set<T>().ToListAsync();

        public async Task<T> GetByIdAsync(int id) => await _dbContext.Set<T>().FindAsync(id);

        public void Update(T entity) => _dbContext.Update(entity);
    }
}
