using ImageResizer;
using maps.Model;
using maps.Web.Global;
using maps.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tool;

namespace maps.Web.Areas.Utility.Controllers
{
    public class UtilityPhotoController : UtilityController
    {
        [ValidateInput(false)]
        [HttpPost]
        public FineUploaderResult Upload(FineUpload upload)
        {
            var uDir = "Content/files/utilities/";
            var extension = Path.GetExtension(upload.Filename);
            var uFile = StringExtension.GenerateNewFile() + extension;
            var filePath = Path.Combine(Path.Combine(Server.MapPath("~"), uDir), uFile);
            try
            {
                ImageBuilder.Current.Build(upload.InputStream, filePath, new ResizeSettings("maxwidth=1600&crop=auto"));
                var utilityPhoto = new Model.UtilityPhoto()
                {
                    Image = "/" + uDir + uFile,
                    State = (int)UtilityPhoto.Type.Problem,
                };
                Repository.CreateUtilityPhoto(utilityPhoto);
                return new FineUploaderResult(true, new { utilityPhoto });
            }
            catch (Exception ex)
            {
                return new FineUploaderResult(false, error: ex.Message);
            }
        }

        public ActionResult Item(int id)
        {
            var utilityPhoto = Repository.UtilityPhotos.FirstOrDefault(p => p.ID == id);
            if (utilityPhoto != null)
            {
                var utilityPhotoView = (UtilityPhotoView)ModelMapper.Map(utilityPhoto, typeof(UtilityPhoto), typeof(UtilityPhotoView));
                return View("UtilityPhoto", new KeyValuePair<string, UtilityPhotoView>(Guid.NewGuid().ToString("N"), utilityPhotoView));
            }
            return null;
        }
	}
}