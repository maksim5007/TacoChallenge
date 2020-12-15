using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace TacoChallenge.Search.Dto
{
    public class CategoryGroupDto: EntityDto
    {
        public CategoryGroupDto()
        {
            MenuItems = new List<MenuItemDto>();
        }

        public string Name { get; set; }
        public List<MenuItemDto> MenuItems { get; set; }
    }
}