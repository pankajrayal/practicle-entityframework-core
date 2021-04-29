﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryModels {
    public class CategoryColor {
        [Key, ForeignKey("Category")]
        [Required]
        public int Id { get; set; }
        [StringLength(InventoryModelConstants.MAX_COLORVALUE_LENGTH)]
        public string ColorValue { get; set; }
        public virtual Category Category { get; set; }
    }
}