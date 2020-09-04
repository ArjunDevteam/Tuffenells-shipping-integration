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
            doc.LoadFromFile(pdfPath);
            Image bmp = doc.SaveAsImage(0);
            Image emf = doc.SaveAsImage(0, Spire.Pdf.Graphics.PdfImageType.Bitmap);
            Image zoomImg = new Bitmap((int)(emf.Size.Width * 2), (int)(emf.Size.Height * 2));
            using (Graphics g = Graphics.FromImage(zoomImg))
            {
                g.ScaleTransform(2.0f, 2.0f);
                g.DrawImage(emf, new Rectangle(new Point(0, 0), emf.Size), new Rectangle(new Point(0, 0), emf.Size), GraphicsUnit.Pixel);
            }
            //bmp.Save(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files\PDF\convertToBmp.bmp"), ImageFormat.Bmp);
            emf.Save(imagePath, ImageFormat.Png);
            //zoomImg.Save(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files\PDF\convertToZoom.png"), ImageFormat.Png);

            return string.Empty;
        }
    }
}
