using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
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

        public ActionResult<IEnumerable<CategoriaDTO>> CategoriaEprodutos()
        {
            var prod = _context.CategoriaRepository.GetCategorias();
            var cat = _mapper.Map<List<CategoriaDTO>>(prod);

            if (prod is null)
            {
                return NotFound("Categorias não encontradas");
            }
            return cat;
        }

        [HttpGet]

        public ActionResult<IEnumerable<CategoriaDTO>> GetCategoria()
        {
            var prod = _context.CategoriaRepository.Get().ToList();
            if (prod is null)
            {
                return NotFound("Categorias não encontradas");
            }
            var catDto = _mapper.Map<List<CategoriaDTO>>(prod);
            return catDto;
        }


        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> GetOne(int id)
        {
            var cat = _context.CategoriaRepository.GetById(x => x.CategoriaId == id);
            if (cat is null)
            {
                return NotFound("Categorias não encontradas");
            }
            cat = _mapper.Map<Categoria>(cat);
            return cat;

        }

        [HttpPost]
        public ActionResult CreateOneCategoria(CategoriaDTO categoria)
        {
            if (categoria is null)
            {
                return BadRequest();
            }
            var cat = _mapper.Map<Categoria>(categoria);
            _context.CategoriaRepository.Add(cat);
            _context.Commit();
            var catD= _mapper.Map<CategoriaDTO>(cat);
            return new CreatedAtRouteResult("ObterCategoria", new { id = catD.CategoriaId }, catD);

        }


        [HttpPut("{id:int}")]
        public ActionResult UpdateOneCategoria(int id, CategoriaDTO categoria)
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest();
            }
            var cat = _mapper.Map<Categoria>(categoria);
            _context.CategoriaRepository.Update(cat);

            return Ok(categoria);

        }

        [HttpDelete("{id:int}")]

        public ActionResult DeleteOneCategoria(int id)
        {
            var categoria = _context.CategoriaRepository.GetById(x => x.CategoriaId == id);
            if (categoria is null)
            {
                return NotFound("Categoria nao localizada");
            }
            _context.CategoriaRepository.Delete(categoria);
            _context.Commit();
            var cat = _mapper.Map<CategoriaDTO>(categoria);
            return Ok(cat);
        }


    }
}
