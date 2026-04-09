using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ContosoDashboard.Data;
using ContosoDashboard.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ContosoDashboard.Services
{
    public class DocumentService
    {
        private readonly ApplicationDbContext _db;
        private readonly IFileStorageService _fileStorage;
        private readonly MockMalwareScannerService _malwareScanner;
        private readonly ILogger<DocumentService> _logger;

        public DocumentService(ApplicationDbContext db, IFileStorageService fileStorage, MockMalwareScannerService malwareScanner, ILogger<DocumentService> logger)
        {
            _db = db;
            _fileStorage = fileStorage;
            _malwareScanner = malwareScanner;
            _logger = logger;
        }

        public async Task<int> UploadDocumentAsync(Document doc, Stream fileStream)
        {
            // Validate file (size/type)
            if (doc.FileSize > 25 * 1024 * 1024)
                throw new InvalidOperationException("File exceeds 25MB limit.");
            // Scan for malware
            if (!await _malwareScanner.ScanAsync(fileStream))
                throw new InvalidOperationException("Malware detected.");
            // Save file
            var storedFileName = await _fileStorage.UploadAsync(fileStream, doc.StoredFilePath, doc.FileType);
            doc.StoredFilePath = storedFileName; // Only store the file name
            doc.UploadDate = DateTime.UtcNow;
            _db.Documents.Add(doc);
            await _db.SaveChangesAsync();
            _logger.LogInformation("Document uploaded: {DocumentId} by {UserId}", doc.DocumentId, doc.UploadedBy);
            return doc.DocumentId;
        }

        public async Task<Document?> GetDocumentAsync(int documentId, int userId)
        {
            var doc = await _db.Documents.FindAsync(documentId);
            if (doc == null) return null;
            // Authorization: Only owner or shared users can access
            if (doc.UploadedBy != userId && !_db.DocumentShares.Any(ds => ds.DocumentId == documentId && ds.SharedWithUserId == userId))
                return null;
            return doc;
        }

        public async Task DeleteDocumentAsync(int documentId, int userId)
        {
            var doc = await _db.Documents.FindAsync(documentId);
            if (doc == null) return;
            if (doc.UploadedBy != userId)
                throw new UnauthorizedAccessException();
            await _fileStorage.DeleteAsync(doc.StoredFilePath);
            _db.Documents.Remove(doc);
            await _db.SaveChangesAsync();
            _logger.LogInformation("Document deleted: {DocumentId} by {UserId}", documentId, userId);
        }


        // Public method to get file stream for preview
        public async Task<Stream?> GetDocumentFileStreamAsync(Document doc)
        {
            if (doc == null) return null;
            return await _fileStorage.DownloadAsync(doc.StoredFilePath);
        }

        public async Task<List<Document>> GetAllDocumentsForUserAsync(int userId)
        {
            // Return all documents uploaded by the user or shared with the user
            var uploadedDocs = await _db.Documents.Where(d => d.UploadedBy == userId).ToListAsync();
            var sharedDocIds = await _db.DocumentShares
                .Where(ds => ds.SharedWithUserId == userId)
                .Select(ds => ds.DocumentId)
                .ToListAsync();
            var sharedDocs = await _db.Documents.Where(d => sharedDocIds.Contains(d.DocumentId)).ToListAsync();
            // Combine and remove duplicates
            return uploadedDocs.Concat(sharedDocs).GroupBy(d => d.DocumentId).Select(g => g.First()).ToList();
        }
    }
}
