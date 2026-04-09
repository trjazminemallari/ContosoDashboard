using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContosoDashboard.Migrations
{
    /// <inheritdoc />
    public partial class AddDocumentShares : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Announcements",
                keyColumn: "AnnouncementId",
                keyValue: 1,
                columns: new[] { "ExpiryDate", "PublishDate" },
                values: new object[] { new DateTime(2026, 5, 8, 18, 10, 34, 420, DateTimeKind.Utc).AddTicks(3420), new DateTime(2026, 4, 8, 18, 10, 34, 420, DateTimeKind.Utc).AddTicks(3420) });

            migrationBuilder.UpdateData(
                table: "ProjectMembers",
                keyColumn: "ProjectMemberId",
                keyValue: 1,
                column: "AssignedDate",
                value: new DateTime(2026, 3, 9, 18, 10, 34, 420, DateTimeKind.Utc).AddTicks(3390));

            migrationBuilder.UpdateData(
                table: "ProjectMembers",
                keyColumn: "ProjectMemberId",
                keyValue: 2,
                column: "AssignedDate",
                value: new DateTime(2026, 3, 9, 18, 10, 34, 420, DateTimeKind.Utc).AddTicks(3390));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "StartDate", "TargetCompletionDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 3, 9, 18, 10, 34, 420, DateTimeKind.Utc).AddTicks(3330), new DateTime(2026, 3, 9, 18, 10, 34, 420, DateTimeKind.Utc).AddTicks(3320), new DateTime(2026, 6, 7, 18, 10, 34, 420, DateTimeKind.Utc).AddTicks(3330), new DateTime(2026, 4, 8, 18, 10, 34, 420, DateTimeKind.Utc).AddTicks(3330) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "DueDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 3, 9, 18, 10, 34, 420, DateTimeKind.Utc).AddTicks(3360), new DateTime(2026, 3, 19, 18, 10, 34, 420, DateTimeKind.Utc).AddTicks(3350), new DateTime(2026, 3, 19, 18, 10, 34, 420, DateTimeKind.Utc).AddTicks(3360) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "DueDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 3, 14, 18, 10, 34, 420, DateTimeKind.Utc).AddTicks(3360), new DateTime(2026, 4, 13, 18, 10, 34, 420, DateTimeKind.Utc).AddTicks(3360), new DateTime(2026, 4, 8, 18, 10, 34, 420, DateTimeKind.Utc).AddTicks(3360) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 3,
                columns: new[] { "CreatedDate", "DueDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 3, 19, 18, 10, 34, 420, DateTimeKind.Utc).AddTicks(3370), new DateTime(2026, 4, 18, 18, 10, 34, 420, DateTimeKind.Utc).AddTicks(3360), new DateTime(2026, 3, 19, 18, 10, 34, 420, DateTimeKind.Utc).AddTicks(3370) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 4, 8, 18, 10, 34, 420, DateTimeKind.Utc).AddTicks(3120));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 4, 8, 18, 10, 34, 420, DateTimeKind.Utc).AddTicks(3200));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 4, 8, 18, 10, 34, 420, DateTimeKind.Utc).AddTicks(3200));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 4, 8, 18, 10, 34, 420, DateTimeKind.Utc).AddTicks(3210));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Announcements",
                keyColumn: "AnnouncementId",
                keyValue: 1,
                columns: new[] { "ExpiryDate", "PublishDate" },
                values: new object[] { new DateTime(2026, 5, 8, 11, 29, 18, 638, DateTimeKind.Utc).AddTicks(5300), new DateTime(2026, 4, 8, 11, 29, 18, 638, DateTimeKind.Utc).AddTicks(5290) });

            migrationBuilder.UpdateData(
                table: "ProjectMembers",
                keyColumn: "ProjectMemberId",
                keyValue: 1,
                column: "AssignedDate",
                value: new DateTime(2026, 3, 9, 11, 29, 18, 638, DateTimeKind.Utc).AddTicks(5260));

            migrationBuilder.UpdateData(
                table: "ProjectMembers",
                keyColumn: "ProjectMemberId",
                keyValue: 2,
                column: "AssignedDate",
                value: new DateTime(2026, 3, 9, 11, 29, 18, 638, DateTimeKind.Utc).AddTicks(5270));

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "ProjectId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "StartDate", "TargetCompletionDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 3, 9, 11, 29, 18, 638, DateTimeKind.Utc).AddTicks(5130), new DateTime(2026, 3, 9, 11, 29, 18, 638, DateTimeKind.Utc).AddTicks(5130), new DateTime(2026, 6, 7, 11, 29, 18, 638, DateTimeKind.Utc).AddTicks(5130), new DateTime(2026, 4, 8, 11, 29, 18, 638, DateTimeKind.Utc).AddTicks(5130) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 1,
                columns: new[] { "CreatedDate", "DueDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 3, 9, 11, 29, 18, 638, DateTimeKind.Utc).AddTicks(5160), new DateTime(2026, 3, 19, 11, 29, 18, 638, DateTimeKind.Utc).AddTicks(5160), new DateTime(2026, 3, 19, 11, 29, 18, 638, DateTimeKind.Utc).AddTicks(5160) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 2,
                columns: new[] { "CreatedDate", "DueDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 3, 14, 11, 29, 18, 638, DateTimeKind.Utc).AddTicks(5170), new DateTime(2026, 4, 13, 11, 29, 18, 638, DateTimeKind.Utc).AddTicks(5170), new DateTime(2026, 4, 8, 11, 29, 18, 638, DateTimeKind.Utc).AddTicks(5170) });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 3,
                columns: new[] { "CreatedDate", "DueDate", "UpdatedDate" },
                values: new object[] { new DateTime(2026, 3, 19, 11, 29, 18, 638, DateTimeKind.Utc).AddTicks(5230), new DateTime(2026, 4, 18, 11, 29, 18, 638, DateTimeKind.Utc).AddTicks(5230), new DateTime(2026, 3, 19, 11, 29, 18, 638, DateTimeKind.Utc).AddTicks(5230) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2026, 4, 8, 11, 29, 18, 638, DateTimeKind.Utc).AddTicks(5000));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2026, 4, 8, 11, 29, 18, 638, DateTimeKind.Utc).AddTicks(5000));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2026, 4, 8, 11, 29, 18, 638, DateTimeKind.Utc).AddTicks(5010));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2026, 4, 8, 11, 29, 18, 638, DateTimeKind.Utc).AddTicks(5010));
        }
    }
}
