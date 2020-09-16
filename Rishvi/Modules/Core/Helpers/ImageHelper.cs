using Spinx.Web.Modules.Core.Aws;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Rishvi.Modules.Core.Helpers
{
    public static class ImageHelper
    {
        public static string PDFtoImage(string pdfPath, string imagePath)
        {
            PdfDocument doc = new PdfDocument();
            Stream S3Stream = AwsS3.GetS3ImageForInputStream(pdfPath);
            doc.LoadFromStream(S3Stream);
            Image bmp = doc.SaveAsImage(0);
            Image emf = doc.SaveAsImage(0, Spire.Pdf.Graphics.PdfImageType.Bitmap);
            Image zoomImg = new Bitmap((int)(emf.Size.Width * 2), (int)(emf.Size.Height * 2));
            using (Graphics g = Graphics.FromImage(zoomImg))
            {
                g.ScaleTransform(2.0f, 2.0f);
                g.DrawImage(emf, new Rectangle(new Point(0, 0), emf.Size), new Rectangle(new Point(0, 0), emf.Size), GraphicsUnit.Pixel);
            }
            //bmp.Save(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files\PDF\convertToBmp.bmp"), ImageFormat.Bmp);
            //emf.Save(imagePath, ImageFormat.Png);
            //zoomImg.Save(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files\PDF\convertToZoom.png"), ImageFormat.Png);
            //save image into S3 Bukets
            Stream stream = ToStream(emf, ImageFormat.Png);
            AwsS3.UploadFileToS3(stream, imagePath);
            return string.Empty;
        }

        public static Stream ToStream(Image image, ImageFormat format)
        {
            var stream = new System.IO.MemoryStream();
            image.Save(stream, format);
            stream.Position = 0;
            return stream;
        }
    }
}
