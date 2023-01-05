using System.Security.Cryptography;
using System.Text;

namespace MyBookShelf.Web.Helpers;

public class CryptoOperations
{
    /// <summary>
        /// Kullanıcı şifresinin SHA yöntemiyle şifrelenmesini sağlayan metottur.
        /// </summary>
        /// <param name="text">Şifrelenecek Metin</param>
        /// <returns>Girdi Metninin Şifrelenmiş Hali</returns>
        public static string CalculateSHA512(string text)
        {
            SHA512Managed sha512 = new SHA512Managed();
            Byte[] encryptedSha512 = sha512.ComputeHash(Encoding.UTF8.GetBytes(text));
            string strHex = "";
            foreach (byte b in encryptedSha512)
            {
                strHex += String.Format("{0:x2}", b);
            }
            return strHex.ToUpper();
        }

        /// <summary>
        /// Kullanıcı şifresinin SHA yöntemiyle şifrelenmesini sağlayan metottur.
        /// </summary>
        /// <param name="text">Şifrelenecek Metin</param>
        /// <returns>Girdi Metninin Şifrelenmiş Hali</returns>
        public static string CalculateSHA256(string text)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            byte[] hash = sha256.ComputeHash(bytes);
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }

        /// <summary>
        /// Kullanıcı şifresinin MD5 yöntemiyle şifrelenmesini sağlayan metottur.
        /// </summary>
        /// <param name="input">Şifrelenecek Metin</param>
        /// <returns>Girdi Metninin Şifrelenmiş Hali</returns>
        public static string CalculateMD5(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            foreach (byte t in hash)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }
}