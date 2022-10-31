using APICatalogo.Models;
using APICatalogo.Pagination;
using System.Linq.Expressions;

namespace APICatalogo.Repository
{
    public interface ICategoriaRepository :IRepository<Categoria>
    {
        Task<IEnumerable<Categoria>> GetCategorias();

        Task<PagedList<Categoria>> FindManyCategoriasPagination(Expression<Func<Categoria, bool>> expression, CategoriaasParameters categoriasParameters);

    }
}
