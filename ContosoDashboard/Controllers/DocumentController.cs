
        using System.IO;
        using System.Threading.Tasks;
        using ContosoDashboard.Models;
        using ContosoDashboard.Services;
        using Microsoft.AspNetCore.Authorization;
        using Microsoft.AspNetCore.Mvc;

namespace ContosoDashboard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly DocumentService _documentService;
        public DocumentController(DocumentService documentService)
        {
            _documentService = documentService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Upload([FromForm] DocumentUploadDto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
                return BadRequest("No file uploaded.");
            using var ms = new MemoryStream();
            await dto.File.CopyToAsync(ms);
            ms.Position = 0;
            var doc = new Document
            {
                Title = dto.Title,
                Description = dto.Description,
                Category = dto.Category,
                OriginalFileName = dto.File.FileName,
                FileType = dto.File.ContentType,
                FileSize = (int)dto.File.Length,
                UploadedBy = 1, // TODO: Replace with actual user ID from auth
                StoredFilePath = Guid.NewGuid() + Path.GetExtension(dto.File.FileName)
            };
            await _documentService.UploadDocumentAsync(doc, ms);
            return Ok();
        }


        [HttpGet("preview/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Preview(int id)
        {
            // For demo, using userId = 1. Replace with actual user auth logic as needed.
            int userId = 1;
            var doc = await _documentService.GetDocumentAsync(id, userId);
            if (doc == null)
                return NotFound();
            // Only allow preview for PDF and common image types
            var allowedTypes = new[] { "application/pdf", "image/png", "image/jpeg", "image/gif", "image/webp" };
            if (!allowedTypes.Contains(doc.FileType))
                return BadRequest("Preview not supported for this file type.");
            var fileStream = await _documentService.GetDocumentFileStreamAsync(doc);
            // Remove X-Frame-Options header to allow iframe embedding
            if (Response.Headers.ContainsKey("X-Frame-Options"))
                Response.Headers.Remove("X-Frame-Options");
            Response.Headers["X-Frame-Options"] = "ALLOWALL";
            return File(fileStream, doc.FileType);
        }

        [HttpGet("download/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Download(int id)
        {
            // For demo, using userId = 1. Replace with actual user auth logic as needed.
            int userId = 1;
            var doc = await _documentService.GetDocumentAsync(id, userId);
            if (doc == null)
                return NotFound();
            var fileStream = await _documentService.GetDocumentFileStreamAsync(doc);
            // Set content-disposition for download
            if (Response.Headers.ContainsKey("X-Frame-Options"))
                Response.Headers.Remove("X-Frame-Options");
            Response.Headers["X-Frame-Options"] = "ALLOWALL";
            return File(fileStream, doc.FileType, doc.OriginalFileName);
        }

    }

    public class DocumentUploadDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Category { get; set; } = string.Empty;
        public IFormFile? File { get; set; }
    }
}
