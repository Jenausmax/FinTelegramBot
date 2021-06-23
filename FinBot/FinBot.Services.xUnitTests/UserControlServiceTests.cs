using FinBot.App.Services;
using FinBot.Domain.Interfaces;
using FinBot.Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FinBot.Services.xUnitTests
{
    public class UserControlServiceTests
    {
        public UserControlServiceTests()
        {
            
        }

        [Fact]
        public async void Create_ShouldNewUserIsCreate()
        {
            //arrange
            var cancel = CancellationToken.None;
            var user1 = new User() { ChatId = 1, Categories = new List<Category>(), FirstName = "afaf", LastName = "sgagdg", NickName = "asgag" };

            var repositoryToUser = new Mock<IBaseRepositoryDb<User>>();
            repositoryToUser.Setup(db => db.Create(user1, cancel)).ReturnsAsync(true);

            var userService = new UserControlService(repositoryToUser.Object);
            
            //act
            var result = await userService.Create(user1, cancel);
            

            //assert
            repositoryToUser.Verify(x => x.Create(user1, cancel), Times.Once);
            Assert.True(result);
        }

        [Fact]
        public void Create_ShouldNewUserNullArgumentException()
        {
            //arrange
            //act
            //assert
        }
    }
}
