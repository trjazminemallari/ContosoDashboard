Quickstart: Document Upload & Management

Prerequisites
- .NET 8.0 SDK
- SQL Server LocalDB
- Visual Studio 2022 or VS Code

Setup Steps
1. Pull latest code and restore dependencies:
   dotnet restore
2. Run the application (auto-creates DB and seeds sample data):
   dotnet run
3. Open browser to http://localhost:5000 and log in as any user.

Feature Usage
- Go to Documents page to upload, browse, search, preview, share, edit, or delete documents.
- Only supported file types (PDF, Office, images, text) ≤25MB are allowed.
- All uploads are scanned by a mock malware scanner in training.
- Shared documents appear in the recipient's "Shared with Me" section.

Migration Notes
- To migrate to Azure Blob Storage, implement AzureBlobStorageService and update DI registration.
