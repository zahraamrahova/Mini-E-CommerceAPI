using E_CommerceAPI.Application.Abstractions.Token;
using E_CommerceAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static E_CommerceAPI.Application.Features.Commands.AppUser.LoginUser.LoginUserCommandResponse;

namespace E_CommerceAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly UserManager<E_CommerceAPI.Domain.Entities.Identity.AppUser> _userManager;
        private readonly SignInManager<E_CommerceAPI.Domain.Entities.Identity.AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;
        public LoginUserCommandHandler(UserManager<E_CommerceAPI.Domain.Entities.Identity.AppUser> userManager, SignInManager<E_CommerceAPI.Domain.Entities.Identity.AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            E_CommerceAPI.Domain.Entities.Identity.AppUser user = await _userManager.FindByNameAsync(request.UserNameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(request.UserNameOrEmail);
            if(user==null)    
               throw new NotFoundUserException();
            SignInResult result= await  _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if(result.Succeeded)
            {
                Application.DTOs.Token token=  _tokenHandler.CreateAccessToken(5);
                return new LoginUserSuccessCommandResponse()
                {
                    Token = token
                };
            }
            //return new LoginUserErrorCommandResponse()
            //{
            //         Message = "Username and password are invalid"
            //};
            throw new AuthenticationErrorException();
        }
    }
}
