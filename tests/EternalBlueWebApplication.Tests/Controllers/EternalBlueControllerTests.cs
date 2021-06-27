using AutoFixture;
using EternalBlueWebApplication.Contracts;
using EternalBlueWebApplication.Controllers;
using EternalBlueWebApplication.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EternalBlueWebApplication.Tests.Controllers
{
    public class EternalBlueControllerTests
    {
        private readonly EternalBlueController _controller;
        private readonly Fixture _fixture;
        private readonly Mock<IPasswordService> _passwordService;

        public EternalBlueControllerTests()
        {
            _fixture = new Fixture();
            _passwordService = new Mock<IPasswordService>(MockBehavior.Strict);

            _controller = new EternalBlueController(_passwordService.Object);
        }

        [Fact(DisplayName = "Should Index return first page")]
        public void ShouldIndex_ReturnFirstPage()
        {
            var firstPasswordAscii = _fixture.Create<string>();
            _passwordService
                .Setup(s => s.GetFirstPasswordAscii())
                .Returns(firstPasswordAscii);

            var result = (ViewResult) _controller.Index();

            result.Should().NotBeNull();
            result.Model.As<FirstLoginModel>().IsPasswordIncorrect.Should().BeFalse();
            result.Model.As<FirstLoginModel>().FirstPasswordASCIIForm.Should().Be(firstPasswordAscii);
            _passwordService.VerifyAll();
        }

        [Fact(DisplayName = "When password incorrect should Index return first page with incorrect passoword message")]
        public void WhenPasswordIncorrect_ShouldIndex_ReturnFirstPageWithIncorrectPasswordMessage()
        {
            var firstPasswordAscii = _fixture.Create<string>();
            var firstPass = _fixture.Create<string>();
            _passwordService
                .Setup(s => s.GetFirstPasswordAscii())
                .Returns(firstPasswordAscii);
            _passwordService
                .Setup(s => s.IsFirstPassword(firstPass))
                .Returns(false);

            var result = (ViewResult) _controller.Index(firstPass);

            result.Should().NotBeNull();
            result.Model.As<FirstLoginModel>().IsPasswordIncorrect.Should().BeTrue();
            result.Model.As<FirstLoginModel>().FirstPasswordASCIIForm.Should().Be(firstPasswordAscii);
            _passwordService.VerifyAll();
        }

        [Fact(DisplayName = "When password correct should Index return second step")]
        public void WhenPasswordCorrect_ShouldIndex_ReturnSecondStep()
        {
            var firstPass = _fixture.Create<string>();
            _passwordService
                .Setup(s => s.IsFirstPassword(firstPass))
                .Returns(true);

            var result = (ViewResult) _controller.Index(firstPass);

            result.Should().NotBeNull();
            result.ViewName.Should().Be("SecondStep");
            _passwordService.VerifyAll();
        }

        [Fact(DisplayName =
            "When password incorrect should SecondStep return second page with incorrect passoword message")]
        public void WhenPasswordIncorrect_ShouldSecondStep_ReturnSecondPageWithIncorrectPasswordMessage()
        {
            var secondPass = _fixture.Create<string>();
            _passwordService
                .Setup(s => s.IsSecondPassword(secondPass))
                .Returns(false);

            var result = (ViewResult) _controller.SecondStep(secondPass);

            result.Should().NotBeNull();
            result.ViewName.Should().Be("SecondStep");
            result.Model.As<bool>().Should().BeTrue();
            _passwordService.VerifyAll();
        }

        [Fact(DisplayName = "When password correct should SecondStep return Download page")]
        public void WhenPasswordCorrect_ShouldSecond_StepReturnDownloadPage()
        {
            var firstPass = _fixture.Create<string>();
            _passwordService
                .Setup(s => s.IsSecondPassword(firstPass))
                .Returns(true);

            var result = (ViewResult) _controller.SecondStep(firstPass);

            result.Should().NotBeNull();
            result.ViewName.Should().Be("DownloadTask");
            _passwordService.VerifyAll();
        }
    }
}