
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Api.Domain.Security
{
    public class SigninConfiguration
    {
        public SecurityKey Key { get; set; }
        public SigningCredentials SigningCredentials { get; set; }

        private const int _bytes = 2048;
        public SigninConfiguration()
        {
            using (var provider = new RSACryptoServiceProvider(_bytes))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }
            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);  
        }
    }
}
