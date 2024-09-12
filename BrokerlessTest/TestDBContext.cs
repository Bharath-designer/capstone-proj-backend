
using Brokerless.Context;
using Microsoft.EntityFrameworkCore;

namespace BrokerlessTest
{
    class TestDBContext : BrokerlessDBContext
    {
        public TestDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        public static DbContextOptions<TestDBContext> GetInMemoryOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder<TestDBContext>();
            optionsBuilder.UseInMemoryDatabase("BrokerlessDb");
            return optionsBuilder.Options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {



            base.OnModelCreating(modelBuilder);
        }


    }
}
