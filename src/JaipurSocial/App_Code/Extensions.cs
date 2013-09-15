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
    public static bool CheckLogin(this User user, string login)
    {
        return user.Login.ToLower() == login.ToLower();
    }

    public static bool CheckPassword(this User user, string password)
    {
        return Array.Equals(user.Password, CryptoHelper.GetHash(password));
    }
}