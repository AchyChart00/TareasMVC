using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TareasMVC.Migrations
{
    /// <inheritdoc />
    public partial class AdminRol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF NOT EXISTS(SELECT Id FROM AspNetRoles WHERE Id = '713416fd-1b99-4fbd-9803-70f190728e25')
                                    BEGIN

	                                    INSERT AspNetRoles(Id, [Name], [NormalizedName])
	                                    VALUES('713416fd-1b99-4fbd-9803-70f190728e25', 'admin', 'ADMIN')

                                    END
                                    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM AspNetRoles WHERE Id = '713416fd-1b99-4fbd-9803-70f190728e25'");
        }
    }
}
