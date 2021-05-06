using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_Activity001.Migrations {
    public partial class EncryptionMigration_Step7_DeleteBackupColumns : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.Sql(@"ALTER TABLE [HumanResources].[Employee] DROP COLUMN NationalIDNumberBackup");
            migrationBuilder.Sql(@"ALTER TABLE [HumanResources].[Employee] DROP COLUMN JobTitleBackup");
            migrationBuilder.Sql(@"ALTER TABLE [HumanResources].[Employee] DROP COLUMN BirthDateBackup");
            migrationBuilder.Sql(@"ALTER TABLE [HumanResources].[Employee] DROP COLUMN MaritalStatusBackup");
            migrationBuilder.Sql(@"ALTER TABLE [HumanResources].[Employee] DROP COLUMN GenderBackup");
            migrationBuilder.Sql(@"ALTER TABLE [HumanResources].[Employee] DROP COLUMN HireDateBackup");
        }

        protected override void Down(MigrationBuilder migrationBuilder) {

        }
    }
}
