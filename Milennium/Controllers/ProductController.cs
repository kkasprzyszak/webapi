using AutoMapper;
using DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Milennium.Model;
using Milennium.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Milennium.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;
        private readonly IRepository<Product> _repository;

        private static readonly Random Random = new Random((int)DateTime.Now.Ticks);

        public ProductController(ILogger<ProductController> logger,
            IMapper mapper,
            IRepository<Product> repository)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }


        [HttpGet]
        public async Task<List<Product>> GetAll()
        {
            await Task.Delay(Random.Next(1000, 2000));
            return _repository.Get();
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Product))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            await Task.Delay(Random.Next(1000, 2000));

            Product item = _repository.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ProductViewModel>(item));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct([FromBody]ProductViewModel product)
        {
            if (!ModelState.IsValid || product.Price <= 0m)
            {
                return BadRequest();
            }

            await Task.Delay(Random.Next(1000, 2000));

            var entity = _mapper.Map<Product>(product);

            _repository.Add(entity);

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProduct([FromBody] ProductViewModel product)
        {
            if (!ModelState.IsValid || product.Price <= 0m)
            {
                return BadRequest();
            }

            await Task.Delay(Random.Next(1000, 2000));

            var entity = _mapper.Map<Product>(product);

            _repository.Update(entity);

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await Task.Delay(Random.Next(1000, 2000));

            var product = _repository.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            _repository.Remove(product);

            return Ok();
        }
    }
}
