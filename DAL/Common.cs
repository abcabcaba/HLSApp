using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Collections.ObjectModel;
using System.Reflection;



namespace DAL.Common
{
    public class General : IDisposable
    {
        string Salt = "QxLUF1bgIAdeQX";
        private byte[] btkey = { };
        private string strEncryptionKey = "!5623a#de";
        private byte[] btIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        public String ConvertUserNamePasswordToSHA512(String strUserId, String strPassword)
        {
            String strSHA512 = SHA512(strUserId.ToUpper()) + SHA512(strPassword);
            for (int i = 0; i < strPassword.Length + strUserId.Length; i++)
            {
                strSHA512 = SHA512(strSHA512.ToUpper());
            }
            return strSHA512.ToUpper();
        }
        public string SHA512(string strPassword)
        {
            SHA512Managed HashTool = new SHA512Managed();
            Byte[] PasswordAsByte = Encoding.UTF8.GetBytes(string.Concat(strPassword, Salt));
            Byte[] EncryptedBytes = HashTool.ComputeHash(PasswordAsByte);
            HashTool.Clear();
            return Convert.ToBase64String(EncryptedBytes).ToUpper();
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public string Encrypt(string strInput)
        {
            btkey = System.Text.Encoding.UTF8.GetBytes(strEncryptionKey.Substring(0, 8));
            DESCryptoServiceProvider objDESCryptoServiceProvider = new DESCryptoServiceProvider();
            Byte[] inputByteArray = Encoding.UTF8.GetBytes(strInput);
            MemoryStream objMemoryStream = new MemoryStream();
            CryptoStream objCryptoStream = new CryptoStream(objMemoryStream, objDESCryptoServiceProvider.CreateEncryptor(btkey, btIV), CryptoStreamMode.Write);
            objCryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
            objCryptoStream.FlushFinalBlock();
            objDESCryptoServiceProvider.Dispose();
            return Convert.ToBase64String(objMemoryStream.ToArray());
        }
        public string Decrypt(string strInput)
        {
            Byte[] inputByteArray = new Byte[strInput.Length];
            try
            {
                strInput = strInput.Replace(' ', '+');
                btkey = System.Text.Encoding.UTF8.GetBytes(strEncryptionKey.Substring(0, 8));
                DESCryptoServiceProvider objDESCryptoServiceProvider = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(strInput);
                MemoryStream objMemoryStream = new MemoryStream();
                CryptoStream objCryptoStream = new CryptoStream(objMemoryStream, objDESCryptoServiceProvider.CreateDecryptor(btkey, btIV), CryptoStreamMode.Write);
                objCryptoStream.Write(inputByteArray, 0, inputByteArray.Length);
                objCryptoStream.FlushFinalBlock();
                objDESCryptoServiceProvider.Dispose();
                Encoding encoding = Encoding.UTF8;
                return encoding.GetString(objMemoryStream.ToArray());
            }
            catch (CryptographicException)
            {
                return "Invalid Character";
            }
            catch (FormatException)
            {
                return "Invalid Format";
            }
        }
    }
}
