using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

/// <summary>
/// Summary description for CryptoHelper
/// </summary>
public class CryptoHelper
{
    public static byte[] GetHash(string text)
    {
        SHA256 hash = SHA256.Create();
        char[] array = text.ToCharArray();
        byte[] chars = new byte[array.Length];
        System.Buffer.BlockCopy(array, 0, chars, 0, chars.Length);
        byte[] bytes = hash.ComputeHash(chars);

        return bytes;
    }
}