using APICatalogo.Models;
using APICatalogo.Pagination;
using System.Linq.Expressions;

namespace APICatalogo.Repository
{
    public interface IProdutoRepository :IRepository<Produto>
    {
        PagedList<Produto> GetProdutos(ProdutosParameters produtosParameters);
        IEnumerable<Produto> GetProdutosPorPreco();
        PagedList<Produto> FindManyProdutosPagination(Expression<Func<Produto, bool>> expression, ProdutosParameters produtosParameters);

    }
}
