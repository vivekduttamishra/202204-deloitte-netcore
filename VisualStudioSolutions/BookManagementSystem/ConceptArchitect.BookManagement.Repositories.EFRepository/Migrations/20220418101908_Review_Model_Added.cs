using Microsoft.EntityFrameworkCore.Migrations;

namespace ConceptArchitect.BookManagement.Repositories.EFRepository.Migrations
{
    public partial class Review_Model_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReviewerEmail = table.Column<string>(nullable: true),
                    BookIsbn = table.Column<string>(nullable: true),
                    ReviewText = table.Column<string>(nullable: true),
                    Rating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Review_Books_BookIsbn",
                        column: x => x.BookIsbn,
                        principalTable: "Books",
                        principalColumn: "Isbn",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Review_Users_ReviewerEmail",
                        column: x => x.ReviewerEmail,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Review_BookIsbn",
                table: "Review",
                column: "BookIsbn");

            migrationBuilder.CreateIndex(
                name: "IX_Review_ReviewerEmail",
                table: "Review",
                column: "ReviewerEmail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Review");
        }
    }
}
