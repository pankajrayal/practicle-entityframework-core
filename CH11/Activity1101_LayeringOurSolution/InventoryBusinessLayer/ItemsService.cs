using AutoMapper;
using InventoryDatabaseCore;
using InventoryDatabaseLayer;
using InventoryModels.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryBusinessLayer {
    public class ItemsService : IItemsService {
        private readonly IInventoryDatabaseRepo _dbRepo;
        private readonly IMapper _mapper;
        public ItemsService(InventoryDbContext dbContext, IMapper mapper) {
            _dbRepo = new InventoryDatabaseRepo(dbContext, mapper);
        }
        public List<GetItemsForListingDto> GetItemsForListingFromProcedure(
            DateTime minDateValue, DateTime maxDateValue) {
            return _dbRepo.GetItemsForListingFromProcedure(minDateValue, maxDateValue);
        }

        public List<GetItemsForListingWithDateDto> GetItemsForListingLinq(
            DateTime minDateValue, DateTime maxDateValue) {
            return _dbRepo.GetItemsForListingLinq(minDateValue, maxDateValue);
        }

        public AllItemsPipeDelimitedStringDto GetItemsPipeDelimitedString(bool isActive) {
            var items = ListInventory();
            var sb = new StringBuilder();
            foreach (var item in items) {
                if (sb.Length > 0) {
                    sb.Append("|");
                }
                sb.Append(item.Name);
            }
            var output = new AllItemsPipeDelimitedStringDto();
            output.AllItems = sb.ToString();
            return output;
        }

        public List<GetItemsTotalValueDto> GetItemsTotalValues(bool isActive) {
            return _dbRepo.GetItemsTotalValue(isActive);
        }

        public List<ItemsWithGenresDto> GetItemsWithGenres() {
            return _dbRepo.GetItemsWithGenres();
        }

        public List<CategoryDto> ListCategoriesAndColors() {
            return _dbRepo.ListCategoriesAndColors();
        }

        public List<ItemDto> ListInventory() {
            return _dbRepo.ListInventory();
        }
    }
}