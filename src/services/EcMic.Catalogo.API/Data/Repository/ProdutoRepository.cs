using Dapper;
using EcMic.Catalogo.API.Models;
using EcMic.Core.DomainObjects.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcMic.Catalogo.API.Data.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly CatalogoContext _context;

        public ProdutoRepository(CatalogoContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async  Task<Produto> ObterPorId(Guid id)
        {            
            return await _context.Produtos.FindAsync(id);
        }

        public async Task<PagedResult<Produto>> ObterTodos(int pageSize, int pageIndex, string query = null)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM Produtos ");
            strSql.Append(" WHERE (@Nome IS NULL OR Nome LIKE '%' + @Nome + '%') ");
            strSql.Append(" ORDER BY [Nome] ");
            strSql.Append($" OFFSET {pageSize * (pageIndex - 1)} ROWS ");
            strSql.Append($" FETCH NEXT {pageSize} ROWS ONLY ");
            strSql.Append(" SELECT COUNT(Id) FROM Produtos ");
            strSql.Append(" WHERE (@Nome IS NULL OR Nome LIKE '%' + @Nome + '%') ");

            var multi = await _context.Database.GetDbConnection()
                                               .QueryMultipleAsync(strSql.ToString(), new { Nome = query });

            var produtos = multi.Read<Produto>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedResult<Produto>()
            {
                List = produtos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query
            };

            //return await _context.Produtos.AsNoTracking()
            //    .Skip(pageSize * (pageIndex - 1))
            //    .Take(pageSize)
            //    .Where(w => query == null || w.Nome.Contains(query))
            //    .ToListAsync();
        }

        public void Adicionar(Produto produto)
        {
            _context.Produtos.Add(produto);
        }

        public void Atualizar(Produto produto)
        {
            _context.Produtos.Update(produto);
        }

        public async Task<List<Produto>> ObterProdutosPorId(string ids)
        {
            var idsGuid = ids.Split(',')
                .Select(id => (Ok: Guid.TryParse(id, out var x), Value: x));

            if (!idsGuid.All(nid => nid.Ok)) return new List<Produto>();

            var idsValue = idsGuid.Select(id => id.Value);

            return await _context.Produtos.AsNoTracking()
                .Where(p => idsValue.Contains(p.Id) && p.Ativo).ToListAsync();
        }


        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
