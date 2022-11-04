using E_CommerceAPI.Application.Abstractions.Storage;
using E_CommerceAPI.Application.Repositories;
using MediatR;
using P = E_CommerceAPI.Domain.Entities.Product;
using PIF= E_CommerceAPI.Domain.Entities.ProductImageFile;

namespace E_CommerceAPI.Application.Features.Commands.ProductImageFile.UploadProductImage
{
    public class UploadProductImageCommandHandler : IRequestHandler<UploadProductImageCommandRequest, UploadProductImageCommandResponse>
    {
        readonly private IProductReadRepository _productReadRepository;
        readonly private IProductImageFileWriteRepository _productImageFileWriteRepository;
        readonly private IStorageService _storageService;

        public UploadProductImageCommandHandler(IProductReadRepository productReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IStorageService storageService)
        {
            _productReadRepository = productReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _storageService = storageService;
        }
        public async Task<UploadProductImageCommandResponse> Handle(UploadProductImageCommandRequest request, CancellationToken cancellationToken)
        {
            var datas = await _storageService.UploadAsync("resource/file", request.Files);
            P product = await _productReadRepository.GetByIdAsync(request.Id);           
            await _productImageFileWriteRepository.AddRangeAsync(datas.Select(r => new Domain.Entities.ProductImageFile
            {
                FileName = r.fileName,
                Path = r.pathOrContainerName,
                Storage = _storageService.StorageName,
                Products = new List<P>() { product }
            }).ToList());
            await _productImageFileWriteRepository.SaveAsync();
            return new UploadProductImageCommandResponse();
        }
    }
}
