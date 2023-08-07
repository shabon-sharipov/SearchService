using Nest;
using SearchService.Models;
using SearchService.Services.Interfacec;

namespace SearchService.ElasticSearchRepository;

public class RepositoryEs<TEntity> : IRepositoryEs<TEntity> where TEntity:class
{
    public RepositoryEs()
    {
    }

    public async Task<List<TEntity>> GetAllData(int pageSize, int pageNumber)
    {
        var settings = new ConnectionSettings(new Uri("http://localhost:9200"))
            .DefaultIndex("students");

        var client = new ElasticClient(settings);

        // Calculate the number of documents to skip
        var skipCount = (pageNumber - 1) * pageSize;

        // Search for documents with pagination
        var searchResponse = await client.SearchAsync<TEntity>(s => s
            .MatchAll()
            .Skip(skipCount)
            .Take(pageSize)
        );

        // Get the documents from the search response
        var documents = searchResponse.Documents.ToList();

        return documents;
    }
}