using Lib.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Models
{
    public class Permission
    {
        public PermissionType PermissionType { get; set; }
        public Identity Grantor { get; set; }
        public Group Grantee { get; set; }
    }
}
