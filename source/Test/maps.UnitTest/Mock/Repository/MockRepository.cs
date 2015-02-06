using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using maps.Model;

namespace maps.UnitTest.Mock.Repository
{
    public partial class MockRepository : Mock<IOldRepository>
    {
        public MockRepository(MockBehavior mockBehavior = MockBehavior.Strict)
            : base(mockBehavior)
        {
            GenerateRoles();
            GenerateUsers();
        }
    }
}
