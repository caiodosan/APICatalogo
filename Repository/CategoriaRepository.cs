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

        public async Task<IEnumerable<Categoria>> GetCategorias()
        {
            return await Get().Include(x => x.Produtos).ToListAsync();
        }

        public async Task<PagedList<Categoria>> FindManyCategoriasPagination(Expression<Func<Categoria, bool>> expression, CategoriaasParameters categoriasParameters)
        {
            return  await PagedList<Categoria>.ToPagedList(Get().AsQueryable().Where(expression), categoriasParameters.PageNumber, categoriasParameters.PageSize);
        }
    }
}
