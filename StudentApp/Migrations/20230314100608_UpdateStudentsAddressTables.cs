using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStudentsAddressTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAddress_Student_StudentsId",
                table: "StudentAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentEmailAddress_Student_StudentsId",
                table: "StudentEmailAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentPhoneNo_Student_StudentsId",
                table: "StudentPhoneNo");

            migrationBuilder.CreateTable(
                name: "StudentImage",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentImage", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_StudentImage_Student_StudentsId",
                        column: x => x.StudentsId,
                        principalTable: "Student",
                        principalColumn: "StudentId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentImage_StudentsId",
                table: "StudentImage",
                column: "StudentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAddress_Student_StudentsId",
                table: "StudentAddress",
                column: "StudentsId",
                principalTable: "Student",
                principalColumn: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentEmailAddress_Student_StudentsId",
                table: "StudentEmailAddress",
                column: "StudentsId",
                principalTable: "Student",
                principalColumn: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPhoneNo_Student_StudentsId",
                table: "StudentPhoneNo",
                column: "StudentsId",
                principalTable: "Student",
                principalColumn: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAddress_Student_StudentsId",
                table: "StudentAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentEmailAddress_Student_StudentsId",
                table: "StudentEmailAddress");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentPhoneNo_Student_StudentsId",
                table: "StudentPhoneNo");

            migrationBuilder.DropTable(
                name: "StudentImage");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAddress_Student_StudentsId",
                table: "StudentAddress",
                column: "StudentsId",
                principalTable: "Student",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentEmailAddress_Student_StudentsId",
                table: "StudentEmailAddress",
                column: "StudentsId",
                principalTable: "Student",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentPhoneNo_Student_StudentsId",
                table: "StudentPhoneNo",
                column: "StudentsId",
                principalTable: "Student",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
