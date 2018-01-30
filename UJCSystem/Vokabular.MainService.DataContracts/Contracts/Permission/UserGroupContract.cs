﻿using System;
using System.Collections.Generic;

namespace Vokabular.MainService.DataContracts.Contracts.Permission
{
    public class UserGroupContract
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class UserGroupDetailContract : UserGroupContract
    {
        public DateTime CreateTime { get; set; }

        public UserContract CreatedBy { get; set; }

        public IList<UserContract> Members { get; set; }
    }
}