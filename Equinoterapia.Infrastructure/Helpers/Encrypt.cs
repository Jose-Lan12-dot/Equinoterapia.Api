using System.Security.Cryptography;
using System.Text;

namespace Equinoterapia.Infrastructure.Helpers
{
    public class Encrypt
    {
        private static string EncryptionKey = "sspcwebfff";
        private static byte[] key = { };
        private static byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        public static string EncryptStringSspcWeb(string texto)
        {
            string returnstring = "";
            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(EncryptionKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                Byte[] inputByteArray = Encoding.UTF8.GetBytes(texto);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                returnstring = Convert.ToBase64String(ms.ToArray());
                returnstring = returnstring.Replace("/", "-2F-");
                returnstring = returnstring.Replace("!", "-21-");
                returnstring = returnstring.Replace("#", "-23-");
                returnstring = returnstring.Replace("$", "-24-");
                returnstring = returnstring.Replace("&", "-26-");
                returnstring = returnstring.Replace("'", "-27-");
                returnstring = returnstring.Replace("(", "-28-");
                returnstring = returnstring.Replace(")", "-29-");
                returnstring = returnstring.Replace("*", "-2A-");
                returnstring = returnstring.Replace("+", "-2B-");
                returnstring = returnstring.Replace(",", "-2C-");
                returnstring = returnstring.Replace(":", "-3A-");
                returnstring = returnstring.Replace(";", "-3B-");
                returnstring = returnstring.Replace("=", "-3D-");
                returnstring = returnstring.Replace("?", "-3F-");
                returnstring = returnstring.Replace("@", "-40-");
                returnstring = returnstring.Replace("[", "-5B-");
                returnstring = returnstring.Replace("]", "-5D-");
                return returnstring;
            }
            catch (Exception ex)
            {
                return "" + ex;
            }
        }

        public static string DecryptStringSspcWeb(string stringToDecrypt)
        {
            stringToDecrypt = stringToDecrypt.Replace("-2F-", "/");
            stringToDecrypt = stringToDecrypt.Replace("-21-", "!");
            stringToDecrypt = stringToDecrypt.Replace("-23-", "#");
            stringToDecrypt = stringToDecrypt.Replace("-24-", "$");
            stringToDecrypt = stringToDecrypt.Replace("-26-", "&");
            stringToDecrypt = stringToDecrypt.Replace("-27-", "'");
            stringToDecrypt = stringToDecrypt.Replace("-28-", "(");
            stringToDecrypt = stringToDecrypt.Replace("-29-", ")");
            stringToDecrypt = stringToDecrypt.Replace("-2A-", "*");
            stringToDecrypt = stringToDecrypt.Replace("-2B-", "+");
            stringToDecrypt = stringToDecrypt.Replace("-2C-", ",");
            stringToDecrypt = stringToDecrypt.Replace("-3A-", ":");
            stringToDecrypt = stringToDecrypt.Replace("-3B-", ";");
            stringToDecrypt = stringToDecrypt.Replace("-3D-", "=");
            stringToDecrypt = stringToDecrypt.Replace("-3F-", "?");
            stringToDecrypt = stringToDecrypt.Replace("-40-", "@");
            stringToDecrypt = stringToDecrypt.Replace("-5B-", "[");
            stringToDecrypt = stringToDecrypt.Replace("-5D-", "]");

            Byte[] inputByteArray = new Byte[stringToDecrypt.Length];

            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(EncryptionKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(stringToDecrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                Encoding encoding = Encoding.UTF8;

                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                return "" + ex;
            }
        }
    }
}
