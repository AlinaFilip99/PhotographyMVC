﻿using System;
using System.Collections.Generic;
using System.Text;
using Photography.ApplicationLogic.Models;

namespace Photography.ApplicationLogic.Abstractions
{
    public interface IAccountRepository : IRepository<Account>
    {
        new IEnumerable<Account> GetAll();
    }
}
