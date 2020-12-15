using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace TacoChallenge.Search.Dto
{
    public class SearchResultDto : EntityDto
    {
        public SearchResultDto()
        {
            CategoryGroups = new List<CategoryGroupDto>();
            MenuItems = new List<MenuItemDto>();
        }

        public string RestaurantName { get; set; }
        public string Suburb { get; set; }
        public int Rank { get; set; }
        public string LogoPath { get; set; }

        public List<CategoryGroupDto> CategoryGroups { get; set; }
        public List<MenuItemDto> MenuItems { get; set; }
    }
}