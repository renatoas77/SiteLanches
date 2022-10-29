using LanchesMac.Context;
using LanchesMac.Models;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Areas.Admin.Services
{
    public class RelatorioVendasService
    {
        private readonly AppDbContext _context;

        public RelatorioVendasService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pedido>> FindByDateAsync(DateTime? initialDate, DateTime? finalDate)
        {
            var resultado = from obj in _context.Pedidos select obj;

            if(initialDate.HasValue)
            {
                resultado = resultado.Where(p => p.PedidoEnviado >= initialDate);
            };

            if (finalDate.HasValue)
            {
                resultado = resultado.Where(p =>p.PedidoEnviado <= finalDate);
            }

            return await resultado.Include(l => l.PedidoItens)
                                  .ThenInclude(l => l.Lanche)
                                  .OrderByDescending(x => x.PedidoEnviado)
                                  .ToListAsync();
        }
    }
}
