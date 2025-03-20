using ETicaretAPI.Application.Abstractions.Storage;
using ETicaretAPI.Application.Features.Commands.Product.CreateProduct;
using ETicaretAPI.Application.Features.Commands.Product.RemoveProduct;
using ETicaretAPI.Application.Features.Commands.Product.UpdateProduct;
using ETicaretAPI.Application.Features.Commands.ProductImageFile.RemoveProductImage;
using ETicaretAPI.Application.Features.Commands.ProductImageFile.UploadProductImage;
using ETicaretAPI.Application.Features.Queries.Product.GetAllProduct;
using ETicaretAPI.Application.Features.Queries.Product.GetByIdProduct;
using ETicaretAPI.Application.Features.Queries.ProductImageFile;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Application.ViewModels.Products;
using ETicaretAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductReadRepository _productReadRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        readonly IFileWriteRepository _fileWriteRepository;
        readonly IFileReadRepository _fileReadRepository;
        readonly IProductImageFileReadRepository _productImageFileReadRepository;
        readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        readonly IInvoiceFileReadRepository _invoiceFileReadRepository;
        readonly IInvoiceFileWriteRepository _invoiceFileWriteRepository;
        readonly IStorageService _storageService;
        readonly IConfiguration configuration;

        readonly IMediator _mediator;


        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IWebHostEnvironment webHostEnvironment, IFileWriteRepository fileWriteRepository, IFileReadRepository fileReadRepository, IProductImageFileReadRepository productImageFileReadRepository, IProductImageFileWriteRepository productImageFileWriteRepository, IInvoiceFileReadRepository invoiceFileReadRepository, IInvoiceFileWriteRepository invoiceFileWriteRepository, IStorageService storageService, IConfiguration configuration, IMediator mediator)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            this._webHostEnvironment = webHostEnvironment;
            _webHostEnvironment = webHostEnvironment;
            _fileWriteRepository = fileWriteRepository;
            _fileReadRepository = fileReadRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _invoiceFileReadRepository = invoiceFileReadRepository;
            _invoiceFileWriteRepository = invoiceFileWriteRepository;
            _storageService = storageService;
            this.configuration = configuration;
            _mediator = mediator;
        }

        // İlgili request parametresini veriyoruz.
        // Bu parametreye karşılık devreye sokulacak handler sınıfını mediator biliyor.
        // Mediatr vermiş olduğum request nesnesine karşılık hangi handler sınıfını devreye sokacağını ve bu handler sınıfında yapılan işlem neticesinde hangi nesneyi response nesnesi geri döndüreceğini biliyor.
        // Bunu yaptığımız isimlendirmelerden ve ServiceRegistration yardımıyla biliyor.
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
        {
            #region
            //var totalCount = _productReadRepository.GetAll(false).Count();
            //var products = _productReadRepository.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(p => new
            //{
            //    p.Id,
            //    p.Name,
            //    p.Stock,
            //    p.Price,
            //    p.CreatedDate,
            //    p.UpdatedDate
            //}).ToList();

            //return Ok(new
            //{
            //    totalCount,
            //    products
            //});
            #endregion

            // geriye GetAllProductQueryResponse döndüreceğini biliyor.ve biz de o türde karşılıyoruz ve responsu döndürüyoruz.
            GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
            return Ok(response);


        }


        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute]GetByIdProductQueryRequest getByIdProductQueryRequest)
        {
           GetByIdProductQueryResponse response= await _mediator.Send(getByIdProductQueryRequest);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Post(/*VM_Create_Product model*/ CreateProductCommandRequest createProductCommandRequest)
        {
            #region
            //await _productWriteRepository.AddAsync(new()
            //{
            //    Name = model.Name,
            //    Stock = model.Stock,
            //    Price = model.Price
            //});
            //await _productWriteRepository.SaveAsync();
            //return StatusCode((int)HttpStatusCode.Created);

            #endregion

            CreateProductCommandResponse response = await _mediator.Send(createProductCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        public async Task<IActionResult> Put(/*VM_Update_Product model*/ [FromBody] UpdateProductCommandRequest updateProductCommandRequest)
        {
            #region eski
            //Product product = await _productReadRepository.GetByIdAsync(model.Id);
            //product.Stock = model.Stock;
            //product.Price = model.Price;
            //product.Name = model.Name;

            //await _productWriteRepository.SaveAsync();

            //return Ok();
            #endregion

            UpdateProductCommandResponse response = await _mediator.Send(updateProductCommandRequest);
            return Ok();

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequest removeProductCommandRequest)
        {
            RemoveProductCommandResponse response = await _mediator.Send(removeProductCommandRequest);
            return Ok();
        }



        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery] UploadProductImageCommandRequest uploadProductImageCommandRequest)
        {
            #region
            //var datas = await _storageService.UploadAsync("files", Request.Form.Files);
            ////var datas = await _fileService.UploadAsync("resource/files", Request.Form.Files);
            //await _productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProductImageFile()
            //{
            //    FileName = d.fileName,
            //    Path = d.pathOrContainerName,
            //    Storage = _storageService.StorageName
            //}).ToList());
            //await _productImageFileWriteRepository.SaveAsync();
            //List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("photo-images", Request.Form.Files);

            ////await _invoiceFileWriteRepository.AddRangeAsync(datas.Select(d => new InvoiceFile()
            ////{
            ////    FileName = d.fileName,
            ////    Path = d.path,
            ////    Price = new Random().Next()
            ////}).ToList());
            ////await _invoiceFileWriteRepository.SaveAsync();

            ////await _fileWriteRepository.AddRangeAsync(datas.Select(d => new ETicaretAPI.Domain.Entities.File()
            //Product product = await _productReadRepository.GetByIdAsync(id);

            ////foreach (var r in result)
            ////{
            ////    FileName = d.fileName,
            ////    Path = d.path
            ////}).ToList());
            ////await _fileWriteRepository.SaveAsync();
            ////    product.ProductImageFiles.Add(new()
            ////    {
            ////        FileName = r.fileName,
            ////        Path = r.pathOrContainerName,
            ////        Storage = _storageService.StorageName,
            ////        Products = new List<Product>() { product }
            ////    });
            ////}

            ////var d1 = _fileReadRepository.GetAll(false);
            ////var d2 = _invoiceFileReadRepository.GetAll(false);
            ////var d3 = _productImageFileReadRepository.GetAll(false);
            //await _productImageFileWriteRepository.AddRangeAsync(result.Select(r => new ProductImageFile
            //{
            //    FileName = r.fileName,
            //    Path = r.pathOrContainerName,
            //    Storage = _storageService.StorageName,
            //    Products = new List<Product>() { product }
            //}).ToList());

            //await _productImageFileWriteRepository.SaveAsync();


            #endregion

            uploadProductImageCommandRequest.Files = Request.Form.Files;
            UploadProductImageCommandResponse response = await _mediator.Send(uploadProductImageCommandRequest);
            return Ok();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImages([FromRoute] GetProductImagesQueryRequest getProductImagesQueryRequest)
        {
            #region
            //Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
            //        .FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));
            //return Ok(product.ProductImageFiles.Select(p => new
            //{
            //    Path = $"{configuration["BaseLocalStorageUrl"]}\\{p.Path}",  // todo :burayı düzenle localstorage göre
            //    p.FileName,
            //    p.Id
            //}));
            #endregion

            List<GetProductImagesQueryResponse> response = await _mediator.Send(getProductImagesQueryRequest);
            return Ok(response);
        }
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteProductImage(/*string id, string imageId*/ [FromRoute] RemoveProductImageCommandRequest removeProductImageCommandRequest, [FromQuery] string imageId)
        {
            //Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
            //      .FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));

            //ProductImageFile productImageFile = product.ProductImageFiles.FirstOrDefault(p => p.Id == Guid.Parse(imageId));
            //product.ProductImageFiles.Remove(productImageFile);
            //await _productWriteRepository.SaveAsync();

            removeProductImageCommandRequest.ImageId = imageId;
            RemoveProductImageCommandResponse response = await _mediator.Send(removeProductImageCommandRequest);
            return Ok();
        }
    }
}
