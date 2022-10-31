using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace APICatalogo.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext contexto) : base(contexto)
        {
        }

        public async Task<IEnumerable<Produto>> GetProdutosPorPreco()
        {
            return await Get().OrderBy(p => p.Preco).ToListAsync();
        }

        public async Task<PagedList<Produto>> GetProdutos(ProdutosParameters produtosParameters)
        {
            return await PagedList<Produto>.ToPagedList(Get().OrderBy(p => p.ProdutoId), produtosParameters.PageNumber, produtosParameters.PageSize);
        }

        public async Task<PagedList<Produto>> FindManyProdutosPagination(Expression<Func<Produto,bool>> expression, ProdutosParameters produtosParameters)
        {
            return await PagedList<Produto>.ToPagedList(Get().AsQueryable().Where(expression), produtosParameters.PageNumber, produtosParameters.PageSize);
        }
    }
}
