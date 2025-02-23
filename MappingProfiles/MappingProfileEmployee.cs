using static System.Runtime.InteropServices.JavaScript.JSType;
using ThothSystemVersion1.DataTransfereObject;
using ThothSystemVersion1.Models;
using AutoMapper;

namespace ThothSystemVersion1.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeDTO, Employee>();
            CreateMap<Employee, EmployeeDTO>();
        }
    }
}
