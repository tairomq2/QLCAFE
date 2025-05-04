using Application.Features.Common.Commands;
using Application.Features.Common.Queries.Products;
using Application.Interfaces;
using Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CafeManagerSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            var products = await _mediator.Send(new GetAllProductsQuery());
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductCommand command)
        {
            var createdProduct = await _mediator.Send(command);
            return Ok(createdProduct);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommand command)
        {
            var updateProduct = await _mediator.Send(command);
            return Ok(updateProduct);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
           var delProduct = await _mediator.Send(new DeleteProductCommand(id));
            return Ok(delProduct);
        }
    }
}
