using AuthenticationUsingJWT.Interface;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationUsingJWT.Repository
{
    public class Auth : IJwtAuth
    {
        private readonly string username = "Asaad";
        private readonly string password = "demo1";
        private readonly string key;

        public Auth(string key)
        {
            this.key = key;
        }
        public string Authentication(string user, string pass)
        {
            if (!(username.Equals(user) || password.Equals(pass)))
            {
                return null;
            }
            //create security token Handler
            var tokenHandler=new JwtSecurityTokenHandler();

            //create private key to Encrypted
            var tokenKey = Encoding.ASCII.GetBytes(key);

            //create JETdescriptor

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[] {
                    new Claim(ClaimTypes.Name,username)
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            //create token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //return token from method
            return tokenHandler.WriteToken(token);

        }
    }
}
