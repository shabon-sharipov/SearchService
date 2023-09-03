using SearchService.Models;

namespace SearchService.Services.Interfaces;

public interface IStudentService
{
    Task<List<StudentResponsModel>> GetAll(int pageSize, int pageNumber, CancellationToken cancellationToken = default);
    Task<List<StudentResponsModel>> Search(string searchSymbol, CancellationToken cancellationToken = default);
}