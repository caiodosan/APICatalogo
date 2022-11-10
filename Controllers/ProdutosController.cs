using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
   // [EnableCors("PermitirApiRequest")]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnityOfWork _context;
        private readonly IMapper _mapper;

        public ProdutosController(IUnityOfWork context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("menorpreco")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosPorPreco()
        {
            var prod = await _context.ProdutoRepository.GetProdutosPorPreco();
            var produtoDTO = _mapper.Map<List<ProdutoDTO>>(prod);
            return produtoDTO;
        }

        [HttpGet("porcategoria")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutoPorCategoria(int categoriaId, [FromQuery] ProdutosParameters produtosParameters)
        {
            var prod = await _context.ProdutoRepository.FindManyProdutosPagination(cat => cat.CategoriaId == categoriaId, produtosParameters);
            var produtoDTO = _mapper.Map<List<ProdutoDTO>>(prod);
            var metadata = new
                {
                    prod.TotalCount,
                    prod.PageSize,
                    prod.CurrentPage,
                    prod.TotalPage,
                    prod.HasNext,
                    prod.HasPrevius

                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return produtoDTO;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get([FromQuery] ProdutosParameters produtosParameters )
        {
            try
            {
                var prod = await _context.ProdutoRepository.GetProdutos(produtosParameters);
                var metadata = new
                {
                    prod.TotalCount,
                    prod.PageSize,
                    prod.CurrentPage,
                    prod.TotalPage,
                    prod.HasNext,
                    prod.HasPrevius

                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                var prodDTO = _mapper.Map<List<ProdutoDTO>>(prod);
                return prodDTO;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message + e.StackTrace);
            }
        }


        [HttpGet("{id:int}", Name ="ObterProduto")]
        public async Task<ActionResult<ProdutoDTO>> GetOne(int id)
        {
            var prod = await _context.ProdutoRepository.GetById(x => x.ProdutoId == id);
            if (prod is null)
            {
                return NotFound("produtos não encontrados");
            }
            var prodDto = _mapper.Map<ProdutoDTO>(prod);
            return prodDto;

        }

        [HttpPost]
        public async Task<ActionResult> CreateOne(ProdutoDTO produto)
        {
            if (produto is null)
            {
                return BadRequest();
            }
            var prod = _mapper.Map<Produto>(produto);
            _context.ProdutoRepository.Add(prod);
            await _context.Commit();
            var produtoDTO = _mapper.Map<ProdutoDTO>(prod);
            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produtoDTO);

        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateOne(int id ,ProdutoDTO produto)
        {
            if(id != produto.ProdutoId)
            {
                return BadRequest();
            }
            var prod = _mapper.Map<Produto>(produto);
            _context.ProdutoRepository.Update(prod);
            await _context.Commit();
            var produtoDTO = _mapper.Map<ProdutoDTO>(prod);

            return Ok(produtoDTO);

        }

        [HttpDelete("{id:int}")]

        public async Task<ActionResult> DeleteOne(int id)
        {
            var produto = await _context.ProdutoRepository.GetById(x => x.ProdutoId == id);
            _context.ProdutoRepository.Delete(produto);
            await _context.Commit();
            if (produto is null)
            {
                return NotFound("produto nao localizado");
            }
            var produtoDTO = _mapper.Map<ProdutoDTO>(produto);
            return Ok(produtoDTO);
        }


    }

}
