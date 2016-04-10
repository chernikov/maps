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

        public static class TransporteurMapper
        {
            public static void Init()
            {
                Mapper.CreateMap<Transporteur, TransporteurView>();
                Mapper.CreateMap<TransporteurView, Transporteur>();
            }
        }

        public static class RouteMapper
        {
            public static void Init()
            {
                Mapper.CreateMap<Route, RouteView>();
                Mapper.CreateMap<RouteView, Route>();
            }
        }

        public static class BrandMapper
        {
            public static void Init()
            {
                Mapper.CreateMap<Brand, BrandView>();
                Mapper.CreateMap<BrandView, Brand>();
            }
        }

        public static class BusMapper
        {
            public static void Init()
            {
                Mapper.CreateMap<Bus, BusView>();
                Mapper.CreateMap<BusView, Bus>();
            }
        }

        public static class FundamentalRuleMapper
        {
            public static void Init()
            {
                Mapper.CreateMap<FundamentalRule, FundamentalRuleView>();
                Mapper.CreateMap<FundamentalRuleView, FundamentalRule>();
            }
        }

        public static class RuleMapper
        {
            public static void Init()
            {
                Mapper.CreateMap<Rule, RuleView>();
                Mapper.CreateMap<RuleView, Rule>();
            }
        }

        public static class ReportMapper
        {
            public static void Init()
            {
                Mapper.CreateMap<CreateReportView, Report>();
                Mapper.CreateMap<NewReportView, Report>();
            }

        }
        public static class ReportPhotoMapper
        {
            public static void Init()
            {
                Mapper.CreateMap<ReportPhoto, ReportPhotoView>();
                Mapper.CreateMap<ReportPhotoView, ReportPhoto>();
            }
        }

        public static class ReportAnswerMapper
        {
            public static void Init()
            {
                Mapper.CreateMap<ReportAnswer,  ReportAnswerView>();
                Mapper.CreateMap<ReportAnswerView,  ReportAnswer>();
            }
        }

        public static class AccessibleDirectionMapper
        {
            public static void Init()
            {
                Mapper.CreateMap<AccessibleDirection, AccessibleDirectionView>();
                Mapper.CreateMap<AccessibleDirectionView, AccessibleDirection>();
            }
        }

        public static class AccessibleObjectMapper
        {
            public static void Init()
            {
                Mapper.CreateMap<AccessibleObject, AccessibleObjectView>();
                Mapper.CreateMap<AccessibleObjectView, AccessibleObject>();
            }
        }

        public static class AccessibleObjectPhotoMapper
        {
            public static void Init()
            {
                Mapper.CreateMap<AccessibleObjectPhoto, AccessibleObjectPhotoView>();
                Mapper.CreateMap<AccessibleObjectPhotoView, AccessibleObjectPhoto>();
            }
        }

        public static class AccessiblePlaceMapper
        {
            public static void Init()
            {
                Mapper.CreateMap<AccessiblePlace, AccessiblePlaceView>();
                Mapper.CreateMap<AccessiblePlaceView, AccessiblePlace>();
            }
        }

        public static class AccessiblePlacePhotoMapper
        {
            public static void Init()
            {
                Mapper.CreateMap<AccessiblePlacePhoto, AccessiblePlacePhotoView>();
                Mapper.CreateMap<AccessiblePlacePhotoView, AccessiblePlacePhoto>();
            }
        }

        public static class VisualizationItemMapper
        {
            public static void Init()
            {
                Mapper.CreateMap<VisualizationItem, VisualizationItemView>();
                Mapper.CreateMap<VisualizationItemView, VisualizationItem>()
                    .ForMember(p => p.DataItems, opt => opt.Ignore());
            }
        }
    }
}