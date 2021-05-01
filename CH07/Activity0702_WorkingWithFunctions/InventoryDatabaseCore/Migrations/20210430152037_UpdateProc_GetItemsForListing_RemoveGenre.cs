﻿using InventoryDatabaseCore.Migrations.Scripts;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryDatabaseCore.Migrations {
    public partial class UpdateProc_GetItemsForListing_RemoveGenre : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.SqlResource("InventoryDatabaseCore.Migrations.Scripts.Procedures.GetItemsForListing.GetItemsForListing.v1.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.SqlResource("InventoryDatabaseCore.Migrations.Scripts.Procedures.GetItemsForListing.GetItemsForListing.v0.sql");
        }
    }
}
