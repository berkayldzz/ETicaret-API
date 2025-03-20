using MediatR;

namespace ETicaretAPI.Application.Features.Queries.ProductImageFile
{
    public class GetProductImagesQueryRequest : IRequest<List<GetProductImagesQueryResponse>>
    {
        public string Id { get; set; }
    }
}
