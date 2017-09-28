using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace FootballBetting.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Color",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Color", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_CompetitionType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_CompetitionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Continent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Continent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Country",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Position",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Position", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ResultPrediction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ResultPrediction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Round",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Round", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Balance = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Competition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompetitionTypeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Competition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Competition_tbl_CompetitionType_CompetitionTypeId",
                        column: x => x.CompetitionTypeId,
                        principalTable: "tbl_CompetitionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_CountryContinent",
                columns: table => new
                {
                    ContinentId = table.Column<int>(type: "int", nullable: false),
                    CountryId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_CountryContinent", x => new { x.ContinentId, x.CountryId });
                    table.ForeignKey(
                        name: "FK_tbl_CountryContinent_tbl_Continent_ContinentId",
                        column: x => x.ContinentId,
                        principalTable: "tbl_Continent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_CountryContinent_tbl_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "tbl_Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Town",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CountryId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Town", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Town_tbl_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "tbl_Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Bet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BetMoney = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    DatePut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Bet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Bet_tbl_User_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Team",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Budget = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Initials = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    PrimaryKitColorId = table.Column<int>(type: "int", nullable: false),
                    SecondaryKitColorId = table.Column<int>(type: "int", nullable: false),
                    TownId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Team", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Team_tbl_Color_PrimaryKitColorId",
                        column: x => x.PrimaryKitColorId,
                        principalTable: "tbl_Color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_Team_tbl_Color_SecondaryKitColorId",
                        column: x => x.SecondaryKitColorId,
                        principalTable: "tbl_Color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_Team_tbl_Town_TownId",
                        column: x => x.TownId,
                        principalTable: "tbl_Town",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Game",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AwayGoals = table.Column<int>(type: "int", nullable: false),
                    AwayTeamId = table.Column<int>(type: "int", nullable: false),
                    AwayTeamWinBetRate = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    CompetitionId = table.Column<int>(type: "int", nullable: false),
                    DrawBetRate = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    HomeGoals = table.Column<int>(type: "int", nullable: false),
                    HomeTeamId = table.Column<int>(type: "int", nullable: false),
                    HomeTeamWinBetRate = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    PlayedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoundId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Game", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Game_tbl_Team_AwayTeamId",
                        column: x => x.AwayTeamId,
                        principalTable: "tbl_Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_Game_tbl_Competition_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "tbl_Competition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_Game_tbl_Team_HomeTeamId",
                        column: x => x.HomeTeamId,
                        principalTable: "tbl_Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_Game_tbl_Round_RoundId",
                        column: x => x.RoundId,
                        principalTable: "tbl_Round",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Player",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsCurrentlyInjured = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PositionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Squadnumber = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Player", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Player_tbl_Position_PositionId",
                        column: x => x.PositionId,
                        principalTable: "tbl_Position",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_Player_tbl_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "tbl_Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BetGame",
                columns: table => new
                {
                    BetId = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    ResultPredictionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BetGame", x => new { x.BetId, x.GameId });
                    table.ForeignKey(
                        name: "FK_tbl_BetGame_tbl_Bet_BetId",
                        column: x => x.BetId,
                        principalTable: "tbl_Bet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_BetGame_tbl_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "tbl_Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_BetGame_tbl_ResultPrediction_ResultPredictionId",
                        column: x => x.ResultPredictionId,
                        principalTable: "tbl_ResultPrediction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PlayerGame",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PlayerGame", x => new { x.PlayerId, x.GameId });
                    table.ForeignKey(
                        name: "FK_tbl_PlayerGame_tbl_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "tbl_Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_PlayerGame_tbl_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "tbl_Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PlayerStatistic",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    MinutesPlayed = table.Column<int>(type: "int", nullable: false),
                    PlayerAssists = table.Column<int>(type: "int", nullable: false),
                    ScoredGoals = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PlayerStatistic", x => new { x.PlayerId, x.GameId });
                    table.ForeignKey(
                        name: "FK_tbl_PlayerStatistic_tbl_Game_GameId",
                        column: x => x.GameId,
                        principalTable: "tbl_Game",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tbl_PlayerStatistic_tbl_Player_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "tbl_Player",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Bet_UserId",
                table: "tbl_Bet",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BetGame_GameId",
                table: "tbl_BetGame",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BetGame_ResultPredictionId",
                table: "tbl_BetGame",
                column: "ResultPredictionId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Competition_CompetitionTypeId",
                table: "tbl_Competition",
                column: "CompetitionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_CountryContinent_CountryId",
                table: "tbl_CountryContinent",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Game_AwayTeamId",
                table: "tbl_Game",
                column: "AwayTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Game_CompetitionId",
                table: "tbl_Game",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Game_HomeTeamId",
                table: "tbl_Game",
                column: "HomeTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Game_RoundId",
                table: "tbl_Game",
                column: "RoundId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Player_PositionId",
                table: "tbl_Player",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Player_TeamId",
                table: "tbl_Player",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PlayerGame_GameId",
                table: "tbl_PlayerGame",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_PlayerStatistic_GameId",
                table: "tbl_PlayerStatistic",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Team_PrimaryKitColorId",
                table: "tbl_Team",
                column: "PrimaryKitColorId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Team_SecondaryKitColorId",
                table: "tbl_Team",
                column: "SecondaryKitColorId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Team_TownId",
                table: "tbl_Team",
                column: "TownId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Town_CountryId",
                table: "tbl_Town",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_BetGame");

            migrationBuilder.DropTable(
                name: "tbl_CountryContinent");

            migrationBuilder.DropTable(
                name: "tbl_PlayerGame");

            migrationBuilder.DropTable(
                name: "tbl_PlayerStatistic");

            migrationBuilder.DropTable(
                name: "tbl_Bet");

            migrationBuilder.DropTable(
                name: "tbl_ResultPrediction");

            migrationBuilder.DropTable(
                name: "tbl_Continent");

            migrationBuilder.DropTable(
                name: "tbl_Game");

            migrationBuilder.DropTable(
                name: "tbl_Player");

            migrationBuilder.DropTable(
                name: "tbl_User");

            migrationBuilder.DropTable(
                name: "tbl_Competition");

            migrationBuilder.DropTable(
                name: "tbl_Round");

            migrationBuilder.DropTable(
                name: "tbl_Position");

            migrationBuilder.DropTable(
                name: "tbl_Team");

            migrationBuilder.DropTable(
                name: "tbl_CompetitionType");

            migrationBuilder.DropTable(
                name: "tbl_Color");

            migrationBuilder.DropTable(
                name: "tbl_Town");

            migrationBuilder.DropTable(
                name: "tbl_Country");
        }
    }
}
