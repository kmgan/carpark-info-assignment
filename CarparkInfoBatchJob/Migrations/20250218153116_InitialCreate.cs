﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarparkInfoBatchJob.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carparks",
                columns: table => new
                {
                    car_park_no = table.Column<string>(type: "TEXT", nullable: false),
                    address = table.Column<string>(type: "TEXT", nullable: false),
                    x_coord = table.Column<float>(type: "REAL", nullable: false),
                    y_coord = table.Column<float>(type: "REAL", nullable: false),
                    car_park_type = table.Column<string>(type: "TEXT", nullable: false),
                    type_of_parking_system = table.Column<string>(type: "TEXT", nullable: false),
                    short_term_parking = table.Column<string>(type: "TEXT", nullable: false),
                    free_parking = table.Column<string>(type: "TEXT", nullable: false),
                    night_parking = table.Column<bool>(type: "INTEGER", nullable: false),
                    car_park_decks = table.Column<int>(type: "INTEGER", nullable: false),
                    gantry_height = table.Column<float>(type: "REAL", nullable: false),
                    car_park_basement = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carparks", x => x.car_park_no);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carparks");
        }
    }
}
