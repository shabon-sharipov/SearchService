using SearchService.Models;

namespace SearchService.Services.Interfacec;

public interface IRepositoryEs<TEntity> where TEntity:class
{
    Task<List<TEntity>> GetAllData(int pageSize, int pageNumber);
}