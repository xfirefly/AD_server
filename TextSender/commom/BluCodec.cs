using System;
using System.Text;
using System.Security.Cryptography;
 
namespace Common
{
 
    public class BluCodec
    {
 

        public static string Encode(string raw)
        {
            return Encrypt(raw);
        }

        public static string Decode(string encrypted)
        {
            return Decrypt(encrypted);
        }

        private static string Encrypt(string raw)
        {
            using (var csp = new AesCryptoServiceProvider())
            {
                ICryptoTransform e = GetCryptoTransform(csp, true);
                byte[] inputBuffer = Encoding.UTF8.GetBytes(raw);
                byte[] output = e.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);

                string encrypted = Convert.ToBase64String(output);

                return encrypted;
            }
        }

        public static string Decrypt(string encrypted)
        {
            using (var csp = new AesCryptoServiceProvider())
            {
                var d = GetCryptoTransform(csp, false);
                byte[] output = Convert.FromBase64String(encrypted);
                byte[] decryptedOutput = d.TransformFinalBlock(output, 0, output.Length);

                string decypted = Encoding.UTF8.GetString(decryptedOutput);
                return decypted;
            }
        }


        // "http://api.blueberry-tek.com/"
        private static string prefix = "l5VKJpn'HRa6xYKmgPOfp@N1\"H!0V.?i.lPE";
        private static string fireplayer = "blumedia/fireplayer.php";

        private static ICryptoTransform GetCryptoTransform(AesCryptoServiceProvider csp, bool encrypting)
        {
            csp.Mode = CipherMode.CBC;
            csp.Padding = PaddingMode.PKCS7;
            String passWord = prefix; // "config.ini";
            String salt = fireplayer; // "1920*1280";

            //a random Init. Vector. just for testing
            String iv = "e675f725e675f725";

            var spec = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(passWord), Encoding.UTF8.GetBytes(salt), 65536);
            byte[] key = spec.GetBytes(16);


            csp.IV = Encoding.UTF8.GetBytes(iv);
            csp.Key = key;
            if (encrypting)
            {
                return csp.CreateEncryptor();
            }
            return csp.CreateDecryptor();
        }

    }
}