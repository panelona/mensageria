using System.Security.Cryptography;
using System.Text;

namespace MS.Cadastro.Shared
{
    public class Criptografia
    {
        public static string Encrypt(string entryText)
        {
            byte[] Key = new byte[] { 12, 2, 56, 117, 12, 67, 33, 23, 12, 2, 56, 117, 12, 67, 33, 23 };

            byte[] IniVetor = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };

            Aes Algorithm = Aes.Create();

            byte[] symEncryptedData;

            var dataToProtectAsArray = Encoding.UTF8.GetBytes(entryText);
            using (var encryptor = Algorithm.CreateEncryptor(Key, IniVetor))
            using (var memoryStream = new MemoryStream())

            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            {
                cryptoStream.Write(dataToProtectAsArray, 0, dataToProtectAsArray.Length);
                cryptoStream.FlushFinalBlock();
                symEncryptedData = memoryStream.ToArray();
            }
            Algorithm.Dispose();
            return Convert.ToBase64String(symEncryptedData);
        }

        public static string Decrypt(string entryText)
        {
            byte[] Key = new byte[] { 12, 2, 56, 117, 12, 67, 33, 23, 12, 2, 56, 117, 12, 67, 33, 23 };

            byte[] IniVetor = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };

            Aes Algorithm = Aes.Create();

            var symEncryptedData = Convert.FromBase64String(entryText);
            byte[] symUnencryptedData;
            using (var decryptor = Algorithm.CreateDecryptor(Key, IniVetor))
            using (var memoryStream = new MemoryStream())
            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write))
            {
                cryptoStream.Write(symEncryptedData, 0, symEncryptedData.Length);
                cryptoStream.FlushFinalBlock();
                symUnencryptedData = memoryStream.ToArray();
            }
            Algorithm.Dispose();
            return Encoding.Default.GetString(symUnencryptedData);
        }
    }
}
