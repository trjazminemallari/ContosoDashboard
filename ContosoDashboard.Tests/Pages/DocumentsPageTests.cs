using System.Collections.Generic;
using System.Threading.Tasks;
using Bunit;
using ContosoDashboard.Models;
using ContosoDashboard.Pages;
using ContosoDashboard.Services;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace ContosoDashboard.Tests.Pages
{
    public class DocumentsPageTests : TestContext
    {
        [Fact]
        public void RendersDocumentListAndSearch()
        {
            // Arrange
            var docs = new List<Document>
            {
                new Document { DocumentId = 1, Title = "Alpha", Category = "Reports", UploadedBy = 1, FileSize = 1000 },
                new Document { DocumentId = 2, Title = "Beta", Category = "Personal", UploadedBy = 2, FileSize = 2000 }
            };
            var docService = new Mock<DocumentService>(null, null, null, null);
            docService.Setup(s => s.GetAllDocumentsForUserAsync(It.IsAny<int>())).ReturnsAsync(docs);
            Services.AddSingleton(docService.Object);

            // Act
            var cut = RenderComponent<Documents>();

            // Assert
            Assert.Contains("Alpha", cut.Markup);
            Assert.Contains("Beta", cut.Markup);
            // Simulate search
            cut.Find("input[placeholder='Search...']").Change("Alpha");
            Assert.Contains("Alpha", cut.Markup);
            Assert.DoesNotContain("Beta", cut.Markup);
        }
    }
}
