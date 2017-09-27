using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNetwork.Data.Migrations
{
    public partial class AddedAlbums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Album",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BackgroundColor = table.Column<int>(type: "int", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Album", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Album_tbl_User_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "tbl_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Picture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Caption = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Picture", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_AlbumParticipates",
                columns: table => new
                {
                    AlbumId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_AlbumParticipates", x => new { x.AlbumId, x.UserId });
                    table.ForeignKey(
                        name: "FK_tbl_AlbumParticipates_tbl_Album_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "tbl_Album",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_AlbumParticipates_tbl_User_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_AlbumPictures",
                columns: table => new
                {
                    AlbumId = table.Column<int>(type: "int", nullable: false),
                    PictureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_AlbumPictures", x => new { x.AlbumId, x.PictureId });
                    table.ForeignKey(
                        name: "FK_tbl_AlbumPictures_tbl_Album_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "tbl_Album",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_AlbumPictures_tbl_Picture_PictureId",
                        column: x => x.PictureId,
                        principalTable: "tbl_Picture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_AlbumTags",
                columns: table => new
                {
                    AlbumId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_AlbumTags", x => new { x.AlbumId, x.TagId });
                    table.ForeignKey(
                        name: "FK_tbl_AlbumTags_tbl_Album_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "tbl_Album",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_AlbumTags_tbl_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "tbl_Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Album_OwnerId",
                table: "tbl_Album",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_AlbumParticipates_UserId",
                table: "tbl_AlbumParticipates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_AlbumPictures_PictureId",
                table: "tbl_AlbumPictures",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_AlbumTags_TagId",
                table: "tbl_AlbumTags",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_AlbumParticipates");

            migrationBuilder.DropTable(
                name: "tbl_AlbumPictures");

            migrationBuilder.DropTable(
                name: "tbl_AlbumTags");

            migrationBuilder.DropTable(
                name: "tbl_Picture");

            migrationBuilder.DropTable(
                name: "tbl_Album");

            migrationBuilder.DropTable(
                name: "tbl_Tag");
        }
    }
}