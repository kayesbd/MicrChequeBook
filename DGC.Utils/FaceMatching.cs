using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using Nuspay.ImageProcessing;
using Nuspay.ImageProcessing.Contract;

namespace NusPay.Utils
{
    public class FaceMatching
    {
        public async Task<bool> ImageFilesMatching(string photoPath, string receivedPhotoBase64)
        {
            try
            {
                Lib lib = new Lib();
                Stream imageFileStream = new FileStream(photoPath, FileMode.Open);
                Guid image1Guid = lib.GetImageGuid(imageFileStream);
                imageFileStream.Close();

                byte[] data = System.Convert.FromBase64String(receivedPhotoBase64);
                MemoryStream ms = new MemoryStream(data);
                Image img = Image.FromStream(ms);

                var stream = new System.IO.MemoryStream();
                img.Save(stream, ImageFormat.Jpeg);
                stream.Position = 0;
                Guid image2Guid = lib.GetImageGuid(stream);
                VerifyResult result = lib.Verify(image2Guid, image1Guid);
                return result.IsIdentical;

            }
            catch (Exception exceptionn)
            {
                return false;
            }
        }
    }
}
