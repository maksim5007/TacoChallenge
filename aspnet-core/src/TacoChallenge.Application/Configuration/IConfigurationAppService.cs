using System.Threading.Tasks;
using TacoChallenge.Configuration.Dto;

namespace TacoChallenge.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
