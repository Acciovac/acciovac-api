using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace acciovac.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLocalExperienceFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LocationName",
                table: "LocalExpirences",
                newName: "expireancename");

            migrationBuilder.Sql(
                "CREATE TABLE IF NOT EXISTS \"Messages\" (" +
                "\"Id\" uuid NOT NULL," +
                "\"UserId\" uuid NOT NULL," +
                "\"Subject\" character varying(256) NOT NULL," +
                "\"Body\" text NOT NULL," +
                "\"IsRead\" boolean NOT NULL," +
                "\"IsResolved\" boolean NOT NULL," +
                "\"ResolvedAt\" timestamp with time zone," +
                "\"CreatedAt\" timestamp with time zone NOT NULL," +
                "\"CreatedBy\" text NOT NULL," +
                "\"LastModifiedAt\" timestamp with time zone," +
                "\"LastModifiedBy\" text," +
                "CONSTRAINT \"PK_Messages\" PRIMARY KEY (\"Id\")" +
                ");");

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LocalExpirencesId = table.Column<Guid>(type: "uuid", nullable: false),
                    PhotoUrl = table.Column<string>(type: "text", nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_LocalExpirences_LocalExpirencesId",
                        column: x => x.LocalExpirencesId,
                        principalTable: "LocalExpirences",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.Sql(
                "CREATE INDEX IF NOT EXISTS \"IX_Messages_CreatedAt\" ON \"Messages\" (\"CreatedAt\");");

            migrationBuilder.Sql(
                "CREATE INDEX IF NOT EXISTS \"IX_Messages_UserId\" ON \"Messages\" (\"UserId\");");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_LocalExpirencesId",
                table: "Photos",
                column: "LocalExpirencesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.RenameColumn(
                name: "expireancename",
                table: "LocalExpirences",
                newName: "LocationName");
        }
    }
}
