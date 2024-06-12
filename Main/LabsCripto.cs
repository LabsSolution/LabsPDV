using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Labs.Main
{
    public class LabsCripto
    {
        public static void Encript(string NomeDoBin, string ParaCripto)
        {
            using (var aes = Aes.Create())
            {
                // Gera uma chave e um vetor de inicialização (IV).
                aes.GenerateKey();
                aes.GenerateIV();

                // Criptografa a string.
                byte[] Criptografado = EncryptStringToBytes_Aes(ParaCripto, aes.Key, aes.IV);

                // Devolve o texto Criptografado e Gera os Arquivos
                File.WriteAllBytes($@".\LabsBin\{NomeDoBin}.labs",Criptografado);
                File.WriteAllBytes($@".\LabsBin\{NomeDoBin}vi.labs", aes.Key);
                File.WriteAllBytes($@".\LabsBin\{NomeDoBin}iv.labs", aes.IV);
            }
        }
        //
        public static bool Decript(string NomeDoBin, out string Decripted)
        {
            Decripted = null!;
            if (File.Exists($@".\LabsBin\{NomeDoBin}.labs"))
            {
                // Lê o texto criptografado, a chave e o IV dos arquivos.
                byte[] encrypted = File.ReadAllBytes($@".\LabsBin\{NomeDoBin}.labs");
                byte[] key = File.ReadAllBytes($@".\LabsBin\{NomeDoBin}vi.labs");
                byte[] iv = File.ReadAllBytes($@".\LabsBin\{NomeDoBin}iv.labs");

                // Descriptografa a string.
                Decripted = DecryptStringFromBytes_Aes(encrypted, key, iv);
                //
                return true;
            }
            return false;
        }

        //Utils Internos
        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            byte[] encrypted;

            using (var aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return encrypted;
        }

        static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            string plaintext = null!;

            using (var aes = Aes.Create())
            {
                aes.Key = Key;
                aes.IV = IV;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (var msDecrypt = new MemoryStream(cipherText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }
    }
}
