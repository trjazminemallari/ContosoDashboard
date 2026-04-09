using System;
using System.IO;
using System.Threading.Tasks;
using ContosoDashboard.Data;
using ContosoDashboard.Models;
using ContosoDashboard.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ContosoDashboard.Tests.Services
{
    public class DocumentServiceTests
    {
        private DocumentService CreateService(ApplicationDbContext db)
        {
            var fileStorage = new Mock<IFileStorageService>();
            fileStorage.Setup(f => f.UploadAsync(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync("test-path");
            fileStorage.Setup(f => f.DeleteAsync(It.IsAny<string>())).Returns(Task.CompletedTask);
            fileStorage.Setup(f => f.DownloadAsync(It.IsAny<string>())).ReturnsAsync(new MemoryStream());
            fileStorage.Setup(f => f.GetUrlAsync(It.IsAny<string>(), It.IsAny<TimeSpan>())).ReturnsAsync("");
            var malwareScanner = new Mock<MockMalwareScannerService>();
            malwareScanner.Setup(m => m.ScanAsync(It.IsAny<Stream>())).ReturnsAsync(true);
            var logger = new Mock<ILogger<DocumentService>>();
            return new DocumentService(db, fileStorage.Object, malwareScanner.Object, logger.Object);
        }

        private ApplicationDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task UploadDocumentAsync_SavesDocument()
        {
            using var db = CreateDbContext();
            var service = CreateService(db);
            var doc = new Document
            {
                Title = "Test",
                Category = "Test",
                OriginalFileName = "test.txt",
                StoredFilePath = "test-path.txt",
                FileType = "text/plain",
                FileSize = 100,
                UploadedBy = 1
            };
            using var ms = new MemoryStream(new byte[] {1,2,3});
            var id = await service.UploadDocumentAsync(doc, ms);
            var saved = await db.Documents.FindAsync(id);
            Assert.NotNull(saved);
            Assert.Equal("Test", saved!.Title);
        }

        [Fact]
        public async Task GetDocumentAsync_ReturnsNullIfUnauthorized()
        {
            using var db = CreateDbContext();
            var service = CreateService(db);
            var doc = new Document
            {
                Title = "Test",
                Category = "Test",
                OriginalFileName = "test.txt",
                StoredFilePath = "test-path.txt",
                FileType = "text/plain",
                FileSize = 100,
                UploadedBy = 1
            };
            db.Documents.Add(doc);
            await db.SaveChangesAsync();
            var result = await service.GetDocumentAsync(doc.DocumentId, 2);
            Assert.Null(result);
        }
    }
}
