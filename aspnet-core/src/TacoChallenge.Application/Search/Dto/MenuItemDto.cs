using Abp.Application.Services.Dto;

namespace TacoChallenge.Search.Dto
{
    public class MenuItemDto : EntityDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}