using AutoMapper;
using SearchService.ElasticSearchRepository;
using SearchService.Models;
using SearchService.Services.Interfaces;

namespace SearchService.Services;

public class StudentService : IStudentService
{
    private readonly IElasticSearchRepository<Student> _repository;
    private IMapper _mapper;

    public StudentService(IElasticSearchRepository<Student> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<StudentResponsModel>> GetAll(int pageSize, int pageNumber, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await _repository.GetAllData(pageSize, pageNumber, cancellationToken);

            return _mapper.Map<List<StudentResponsModel>>(result);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<StudentResponsModel>> Search(string searchSymbol, CancellationToken cancellationToken = default)
    {
        var result = await _repository.Search(searchSymbol, cancellationToken);

        return _mapper.Map<List<StudentResponsModel>>(result);
    }
}