﻿using E_CommerceAPI.Application.Features.Commands.AppUser.CreateUser;
using E_CommerceAPI.Application.Features.Commands.AppUser.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async  Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
           CreateUserCommandResponse createUserCommandResponse= await _mediator.Send(createUserCommandRequest);
            return Ok(createUserCommandResponse);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
           LoginUserCommandResponse loginUserCommandResponse= await _mediator.Send(loginUserCommandRequest);
            return Ok(loginUserCommandResponse);
        }
    }
}
