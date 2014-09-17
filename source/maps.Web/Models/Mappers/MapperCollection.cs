using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using maps.Model;
using maps.Web.Models.Api;
using maps.Web.Models.Dto;
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
                Mapper.CreateMap<BicycleParkingView, BicycleParking>();
                Mapper.CreateMap<AdminBicycleParkingView, BicycleParking>();
                Mapper.CreateMap<BicycleParking, AdminBicycleParkingView>();

                Mapper.CreateMap<BicycleParking, BicycleParkingDto>();
                Mapper.CreateMap<BicycleParkingApiModel, BicycleParking>();
        	}
        }

        
        public static class GoalMapper
        {
        	public static void Init()
        	{
        		Mapper.CreateMap<Goal, GoalView>();
        		Mapper.CreateMap<GoalView, Goal>();
        	}
        }

        
        public static class CityMapper
        {
        	public static void Init()
        	{
        		Mapper.CreateMap<City, CityView>();
        		Mapper.CreateMap<CityView, City>();
        	}
        }

        
        public static class UtilityIssueMapper
        {
        	public static void Init()
        	{
        		Mapper.CreateMap<UtilityIssue, NewUtilityIssueView>();
        		Mapper.CreateMap<NewUtilityIssueView, UtilityIssue>();

                Mapper.CreateMap<UtilityIssue, UtilityIssueView>()
                      .ForMember(dest => dest.Photos,
                        opt => opt.MapFrom(p =>
                        p.UtilityPhotos.Where(r => !r.IsRemoved).Select(r => new KeyValuePair<string, UtilityPhotoView>(Guid.NewGuid().ToString("N"), (UtilityPhotoView)Mapper.Map(r, typeof(UtilityPhoto), typeof(UtilityPhotoView))))))
                      .ForMember(dest => dest.UtilityTagList,
                        opt => opt.MapFrom(p => p.UtilityIssueTags.Select(r => r.UtilityTagID)));
                Mapper.CreateMap<UtilityIssueView, UtilityIssue>();
             
        	}
        }

        
        public static class UtilityTagMapper
        {
        	public static void Init()
        	{
        		Mapper.CreateMap<UtilityTag, UtilityTagView>();
        		Mapper.CreateMap<UtilityTagView, UtilityTag>();
        	}
        }

        public static class UtilityPhotoMapper
        {
            public static void Init()
            {
                Mapper.CreateMap<UtilityPhoto, UtilityPhotoView>();
                Mapper.CreateMap<UtilityPhotoView, UtilityPhoto>();
            }
        }

        public static class UtilityDepartmentMapper
        {
            public static void Init()
            {
                Mapper.CreateMap<UtilityDepartment, UtilityDepartmentView>();
                Mapper.CreateMap<UtilityDepartmentView, UtilityDepartment>();
            }
        }

        public static class CommentMapper
        {
            public static void Init()
            {
                Mapper.CreateMap<Comment, CommentView>();
                Mapper.CreateMap<CommentView, Comment>();
            }
        }
    }
}