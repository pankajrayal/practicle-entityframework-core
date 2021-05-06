using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EF_Activity001.Migrations
{
    public partial class Encryption_Step5_ChangeColumnDataTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "NationalIDNumber",
            //    schema: "HumanResources",
            //    table: "Employee",
            //    newName: "NationalIdnumber");

            //migrationBuilder.AlterColumn<byte[]>(
            //    name: "NationalIdnumber",
            //    schema: "HumanResources",
            //    table: "Employee",
            //    type: "varbinary(900)",
            //    nullable: false,
            //    comment: "Unique national identification number such as a social security number.",
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(15)",
            //    oldMaxLength: 15,
            //    oldComment: "Unique national identification number such as a social security number.");

            migrationBuilder.AddColumn<byte[]>(
                name: "NationalIdnumberTemp",
                schema: "HumanResources",
                table: "Employee",
                nullable: true,
                comment: "Unique national identification number such as a social security number.");
           
            migrationBuilder.Sql(@"UPDATE HumanResources.Employee SET NationalIdnumberTemp = CONVERT(varbinary, NationalIdNumber)");

            migrationBuilder.DropColumn("NationalIdNumber", "Employee", "HumanResources");

            migrationBuilder.RenameColumn(
                name: "NationalIdnumberTemp",
                schema: "HumanResources",
                table: "Employee",
                newName: "NationalIdnumber");

            migrationBuilder.AlterColumn<bool>(
                name: "SalariedFlag",
                schema: "HumanResources",
                table: "Employee",
                type: "bit",
                nullable: true,
                defaultValueSql: "((1))",
                comment: "Job classification. 0 = Hourly, not exempt from collective bargaining. 1 = Salaried, exempt from collective bargaining.",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "((1))",
                oldComment: "Job classification. 0 = Hourly, not exempt from collective bargaining. 1 = Salaried, exempt from collective bargaining.");

            //migrationBuilder.AlterColumn<byte[]>(
            //    name: "MaritalStatus",
            //    schema: "HumanResources",
            //    table: "Employee",
            //    type: "varbinary(max)",
            //    fixedLength: true,
            //    nullable: false,
            //    comment: "M = Married, S = Single",
            //    oldClrType: typeof(string),
            //    oldType: "nchar(1)",
            //    oldFixedLength: true,
            //    oldMaxLength: 1,
            //    oldComment: "M = Married, S = Single");

            migrationBuilder.AddColumn<byte[]>(
                name: "MaritalStatusTemp",
                schema: "HumanResources",
                table: "Employee",
                nullable: true,
                comment: "M = Married, S = Single");
            
            migrationBuilder.Sql(@"UPDATE HumanResources.Employee SET MaritalStatusTemp = CONVERT(varbinary, MaritalStatus)");
            
            migrationBuilder.DropColumn("MaritalStatus", "Employee", "HumanResources");
            
            migrationBuilder.RenameColumn(
                name: "MaritalStatusTemp",
                schema: "HumanResources",
                table: "Employee",
                newName: "MaritalStatus");


            //migrationBuilder.AlterColumn<byte[]>(
            //    name: "JobTitle",
            //    schema: "HumanResources",
            //    table: "Employee",
            //    type: "varbinary(max)",
            //    nullable: false,
            //    comment: "Work title such as Buyer or Sales Representative.",
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(50)",
            //    oldMaxLength: 50,
            //    oldComment: "Work title such as Buyer or Sales Representative.");
            migrationBuilder.AddColumn<byte[]>(
                name: "JobTitleTemp",
                schema: "HumanResources",
                table: "Employee",
                nullable: true,
                comment: "Work title such as Buyer or Sales Representative.");

            migrationBuilder.Sql(@"UPDATE HumanResources.Employee SET JobTitleTemp = CONVERT(varbinary, JobTitle)");

            migrationBuilder.DropColumn("JobTitle", "Employee", "HumanResources");

            migrationBuilder.RenameColumn(
                name: "JobTitleTemp",
                schema: "HumanResources",
                table: "Employee",
                newName: "JobTitle");


            //migrationBuilder.AlterColumn<byte[]>(
            //    name: "HireDate",
            //    schema: "HumanResources",
            //    table: "Employee",
            //    type: "varbinary(max)",
            //    nullable: true,
            //    comment: "Employee hired on this date.",
            //    oldClrType: typeof(DateTime),
            //    oldType: "date",
            //    oldComment: "Employee hired on this date.");

            migrationBuilder.AddColumn<byte[]>(
                name: "HireDateTemp",
                schema: "HumanResources",
                table: "Employee",
                nullable: true,
                comment: "Employee hired on this date.");

            migrationBuilder.Sql(@"UPDATE HumanResources.Employee SET HireDateTemp = CONVERT(varbinary, HireDate)");

            migrationBuilder.DropColumn("HireDate", "Employee", "HumanResources");

            migrationBuilder.RenameColumn(
                name: "HireDateTemp",
                schema: "HumanResources",
                table: "Employee",
                newName: "HireDate");


            //migrationBuilder.AlterColumn<byte[]>(
            //    name: "Gender",
            //    schema: "HumanResources",
            //    table: "Employee",
            //    type: "varbinary(max)",
            //    fixedLength: true,
            //    nullable: false,
            //    comment: "M = Male, F = Female",
            //    oldClrType: typeof(string),
            //    oldType: "nchar(1)",
            //    oldFixedLength: true,
            //    oldMaxLength: 1,
            //    oldComment: "M = Male, F = Female");

            migrationBuilder.AddColumn<byte[]>(
                name: "GenderTemp",
                schema: "HumanResources",
                table: "Employee",
                nullable: true,
                comment: "M = Male, F = Female");
            
            migrationBuilder.Sql(@"UPDATE HumanResources.Employee SET GenderTemp = CONVERT(varbinary, Gender)");
            
            migrationBuilder.DropColumn("Gender", "Employee", "HumanResources");
            
            migrationBuilder.RenameColumn(
                name: "GenderTemp",
                schema: "HumanResources",
                table: "Employee",
                newName: "Gender");

            //migrationBuilder.AlterColumn<byte[]>(
            //    name: "BirthDate",
            //    schema: "HumanResources",
            //    table: "Employee",
            //    type: "varbinary(max)",
            //    nullable: true,
            //    comment: "Date of birth.",
            //    oldClrType: typeof(DateTime),
            //    oldType: "date",
            //    oldComment: "Date of birth.");
            migrationBuilder.AddColumn<byte[]>(
                name: "BirthDateTemp",
                schema: "HumanResources",
                table: "Employee",
                nullable: true,
                comment: "Date of birth.");

            migrationBuilder.Sql(@"UPDATE HumanResources.Employee SET BirthDateTemp = CONVERT(varbinary, BirthDate)");

            migrationBuilder.DropColumn("BirthDate", "Employee", "HumanResources");

            migrationBuilder.RenameColumn(
                name: "BirthDateTemp",
                schema: "HumanResources",
                table: "Employee",
                newName: "BirthDate");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "NationalIdnumber",
            //    schema: "HumanResources",
            //    table: "Employee",
            //    newName: "NationalIDNumber");

            //migrationBuilder.AlterColumn<bool>(
            //    name: "SalariedFlag",
            //    schema: "HumanResources",
            //    table: "Employee",
            //    type: "bit",
            //    nullable: false,
            //    defaultValueSql: "((1))",
            //    comment: "Job classification. 0 = Hourly, not exempt from collective bargaining. 1 = Salaried, exempt from collective bargaining.",
            //    oldClrType: typeof(bool),
            //    oldType: "bit",
            //    oldNullable: true,
            //    oldDefaultValueSql: "((1))",
            //    oldComment: "Job classification. 0 = Hourly, not exempt from collective bargaining. 1 = Salaried, exempt from collective bargaining.");

            //migrationBuilder.AlterColumn<string>(
            //    name: "NationalIDNumber",
            //    schema: "HumanResources",
            //    table: "Employee",
            //    type: "nvarchar(15)",
            //    maxLength: 15,
            //    nullable: false,
            //    comment: "Unique national identification number such as a social security number.",
            //    oldClrType: typeof(byte[]),
            //    oldType: "varbinary(900)",
            //    oldComment: "Unique national identification number such as a social security number.");

            //migrationBuilder.AlterColumn<string>(
            //    name: "MaritalStatus",
            //    schema: "HumanResources",
            //    table: "Employee",
            //    type: "nchar(1)",
            //    fixedLength: true,
            //    maxLength: 1,
            //    nullable: false,
            //    comment: "M = Married, S = Single",
            //    oldClrType: typeof(byte[]),
            //    oldType: "varbinary(max)",
            //    oldFixedLength: true,
            //    oldComment: "M = Married, S = Single");

            //migrationBuilder.AlterColumn<string>(
            //    name: "JobTitle",
            //    schema: "HumanResources",
            //    table: "Employee",
            //    type: "nvarchar(50)",
            //    maxLength: 50,
            //    nullable: false,
            //    comment: "Work title such as Buyer or Sales Representative.",
            //    oldClrType: typeof(byte[]),
            //    oldType: "varbinary(max)",
            //    oldComment: "Work title such as Buyer or Sales Representative.");

            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "HireDate",
            //    schema: "HumanResources",
            //    table: "Employee",
            //    type: "date",
            //    nullable: false,
            //    defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            //    comment: "Employee hired on this date.",
            //    oldClrType: typeof(byte[]),
            //    oldType: "varbinary(max)",
            //    oldNullable: true,
            //    oldComment: "Employee hired on this date.");

            //migrationBuilder.AlterColumn<string>(
            //    name: "Gender",
            //    schema: "HumanResources",
            //    table: "Employee",
            //    type: "nchar(1)",
            //    fixedLength: true,
            //    maxLength: 1,
            //    nullable: false,
            //    comment: "M = Male, F = Female",
            //    oldClrType: typeof(byte[]),
            //    oldType: "varbinary(max)",
            //    oldFixedLength: true,
            //    oldComment: "M = Male, F = Female");

            //migrationBuilder.AlterColumn<DateTime>(
            //    name: "BirthDate",
            //    schema: "HumanResources",
            //    table: "Employee",
            //    type: "date",
            //    nullable: false,
            //    defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            //    comment: "Date of birth.",
            //    oldClrType: typeof(byte[]),
            //    oldType: "varbinary(max)",
            //    oldNullable: true,
            //    oldComment: "Date of birth.");
        }
    }
}
