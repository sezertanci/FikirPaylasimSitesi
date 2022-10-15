using Application.Interfaces.Repositories;
using AutoMapper;
using Common.Infrastructure;
using Common.Infrastructure.Exceptions;
using Common.Models.Queries;
using Common.Models.RequestModels.UserRequestModels.LoginRequestModels;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Features.Commands.UserCommand.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private IUserRepository userRepository;
        private IMapper mapper;
        private IConfiguration configuration;

        public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetSingleAsync(x => x.EmailAddress == request.EmailAddress);

            if(user is null)
                throw new DatabaseValidationException("User not found!");

            if(!PasswordEncryptor.VerifyPassword(request.Password, user.Password, user.PasswordSalt))
                throw new DatabaseValidationException("Password is wrong!");

            if(!user.EmailConfirmed)
                throw new DatabaseValidationException("Email address is not confirmed yet!");

            var result = mapper.Map<LoginUserViewModel>(user);

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.EmailAddress),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Surname,user.LastName),
                new Claim(ClaimTypes.Name,user.UserName),
            };

            result.Token = GenerateToken(claims);

            return result;
        }

        private string GenerateToken(Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthConfig:Secret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(1);

            var token = new JwtSecurityToken(claims: claims, expires: expiry, signingCredentials: credentials, notBefore: DateTime.Now);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
