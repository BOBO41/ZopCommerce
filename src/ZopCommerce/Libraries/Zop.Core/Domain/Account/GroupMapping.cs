using System;
using System.Collections.Generic;
using System.Text;

namespace Zop.Core.Domain.Account
{
    /// <summary>
    /// 用户群组映射
    /// </summary>
    public class GroupMapping : BaseEntity
    {
        public string UserId { get; set; }
        public string GroupId { get; set; }
    }
}
