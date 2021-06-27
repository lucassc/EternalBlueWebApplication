using EternalBlueWebApplication.Configurations;
using EternalBlueWebApplication.Contracts;
using Microsoft.Extensions.Configuration;

namespace EternalBlueWebApplication.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly AzureSecretsName _azureSecretsName;
        private readonly IConfiguration _configuration;

        public PasswordService(
            AzureSecretsName azureSecretsName,
            IConfiguration configuration)
        {
            _azureSecretsName = azureSecretsName;
            _configuration = configuration;
        }

        public bool IsFirstPassword(string pass) =>
            IsPasswordCorrect(pass, _azureSecretsName.FirstPassword);

        public bool IsSecondPassword(string pass) =>
            IsPasswordCorrect(pass, _azureSecretsName.SecondPassword);

        public string GetFirstPasswordAscii() =>
            _configuration.GetValue<string>(_azureSecretsName.FirstPasswordAscii);

        private bool IsPasswordCorrect(string pass, string configKey)
        {
            var hashedPassword = _configuration.GetValue<string>(configKey);
            return BCrypt.Net.BCrypt.Verify(pass, hashedPassword);
        }
    }
}