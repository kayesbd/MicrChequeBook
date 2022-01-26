using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace KBZ.Utils
{
    public class Utility
    {
        public static string GenerateMerchantSecurityKey()
        {
            int length = 9;
            Random random = new Random();
            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            string specialCharacters = "!@#$%*_+?:";
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }

            result.Insert(random.Next(9), specialCharacters[random.Next(specialCharacters.Length)]);
            return result.ToString();
        }

        public static string GenerateInstitutionUserSecurityKey()
        {
            int length = 7;
            Random random = new Random();
            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            string specialCharacters = "!@#$%*_+?:";
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }

            result.Insert(random.Next(7), specialCharacters[random.Next(specialCharacters.Length)]);
            return result.ToString();
        }

        public static string GenerateMerchantCategoryCode()
        {
            Random randomPIN = new Random();
            var randomPINResult = randomPIN.Next(0, 9999).ToString();
            return randomPINResult.PadLeft(4, '0');
        }

        public static string GenerateTransactionId(string transactionType, string stan)
        {
            var otpRandom = new Random();
            char padLeft = Convert.ToChar(otpRandom.Next(1, 8).ToString());
            return transactionType + stan + otpRandom.Next(1, 99999999).ToString().PadLeft(8, padLeft);
        }

        public static string GetRetrievalReferenceNumber(DateTime transactionDateTime, string stan)
        {
            var sbTransactionId = new StringBuilder();
            sbTransactionId.AppendFormat("{0}{1}{2}", transactionDateTime.ToString("yy").Last(), transactionDateTime.DayOfYear, transactionDateTime.ToString("HH"));
            sbTransactionId.Append(stan);
            return sbTransactionId.ToString();
        }

        public static string GenerateVirtualAccount()
        {
            var otpRandom = new Random();
            char padLeft = Convert.ToChar(otpRandom.Next(1, 9).ToString());
            return otpRandom.Next(1, 999999999).ToString().PadLeft(10, padLeft);
        }

        public static string GenerateOTP()
        {
            Random randomPIN = new Random();
            var randomPINResult = randomPIN.Next(0, 9999).ToString();
            return randomPINResult.PadLeft(4, '0');
        }

        public static string GenerateUserVerificationCode()
        {
            return new Random().Next(100000, 999999).ToString();
        }

        public static string GenerateMobileVerificationPIN()
        {
            Random randomPIN = new Random();
            var randomPINResult = randomPIN.Next(0, 9999).ToString();
            return randomPINResult.PadLeft(4, '0');
        }

        public static string GetUserDatetime(DateTime utcDateTime, string userTimeZoneId, string countryShortCode)
        {
            TimeZoneInfo userTimeZone = TimeZoneInfo.FindSystemTimeZoneById(userTimeZoneId);
            TimeZone u = TimeZone.CurrentTimeZone;
            DateTime userDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, userTimeZone);
            var culture = CultureInfo.GetCultures(CultureTypes.AllCultures).FirstOrDefault(c => c.Name.EndsWith(countryShortCode.Trim()));
            if (culture != null)
            {
                StringBuilder dateString = new StringBuilder();
                dateString.AppendFormat("{0} {1}", userDateTime.ToString(culture.DateTimeFormat.LongDatePattern), userDateTime.ToString("HH:mm:ss"));
                return dateString.ToString();
            }
            return userDateTime.ToString("MM/dd/yyyy HH:mm:ss");
        }

        public static void RedirectWithData(NameValueCollection data, string url)
        {
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();

            StringBuilder s = new StringBuilder();
            s.Append("<html>");
            s.AppendFormat("<body onload='document.forms[\"form\"].submit()'>");
            s.AppendFormat("<form name='form' action='{0}' method='post'>", url);
            foreach (string key in data)
            {
                s.AppendFormat("<input type='hidden' name='{0}' value='{1}' />", key, data[key]);
            }
            s.Append("</form></body></html>");
            response.Write(s.ToString());
            response.End();
        }

        public static void RedirectWithJsonData(NameValueCollection data, string url)
        {
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();

            StringBuilder s = new StringBuilder();
            foreach (string key in data)
            {
                s.AppendFormat("<input type='hidden' name='{0}' value='{1}' />", key, data[key]);
            }
            s.Append("</form></body></html>");
            response.Write(s.ToString());
            response.End();
        }

        ///// <summary>
        ///// Serializes an object to Xml as a string.
        ///// </summary>
        ///// <typeparam name="T">Datatype T.</typeparam>
        ///// <param name="toSerialize">Object of type T to be serialized.</param>
        ///// <returns>Xml string of serialized type T object.</returns>
        ///// 
        //public static string SerializeToXmlString<T>(T toSerialize)
        //{
        //    string xmlStream;

        //    try
        //    {
        //        using (MemoryStream memstream = new MemoryStream())
        //        {
        //            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
        //            System.Xml.XmlTextWriter xmlWriter = new System.Xml.XmlTextWriter(memstream, Encoding.ASCII);

        //            xmlSerializer.Serialize(xmlWriter, toSerialize);
        //            xmlStream = ASCIIByteArrayToString(((MemoryStream)xmlWriter.BaseStream).ToArray());
        //        }
        //    }
        //    catch (Exception ex) { throw ex; }

        //    return xmlStream;
        //}

        ///// <summary>
        ///// Deserializes Xml string of type T.
        ///// </summary>
        ///// <typeparam name="T">Datatype T.</typeparam>
        ///// <param name="xmlString">Input Xml string from which to read.</param>
        ///// <returns>Returns rehydrated object of type T.</returns>
        ///// 
        //public static T DeserializeXmlString<T>(String xmlString)
        //{
        //    T tempObject = default(T);

        //    using (MemoryStream memoryStream = new MemoryStream(StringToASCIIByteArray(xmlString)))
        //    {
        //        System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
        //        System.Xml.XmlTextWriter xmlTextWriter = new System.Xml.XmlTextWriter(memoryStream, Encoding.ASCII);

        //        tempObject = (T)xs.Deserialize(memoryStream);
        //    }
        //    return tempObject;
        //}

        // Convert Array to String

        //private static String ASCIIByteArrayToString(Byte[] arrBytes)
        //{
        //    return new ASCIIEncoding().GetString(arrBytes);
        //}

        //// Convert String to Array
        //private static Byte[] StringToASCIIByteArray(String xmlString)
        //{
        //    return new ASCIIEncoding().GetBytes(xmlString);
        //}

        public static string SerializeToXmlString<T>(T toSerialize)
        {
            string xmlstream = String.Empty;

            try
            {
                using (MemoryStream memstream = new MemoryStream())
                {
                    System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                    System.Xml.XmlTextWriter xmlWriter = new System.Xml.XmlTextWriter(memstream, Encoding.ASCII);

                    xmlSerializer.Serialize(xmlWriter, toSerialize);
                    xmlstream = ASCIIByteArrayToString(((MemoryStream)xmlWriter.BaseStream).ToArray());
                }
            }
            catch (Exception ex) { throw ex; }

            return xmlstream;
        }

        /// <summary>
        /// Deserializes Xml string of type T.
        /// </summary>
        /// <typeparam name="T">Datatype T.</typeparam>
        /// <param name="xmlString">Input Xml string from which to read.</param>
        /// <returns>Returns rehydrated object of type T.</returns>
        public static T DeserializeXmlString<T>(String xmlString)
        {
            T tempObject = default(T);

            using (MemoryStream memoryStream = new MemoryStream(StringToASCIIByteArray(xmlString)))
            {
                System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
                System.Xml.XmlTextWriter xmlTextWriter = new System.Xml.XmlTextWriter(memoryStream, Encoding.ASCII);

                tempObject = (T)xs.Deserialize(memoryStream);
            }
            return tempObject;
        }

        // Convert Array to String
        private static String ASCIIByteArrayToString(Byte[] arrBytes)
        {
            return new ASCIIEncoding().GetString(arrBytes);
        }

        // Convert String to Array
        private static Byte[] StringToASCIIByteArray(String xmlString)
        {
            return new ASCIIEncoding().GetBytes(xmlString);
        }

        public static bool Base64ToImage(string base64String, string path)
        {
            try
            {
                //base64String = base64String.Replace("\r\n  ", "");
                int base64StringLength = base64String.Length;
                byte[] imageBytes = Convert.FromBase64String(base64String);
                MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                ms.Write(imageBytes, 0, imageBytes.Length);
                Image image = Image.FromStream(ms, true);
                image = ScaleImage(image, 200, 400);
                image.Save(path);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }

        public static string ImageToBase64(string imagePath)
        {
            try
            {
                using (Image image = Image.FromFile(imagePath))
                {
                    using (MemoryStream m = new MemoryStream())
                    {
                        image.Save(m, image.RawFormat);
                        byte[] imageBytes = m.ToArray();

                        // Convert byte[] to Base64 String
                        string base64String = Convert.ToBase64String(imageBytes);
                        return base64String;
                    }
                }
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static Stream ToStream(Image image, ImageFormat formaw)
        {
            var stream = new MemoryStream();
            image.Save(stream, formaw);
            stream.Position = 0;
            return stream;
        }
    }
}
