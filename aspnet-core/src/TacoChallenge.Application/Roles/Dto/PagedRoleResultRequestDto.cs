using Abp.Application.Services.Dto;

namespace TacoChallenge.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

