using System;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main()
    {
        WithXML();
        WithoutXML();
    }

    
    private static void WithXML()
    {
        // Generate key pair
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            // Get public and private key
            string publicKey = rsa.ToXmlString(false);
            string privateKey = rsa.ToXmlString(true);

            // Display keys (for demonstration purposes)
            Console.WriteLine("Public Key:");
            Console.WriteLine(publicKey);
            Console.WriteLine();
            Console.WriteLine("Private Key:");
            Console.WriteLine(privateKey);
            Console.WriteLine();

            // Message to be encrypted
            string originalMessage = "Hello, this is a secret message!";

            // Convert the message to bytes
            byte[] dataToEncrypt = Encoding.UTF8.GetBytes(originalMessage);

            // Encrypt using the public key
            byte[] encryptedData = EncryptData(dataToEncrypt, publicKey);

            // Decrypt using the private key
            byte[] decryptedData = DecryptData(encryptedData, privateKey);

            // Convert the decrypted data back to a string
            string decryptedMessage = Encoding.UTF8.GetString(decryptedData);

            // Display the decrypted message
            Console.WriteLine("Decrypted Message:");
            Console.WriteLine(decryptedMessage);
            Console.Read();
        }
    }
    static byte[] EncryptData(byte[] dataToEncrypt, string publicKey)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(publicKey);
            return rsa.Encrypt(dataToEncrypt, false);
        }
    }

    static byte[] DecryptData(byte[] encryptedData, string privateKey)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(privateKey);
            return rsa.Decrypt(encryptedData, false);
        }
    }

    private static void WithoutXML()
    {
        // Generate key pair
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            // Get public and private key parameters
            RSAParameters publicKeyParams = rsa.ExportParameters(false);
            RSAParameters privateKeyParams = rsa.ExportParameters(true);

            // Message to be encrypted
            string originalMessage = "Hello, this is a secret message!";

            // Convert the message to bytes
            byte[] dataToEncrypt = Encoding.UTF8.GetBytes(originalMessage);

            // Encrypt using the public key parameters
            byte[] encryptedData = EncryptData(dataToEncrypt, publicKeyParams);

            // Decrypt using the private key parameters
            byte[] decryptedData = DecryptData(encryptedData, privateKeyParams);

            // Convert the decrypted data back to a string
            string decryptedMessage = Encoding.UTF8.GetString(decryptedData);

            // Display the decrypted message
            Console.WriteLine("Decrypted Message:");
            Console.WriteLine(decryptedMessage);
            Console.Read();
        }
    }
    static byte[] EncryptData(byte[] dataToEncrypt, RSAParameters publicKeyParams)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.ImportParameters(publicKeyParams);
            return rsa.Encrypt(dataToEncrypt, false);
        }
    }

    static byte[] DecryptData(byte[] encryptedData, RSAParameters privateKeyParams)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.ImportParameters(privateKeyParams);
            return rsa.Decrypt(encryptedData, false);
        }
    }
}
