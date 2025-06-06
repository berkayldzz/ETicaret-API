﻿using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.Abstractions.Token;
using ETicaretAPI.Application.DTOs;
using ETicaretAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETicaretAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        #region eski kod
        //readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        //readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager; // Kullanıcının giriş işlemlerinden sorumlu servis.   
        //readonly ITokenHandler _tokenHandler; // Token üreten servisimiz


        //public LoginUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager, SignInManager<Domain.Entities.Identity.AppUser> signInManager, ITokenHandler tokenHandler)
        //{
        //    _userManager = userManager;
        //    _signInManager = signInManager;
        //    _tokenHandler = tokenHandler;
        //}

        //public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        //{
        //    Domain.Entities.Identity.AppUser user = await _userManager.FindByNameAsync(request.UsernameOrEmail);
        //    if (user == null)
        //        user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);

        //    if (user == null)
        //        throw new NotFoundUserException("Kullanıcı veya şifre hatalı...");

        //    SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        //    if (result.Succeeded) //Authentication başarılı!
        //    {
        //        // Toke oluşturuyoruz.
        //       Token token= _tokenHandler.CreateAccessToken(5);
        //        return new LoginUserSuccessCommandResponse()
        //        {
        //            Token = token
        //        };
        //    }
        //    //return new LoginUserErrorCommandResponse()
        //    //{
        //    //    Message = "Kullanıcı adı veya şifre hatalı..."
        //    //};

        //    throw new AuthenticationErrorException();

        //}

        #endregion

        readonly IAuthService _authService;
        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var token = await _authService.LoginAsync(request.UsernameOrEmail, request.Password, 900);
            return new LoginUserSuccessCommandResponse()
            {
                Token = token
            };
        }

    }
}
