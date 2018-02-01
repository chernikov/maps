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
            public static void Init(IMapperConfigurationExpression cfg)
            {
               cfg.CreateMap<User, LoginView>();
               cfg.CreateMap<LoginView, User>();
            }
        }

        public static class UserMapper
        {
            public static void Init(IMapperConfigurationExpression cfg)
            {
               cfg.CreateMap<User, RegisterUserView>();
               cfg.CreateMap<RegisterUserView, User>();
               cfg.CreateMap<SocialRegisterUserView, User>();

               cfg.CreateMap<User, UserView>();
               cfg.CreateMap<UserView, User>();
            }
        }
        
        public static class BycicleDirectionMapper
        {
        	public static void Init(IMapperConfigurationExpression cfg)
        	{
        		cfg.CreateMap<BycicleDirection, BycicleDirectionView>();
        		cfg.CreateMap<BycicleDirectionView, BycicleDirection>();
        	}
        }
        
        public static class BicycleParkingMapper
        {
        	public static void Init(IMapperConfigurationExpression cfg)
        	{
        		cfg.CreateMap<BicycleParking, BicycleParkingView>();
               cfg.CreateMap<BicycleParkingView, BicycleParking>();
               cfg.CreateMap<AdminBicycleParkingView, BicycleParking>();
               cfg.CreateMap<BicycleParking, AdminBicycleParkingView>();

               cfg.CreateMap<BicycleParking, BicycleParkingDto>();
               cfg.CreateMap<BicycleParkingApiModel, BicycleParking>();
        	}
        }

        
        public static class GoalMapper
        {
        	public static void Init(IMapperConfigurationExpression cfg)
        	{
        		cfg.CreateMap<Goal, GoalView>();
        		cfg.CreateMap<GoalView, Goal>();
        	}
        }

        
        public static class CityMapper
        {
        	public static void Init(IMapperConfigurationExpression cfg)
        	{
        		cfg.CreateMap<City, CityView>();
        		cfg.CreateMap<CityView, City>();
        	}
        }

        
        public static class UtilityIssueMapper
        {
        	public static void Init(IMapperConfigurationExpression cfg)
        	{
        		cfg.CreateMap<UtilityIssue, NewUtilityIssueView>();
        		cfg.CreateMap<NewUtilityIssueView, UtilityIssue>();

               cfg.CreateMap<UtilityIssue, UtilityIssueView>()
                      .ForMember(dest => dest.Photos,
                        opt => opt.MapFrom(p =>
                        p.UtilityPhotos.Where(r => !r.IsRemoved).Select(r => new KeyValuePair<string, UtilityPhotoView>(Guid.NewGuid().ToString("N"), (UtilityPhotoView)Mapper.Map(r, typeof(UtilityPhoto), typeof(UtilityPhotoView))))))
                      .ForMember(dest => dest.UtilityTagList,
                        opt => opt.MapFrom(p => p.UtilityIssueTags.Select(r => r.UtilityTagID)));
               cfg.CreateMap<UtilityIssueView, UtilityIssue>();
             
        	}
        }

        
        public static class UtilityTagMapper
        {
        	public static void Init(IMapperConfigurationExpression cfg)
        	{
        		cfg.CreateMap<UtilityTag, UtilityTagView>();
        		cfg.CreateMap<UtilityTagView, UtilityTag>();
        	}
        }

        public static class UtilityPhotoMapper
        {
            public static void Init(IMapperConfigurationExpression cfg)
            {
               cfg.CreateMap<UtilityPhoto, UtilityPhotoView>();
               cfg.CreateMap<UtilityPhotoView, UtilityPhoto>();
            }
        }

        public static class UtilityDepartmentMapper
        {
            public static void Init(IMapperConfigurationExpression cfg)
            {
               cfg.CreateMap<UtilityDepartment, UtilityDepartmentView>();
               cfg.CreateMap<UtilityDepartmentView, UtilityDepartment>();
            }
        }

        public static class CommentMapper
        {
            public static void Init(IMapperConfigurationExpression cfg)
            {
               cfg.CreateMap<Comment, CommentView>();
               cfg.CreateMap<CommentView, Comment>();
            }
        }

        public static class TransporteurMapper
        {
            public static void Init(IMapperConfigurationExpression cfg)
            {
               cfg.CreateMap<Transporteur, TransporteurView>();
               cfg.CreateMap<TransporteurView, Transporteur>();
            }
        }

        public static class RouteMapper
        {
            public static void Init(IMapperConfigurationExpression cfg)
            {
               cfg.CreateMap<Route, RouteView>();
               cfg.CreateMap<RouteView, Route>();
            }
        }

        public static class BrandMapper
        {
            public static void Init(IMapperConfigurationExpression cfg)
            {
               cfg.CreateMap<Brand, BrandView>();
               cfg.CreateMap<BrandView, Brand>();
            }
        }

        public static class BusMapper
        {
            public static void Init(IMapperConfigurationExpression cfg)
            {
               cfg.CreateMap<Bus, BusView>();
               cfg.CreateMap<BusView, Bus>();
            }
        }

        public static class FundamentalRuleMapper
        {
            public static void Init(IMapperConfigurationExpression cfg)
            {
               cfg.CreateMap<FundamentalRule, FundamentalRuleView>();
               cfg.CreateMap<FundamentalRuleView, FundamentalRule>();
            }
        }

        public static class RuleMapper
        {
            public static void Init(IMapperConfigurationExpression cfg)
            {
               cfg.CreateMap<Rule, RuleView>();
               cfg.CreateMap<RuleView, Rule>();
            }
        }

        public static class ReportMapper
        {
            public static void Init(IMapperConfigurationExpression cfg)
            {
               cfg.CreateMap<CreateReportView, Report>();
               cfg.CreateMap<NewReportView, Report>();
            }

        }
        public static class ReportPhotoMapper
        {
            public static void Init(IMapperConfigurationExpression cfg)
            {
               cfg.CreateMap<ReportPhoto, ReportPhotoView>();
               cfg.CreateMap<ReportPhotoView, ReportPhoto>();
            }
        }

        public static class ReportAnswerMapper
        {
            public static void Init(IMapperConfigurationExpression cfg)
            {
               cfg.CreateMap<ReportAnswer,  ReportAnswerView>();
               cfg.CreateMap<ReportAnswerView,  ReportAnswer>();
            }
        }

        public static class AccessibleDirectionMapper
        {
            public static void Init(IMapperConfigurationExpression cfg)
            {
               cfg.CreateMap<AccessibleDirection, AccessibleDirectionView>();
               cfg.CreateMap<AccessibleDirectionView, AccessibleDirection>();
            }
        }

        public static class AccessibleObjectMapper
        {
            public static void Init(IMapperConfigurationExpression cfg)
            {
               cfg.CreateMap<AccessibleObject, AccessibleObjectView>();
               cfg.CreateMap<AccessibleObjectView, AccessibleObject>();
            }
        }

        public static class AccessibleObjectPhotoMapper
        {
            public static void Init(IMapperConfigurationExpression cfg)
            {
               cfg.CreateMap<AccessibleObjectPhoto, AccessibleObjectPhotoView>();
               cfg.CreateMap<AccessibleObjectPhotoView, AccessibleObjectPhoto>();
            }
        }

        public static class AccessiblePlaceMapper
        {
            public static void Init(IMapperConfigurationExpression cfg)
            {
               cfg.CreateMap<AccessiblePlace, AccessiblePlaceView>();
               cfg.CreateMap<AccessiblePlaceView, AccessiblePlace>();
            }
        }

        public static class AccessiblePlacePhotoMapper
        {
            public static void Init(IMapperConfigurationExpression cfg)
            {
               cfg.CreateMap<AccessiblePlacePhoto, AccessiblePlacePhotoView>();
               cfg.CreateMap<AccessiblePlacePhotoView, AccessiblePlacePhoto>();
            }
        }

        public static class VisualizationItemMapper
        {
            public static void Init(IMapperConfigurationExpression cfg)
            {
               cfg.CreateMap<VisualizationItem, VisualizationItemView>();
               cfg.CreateMap<VisualizationItemView, VisualizationItem>()
                    .ForMember(p => p.DataItems, opt => opt.Ignore());
            }
        }
    }
}