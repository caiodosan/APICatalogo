using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    public partial class Populabd : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Categorias(Nome,ImagemUrl) Values('Bebidas','C:\\Users\\caioc\\Downloads\\fiancas')");
            mb.Sql("Insert into Produtos(Nome,Descricao,preco,Estoque,DataCadastro,CategoriaId,ImagemUrl) Values('Coca-Cola','Refrigerante famoso','10','1000',now()," +
                    "(Select CategoriaId from Categorias where Nome='Bebidas'),'C:\\Users\\caioc\\Downloads\\fiancas')");                   
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Categorias");
            mb.Sql("Delete from Produtos");
        }
    }
}
