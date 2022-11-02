using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class labeltab : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_collabratorTable_NotesTable_NoteID",
                table: "collabratorTable");

            migrationBuilder.RenameColumn(
                name: "NoteID",
                table: "collabratorTable",
                newName: "NoteId");

            migrationBuilder.RenameIndex(
                name: "IX_collabratorTable_NoteID",
                table: "collabratorTable",
                newName: "IX_collabratorTable_NoteId");

            migrationBuilder.CreateTable(
                name: "LabelTable",
                columns: table => new
                {
                    LabelId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label_Name = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    NoteId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabelTable", x => x.LabelId);
                    table.ForeignKey(
                        name: "FK_LabelTable_NotesTable_NoteId",
                        column: x => x.NoteId,
                        principalTable: "NotesTable",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LabelTable_UserTable_UserId",
                        column: x => x.UserId,
                        principalTable: "UserTable",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LabelTable_NoteId",
                table: "LabelTable",
                column: "NoteId");

            migrationBuilder.CreateIndex(
                name: "IX_LabelTable_UserId",
                table: "LabelTable",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_collabratorTable_NotesTable_NoteId",
                table: "collabratorTable",
                column: "NoteId",
                principalTable: "NotesTable",
                principalColumn: "NoteId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_collabratorTable_NotesTable_NoteId",
                table: "collabratorTable");

            migrationBuilder.DropTable(
                name: "LabelTable");

            migrationBuilder.RenameColumn(
                name: "NoteId",
                table: "collabratorTable",
                newName: "NoteID");

            migrationBuilder.RenameIndex(
                name: "IX_collabratorTable_NoteId",
                table: "collabratorTable",
                newName: "IX_collabratorTable_NoteID");

            migrationBuilder.AddForeignKey(
                name: "FK_collabratorTable_NotesTable_NoteID",
                table: "collabratorTable",
                column: "NoteID",
                principalTable: "NotesTable",
                principalColumn: "NoteId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
