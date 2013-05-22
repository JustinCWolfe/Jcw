using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Jcw.Common
{
    public class Crypto
    {
        /// <summary>
        /// Call this function to remove the key from memory after use for security
        /// </summary>
        /// <param name="destination">memory location where key is stored</param>
        /// <param name="length">length of key to be removed from memory</param>
        /// <returns>true if memory was zeroed successfully</returns>
        [System.Runtime.InteropServices.DllImport ( "KERNEL32.DLL", EntryPoint = "RtlZeroMemory" )]
        public static extern bool ZeroMemory ( IntPtr destination, int length );

        /// <summary>
        /// Function to Generate a 64 bit key
        /// </summary>
        /// <returns>encryption key</returns>
        public static string GenerateKey ()
        {
            // Create an instance of Symetric Algorithm. Key and IV is generated automatically
            using (DESCryptoServiceProvider desCrypto = DESCryptoServiceProvider.Create () as DESCryptoServiceProvider)
            {
                // Use the Automatically generated key for Encryption. 
                return ASCIIEncoding.ASCII.GetString (desCrypto.Key);
            }
        }

        /// <summary>
        /// Encrypt file using passed-in secret key
        /// </summary>
        /// <param name="inputFilename">decrypted filename</param>
        /// <param name="outputFilename">encrypted filename</param>
        /// <param name="secretKey">secret key</param>
        public static void EncryptFile ( string inputFilename, string outputFilename, string secretKey )
        {
            FileStream fsInput = null;
            FileStream fsEncrypted = null;
            CryptoStream cryptoStream = null;

            try
            {
                fsInput = new FileStream ( inputFilename, FileMode.Open, FileAccess.Read );
                fsEncrypted = new FileStream ( outputFilename, FileMode.Create, FileAccess.Write );

                if ( fsInput.Length == 0 )
                    return;

                using (DESCryptoServiceProvider DES = new DESCryptoServiceProvider ())
                {
                    //A 64 bit key and IV is required for this provider.
                    //Set secret key For DES algorithm.
                    DES.Key = ASCIIEncoding.ASCII.GetBytes (secretKey);
                    //Set initialization vector.
                    DES.IV = ASCIIEncoding.ASCII.GetBytes (secretKey);

                    ICryptoTransform desEncrypt = DES.CreateEncryptor ();
                    cryptoStream = new CryptoStream (fsEncrypted, desEncrypt, CryptoStreamMode.Write);
                }

                byte[] byteArrayInput = new byte[fsInput.Length];
                fsInput.Read ( byteArrayInput, 0, byteArrayInput.Length );
                cryptoStream.Write ( byteArrayInput, 0, byteArrayInput.Length );
            }
            finally
            {
                if ( cryptoStream != null )
                    cryptoStream.Close ();

                if ( fsInput != null )
                    fsInput.Close ();

                if ( fsEncrypted != null )
                    fsEncrypted.Close ();
            }
        }

        /// <summary>
        /// Decrypt a file using the passed in secret key
        /// </summary>
        /// <param name="inputFilename">encrypted filename</param>
        /// <param name="outputFilename">decrypted filename</param>
        /// <param name="secretKey">secret key</param>
        public static void DecryptFile ( string inputFilename, string outputFilename, string secretKey )
        {
            FileStream fsRead = null;
            StreamWriter fsDecrypted = null;
            CryptoStream cryptoStreamDecrypt = null;

            try
            {
                using (DESCryptoServiceProvider DES = new DESCryptoServiceProvider ())
                {
                    //A 64 bit key and IV is required for this provider.
                    //Set secret key For DES algorithm.
                    DES.Key = ASCIIEncoding.ASCII.GetBytes (secretKey);
                    //Set initialization vector.
                    DES.IV = ASCIIEncoding.ASCII.GetBytes (secretKey);

                    //Create a file stream to read the encrypted file back.
                    fsRead = new FileStream (inputFilename, FileMode.Open, FileAccess.Read);

                    if (fsRead.Length == 0)
                        return;

                    //Create a DES decryptor from the DES instance.
                    ICryptoTransform desDecrypt = DES.CreateDecryptor ();

                    //Create crypto stream set to read and do a 
                    //DES decryption transform on incoming bytes.
                    cryptoStreamDecrypt = new CryptoStream (fsRead, desDecrypt, CryptoStreamMode.Read);
                }

                //Print the contents of the decrypted file
                fsDecrypted = new StreamWriter ( outputFilename );
                fsDecrypted.Write ( new StreamReader ( cryptoStreamDecrypt ).ReadToEnd () );
            }
            finally
            {
                if ( fsRead != null )
                    fsRead.Close ();

                if ( cryptoStreamDecrypt != null )
                    cryptoStreamDecrypt.Close ();

                if ( fsDecrypted != null )
                {
                    fsDecrypted.Flush ();
                    fsDecrypted.Close ();
                }
            }
        }
    }
}