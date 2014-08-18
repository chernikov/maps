using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ImageResizer;
using maps.Web.Global;
using Newtonsoft.Json;
using Tool;
using System.Net.Http.Formatting;

namespace maps.Web.Controllers
{
    public class UploadController : BaseApiController
    {

        public class FileUploadAnswer
        {
            public bool success { get; set; }

            public List<KeyValuePair<string, string>> files { get; set; }

            public string error { get; set; }
        }

        public async Task<HttpResponseMessage> PostFormData()
        {

            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                var list = new List<KeyValuePair<string, string>>();
                foreach (var file in provider.FileData)
                {
                    var uFile = StringExtension.GenerateNewFile() +
                                Path.GetExtension(file.Headers.ContentDisposition.FileName.Replace("\"", ""));
                    var filePath = Path.Combine(Path.Combine(HttpContext.Current.Server.MapPath("~"), DestinationDir),
                        uFile);
                    using (var fs = new FileStream(file.LocalFileName, FileMode.Open, FileAccess.Read))
                    {
                        ImageBuilder.Current.Build(fs, filePath, new ResizeSettings("maxwidth=1600&crop=auto"));
                        list.Add(new KeyValuePair<string, string>(
                            file.Headers.ContentDisposition.FileName.Replace("\"", ""),
                            "/" + DestinationDir + uFile));
                    }
                }
                var response = Request.CreateResponse(HttpStatusCode.OK);
                response.Content = new ObjectContent(typeof (FileUploadAnswer),
                    new FileUploadAnswer()
                    {
                        success = true,
                        files = list
                    }, new JsonMediaTypeFormatter(), "application/json");
                return response;
            }
            catch (System.Exception ex)
            {
                
                var responseFail = Request.CreateResponse(HttpStatusCode.InternalServerError);
                responseFail.Content = new ObjectContent(typeof(FileUploadAnswer),
                new FileUploadAnswer()
                {
                    success = false,
                    error = ex.Message
                }, new JsonMediaTypeFormatter(), "application/json");
                return responseFail;
            }
            finally
            {
                try
                {
                    foreach (var file in provider.FileData)
                    {
                        var fi = new FileInfo(file.LocalFileName);
                        if (fi.Exists)
                        {
                            fi.Delete();
                        }
                    }
                } catch {}
            }
        }
    }
}