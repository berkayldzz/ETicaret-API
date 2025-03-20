using ETicaretAPI.Application.Repositories;
using MediatR;

namespace ETicaretAPI.Application.Features.Queries.Product.GetAllProduct
{
    // Bu handler sınıfının GetAllProductQueryRequest sınıfına karşılık GetAllProductQueryResponse nesnesini döndüreceğini bildirmemiz gerekiyor.IRequestHandler interface'i sayesinde.
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, GetAllProductQueryResponse>
    {
        readonly IProductReadRepository _productReadRepository;
        public GetAllProductQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }
        // operasyonumuz bu metot içersinde olacaktır.
        public async Task<GetAllProductQueryResponse> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            var totalCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false).Skip(request.Page * request.Size).Take(request.Size).Select(p => new
            {
                p.Id,
                p.Name,
                p.Stock,
                p.Price,
                p.CreatedDate,
                p.UpdatedDate
            }).ToList();

            return new() // buradaki new GetAllProductQueryResponse'a karşılık geliyor.
            {
                Products = products,
                TotalCount = totalCount
            };
        }
    }
}
