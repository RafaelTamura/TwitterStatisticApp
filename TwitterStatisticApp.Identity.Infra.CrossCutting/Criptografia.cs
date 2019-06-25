using System;
using System.Security.Cryptography;
using System.Text;

namespace TwitterStatisticApp.Identity.Infra.CrossCutting
{
    public class Criptografia
    {
        #region Métodos Estáticos
        public static string CriptografarMD5(string texto)
        {
            if (string.IsNullOrEmpty(texto))
            {
                return null;
            }

            byte[] encodedBytes;

            using (var md5 = new MD5CryptoServiceProvider())
            {
                var originalBytes = Encoding.Default.GetBytes(texto);
                encodedBytes = md5.ComputeHash(originalBytes);
            }

            return Convert.ToBase64String(encodedBytes);
        }
        #endregion
    }
}
