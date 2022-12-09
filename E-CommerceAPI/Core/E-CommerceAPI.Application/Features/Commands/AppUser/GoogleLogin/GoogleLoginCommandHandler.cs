using E_CommerceAPI.Application.Abstractions.Token;
using E_CommerceAPI.Application.DTOs;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Application.Features.Commands.AppUser.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        private readonly UserManager<E_CommerceAPI.Domain.Entities.Identity.AppUser> _userManager;
        private readonly ITokenHandler _tokenHandler;

        public GoogleLoginCommandHandler(UserManager<E_CommerceAPI.Domain.Entities.Identity.AppUser> userManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
            var settings= new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience= new List<string> { "240293118391-ufpe67gl3qq94e9k1rr15kk0cli9hp28.apps.googleusercontent.com" }
            };
           var payload= await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);
           var info = new UserLoginInfo(request.Provider, payload.Subject, request.Provider);
            E_CommerceAPI.Domain.Entities.Identity.AppUser user = await  _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            bool result = user != null;
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = payload.Email,
                        UserName= payload.Email,
                        NameSurname = payload.Email
                    };
                    var identityResult = await _userManager.CreateAsync(user);

                    result = identityResult.Succeeded;

                }
            }
            if (result)
                await _userManager.AddLoginAsync(user, info);
            else
                throw new Exception("External Authentication Error!");
            Token token = _tokenHandler.CreateAccessToken(5);
            return new GoogleLoginCommandResponse()
            {
                Token = token
            };

        }
    }
}
