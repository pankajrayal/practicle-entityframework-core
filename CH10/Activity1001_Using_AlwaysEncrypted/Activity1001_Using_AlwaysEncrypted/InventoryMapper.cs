using AutoMapper;
using InventoryModels;
using InventoryModels.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Activity1001_Using_AlwaysEncrypted {
    public class InventoryMapper : Profile {
        public InventoryMapper() {
            CreateMaps();
        }

        private void CreateMaps() {
            CreateMap<Item, ItemDto>();
            CreateMap<Category, CategoryDto>()
                .ForMember(x => x.Category, opt => opt.MapFrom(y => y.Name))
                .ReverseMap()
                .ForMember(y => y.Name, opt => opt.MapFrom(x => x.Category));
            CreateMap<CategoryColor, CategoryColorDto>()
                .ForMember(x => x.Color, opt => opt.MapFrom(y => y.ColorValue))
                .ReverseMap()
                .ForMember(y => y.ColorValue, opt => opt.MapFrom(x => x.Color));
        }
    }
}
