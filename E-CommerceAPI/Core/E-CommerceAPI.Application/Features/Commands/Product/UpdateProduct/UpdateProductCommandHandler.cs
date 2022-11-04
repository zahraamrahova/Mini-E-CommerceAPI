using E_CommerceAPI.Application.Repositories;
using MediatR;
using P=E_CommerceAPI.Domain.Entities.Product;



namespace E_CommerceAPI.Application.Features.Commands.Product.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
    {
        readonly private IProductReadRepository _productReadRepository;
        readonly private IProductWriteRepository _productWriteRepository;

        public UpdateProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
        }  
        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            P product = await _productReadRepository.GetByIdAsync(request.Id);
            product.Stock = request.Stock;
            product.Price = request.Price;
            product.Name = request.Name;
            await _productWriteRepository.SaveAsync();
            return new UpdateProductCommandResponse();
        }
    }
}
