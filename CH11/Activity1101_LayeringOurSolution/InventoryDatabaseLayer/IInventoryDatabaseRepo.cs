using InventoryModels.Dtos;
using System;
using System.Collections.Generic;

namespace InventoryDatabaseLayer {
    public interface IInventoryDatabaseRepo {
        List<ItemDto> ListInventory();
        List<GetItemsForListingWithDateDto> GetItemsForListingLinq(
            DateTime minDateValue, DateTime maxDateValue);
        List<GetItemsForListingDto> GetItemsForListingFromProcedure(
            DateTime minDateValue, DateTime maxDateValue);
        List<GetItemsTotalValueDto> GetItemsTotalValue(bool isActive);
        List<ItemsWithGenresDto> GetItemsWithGenres();
        List<CategoryDto> ListCategoriesAndColors();
    }
}