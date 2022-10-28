using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace APICatalogo.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext contexto) : base(contexto)
        {
        }

        public IEnumerable<Categoria> GetCategorias()
        {
            return Get().Include(x => x.Produtos);
        }

        public PagedList<Categoria> FindManyCategoriasPagination(Expression<Func<Categoria, bool>> expression, CategoriaasParameters categoriasParameters)
        {
            return PagedList<Categoria>.ToPagedList(Get().AsQueryable().Where(expression), categoriasParameters.PageNumber, categoriasParameters.PageSize);
        }
    }
}
