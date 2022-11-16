using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgileWebApi.Migrations
{
    public partial class elevatorfloors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ElevatorType",
                table: "Elevators",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MaxFloor",
                table: "Elevators",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinFloor",
                table: "Elevators",
                type: "int",
                nullable: false,
                defaultValue: 0);

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropColumn(
                name: "ElevatorType",
                table: "Elevators");

            migrationBuilder.DropColumn(
                name: "MaxFloor",
                table: "Elevators");

            migrationBuilder.DropColumn(
                name: "MinFloor",
                table: "Elevators");
        }
    }
}
