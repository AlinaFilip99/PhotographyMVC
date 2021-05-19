using Microsoft.EntityFrameworkCore.Migrations;

namespace Photography.DataAccess.Migrations
{
    public partial class Account : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactForms_AspNetUsers_AccountId1",
                table: "ContactForms");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_AccountId1",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_AccountId1",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_ContactForms_AccountId1",
                table: "ContactForms");

            migrationBuilder.DropColumn(
                name: "AccountId1",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "AccountId1",
                table: "ContactForms");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "Posts",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "ContactForms",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AccountId",
                table: "Posts",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactForms_AccountId",
                table: "ContactForms",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactForms_AspNetUsers_AccountId",
                table: "ContactForms",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_AccountId",
                table: "Posts",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactForms_AspNetUsers_AccountId",
                table: "ContactForms");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_AccountId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_AccountId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_ContactForms_AccountId",
                table: "ContactForms");

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "Posts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountId1",
                table: "Posts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "ContactForms",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountId1",
                table: "ContactForms",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AccountId1",
                table: "Posts",
                column: "AccountId1");

            migrationBuilder.CreateIndex(
                name: "IX_ContactForms_AccountId1",
                table: "ContactForms",
                column: "AccountId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactForms_AspNetUsers_AccountId1",
                table: "ContactForms",
                column: "AccountId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_AccountId1",
                table: "Posts",
                column: "AccountId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
