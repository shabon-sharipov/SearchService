using SearchService.Models;

namespace SearchService.ElasticSearchRepository;

public interface IElasticSearchRepository<TEntity> where TEntity : class
{
    Task<List<TEntity>> GetAllData(int pageSize, int pageNumber, CancellationToken cancellationToken = default);

    Task<List<TEntity>> Search(string searchSymbol, CancellationToken cancellationToken = default);
}