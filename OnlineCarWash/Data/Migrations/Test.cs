using FluentMigrator;

namespace OnlineCarWash.Data.Migrations
{
    [Migration(123)]
    public class Test : Migration
    {
        public override void Up()
        {
            Execute.Script(@"./Data/Script/data.sql");
        }

        public override void Down() { }

    }
}
