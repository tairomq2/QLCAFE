using Application.Features.Common.DTOs.Products;
using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Common.Queries.Products
{
    public class GetAllProductsQuery : IRequest<ResponseApi<List<ProductDTO>>>
    {
    }
}
