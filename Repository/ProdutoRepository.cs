using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using System.Linq.Expressions;

namespace APICatalogo.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext contexto) : base(contexto)
        {
        }

        public IEnumerable<Produto> GetProdutosPorPreco()
        {
            return Get().OrderBy(p => p.Preco).ToList();
        }

        public PagedList<Produto> GetProdutos(ProdutosParameters produtosParameters)
        {
            return PagedList<Produto>.ToPagedList(Get().OrderBy(p => p.ProdutoId), produtosParameters.PageNumber, produtosParameters.PageSize);
        }

        public PagedList<Produto> FindManyProdutosPagination(Expression<Func<Produto,bool>> expression, ProdutosParameters produtosParameters)
        {
            return PagedList<Produto>.ToPagedList(Get().AsQueryable().Where(expression), produtosParameters.PageNumber, produtosParameters.PageSize);
        }
    }
}
