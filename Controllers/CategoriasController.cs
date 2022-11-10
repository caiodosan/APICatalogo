using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace APICatalogo.Controllers
{
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]")]
    [ApiController]
    [EnableCors("EnableApi")]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnityOfWork _context;
        private readonly IMapper _mapper;

        public CategoriasController(IUnityOfWork context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("produtos")]

        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> CategoriaEprodutos()
        {
            var prod = await _context.CategoriaRepository.GetCategorias();
            var cat = _mapper.Map<List<CategoriaDTO>>(prod);

            if (prod is null)
            {
                return NotFound("Categorias não encontradas");
            }
            return cat;
        }


        [HttpGet("categoriaPagina")]

        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> CategoriaPagina([FromQuery] CategoriaasParameters categoriasParameters)
        {
            var cat = await _context.CategoriaRepository.FindManyCategoriasPagination(_ => true, categoriasParameters);
            var metadata = new
            {
                cat.TotalCount,
                cat.PageSize,
                cat.CurrentPage,
                cat.TotalPage,
                cat.HasNext,
                cat.HasPrevius

            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            var catDTO = _mapper.Map<List<CategoriaDTO>>(cat);
            return catDTO;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoria()
        {
            var prod = await _context.CategoriaRepository.Get().ToListAsync();
            if (prod is null)
            {
                return NotFound("Categorias não encontradas");
            }
            var catDto = _mapper.Map<List<CategoriaDTO>>(prod);
            return catDto;
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoriaDTO>> GetOne(int id)
        {
            var cat = await _context.CategoriaRepository.GetById(x => x.CategoriaId == id);
            if (cat is null)
            {
                return NotFound("Categorias não encontradas");
            }
            var catdto = _mapper.Map<CategoriaDTO>(cat);
            return catdto;

        }

        [HttpPost]
        public async Task<ActionResult> CreateOneCategoria(CategoriaDTO categoria)
        {
            if (categoria is null)
            {
                return BadRequest();
            }
            var cat = _mapper.Map<Categoria>(categoria);
            _context.CategoriaRepository.Add(cat);
            await _context.Commit();
            var catD= _mapper.Map<CategoriaDTO>(cat);
            return new CreatedAtRouteResult("ObterCategoria", new { id = catD.CategoriaId }, catD);

        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateOneCategoria(int id, CategoriaDTO categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest();
            }
            var cat = _mapper.Map<Categoria>(categoria);
            _context.CategoriaRepository.Update(cat);
            await _context.Commit();
            return Ok(categoria);

        }

        [HttpDelete("{id:int}")]

        public async Task<ActionResult> DeleteOneCategoria(int id)
        {
            var categoria = await _context.CategoriaRepository.GetById(x => x.CategoriaId == id);
            if (categoria is null)
            {
                return NotFound("Categoria nao localizada");
            }
            _context.CategoriaRepository.Delete(categoria);
            await _context.Commit();
            var cat = _mapper.Map<CategoriaDTO>(categoria);
            return Ok(cat);
        }


    }
}
