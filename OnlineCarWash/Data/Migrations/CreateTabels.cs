using FluentMigrator;
using Microsoft.Extensions.Options;
using OnlineCarWash.Customers.Models;
using OnlineCarWash.Services.Models;
using OnlineCarWash.ServicesOptions.Models;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace OnlineCarWash.Data.Migrations
{
    [Migration(2)]
    public class CreateTabels : Migration
    {

        public override void Up()
        {

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

            Create.Table("appointments")
                        .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                        .WithColumn("CustomerId").AsInt32().NotNullable()
                        .WithColumn("ServiceId").AsInt32().NotNullable()
                        .WithColumn("OptionId").AsInt32().NotNullable()
                        .WithColumn("ReservationDate").AsDateTime().NotNullable()
                        .WithColumn("TotalAmount").AsInt32().NotNullable();



            Create.Table("customers")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("UserName").AsString(256).Nullable()
            .WithColumn("NormalizedUserName").AsString(256).Nullable()
            .WithColumn("Email").AsString(256).Nullable()
            .WithColumn("NormalizedEmail").AsString(256).Nullable()
            .WithColumn("EmailConfirmed").AsBoolean().NotNullable()
            .WithColumn("PasswordHash").AsString().Nullable()
            .WithColumn("SecurityStamp").AsString().Nullable()
            .WithColumn("ConcurrencyStamp").AsString().Nullable()
            .WithColumn("PhoneNumber").AsString().Nullable()
            .WithColumn("PhoneNumberConfirmed").AsBoolean().NotNullable()
            .WithColumn("TwoFactorEnabled").AsBoolean().NotNullable()
            .WithColumn("LockoutEnd").AsDateTime().Nullable()
            .WithColumn("LockoutEnabled").AsBoolean().NotNullable()
            .WithColumn("AccessFailedCount").AsInt32().NotNullable()
            .WithColumn("Name").AsString().NotNullable()
            .WithColumn("Discriminator").AsString().NotNullable();

            Create.Table("AspNetRoles")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(256).Nullable()
                .WithColumn("NormalizedName").AsString().Nullable()
                .WithColumn("ConcurrencyStamp").AsInt32().Nullable();

            Create.Table("AspNetUserRoles")
                .WithColumn("UserId").AsInt32().NotNullable()
                .WithColumn("RoleId").AsInt32().NotNullable()
                .ForeignKey("FK_AspNetUserRoles_Customers","Customers","Id")
                .ForeignKey("FK_AspNetUserRoles_AspNetRoles", "AspNetRoles", "Id");

            Create.Table("AspNetRolesClaims")
             .WithColumn("Id").AsInt32().PrimaryKey().Identity()
             .WithColumn("RoleId").AsInt32().NotNullable()
             .WithColumn("ClaimType").AsString(256).Nullable()
             .WithColumn("ClaimValue").AsInt32().Nullable()
             .ForeignKey("FK_AspNetRolesClaims_AspNetRoles", "AspNetRoles","Id");

            Create.Table("AspNetUserClaims")
             .WithColumn("Id").AsInt32().PrimaryKey().Identity()
             .WithColumn("UserId").AsInt32().NotNullable()
             .WithColumn("ClaimType").AsString().Nullable()
             .WithColumn("ClaimValue").AsInt32().Nullable()
             .ForeignKey("FK_AspNetRolesClaims_Customers", "Customers", "Id");

            Create.Table("AspNetUserLogins")
           .WithColumn("LoginProvider").AsString(256).PrimaryKey()
           .WithColumn("ProviderKey").AsString(256).PrimaryKey()
           .WithColumn("ProviderDisplayName").AsString().Nullable()
           .WithColumn("UserId").AsInt32().NotNullable()
           .ForeignKey("FK_AspNetUserLogins_Customers", "Customers", "Id");


            Create.Table("AspNetUserTokens")
           .WithColumn("UserId").AsInt32().PrimaryKey()
           .WithColumn("LoginProvider").AsString(256).PrimaryKey()
           .WithColumn("Name").AsString(256).PrimaryKey()
           .WithColumn("Value").AsInt32().Nullable()
           .ForeignKey("FK_AspNetUserTokens_Customers", "Customers", "Id");

          //  CreateCustomers();
            CreateServices();
            CreateOption();
            CreateServiceOptions();
            CreateRoles();
            AddPermissionsToRoles();
        }

        public override void Down()
        {

        }

        private void CreateRoles()
        {
            Insert.IntoTable("AspNetRoles").Row(new { Name = "Admin", NormalizedName = "ADMIN" });
            Insert.IntoTable("AspNetRoles").Row(new { Name = "Editor", NormalizedName = "EDITOR" });
        }

        private void CreateCustomers()
        {
            Insert.IntoTable("Customers").Row(new {Name = "gabi", Email = "gabi@gmail.com", Password = "gabi1234",PhoneNumber = "07737777"});
            Insert.IntoTable("Customers").Row(new { Name = "filip", Email = "fil@gmail.com", Password = "filip1234", PhoneNumber = "07757777" });
            Insert.IntoTable("Customers").Row(new { Name = "ana", Email = "ana@gmail.com", Password = "ana1234", PhoneNumber = "07767777" });
            Insert.IntoTable("Customers").Row(new { Name = "david", Email = "d@gmail.com", Password = "davi1234", PhoneNumber = "07797777" });

        }

        private void CreateServices()
        {

            Insert.IntoTable("Services").Row(new {Name = "standard", Descriptions = "asdasd", Price = 50, Type = "suv"});
            Insert.IntoTable("Services").Row(new { Name = "test", Descriptions = "asdasasdasd", Price = 60, Type = "suv" });
            Insert.IntoTable("Services").Row(new { Name = "test1", Descriptions = "asdgfdfgsdasd", Price = 50, Type = "suv" });
            Insert.IntoTable("Services").Row(new { Name = "test2", Descriptions = "asdasasdasdd", Price = 40, Type = "suv" });

        }

        public void CreateOption()
        {
            Insert.IntoTable("Options").Row(new {Name = "cleaning inside" , Price = 30});
        }

        public void CreateServiceOptions()
        {
            Insert.IntoTable("serviceoptions").Row(new { OptionId = 1, ServiceId = 1});
        }

        private void AddPermissionsToRoles()
        {
            Insert.IntoTable("AspNetRolesClaims").Row(new
            {
                RoleId = "1",
                ClaimType = "Permission",
                ClaimValue = "1"
            });

            Insert.IntoTable("AspNetRolesClaims").Row(new
            {
                RoleId = "2",
                ClaimType = "Permission",
                ClaimValue = "1"
            });
        }
    }
}
