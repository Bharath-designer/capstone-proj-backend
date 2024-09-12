using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Brokerless.Interfaces.Repositories;
using Brokerless.Repositories;
using Brokerless.Services;
using Microsoft.EntityFrameworkCore;

namespace BrokerlessTest
{
    class UserTest
    {
        private TestDBContext _context;
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            _context = new TestDBContext(TestDBContext.GetInMemoryOptions());
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            IUserRepository userRepo = new UserRepository(_context);
            ISubscriptionTemplateRepository subscriptionTemplateRepository = new SubscriptionTemplateRepository(_context);
            IPropertyRepository propertyRepository = new PropertyRepository(_context);
            IConversationRepository conversationRepository = new ConversationRepository(_context);
            _userService = new UserService(userRepo, subscriptionTemplateRepository, propertyRepository, conversationRepository);

        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task CreateUser()
        {
            var result = await _userService.CreateUser("test@gmail.com", "Test", "samplepic.jpg");
            Assert.IsNotNull(result);
        }
    }
}
