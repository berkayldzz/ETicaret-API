using ETicaretAPI.Application.RequestParameters;
using MediatR;

namespace ETicaretAPI.Application.Features.Queries.Product.GetAllProduct
{

    // MediatR kütüphanesi sayesinde hangi sınıfın request sınıfı olduğunu ve bu request sınıfı neticesinde hangi sınıfın geriye response olarak döndüreleceğini bildirmiş oluyoruz. IRequest interfaci sayesinde.Aşağıdaki  : IRequest<GetAllProductQueryResponse> sayesinde.

    // IRequest interface'inden türeyen sınıf mediatr kütüphanesinde request sınıfına karşılık gelmektedir.

    // Sonrasında hangi sınıfın buradaki sınıfa karşılık handler sınıfı olarak bildirilmesi var onu da handler da yapıyoruz.
    public class GetAllProductQueryRequest:IRequest<GetAllProductQueryResponse>
    {
        //public Pagination Pagination { get; set; }

        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
    }
}
