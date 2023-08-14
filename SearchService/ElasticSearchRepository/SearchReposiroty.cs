using Nest;
using SearchService.Models;

namespace SearchService.ElasticSearchRepository
{
    public class SearchReposiroty
    {
        public async Task<List<Student>> Search(string searchSymbol, CancellationToken cancellationToken)
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("students");
            var client = new ElasticClient(settings);

            var searchResponse = client.Search<Student>(s => s
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
            );

            if (!searchResponse.IsValid && !searchResponse.Documents.Any())
                throw new Exception();

            var searchResults = searchResponse.Documents.ToList();

            return searchResults;
        }
    }
}
