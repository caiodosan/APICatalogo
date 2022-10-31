using APICatalogo.Models;
using APICatalogo.Pagination;
using System.Linq.Expressions;

namespace APICatalogo.Repository
{
    public interface IProdutoRepository :IRepository<Produto>
    {
        Task<PagedList<Produto>> GetProdutos(ProdutosParameters produtosParameters);
        Task<IEnumerable<Produto>> GetProdutosPorPreco();
        Task<PagedList<Produto>> FindManyProdutosPagination(Expression<Func<Produto, bool>> expression, ProdutosParameters produtosParameters);

    }
}
