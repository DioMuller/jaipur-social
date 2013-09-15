using JaipurSocial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Extensions
/// </summary>
public static class Extensions
{
    public static bool CheckPassword(this User user, string password)
    {
        return user.Password.SequenceEqual(CryptoHelper.GetHash(password));
    }
}