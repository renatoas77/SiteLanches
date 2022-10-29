using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanchesMac.Migrations
{
    public partial class Lanche : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Lanches (CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemThumbnailUrl,ImagemUrl,IsLanchePreferido,Nome,Preco) values (1,'Pão, hamburguer, queijo e maionese','Pão de hamburguer, hamburguer de 90 grs, queijo cheddar e maionese caseira',1,'https://bobs.com.br/media/filer_public_thumbnails/filer_public/10/f3/10f34bfd-0670-45de-b1cb-e34abd419ba2/cheeseburger.png__800x400_subsampling-2.png','https://bobs.com.br/media/filer_public_thumbnails/filer_public/10/f3/10f34bfd-0670-45de-b1cb-e34abd419ba2/cheeseburger.png__800x400_subsampling-2.png',0,'Cheese burguer',10.00)");
            migrationBuilder.Sql("Insert into Lanches (CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemThumbnailUrl,ImagemUrl,IsLanchePreferido,Nome,Preco) values (1,'Pão, hamburguer, queijo, alface, tomate, cebola e maionese','Pão de hamburguer, hamburguer de 90 grs, queijo mussarela, alface americana, tomate,cebola roxa e maionse caseira',1,'https://static.clubedaanamariabraga.com.br/wp-content/uploads/2016/11/x-salada-classico.jpg','https://static.clubedaanamariabraga.com.br/wp-content/uploads/2016/11/x-salada-classico.jpg',0,'Cheese Salada',12.50)");
            migrationBuilder.Sql("Insert into Lanches (CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemThumbnailUrl,ImagemUrl,IsLanchePreferido,Nome,Preco) values (1,'Pão, presunto e queijo','Pão de forma com preseunto e queijo mussarela grelhado na chapa',1,'https://i.pinimg.com/originals/df/30/54/df30542d71b010c25934f8b76bf984f5.jpg','https://i.pinimg.com/originals/df/30/54/df30542d71b010c25934f8b76bf984f5.jpg',0,'Misto quente',8.00)");
            migrationBuilder.Sql("Insert into Lanches (CategoriaId,DescricaoCurta,DescricaoDetalhada,EmEstoque,ImagemThumbnailUrl,ImagemUrl,IsLanchePreferido,Nome,Preco) values (2,'Pão integral, peito de peru, queijo, alface, tomate e molho caseiro','Baguete integral, 200 grs de peito de peru, queijo branco,alface crespa,tomate e molho caseiro',1,'https://s2.glbimg.com/ZhYCdHbptcLkvvMSh3w7GyKwLIM=/0x0:495x250/984x0/smart/filters:strip_icc()/s.glbimg.com/po/rc/media/2012/06/13/15/55/17/603/receita_sanduiche_peito_peru.jpg','https://s2.glbimg.com/ZhYCdHbptcLkvvMSh3w7GyKwLIM=/0x0:495x250/984x0/smart/filters:strip_icc()/s.glbimg.com/po/rc/media/2012/06/13/15/55/17/603/receita_sanduiche_peito_peru.jpg',1,'Peito de peru',15.00)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("REMOVE from lanches");
        }
    }
}
