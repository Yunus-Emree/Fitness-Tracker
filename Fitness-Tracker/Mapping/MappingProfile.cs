using AutoMapper;
using Fitness_Tracker.DTOs;
using Fitness_Tracker.Entities;

namespace Fitness_Tracker.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, RegisterDto>().ReverseMap();
            CreateMap<AppUser, LoginDto>().ReverseMap();
            CreateMap<Exercise, ExerciseDto>().ReverseMap();
            CreateMap<Diet, DietDto>().ReverseMap();

        }
    }
}
