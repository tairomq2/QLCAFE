using Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Common.Commands
{
    public class DeleteProductCommand : IRequest<ResponseApi<bool>>
    {
        public int ProductId { get; set; }
        public DeleteProductCommand(int productId) => ProductId = productId;
    }
}
