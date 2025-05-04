using Application.Features.Common.DTOs.Products;
using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Common.Commands
{
    public class CreateProductCommand : IRequest<ResponseApi<ProductDTO>>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? Category { get; set; }
    }
}
