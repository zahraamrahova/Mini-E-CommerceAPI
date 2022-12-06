using E_CommerceAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace E_CommerceAPI.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly UserManager<E_CommerceAPI.Domain.Entities.Identity.AppUser> _userManager;
        public CreateUserCommandHandler(UserManager<E_CommerceAPI.Domain.Entities.Identity.AppUser> userManager)
        {
            _userManager= userManager;
        }
        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
          IdentityResult result=  await _userManager.CreateAsync(new()
            {
                Id= Guid.NewGuid().ToString(),
                NameSurname=request.NameSurname,
                UserName=request.UserName,
                Email=request.Email
            }, request.Password);
            CreateUserCommandResponse response = new CreateUserCommandResponse() { Succeeded= result.Succeeded};
            if (result.Succeeded)
                response.Message = "User addedd Succefully!";
            else          
                foreach(var error in result.Errors)                
                    response.Message += $"{error.Code} -- {error.Description}\n";
            return response;
                
            
        }
    }
}
