using E_CommerceAPI.Application.Abstractions.Storage;
using E_CommerceAPI.Application.Features.Commands.Product.CreateProduct;
using E_CommerceAPI.Application.Features.Commands.Product.RemoveProduct;
using E_CommerceAPI.Application.Features.Commands.Product.UpdateProduct;
using E_CommerceAPI.Application.Features.Commands.ProductImageFile.RemoveProductImage;
using E_CommerceAPI.Application.Features.Commands.ProductImageFile.UploadProductImage;
using E_CommerceAPI.Application.Features.Queries.Product.GetByIdProduct;
using E_CommerceAPI.Application.Features.Queries.Product.GettAllProduct;
using E_CommerceAPI.Application.Features.Queries.ProductImageFile.GetProductImages;
using E_CommerceAPI.Application.Repositories;
using E_CommerceAPI.Application.RequestParameters;
using E_CommerceAPI.Application.ViewModels.Products;
using E_CommerceAPI.Domain.Entities;
using E_CommerceAPI.Infrastructure.Services.Storage;
using E_CommerceAPI.Persistence.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace E_CommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes="Admin")]
    public class ProductsController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly private IProductReadRepository _productReadRepository;
        readonly private IStorageService _storageService;
        readonly private IProductImageFileWriteRepository _productImageFileWriteRepository;

        public ProductsController(IMediator mediator,
            IProductReadRepository productReadRepository,
            IStorageService storageService,
            IProductImageFileWriteRepository productImageFileWriteRepository
            )
        {
            _mediator = mediator;
            _productReadRepository = productReadRepository;
            _storageService = storageService;
            _productImageFileWriteRepository = productImageFileWriteRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            GetAllProductQueryResponse getAllProductQueryResponse = await _mediator.Send(getAllProductQueryRequest);
            return Ok(getAllProductQueryResponse);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] GetByIdProductQueryRequest getByIdProductQueryRequest)
        {
            GetByIdProductQueryResponse getByIdProductQueryResponse = await _mediator.Send(getByIdProductQueryRequest);
            return Ok(getByIdProductQueryResponse);

        }
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequest createProductCommandRequest)
        {

            CreateProductCommandResponse createProductCommandResponse = await _mediator.Send(createProductCommandRequest);

            return StatusCode((int)HttpStatusCode.Created);
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateProductCommandRequest updateProductCommandRequest)
        {
            UpdateProductCommandResponse updateProductCommandResponse = await _mediator.Send(updateProductCommandRequest);
            return Ok();
        }
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest removeProductCommandRequest)
        {
            RemoveProductCommandResponse removeProductCommandResponse = await _mediator.Send(removeProductCommandRequest);
            return Ok();
        }       
        [HttpGet("[action]/{Id}")]
        public async Task<IActionResult> GetProductImages([FromRoute] GetProductImagesQueryRequest getProductImagesQueryRequest)
        {
            List<GetProductImagesQueryResponse> getProductImagesQueryResponse =await _mediator.Send(getProductImagesQueryRequest);
            return Ok(getProductImagesQueryResponse);
        }
        [HttpDelete("[action]/{Id}")]
        public async Task<IActionResult> DeleteProductImages([FromRoute] RemoveProductImageCommandRequest removeProductImageCommandRequest, [FromQuery] string imageId)
        {
            removeProductImageCommandRequest.ImageId = imageId;
            RemoveProductImageCommandResponse removeProductImageCommandResponse =await  _mediator.Send(removeProductImageCommandRequest);

            return Ok();

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery] UploadProductImageCommandRequest uploadProductImageCommandRequest)
        {
            uploadProductImageCommandRequest.Files = Request.Form.Files;
            UploadProductImageCommandResponse uploadProductImageCommandResponse = await _mediator.Send(uploadProductImageCommandRequest);
            return Ok();
        }

        //[HttpPost("[action]")]
        //public async Task<IActionResult> Upload(string id)
        //{
        //    //var datas = await _storageService.UploadAsync("files", Request.Form.Files);// for Azure blob storage
        //    var datas = await _storageService.UploadAsync("resource/file", Request.Form.Files);
        //    Product product = await _productReadRepository.GetByIdAsync(id);
        //    await _productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProductImageFile()
        //    {
        //        FileName = d.fileName,
        //        Path = d.pathOrContainerName,
        //        Storage = _storageService.StorageName,
        //        Products = new List<Product>() { product }

        //    }).ToList());
        //    await _productImageFileWriteRepository.SaveAsync();
        //    return Ok();
        //}

    }
}
