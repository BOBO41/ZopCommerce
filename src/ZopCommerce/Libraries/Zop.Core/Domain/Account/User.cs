using System;
using System.Collections.Generic;
using System.Text;

namespace Zop.Core.Domain.Account
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User : BaseEntity
    {
        private ICollection<Group> _groups;
        public string Name { get; set; }
        public string Phone { get; set; }
        public int Score { get; set; }

        public virtual ICollection<Group> Groups
        {
            get { return _groups ?? (_groups = new List<Group>()); }
            set { _groups = value; }
        }
    }
}
