﻿using System.Net;
using Vokabular.DataEntities.Database.Entities;
using Vokabular.RestClient.Errors;

namespace Vokabular.MainService.Core.Utils
{
    public class OwnershipHelper
    {
        public static void CheckItemOwnership(long itemOwnerUserId, int userId)
        {
            if (userId != itemOwnerUserId)
            {
                throw new HttpErrorCodeException(
                    $"Current user ID=({userId}) doesn't have permission manipulate with specified item owned by user with ID={itemOwnerUserId}", HttpStatusCode.Forbidden);
            }
        }

        private void CheckItemOwnership(User itemOwnerUser, User user)
        {
            if (user.Id != itemOwnerUser.Id)
            {
                throw new HttpErrorCodeException(
                    $"Current user ({user.UserName}) doesn't have permission manipulate with specified item owned by user with ID={itemOwnerUser.Id}", HttpStatusCode.Forbidden);
            }
        }
    }
}