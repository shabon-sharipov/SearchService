using AutoMapper;
using SearchService.Models;
using SearchService.Services.Interfacec;

namespace SearchService.Services;

public class StudentService : IStudentService
{
    private IMapper _mapper;
    private IRepositoryEs<Student> _repositoryEs;

    public StudentService(IRepositoryEs<Student> repositoryEs,IMapper mapper)
    {
        _repositoryEs = repositoryEs;
        _mapper = mapper;
    }

    public async Task<List<StudentResponsModel>> GetAll(int pageSize, int pageNumber)
    {
        var result =await _repositoryEs.GetAllData(pageSize, pageNumber);
    
        return _mapper.Map<List<StudentResponsModel> >(result);;
    }   
}