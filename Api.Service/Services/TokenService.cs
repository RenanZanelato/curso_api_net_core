using Api.Domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace Api.Service.Services
{
    public class TokenService
    {
        private IConfiguration _configuration { get; set; }
        private readonly SigninConfiguration _signingConfiguration;
        private readonly TokenConfiguration _tokenConfigurations;
        public TokenService(SigninConfiguration signingConfiguration,
            TokenConfiguration tokenConfigurations, IConfiguration configuration)
        {
            _signingConfiguration = signingConfiguration;
            _tokenConfigurations = tokenConfigurations;
            _configuration = configuration;
        }
        public object GetToken(dynamic data) {
            ClaimsIdentity identify = new ClaimsIdentity(
              new GenericIdentity(data),
              new[]
              {
                  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                  new Claim(JwtRegisteredClaimNames.UniqueName,data),
              }
            );
            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            string token = CreateToken(identify, createDate,expirationDate,handler);
            return SuccessObject(createDate, expirationDate, token);

        }

        private string CreateToken(ClaimsIdentity identify, DateTime createDate,DateTime expirationDate,
            JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfiguration.SigningCredentials,
                Subject = identify,
                NotBefore = createDate,
                Expires = expirationDate
            });
            return handler.WriteToken(securityToken);
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate,string token)
        {
            return new
            {
                authenticate = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                message = "success"
            };
        }
    }
}
