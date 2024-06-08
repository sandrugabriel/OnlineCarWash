using FluentMigrator;

namespace OnlineCarWash.Data.Migrations
{
    [Migration(2)]
    public class CreateTabels : Migration
    {

        public override void Up()
        {

            Create.Table("customers")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Name").AsString().NotNullable()
            .WithColumn("Email").AsString().NotNullable()
            .WithColumn("Password").AsString().NotNullable()
            .WithColumn("PhoneNumber").AsString().NotNullable();

            Create.Table("options")
                    .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                    .WithColumn("Name").AsString().NotNullable()
                    .WithColumn("Price").AsInt32().NotNullable();

            Create.Table("services")
                       .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                       .WithColumn("Name").AsString().NotNullable()
                       .WithColumn("Descriptions").AsString().NotNullable()
                       .WithColumn("Price").AsInt32().NotNullable()
                       .WithColumn("Type").AsString().NotNullable();

            Create.Table("serviceoptions")
                  .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                  .WithColumn("OptionId").AsInt32().NotNullable()
                  .WithColumn("ServiceId").AsInt32().NotNullable();

        }

        
/*
           
            Create.Table("appointments")
            .WithColumn("Id").AsInt32().PrimaryKey().NotNullable()
            .WithColumn("CustomerId").AsInt32().NotNullable()
            .WithColumn("ServiceId").AsInt32().NotNullable()
            .WithColumn("OptionId").AsInt32().NotNullable()
            .WithColumn("TotalAmount").AsInt32().NotNullable();*/

        public override void Down()
        {

        }
    }
}
