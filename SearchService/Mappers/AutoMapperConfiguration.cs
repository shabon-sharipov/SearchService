using AutoMapper;
using SearchService.Models;

namespace SearchService.Mappers;

public class AutoMapperConfiguration:Profile
{
    public AutoMapperConfiguration()
    {
        CreateMap<Student, StudentResponsModel>();
    }
}