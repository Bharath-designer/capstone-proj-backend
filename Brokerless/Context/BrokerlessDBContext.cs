using Brokerless.Models;
using Microsoft.EntityFrameworkCore;

namespace Brokerless.Context
{
    public class BrokerlessDBContext: DbContext
    {
        public BrokerlessDBContext(DbContextOptions options): base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Chat> Chat { get; set; }
        public DbSet<CommercialDetails> CommercialDetails { get; set; }
        public DbSet<Conversation> Conversation { get; set; }
        public DbSet<HostelDetails> HostelDetails { get; set; }
        public DbSet<HouseDetails> HouseDetails { get; set; }
        public DbSet<LandDetails> LandDetails { get; set; }
        public DbSet<ProductDetails> ProductDetails { get; set; }
        public DbSet<Property> Property { get; set; }
        public DbSet<PropertyFile> PropertyFile { get; set; }
        public DbSet<SubscriptionTemplate> SubscriptionTemplate { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserSubscription> UserSubscription { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserSubscription>()
                .HasOne(u => u.User)
                .WithOne(us => us.UserSubscription)
                .HasForeignKey<UserSubscription>(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserSubscription>()
                .HasOne(u => u.SubscriptionTemplate)
                .WithMany()
                .HasForeignKey(u=>u.SubscriptionTemplateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.SubscriptionTemplate)
                .WithMany()
                .HasForeignKey(t => t.SubscriptionTemplateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.User)
                .WithMany(u=>u.Transactions)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Tag>()
                .HasMany(t => t.Properties)
                .WithMany(p => p.Tags)
                .UsingEntity(j => j.ToTable("PropertyTag"));


            modelBuilder.Entity<PropertyFile>()
                .HasOne(pf => pf.Property)
                .WithMany(p => p.Files)
                .HasForeignKey(p => p.PropertyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Property>()
                .HasOne(p => p.Seller)
                .WithMany(u => u.Listings)
                .HasForeignKey(p => p.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CommercialDetails>()
                .HasOne(c => c.Property)
                .WithOne(p => p.CommercialDetails)
                .HasForeignKey<CommercialDetails>(c => c.PropertyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Conversation>()
                .HasMany(c => c.Chats)
                .WithMany()
                .UsingEntity(j => j.ToTable("ConversationChat"));

            modelBuilder.Entity<Conversation>()
                .HasOne(c => c.User)
                .WithMany(u => u.Conversations)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            //modelBuilder.Entity<Conversation>()
            //    .HasMany(co => co.Chats)
            //    .WithMany()
            //    .UsingEntity(j => j.ToTable("ConversationChat"));

            //modelBuilder.Entity<Conversation>()
            //    .HasOne(u => u.User)
            //    .WithMany(u=>u.Conversations)
            //    .HasForeignKey(c => c.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Property>()
            //    .HasOne(p => p.CommercialDetails)
            //    .WithOne()
            //    .HasForeignKey<CommercialDetails>(c => c.PropertyId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Property>()
            //    .HasOne(p => p.HostelDetails)
            //    .WithOne()
            //    .HasForeignKey<HostelDetails>(p => p.PropertyId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Property>()
            //    .HasOne(p => p.HouseDetails)
            //    .WithOne()
            //    .HasForeignKey<HouseDetails>(p => p.PropertyId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Property>()
            //    .HasOne(p => p.LandDetails)
            //    .WithOne()
            //    .HasForeignKey<LandDetails>(p => p.PropertyId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Property>()
            //    .HasOne(p => p.ProductDetails)
            //    .WithOne()
            //    .HasForeignKey<ProductDetails>(p => p.PropertyId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Property>()
            //    .HasOne(p => p.Seller)
            //    .WithMany(s => s.Listings)
            //    .HasForeignKey(p => p.SellerId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Property>()
            //    .HasMany(p => p.Tags)
            //    .WithMany(t => t.Properties)
            //    .UsingEntity(j => j.ToTable("PropertyTag"));

            //modelBuilder.Entity<Property>()
            //    .HasMany(p => p.PropertyFiles)
            //    .WithOne()
            //    .HasForeignKey(p => p.PropertyId)
            //    .OnDelete(DeleteBehavior.Restrict);


            //modelBuilder.Entity<Transaction>()
            //    .HasOne(t => t.User)
            //    .WithMany(u => u.Transactions)
            //    .HasForeignKey(t => t.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);


            //modelBuilder.Entity<Transaction>()
            //    .HasOne(t => t.SubscriptionTemplate)
            //    .WithMany()
            //    .HasForeignKey(t => t.SubscriptionTemplateId)
            //    .OnDelete(DeleteBehavior.Restrict);


            //modelBuilder.Entity<User>()
            //    .HasOne(u => u.UserSubscription)
            //    .WithOne(us=> us.User)
            //    .HasForeignKey<User>(u => u.UserSubscriptionId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<UserSubscription>()
            //    .HasOne(u => u.SubscriptionTemplate)
            //    .WithMany()
            //    .HasForeignKey(u => u.SubscriptionTemplateId)
            //    .OnDelete(DeleteBehavior.Restrict);


            // For converting enum to string (int by default)
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var enumProperties = entityType.ClrType.GetProperties()
                    .Where(p => p.PropertyType.IsEnum);

                foreach (var property in enumProperties)
                {
                    modelBuilder.Entity(entityType.Name)
                        .Property(property.Name)
                        .HasConversion<string>();
                }
            }

            base.OnModelCreating(modelBuilder);
        }

    }
}
