using JaipurSocial.Core;
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

    public static string GetStatus(this GameData gameData, User currentUser)
    {
        if (gameData.IsGameFinished)
        {
            if (gameData.Winner.Id == currentUser.Id)
                return Resources.Localization.YouWon;
            return Resources.Localization.YouLost;
        }

        if (gameData.IsCurrentTurn(currentUser))
            return Resources.Localization.Ready;
        return Resources.Localization.WaitingEnemy;
    }
}