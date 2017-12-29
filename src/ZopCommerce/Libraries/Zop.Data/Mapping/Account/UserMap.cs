using System;
using System.Collections.Generic;
using System.Text;
using Zop.Core.Domain.Account;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Zop.Data.Mapping.Account
{
    public class UserMap : ZopEntityTypeConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(u => u.Id);
            builder.Ignore(u => u.Groups);
        }
    }
}
