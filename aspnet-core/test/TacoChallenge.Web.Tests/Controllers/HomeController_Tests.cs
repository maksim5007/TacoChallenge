using System.Threading.Tasks;
using TacoChallenge.Models.TokenAuth;
using TacoChallenge.Web.Controllers;
using Shouldly;
using Xunit;

namespace TacoChallenge.Web.Tests.Controllers
{
    public class HomeController_Tests: TacoChallengeWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}