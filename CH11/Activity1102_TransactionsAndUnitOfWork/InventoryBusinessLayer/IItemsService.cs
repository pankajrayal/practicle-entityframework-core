using InventoryModels;
using InventoryModels.Dtos;
using System;
using System.Collections.Generic;

namespace InventoryBusinessLayer {
    public interface IItemsService {
        List<GetItemsForListingWithDateDto> GetItemsForListingLinq(
            DateTime minDateValue, DateTime maxDateValue);
        List<GetItemsForListingDto> GetItemsForListingFromProcedure(
            DateTime minDateValue, DateTime maxDateValue);
        AllItemsPipeDelimitedStringDto GetItemsPipeDelimitedString(bool isActive);
        List<GetItemsTotalValueDto> GetItemsTotalValues(bool isActive);
        List<ItemsWithGenresDto> GetItemsWithGenres();
        List<CategoryDto> ListCategoriesAndColors();
        List<ItemDto> ListInventory();
        int InsertOrUpdateItem(Item item);
        void InsertOrUpdateItems(List<Item> item);
        void DeleteItem(int id);
        void DeleteItems(List<int> itemIds);
    }
}