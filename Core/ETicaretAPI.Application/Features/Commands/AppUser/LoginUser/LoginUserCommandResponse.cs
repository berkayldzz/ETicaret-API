using ETicaretAPI.Application.DTOs;

namespace ETicaretAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandResponse
    {

        // Böyle yapmak yerine succes ve error diye 2 parçaya ayırabiliriz.Solidden SRP uygulamış oluyoruz.

        //public Token Token { get; set; }
        //public string Message { get; set; }
    }
    public class LoginUserSuccessCommandResponse : LoginUserCommandResponse
    {
        public Token Token { get; set; }
    }
    public class LoginUserErrorCommandResponse : LoginUserCommandResponse
    {
        public string Message { get; set; }
    }
}
