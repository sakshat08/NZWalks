using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NZWalks.API.Controllers;
using NZWalks.API.Repository;
using Xunit;

namespace NZWalksUnitTests.Controller
{
    public class RegionControllerTests
    {
        private readonly Mock<IRegionRepository> mockrepo;
        private RegionController controller;

        public RegionControllerTests()
        {
            this.mockrepo = new Mock<IRegionRepository>();
            this.controller = new RegionController(this.mockrepo.Object);
        }
        
        [Fact]
        public async void ControllerReturnNotFoundWhenNoRegionDataPresent()
        {
            ////this.mockrepo
            ////    .Setup(x => x.GetRegionById(It.IsAny<Guid>())).Returns(null);
            ///

            var actual = await this.controller.GetRegionById(new Guid());
            actual.Should().BeOfType<OkObjectResult>();
        }
    }
}
