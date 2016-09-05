using maps.Web.Controllers;
using maps.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace maps.Web.Areas.Sport.Controllers
{
    public class HomeController :BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View(new SportAvatarView());
        }

        [HttpGet]
        public ActionResult Download(string PhotoUrl)
        {
            var sourceImage = Image.FromFile(Server.MapPath("~" + PhotoUrl));
            var secondImage = Image.FromFile(Server.MapPath("~/Content/images/ava/illlegal.png"));
            var data = MergeTwoImages(sourceImage, secondImage);
            FileContentResult result;
            using (var memStream = new System.IO.MemoryStream())
            {
                ImageCodecInfo jgpEncoder = GetEncoder(ImageFormat.Jpeg);
                System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                var myEncoderParameter = new EncoderParameter(myEncoder, 100L);
                EncoderParameters myEncoderParameters = new EncoderParameters(1);
                myEncoderParameters.Param[0] = myEncoderParameter;

                data.Save(memStream, jgpEncoder, myEncoderParameters);
                result = this.File(memStream.GetBuffer(), "image/jpeg");
            }
            return result;
        }


        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        public static Bitmap MergeTwoImages(Image firstImage, Image secondImage)
        {
            if (firstImage == null)
            {
                throw new ArgumentNullException("firstImage");
            }

            if (secondImage == null)
            {
                throw new ArgumentNullException("secondImage");
            }

            int outputImageWidth = 800;

            int outputImageHeight = 800;

            Bitmap outputImage = new Bitmap(outputImageWidth, outputImageHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            using (Graphics graphics = Graphics.FromImage(outputImage))
            {
                graphics.DrawImage(firstImage, new Rectangle(new Point(), firstImage.Size),
                    new Rectangle(new Point(), firstImage.Size), GraphicsUnit.Pixel);
                graphics.DrawImage(secondImage, new Rectangle(new Point(), secondImage.Size),
                    new Rectangle(new Point(), secondImage.Size), GraphicsUnit.Pixel);
            }

            return outputImage;
        }
    }
}