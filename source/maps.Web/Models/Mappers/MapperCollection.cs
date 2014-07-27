using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using maps.Model;
using maps.Web.Models.ViewModels;
using maps.Web.Models.ViewModels.User;

namespace maps.Web.Models.Mappers
{
    public static class MapperCollection
    {
        public static class LoginUserMapper
        {
            public static void Init()
            {
                Mapper.CreateMap<User, LoginView>();
                Mapper.CreateMap<LoginView, User>();
            }
        }

        public static class UserMapper
        {
            public static void Init()
            {
                Mapper.CreateMap<User, RegisterUserView>();
                Mapper.CreateMap<RegisterUserView, User>();
                Mapper.CreateMap<SocialRegisterUserView, User>();

                Mapper.CreateMap<User, UserView>();
                Mapper.CreateMap<UserView, User>();
            }
        }

        
        public static class BycicleDirectionMapper
        {
        	public static void Init()
        	{
        		Mapper.CreateMap<BycicleDirection, BycicleDirectionView>();
        		Mapper.CreateMap<BycicleDirectionView, BycicleDirection>();
        	}
        }

        
        public static class BicycleParkingMapper
        {
        	public static void Init()
        	{
        		Mapper.CreateMap<BicycleParking, BicycleParkingView>();
                Mapper.CreateMap<BicycleParkingView, BicycleParking>()
                    .ForMember(p => p.Type, opt => opt.Ignore());
        	}
        }
    }
}