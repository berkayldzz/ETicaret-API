using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETicaretAPI.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        // Identity de diğer entitylerimiz de olduğu gibi repository pattern ile crud işlemlerini gerçekleştirmiyoruz.
        // Çünkü identity'nin hazır servisleri vardır.Onları kullanıyoruz.

        // Burada da kullanıcı eklerken bu servisimiz UserManager'dır. Kullanıcı işlemlerinden sorumlu hazır bir servistir.
        // Bundan dolayı repository nesneleri oluşturmuyoruz.



        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        public CreateUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.Username,
                Email = request.Email,
                NameSurname = request.NameSurname,
            }, request.Password);

            CreateUserCommandResponse response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
                response.Message = "Kullanıcı başarıyla oluşturulmuştur.";
            else
                foreach (var error in result.Errors)
                    response.Message += $"{error.Code} - {error.Description}\n";

            return response;

            //throw new UserCreateFailedException();
        }
    }
}
