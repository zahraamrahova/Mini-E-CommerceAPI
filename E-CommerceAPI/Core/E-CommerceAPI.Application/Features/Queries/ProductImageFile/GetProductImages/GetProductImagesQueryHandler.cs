using E_CommerceAPI.Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using P = E_CommerceAPI.Domain.Entities.Product;


namespace E_CommerceAPI.Application.Features.Queries.ProductImageFile.GetProductImages
{
    public class GetProductImagesQueryHandler : IRequestHandler<GetProductImagesQueryRequest, List<GetProductImagesQueryResponse>>
    {
        readonly private IProductReadRepository _productReadRepository;
        public GetProductImagesQueryHandler(IProductReadRepository productReadRepository )
        {
            _productReadRepository = productReadRepository;
        }
        public async Task<List<GetProductImagesQueryResponse>> Handle(GetProductImagesQueryRequest request, CancellationToken cancellationToken)
        {
            P? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles).FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));
            return (product?.ProductImageFiles.Select(p => new GetProductImagesQueryResponse
            {
                //Path= $"{_configuration["BaseStorageUrl"]}/{p.Path}",
                Path = Path.Combine("http://127.0.0.1:8887/", p.Path),
                FileName=p.FileName,
                Id=p.Id
            })).ToList();
            return new();
        }
    }
}
