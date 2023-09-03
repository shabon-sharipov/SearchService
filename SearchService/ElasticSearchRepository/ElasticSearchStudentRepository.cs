using Nest;
using SearchService.Models;

namespace SearchService.ElasticSearchRepository;

public class ElasticSearchStudentRepository : IElasticSearchRepository<Student>
{
    private readonly ConnectionSettings _connectionSettings;

    public ElasticSearchStudentRepository(IConfiguration configuration)
    {
        _connectionSettings = new ConnectionSettings(new Uri(configuration["ElasticSearchConfiguration:Uri"]))
                        .DefaultIndex(configuration["ElasticSearchConfiguration:StudentIndex"]);
    }

    public async Task<List<Student>> GetAllData(int pageSize, int pageNumber, CancellationToken cancellationToken = default)
    {
        try
        {
            var client = new ElasticClient(_connectionSettings);

            // Calculate the number of documents to skip
            var skipCount = (pageNumber - 1) * pageSize;

            // Search for documents with pagination
            var searchResponse = await client.SearchAsync<Student>(s => s
                .MatchAll()
                .Skip(skipCount)
                .Take(pageSize)
            , cancellationToken);

            // Get the documents from the search response
            var documents = searchResponse.Documents.ToList();

            return documents;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<Student>> Search(string searchSymbol, CancellationToken cancellationToken = default)
    {
        var settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("students");
        var client = new ElasticClient(_connectionSettings);

        var searchResponse = await client.SearchAsync<Student>(s => s
            .Query(q => q
                .Bool(b => b
                    .Should(sh => sh
                        .Match(m => m
                            .Field(f => f.FirstName)
                            .Query($"*{searchSymbol}*")
                        ),
                        sh => sh
                        .Match(m => m
                            .Field(f => f.LastName)
                            .Query($"*{searchSymbol}*")
                        )
                    )
                )
            )
        , cancellationToken);

        if (!searchResponse.IsValid && !searchResponse.Documents.Any())
            throw new Exception();

        var searchResults = searchResponse.Documents.ToList();

        return searchResults;
    }
}