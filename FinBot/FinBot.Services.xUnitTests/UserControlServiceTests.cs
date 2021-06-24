using AutoFixture;
using FinBot.App.Services;
using FinBot.Domain.Interfaces;
using FinBot.Domain.Models;
using Moq;
using System;
using System.Linq;
using System.Threading;
using Xunit;

namespace FinBot.Services.xUnitTests
{
    public class UserControlServiceTests
    {
        public UserControlServiceTests()
        {
            UserMock = new Mock<IBaseRepositoryDb<User>>();
            _Fix = new Fixture();
            _cancel = CancellationToken.None;
            _Fix.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _Fix.Behaviors.Remove(b));
            _Fix.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        private Mock<IBaseRepositoryDb<User>> UserMock { get; set; }

        private readonly CancellationToken _cancel;
        private Fixture _Fix { get; set; }

        [Fact]
        public async void Create_ShouldNewUserIsCreate()
        {
            //arrange
            var user = _Fix.Build<User>()
                .Without(p => p.Id)
                .Create();
            UserMock.Setup(db => db.Create(user, _cancel)).ReturnsAsync(true);
            var userService = new UserControlService(UserMock.Object);
            
            //act
            var result = await userService.Create(user, _cancel);
            

            //assert
            UserMock.Verify(x => x.Create(user, _cancel), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public void Create_ShouldNewUserNullArgumentExceptionAsync()
        {
            //arrange
            User user = null;
            var userService = new UserControlService(UserMock.Object);
            //act
            //var result = await userService.Create(user, _cancel);

            //assert
            UserMock.Verify(x => x.Create(user, _cancel), Times.Never);
            Assert.ThrowsAsync(typeof(ArgumentNullException), async () => await userService.Create(user, _cancel));
        }
    }
}
