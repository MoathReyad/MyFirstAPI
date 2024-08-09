using AutoMapper;
using MoathAPI.DTOs;
using MoathAPI.Models;

namespace MoathAPI.Mapping
{
    public class StudentMapper : Profile
    {

        public StudentMapper() 
        {
            CreateMap<AddDTO, Student>().ReverseMap();
            CreateMap<UpdateDTO, Student>().ReverseMap();
            CreateMap<Student, GetDTOByID>().ReverseMap();
            CreateMap<Student, GetAllDTO>().ReverseMap();
        }
    }
}
