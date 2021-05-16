using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Sendo.WebApi.Data.Models;

namespace Sendo.WebApi.Data.Access
{
    public class UserDataPostgresContext : DbContext
    {
        public DbSet<Campaign> Campaigns { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<ContactGroup> ContactGroups { get; set; }

        public DbSet<MailTemplate> MailTemplates { get; set; }

        public DbSet<SessionToken> SessionTokens { get; set; }

        public DbSet<User> Users { get; set; }

        static UserDataPostgresContext() => NpgsqlConnection.GlobalTypeMapper.MapEnum<Gender>("user_data.gender");

        public UserDataPostgresContext(DbContextOptions<UserDataPostgresContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasPostgresEnum<Gender>("user_data");
            builder.Entity<ContactGroup>()
                .HasMany(left => left.Contacts)
                .WithMany(right => right.ContactGroups)
                .UsingEntity<Dictionary<string, object>>("group_membership",
                    x => x.HasOne<Contact>().WithMany().HasForeignKey("contact_id"),
                    x => x.HasOne<ContactGroup>().WithMany().HasForeignKey("contact_group_id"),
                    x => x.ToTable("group_membership", "user_data")
                );
        }
    }
}
