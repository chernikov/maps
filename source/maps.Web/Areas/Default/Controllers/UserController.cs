using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tool;
using maps.Web.Mail;
using maps.Model;
using maps.Web.Models.ViewModels.User;
using maps.Social.Facebook;
using Newtonsoft.Json;
using maps.Web.Models.ViewModels;
using System.Net;
using ImageResizer;
using System.Drawing;

namespace maps.Web.Areas.Default.Controllers
{
    public class UserController : DefaultController
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private FbProvider fbProvider;

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            fbProvider = new FbProvider();
            fbProvider.Config = Config.FacebookAppConfig;
            base.Initialize(requestContext);
        }


        #region Facebook

        public ActionResult Connect()
        {
            return Redirect(fbProvider.Authorize("http://" + HostName + "/User/SaveFbCode"));
        }

        public ActionResult SaveFbCode()
        {
            if (Request.Params.AllKeys.Contains("code"))
            {
                var code = Request.Params["code"];
                return ProcessFbCode(code);
            }
            return RedirectBack("~/");
        }

        protected ActionResult ProcessFbCode(string code)
        {
            if (fbProvider.GetAccessToken(code, "http://" + HostName + "/User/SaveFbCode"))
            {
                var jObj = fbProvider.GetUserInfo();
                var fbUserInfo = JsonConvert.DeserializeObject<FbUserInfo>(jObj.ToString());
                var identifier = fbUserInfo.Identifier;
                if (TryLogin(identifier))
                {
                    return RedirectBack("~/");
                }
                var registerView = new SocialRegisterUserView()
                {
                    Avatar = string.Format("http://graph.facebook.com/{0}/picture", fbUserInfo.Id),
                    Email = fbUserInfo.Email,
                    FirstName = fbUserInfo.FirstName,
                    LastName = fbUserInfo.LastName,
                    Login = fbUserInfo.Name,
                    Identifier = fbUserInfo.Identifier,
                    Provider = Model.Social.ProviderType.facebook,
                    UserInfo = jObj.ToString()
                };
                try
                {
                    //проверить логин (при необходимости - изменить)
                    registerView.Login = Translit.Translate(registerView.Login).ToLower();
                    registerView.Login = registerView.Login.Replace("-", "_");
                    //создать аккаунт 
                    var user = (User)ModelMapper.Map(registerView, typeof(SocialRegisterUserView), typeof(User));
                    
                    user.Password = StringExtension.GenerateNewFile();
                    user.CityID = CurrentCity.ID;
                    Repository.CreateUser(user);

                    //создать Social

                    var social = new Model.Social()
                    {
                        Identified = registerView.Identifier,
                        Provider = (int)registerView.Provider,
                        UserInfo = registerView.UserInfo,
                        JsonResource = string.Empty,
                        UserID = user.ID
                    };
                    Repository.CreateSocial(social);

                    //скачать картинку (если есть)
                    if (!string.IsNullOrWhiteSpace(registerView.Avatar))
                    {
                        CreateAvatar(user, registerView.Avatar);
                    }
                    Auth.Login(user.Login);
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    logger.Debug("Can't initialize : " + registerView.UserInfo);
                }
            }
            return RedirectBack("~/");
        }
        #endregion

        private bool TryLogin(string identifier)
        {
            var social = Repository.Socials.FirstOrDefault(p => p.Identified == identifier);

            if (social != null)
            {
                Auth.Login(social.User.Login);
                return true;
            }
            return false;
        }

        public void CreateAvatar(User user, string pictureUrl)
        {
            var webClient = new WebClient();
            var bytes = webClient.DownloadData(pictureUrl);
            var ms = new MemoryStream(bytes);

            var uFile = StringExtension.GenerateNewFile() + ".jpg";
            var filePath = Path.Combine(Path.Combine(Server.MapPath("~"), DestinationDir), uFile);
            ImageBuilder.Current.Build(ms, filePath, new ResizeSettings("maxwidth=1600&crop=auto"));

            user.AvatarPath = "/" + DestinationDir + uFile;
            Repository.UpdateUser(user);

        }


    }
}