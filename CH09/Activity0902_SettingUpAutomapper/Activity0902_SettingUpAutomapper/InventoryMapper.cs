using AutoMapper;
using InventoryModels;
using InventoryModels.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Activity0902_SettingUpAutomapper {
    public class InventoryMapper : Profile {
        public InventoryMapper() {
            CreateMaps();
        }

        private void CreateMaps() {
            CreateMap<Item, ItemDto>();
            CreateMap<Category, CategoryDto>();
        }
    }
}
