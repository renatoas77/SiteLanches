using LanchesMac.Context;
using LanchesMac.Models;

namespace LanchesMac.Areas.Admin.Services
{
    public class GraficoVendaService
    {
        private readonly AppDbContext _appDbContext;

        public GraficoVendaService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public List<LancheGrafico> GetVendasLanches(int dias = 360)
        {
            var data = DateTime.Now.AddDays(-dias);

            var lanches =  (from pd in _appDbContext.PedidoDetalhes
                           join l in _appDbContext.Lanches
                           on pd.LancheId equals l.LancheId
                           where pd.Pedido.PedidoEnviado >= data
                           group pd by new {l.Nome } into g
                           select new
                           {
                               LancheNome= g.Key.Nome,
                               LanchesQuantidade = g.Sum(q => q.Quantidade),
                               ValorTotalLanche = g.Sum(a => a.Preco * a.Quantidade),
                           });

            var lista = new List<LancheGrafico>();
            foreach (var item in lanches)
            {
                var lanche = new LancheGrafico();
                lanche.LancheNome = item.LancheNome;
                lanche.LanchesQuantidade = item.LanchesQuantidade;
                lanche.ValorTotalLanche = item.ValorTotalLanche; 
                lista.Add(lanche);
            }
            return lista;
        }
    }
}
