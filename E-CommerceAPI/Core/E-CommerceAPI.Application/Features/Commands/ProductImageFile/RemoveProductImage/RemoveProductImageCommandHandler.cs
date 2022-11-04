using E_CommerceAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using P = E_CommerceAPI.Domain.Entities.Product;
using PIF= E_CommerceAPI.Domain.Entities.ProductImageFile;

namespace E_CommerceAPI.Application.Features.Commands.ProductImageFile.RemoveProductImage
{
    public class RemoveProductImageCommandHandler : IRequestHandler<RemoveProductImageCommandRequest, RemoveProductImageCommandResponse>
    {
        readonly private IProductReadRepository _productReadRepository;
        readonly private IProductWriteRepository _productWriteRepository;
        public RemoveProductImageCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }
        public async Task<RemoveProductImageCommandResponse> Handle(RemoveProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            P? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));

            PIF? productImageFile = product?.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(request.ImageId));
            if (productImageFile!=null)
            {
                product?.ProductImageFiles.Remove(productImageFile);
            }          
            await _productWriteRepository.SaveAsync();
            return new();
        }
    }
}
