﻿
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models.IdentityOverrides
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<GruppoUser>? JoinedGroups { get; set; } = null;
    }
}
