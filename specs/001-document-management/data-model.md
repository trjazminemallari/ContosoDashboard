Data Model: Document Upload & Management

Entities
Document
- DocumentId (int, PK)
- Title (string, required)
- Description (string, optional)
- Category (string, required; e.g., Project Documents, Team Resources, etc.)
- OriginalFileName (string, required)
- StoredFilePath (string, required, GUID-based, not exposed)
- FileType (string, required, 255 chars)
- FileSize (int, required)
- UploadDate (DateTime, required)
- UploadedBy (UserId, FK)
- ProjectId (int, FK, nullable)

DocumentShare
- DocumentId (int, FK)
- SharedWithUserId (int, FK)
- SharedByUserId (int, FK)
- SharedAt (DateTime)

Relationships
- Document.UploadedBy → User.UserId
- Document.ProjectId → Project.ProjectId (nullable)
- DocumentShare.DocumentId → Document.DocumentId
- DocumentShare.SharedWithUserId → User.UserId

Validation Rules
- Title, Category, and OriginalFileName required on upload
- FileType must be in allowed list
- FileSize ≤ 25MB
- StoredFilePath must be unique

State Transitions
- Uploaded → Shared → Edited/Deleted
