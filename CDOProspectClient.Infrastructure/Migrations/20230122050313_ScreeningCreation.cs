using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CDOProspectClient.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ScreeningCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Properties",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.CreateTable(
                name: "Screenings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    InterviewedBy = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Conforme = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Remarks = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screenings", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BuyerInformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ScreeningId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ContactNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Citizenship = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Facebook = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gender = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CivilStatus = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Financing = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuyerInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuyerInformations_Screenings_ScreeningId",
                        column: x => x.ScreeningId,
                        principalTable: "Screenings",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Computations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ScreeningId = table.Column<int>(type: "int", nullable: false),
                    SellingPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    NetSellingPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TaxesAndFees = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TotalReceivable = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    NumberOfDownpayments = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    EMA = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    GrossIncome = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MonthlyIncomeRatio = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Computations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Computations_Screenings_ScreeningId",
                        column: x => x.ScreeningId,
                        principalTable: "Screenings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ScreeningId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_Screenings_ScreeningId",
                        column: x => x.ScreeningId,
                        principalTable: "Screenings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EmployerDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BuyerInformationId = table.Column<int>(type: "int", nullable: false),
                    CompanyName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContactNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImmedaiteSuperior = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployerDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployerDetail_BuyerInformations_BuyerInformationId",
                        column: x => x.BuyerInformationId,
                        principalTable: "BuyerInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NegativeDataBankRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BuyerInformationId = table.Column<int>(type: "int", nullable: false),
                    CancelledCreditCard = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    BouncedCheck = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PendingCourtCases = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UnpaidTelecomBill = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Others = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NegativeDataBankRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NegativeDataBankRecord_BuyerInformations_BuyerInformationId",
                        column: x => x.BuyerInformationId,
                        principalTable: "BuyerInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PagIbigMembership",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BuyerInformationId = table.Column<int>(type: "int", nullable: false),
                    PagIBIGMembership = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    NumberOfYears = table.Column<int>(type: "int", nullable: false),
                    WOHML = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Updated = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    WFHL = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PagIbigMembership", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PagIbigMembership_BuyerInformations_BuyerInformationId",
                        column: x => x.BuyerInformationId,
                        principalTable: "BuyerInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TitlingInstruction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BuyerInformationId = table.Column<int>(type: "int", nullable: false),
                    TitlingInstructionOption = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Data = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitlingInstruction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TitlingInstruction_BuyerInformations_BuyerInformationId",
                        column: x => x.BuyerInformationId,
                        principalTable: "BuyerInformations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SourceOfIncome",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DocumentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceOfIncome", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SourceOfIncome_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StandardDocument",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    GovtIssuedValidIds = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GovtIssuedSpouseValidIds = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TINNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthCertificate = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MarriageCertificate = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClearOneByOnePicture = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProofOfMailingOrBilling = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PostDatedChecks = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AuthorizeRepresentative = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SPANotarizedAndConsularized = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BankAndPagIbigSPA = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OathOfAllegianceOrAffidavitOfCitizenship = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Others = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StandardDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StandardDocument_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LocallyEmployed",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SourceOfIncomeId = table.Column<int>(type: "int", nullable: false),
                    Compensation = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LatestITR = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ThreeMonthsOfPayslips = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocallyEmployed", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocallyEmployed_SourceOfIncome_SourceOfIncomeId",
                        column: x => x.SourceOfIncomeId,
                        principalTable: "SourceOfIncome",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OverseasFilipinoWorker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SourceOfIncomeId = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NCEC = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ThreeMonthsPayslipsOrRemittance = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    BankStatements = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PassportWithEntryAndExit = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OverseasFilipinoWorker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OverseasFilipinoWorker_SourceOfIncome_SourceOfIncomeId",
                        column: x => x.SourceOfIncomeId,
                        principalTable: "SourceOfIncome",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SelfEmployed",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SourceOfIncomeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelfEmployed", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SelfEmployed_SourceOfIncome_SourceOfIncomeId",
                        column: x => x.SourceOfIncomeId,
                        principalTable: "SourceOfIncome",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SelfEmployedFormal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SelfEmployedId = table.Column<int>(type: "int", nullable: false),
                    Latest2YearsITR = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Latest2YearsAFS = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Latest6MonthsBankStatements = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelfEmployedFormal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SelfEmployedFormal_SelfEmployed_SelfEmployedId",
                        column: x => x.SelfEmployedId,
                        principalTable: "SelfEmployed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SelfEmployedInformal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SelfEmployedId = table.Column<int>(type: "int", nullable: false),
                    COEIdOfSignatory = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    COEOtherAttachments = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SelfEmployedInformal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SelfEmployedInformal_SelfEmployed_SelfEmployedId",
                        column: x => x.SelfEmployedId,
                        principalTable: "SelfEmployed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_BuyerInformations_ScreeningId",
                table: "BuyerInformations",
                column: "ScreeningId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Computations_ScreeningId",
                table: "Computations",
                column: "ScreeningId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ScreeningId",
                table: "Documents",
                column: "ScreeningId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployerDetail_BuyerInformationId",
                table: "EmployerDetail",
                column: "BuyerInformationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LocallyEmployed_SourceOfIncomeId",
                table: "LocallyEmployed",
                column: "SourceOfIncomeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NegativeDataBankRecord_BuyerInformationId",
                table: "NegativeDataBankRecord",
                column: "BuyerInformationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OverseasFilipinoWorker_SourceOfIncomeId",
                table: "OverseasFilipinoWorker",
                column: "SourceOfIncomeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PagIbigMembership_BuyerInformationId",
                table: "PagIbigMembership",
                column: "BuyerInformationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SelfEmployed_SourceOfIncomeId",
                table: "SelfEmployed",
                column: "SourceOfIncomeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SelfEmployedFormal_SelfEmployedId",
                table: "SelfEmployedFormal",
                column: "SelfEmployedId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SelfEmployedInformal_SelfEmployedId",
                table: "SelfEmployedInformal",
                column: "SelfEmployedId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SourceOfIncome_DocumentId",
                table: "SourceOfIncome",
                column: "DocumentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StandardDocument_DocumentId",
                table: "StandardDocument",
                column: "DocumentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TitlingInstruction_BuyerInformationId",
                table: "TitlingInstruction",
                column: "BuyerInformationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Computations");

            migrationBuilder.DropTable(
                name: "EmployerDetail");

            migrationBuilder.DropTable(
                name: "LocallyEmployed");

            migrationBuilder.DropTable(
                name: "NegativeDataBankRecord");

            migrationBuilder.DropTable(
                name: "OverseasFilipinoWorker");

            migrationBuilder.DropTable(
                name: "PagIbigMembership");

            migrationBuilder.DropTable(
                name: "SelfEmployedFormal");

            migrationBuilder.DropTable(
                name: "SelfEmployedInformal");

            migrationBuilder.DropTable(
                name: "StandardDocument");

            migrationBuilder.DropTable(
                name: "TitlingInstruction");

            migrationBuilder.DropTable(
                name: "SelfEmployed");

            migrationBuilder.DropTable(
                name: "BuyerInformations");

            migrationBuilder.DropTable(
                name: "SourceOfIncome");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "Screenings");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Properties",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);
        }
    }
}
