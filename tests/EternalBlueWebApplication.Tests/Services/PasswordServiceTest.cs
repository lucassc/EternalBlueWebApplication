using AutoFixture;
using EternalBlueWebApplication.Configurations;
using EternalBlueWebApplication.Services;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace EternalBlueWebApplication.Tests.Services
{
    public class PasswordServiceTest
    {
        private readonly AzureSecretsName _azureSecretsName;
        private readonly Mock<IConfiguration> _configuration;
        private readonly Mock<IConfigurationSection> _configurationSection;
        private readonly Fixture _fixture;
        private readonly PasswordService _service;

        public PasswordServiceTest()
        {
            _fixture = new Fixture();
            _configuration = new Mock<IConfiguration>(MockBehavior.Strict);
            _configurationSection = new Mock<IConfigurationSection>();
            _azureSecretsName = _fixture.Create<AzureSecretsName>();

            _service = new PasswordService(
                _azureSecretsName,
                _configuration.Object);
        }

        [Theory(DisplayName = "Should FirstPassword validade first password correctly")]
        [InlineData("test", "$2y$10$uvDOXL/VDMrqwyHaMFZjQuTMu/70uxF.SkrQF3CUqafqQI8E2ldva", false)]
        [InlineData("test", "$2y$10$ZmRILfi9cnliGeC659g7G.4extZMGof2r7sB86j7J4nwXsdZmGOZy", true)]
        public void ShouldIsFirstPassword_ValidadeFirstPasswordCorrectly(string pass, string hash, bool isCorrect)
        {
            SetupConfiguration(_azureSecretsName.FirstPassword, hash);

            var result = _service.IsFirstPassword(pass);

            result.Should().Be(isCorrect);
            _configuration.VerifyAll();
        }

        [Theory(DisplayName = "Should SecondPassword validade second password correctly")]
        [InlineData("test", "$2y$10$uvDOXL/VDMrqwyHaMFZjQuTMu/70uxF.SkrQF3CUqafqQI8E2ldva", false)]
        [InlineData("test", "$2y$10$ZmRILfi9cnliGeC659g7G.4extZMGof2r7sB86j7J4nwXsdZmGOZy", true)]
        public void ShouldIsSecondPassword_ValidadeSecondPasswordCorrectly(string pass, string hash, bool isCorrect)
        {
            SetupConfiguration(_azureSecretsName.SecondPassword, hash);

            var result = _service.IsSecondPassword(pass);

            result.Should().Be(isCorrect);
            _configuration.VerifyAll();
        }

        [Fact(DisplayName = "Should GetFirstPasswordAscii return first password ASCII correctly")]
        public void ShouldGetFirstPasswordAscii_ReturnFirstPasswordASCIICorrectly()
        {
            var firstPassAscii = _fixture.Create<string>();
            SetupConfiguration(_azureSecretsName.FirstPasswordAscii, firstPassAscii);

            var result = _service.GetFirstPasswordAscii();

            result.Should().Be(firstPassAscii);
            _configuration.VerifyAll();
        }

        private void SetupConfiguration(string key, string value)
        {
            _configuration
                .Setup(s => s.GetSection(key))
                .Returns(_configurationSection.Object);
            _configurationSection
                .Setup(s => s.Value)
                .Returns(value);
        }
    }
}