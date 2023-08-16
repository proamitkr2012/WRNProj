using System.Text;
using System.Security.Cryptography;
using System.IO;
using System;

namespace CypherUtility
{
    public class Cypher
    {
        static string CypherKey = "UI29L24523";
        private static string GetCypherKey()
        {
            return CypherKey;
                //System.Configuration.ConfigurationManager.AppSettings["CypherKey"];
        }

        private static Rfc2898DeriveBytes GetDerivedBytes(string Key)
        {
            return new Rfc2898DeriveBytes(Key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        }

        public static string Encrypt(string Text)
        {
            return Encrypt(Text, GetCypherKey());
        }

        public static string Encrypt(string Text, string Key)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(Text);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = GetDerivedBytes(Key);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    Text = Convert.ToBase64String(ms.ToArray());
                }
            }
            return Text;
        }

        public static string Decrypt(string Text)
        {
            return Decrypt(Text, GetCypherKey());
        }

        public static string Decrypt(string Text, string Key)
        {
            //byte[] cipherBytes = Convert.FromBase64String(Text);
            byte[] cipherBytes = Convert.FromBase64String(Text.Replace(" ", "+"));
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = GetDerivedBytes(Key);
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    Text = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return Text;
        }
    }
}