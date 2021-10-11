using AutoMapper;
using CentWorkTimeTracker.Dtos;
using CentWorkTimeTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CentWorkTimeTracker.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserReadDto>();
            CreateMap<UserAddDto, User>();
        }
    }
}