using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
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
        public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosPorPreco()
        {
            var prod = _context.ProdutoRepository.GetProdutosPorPreco().ToList();
            var produtoDTO = _mapper.Map<List<ProdutoDTO>>(prod);
            return produtoDTO;
        }

        [HttpGet]

        public ActionResult<IEnumerable<ProdutoDTO>> Get([FromQuery] ProdutosParameters produtosParameters )
        {
            try
            {
                var prod = _context.ProdutoRepository.GetProdutos(produtosParameters);
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
        public ActionResult<ProdutoDTO> GetOne(int id)
        {
            var prod = _context.ProdutoRepository.GetById(x => x.ProdutoId == id);
            if (prod is null)
            {
                return NotFound("produtos não encontrados");
            }
            var prodDto = _mapper.Map<ProdutoDTO>(prod);
            return prodDto;

        }

        [HttpPost]
        public ActionResult CreateOne(ProdutoDTO produto)
        {
            if (produto is null)
            {
                return BadRequest();
            }
            var prod = _mapper.Map<Produto>(produto);
            _context.ProdutoRepository.Add(prod);
            _context.Commit();
            var produtoDTO = _mapper.Map<ProdutoDTO>(prod);
            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produtoDTO);

        }


        [HttpPut("{id:int}")]
        public ActionResult UpdateOne(int id ,ProdutoDTO produto)
        {
            if(id != produto.ProdutoId)
            {
                return BadRequest();
            }
            var prod = _mapper.Map<Produto>(produto);
            _context.ProdutoRepository.Update(prod);
            _context.Commit();
            var produtoDTO = _mapper.Map<ProdutoDTO>(prod);

            return Ok(produtoDTO);

        }

        [HttpDelete("{id:int}")]

        public ActionResult DeleteOne(int id)
        {
            var produto = _context.ProdutoRepository.GetById(x => x.ProdutoId == id);
            _context.ProdutoRepository.Delete(produto);
            _context.Commit();
            if (produto is null)
            {
                return NotFound("produto nao localizado");
            }
            var produtoDTO = _mapper.Map<ProdutoDTO>(produto);
            return Ok(produtoDTO);
        }


    }

}
