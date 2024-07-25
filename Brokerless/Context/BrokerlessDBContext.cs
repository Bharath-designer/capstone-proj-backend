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

        public DbSet<Chat> Chats { get; set; }
        public DbSet<CommercialDetails> CommercialDetails { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<HostelDetails> HostelDetails { get; set; }
        public DbSet<HouseDetails> HouseDetails { get; set; }
        public DbSet<LandDetails> LandDetails { get; set; }
        public DbSet<ProductDetails> ProductDetails { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyFile> PropertyFiles { get; set; }
        public DbSet<SubscriptionTemplate> SubscriptionTemplates { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserSubscription> UserSubscriptions { get; set; }

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


            modelBuilder.Entity<SubscriptionTemplate>()
                .HasData(
                new SubscriptionTemplate
                {
                    SubsriptionName = "Free",
                    Description = "This subsctiption is defaultf for user.",
                    Validity = null,
                    MaxListingCount = 1,
                    MaxSellerViewCount = 1
                });
            

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
