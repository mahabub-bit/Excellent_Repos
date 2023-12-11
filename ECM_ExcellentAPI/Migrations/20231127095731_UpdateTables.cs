using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECM_ExcellentAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CBusinessPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CGST = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CBank = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CBankAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CBankBranch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CIFSC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CC1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CC2 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CC3 = table.Column<int>(type: "int", nullable: true),
                    CC4 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerLandLine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerFax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerCompany = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerJobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerHomeNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerZip = table.Column<int>(type: "int", nullable: false),
                    CustomerPan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerAccNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerBank = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerBankBranch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerBankDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerGSTIN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerDetails1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerDetails2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerDetails3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerDetails4 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryImage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Supplier_Company_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Supplier_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Supplier_JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Supplier_Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Supplier_Pan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Supplier_Contact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Supplier_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Supplier_Webpage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Supplier_Business_Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Supplier_Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Supplier_Home_Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Supplier_Moblie_Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Supplier_City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Supplier_State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Supplier_ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Supplier_Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Supplier_Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Supplier_D = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Suppliers_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomersAddress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ShipHNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShipStreet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShipCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShipState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShipCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShipZip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShipCol1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShipCol2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShipCol3 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomersAddress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomersAddress_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CatTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CatTypeDesc2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CatTypeDesc3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CatTypeDesc4 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryTypes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderStatusId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    OrderAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ShipDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShipAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Taxes = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShipAddressId = table.Column<int>(type: "int", nullable: false),
                    CustomerAddressId = table.Column<int>(type: "int", nullable: true),
                    OrderCloseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderDesc1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderDesc2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderDesc3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_CustomersAddress_CustomerAddressId",
                        column: x => x.CustomerAddressId,
                        principalTable: "CustomersAddress",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_OrderStatuses_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalTable: "OrderStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CategoryTypeId = table.Column<int>(type: "int", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    QtyPerUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PackageSize = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RetailerPrice = table.Column<double>(type: "float", nullable: false),
                    MRPPrice = table.Column<double>(type: "float", nullable: false),
                    Gst = table.Column<double>(type: "float", nullable: false),
                    GstSlab = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discontinued = table.Column<bool>(type: "bit", nullable: false),
                    CostPrice = table.Column<double>(type: "float", nullable: false),
                    PDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PAddColumn1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PAddColumn2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PAddColumn3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PAddColumn4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PAddColumn5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PAddColumn6 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_CategoryTypes_CategoryTypeId",
                        column: x => x.CategoryTypeId,
                        principalTable: "CategoryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Products_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "OrdersDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderItemAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CGst = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SGst = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxableValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderDeatailStatusId = table.Column<int>(type: "int", nullable: false),
                    OrderStatusId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    OrderDetailDesc1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderDetailDesc2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderDetailDesc3 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdersDetail_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdersDetail_OrderStatuses_OrderStatusId",
                        column: x => x.OrderStatusId,
                        principalTable: "OrderStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrdersDetail_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_OrdersDetail_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ProductRates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Daily_Product_Price = table.Column<double>(type: "float", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CategoryTypeId = table.Column<int>(type: "int", nullable: false),
                    PRH_AddColunm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PRH_AddColunm2 = table.Column<int>(type: "int", nullable: false),
                    PRH_AddColunm3 = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PRH_AddColunm4 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductRates_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductRates_CategoryTypes_CategoryTypeId",
                        column: x => x.CategoryTypeId,
                        principalTable: "CategoryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductRates_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PO_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<float>(type: "real", nullable: false),
                    PO_Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PO_TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PO_Invoice = table.Column<int>(type: "int", nullable: false),
                    ShippingFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Taxes = table.Column<float>(type: "real", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentType = table.Column<int>(type: "int", nullable: false),
                    ClosedBy = table.Column<int>(type: "int", nullable: false),
                    PaymentAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ExpectedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClosedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    AddPurchaseColumn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddPurchaseColumn2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddPurchaseColumn3 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrdersDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseId = table.Column<int>(type: "int", nullable: false),
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    PodQty = table.Column<int>(type: "int", nullable: false),
                    PodUnitPrice = table.Column<float>(type: "real", nullable: false),
                    PodTotalPrice = table.Column<float>(type: "real", nullable: false),
                    PodDiscount = table.Column<float>(type: "real", nullable: false),
                    PodTaxableValue = table.Column<float>(type: "real", nullable: false),
                    SGst = table.Column<float>(type: "real", nullable: false),
                    CGst = table.Column<float>(type: "real", nullable: false),
                    PodItemAmount = table.Column<float>(type: "real", nullable: false),
                    PodMfgDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PurchaseInvoiceNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PodAddInfo1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PodAddInfo2 = table.Column<int>(type: "int", nullable: false),
                    PodAddInfo3 = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PodAddInfo4 = table.Column<float>(type: "real", nullable: false),
                    PodAddInfo5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PodAddInfo6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PodAddInfo7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PodAddInfo8 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrdersDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrdersDetail_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrdersDetail_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CompanyId",
                table: "Categories",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryTypes_CategoryId",
                table: "CategoryTypes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomersAddress_CustomerId",
                table: "CustomersAddress",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CompanyId",
                table: "Orders",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerAddressId",
                table: "Orders",
                column: "CustomerAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersDetail_CompanyId",
                table: "OrdersDetail",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersDetail_OrderId",
                table: "OrdersDetail",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersDetail_OrderStatusId",
                table: "OrdersDetail",
                column: "OrderStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersDetail_ProductId",
                table: "OrdersDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRates_CategoryId",
                table: "ProductRates",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRates_CategoryTypeId",
                table: "ProductRates",
                column: "CategoryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRates_ProductId",
                table: "ProductRates",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryTypeId",
                table: "Products",
                column: "CategoryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CompanyId",
                table: "Products",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierId",
                table: "Products",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_CompanyId",
                table: "PurchaseOrders",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_ProductId",
                table: "PurchaseOrders",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_SupplierId",
                table: "PurchaseOrders",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_UserId",
                table: "PurchaseOrders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrdersDetail_ProductId",
                table: "PurchaseOrdersDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrdersDetail_PurchaseOrderId",
                table: "PurchaseOrdersDetail",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_CompanyId",
                table: "Suppliers",
                column: "CompanyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrdersDetail");

            migrationBuilder.DropTable(
                name: "ProductRates");

            migrationBuilder.DropTable(
                name: "PurchaseOrdersDetail");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "CustomersAddress");

            migrationBuilder.DropTable(
                name: "OrderStatuses");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "CategoryTypes");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
