using AutoMapper;
using SearchService.ElasticSearchRepository;
using SearchService.Models;
using SearchService.Services.Interfacec;

namespace SearchService.Services;

public class StudentService : IStudentService
{
    private IMapper _mapper;
    private IRepositoryEs<Student> _repositoryEs;
    private SearchReposiroty _searchReposiroty;

    public StudentService(IRepositoryEs<Student> repositoryEs, IMapper mapper, SearchReposiroty searchReposiroty)
    {
        _repositoryEs = repositoryEs;
        _mapper = mapper;
        _searchReposiroty = searchReposiroty;
    }

    public async Task<List<StudentResponsModel>> GetAll(int pageSize, int pageNumber)
    {
        try
        {
            var result = await _repositoryEs.GetAllData(pageSize, pageNumber);

            return _mapper.Map<List<StudentResponsModel>>(result);
        }
        catch (Exception e)
        {
          throw e;
        }
    }

    public async Task<List<StudentResponsModel>> Search(string searchSymbol, CancellationToken cancellationToken)
    {
        var result = await _searchReposiroty.Search(searchSymbol, cancellationToken);

        return _mapper.Map<List<StudentResponsModel>>(result);
    }
}