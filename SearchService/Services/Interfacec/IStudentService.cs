using SearchService.Models;

namespace SearchService.Services.Interfacec;

public interface IStudentService
{
    Task<List<StudentResponsModel>> GetAll(int pageSize, int pageNumber);
    Task<List<StudentResponsModel>> Search(string searchSymbol, CancellationToken cancellationToken);
}