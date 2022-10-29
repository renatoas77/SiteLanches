using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanchesMac.Migrations
{
    public partial class Categoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Categorias (CategoriaNome,Descricao) VALUES ('Normal','Lanches feitos com ingrediente normais')");
            migrationBuilder.Sql("INSERT INTO Categorias (CategoriaNome,Descricao) VALUES ('Natural','Lanches feitos com ingredientes naturais e integrais')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Categorias");
        }
    }
}
