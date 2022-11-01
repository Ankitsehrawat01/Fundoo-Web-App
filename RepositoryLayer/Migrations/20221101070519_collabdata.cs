using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class collabdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "collabratorTable",
                columns: table => new
                {
                    CollabratorId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    NoteID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_collabratorTable", x => x.CollabratorId);
                    table.ForeignKey(
                        name: "FK_collabratorTable_NotesTable_NoteID",
                        column: x => x.NoteID,
                        principalTable: "NotesTable",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_collabratorTable_UserTable_UserId",
                        column: x => x.UserId,
                        principalTable: "UserTable",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_collabratorTable_NoteID",
                table: "collabratorTable",
                column: "NoteID");

            migrationBuilder.CreateIndex(
                name: "IX_collabratorTable_UserId",
                table: "collabratorTable",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "collabratorTable");
        }
    }
}
