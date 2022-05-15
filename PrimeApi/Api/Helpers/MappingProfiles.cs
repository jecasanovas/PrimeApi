using AutoMapper;
using BLL.Dtos;
using Core.Entities;

namespace Courses.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<TeacherDto, Teacher>();
            CreateMap<CourseDetail, CourseDetailDto>().ReverseMap();
            CreateMap<UserInfo, UserInfoDto>().ReverseMap();
            CreateMap<Adresses,AdressesDto>().ReverseMap();
            CreateMap<PaymentInfo, PaymentInfoDto>().ReverseMap();
            CreateMap<Technology, TechnologyDto>().ReverseMap();
            CreateMap<TechnologyDetails, TechnologyDetailsDto>().ReverseMap();
            CreateMap<Teacher, TeacherDto>().ForMember(dest => dest.CountryName, opt => opt.MapFrom(t => t.Country.CountryDesc));
            CreateMap<Course, CourseDto>().ForMember(dest => dest.TeacherName, opt => opt.MapFrom(t => t.Teacher.Name + ' ' + t.Teacher.Surname))
                                          .ForMember(dest => dest.TechnologyName, opt => opt.MapFrom(t => t.Technology.Description))
                                          .ForMember(dest => dest.TechnologyDetailsName, opt=> opt.MapFrom(t=> t.TechnologyDetails.Description))
                                          .ForMember(dest => dest.Description, opt => opt.MapFrom(t => t.Description))
                                          .ForMember(dest => dest.TeacherDescription, opt => opt.MapFrom(t => t.Teacher.description))
                                          .ForMember(dest => dest.TeacherPhoto, opt => opt.MapFrom(t => t.Teacher.Photo))
                                          .ForMember(dest => dest.CountryName, opt => opt.MapFrom(t => t.Teacher.Country.CountryDesc));
            CreateMap<CourseDto, Course>();
                                          
        }
    }
}