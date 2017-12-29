using System;
using System.Collections.Generic;
using System.Text;
using Zop.Core.Domain.Account;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Zop.Data.Mapping.Account
{
    public class GroupMappingMap : ZopEntityTypeConfiguration<GroupMapping>
    {
        public override void Configure(EntityTypeBuilder<GroupMapping> builder)
        {
            builder.ToTable("GroupMapping");

            builder.HasKey(m => m.Id);

            builder.HasOne<Group>()
                   .WithMany()
                   .HasForeignKey(m => m.GroupId)
                   .HasPrincipalKey(g => g.Id)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<User>()
                   .WithMany()
                   .HasForeignKey(m => m.UserId)
                   .HasPrincipalKey(u => u.Id)
                   .OnDelete(DeleteBehavior.Cascade);
                   
        }
    }
}
