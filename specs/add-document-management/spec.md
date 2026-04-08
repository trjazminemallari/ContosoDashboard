# Feature Specification: Document Upload & Management

**Feature Branch**: `[add-document-management]`  
**Created**: 2026-04-08  
**Status**: Draft  
**Input**: User description: "Document Upload and Management Feature - Requirements"

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Upload Document (Priority: P1)

An authenticated user uploads one or more documents (PDF, Office, images, text) and provides required metadata.

**Why this priority**: Upload is core value — without it users cannot centralize documents.

**Independent Test**: Select file(s) ≤25MB, provide title and category, submit upload. Verify file stored outside `wwwroot`, metadata persisted, and user sees success message.

**Acceptance Scenarios**:
1. **Given** user is authenticated, **When** they upload a supported file ≤25MB with title and category, **Then** upload succeeds, progress shown, metadata saved, and document appears in "My Documents".
2. **Given** user uploads unsupported file type or file >25MB, **When** upload is attempted, **Then** system rejects with clear error.

---

### User Story 2 - Browse, Search & Preview (Priority: P1)

Users browse their documents, filter/sort results, search by title/description/tags, and preview common file types (PDF, images).

**Independent Test**: Upload several documents with different categories and tags, then filter/sort/search and open a PDF preview.

**Acceptance Scenarios**:
1. **Given** multiple documents uploaded, **When** user filters by category or project, **Then** list shows only matching documents and loads within 2s.
2. **Given** a PDF or image, **When** user clicks preview, **Then** document displays inline without download.

---

### User Story 3 - Share, Edit Metadata, Delete (Priority: P2)

Owners can share with users/teams, edit metadata, replace file, and delete their documents. Sharing grants access only to the specified users/teams (no project membership change). Project Managers can manage project documents.

**Independent Test**: Owner shares document with another user; recipient sees notification and "Shared with Me" entry.

**Acceptance Scenarios**:
1. **Given** a document owner, **When** they share with a user, **Then** recipient receives in-app notification and can access the document (subject to permissions), but does not gain project membership or broader access.
2. **Given** a document owner, **When** they delete a document and confirm, **Then** file and metadata are permanently removed.

---

### Edge Cases

- What if file save succeeds but DB insert fails? -> Upload sequence must generate path, save file, then persist metadata; on DB failure, delete saved file to avoid orphaned files.
- Concurrent uploads with same filename -> use GUID-based stored filename; preserve original filename in metadata.
- Offline/no-disk space -> surface clear error and abort upload.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: System MUST allow authenticated users to upload one or more files with metadata (title required, description optional, category required).
- **FR-002**: System MUST support file types: PDF, DOCX, XLSX, PPTX, TXT, JPEG, PNG.
- **FR-003**: System MUST reject files larger than 25 MB with a descriptive error.
- **FR-004**: System MUST display upload progress and final success/error messages.
- **FR-005**: System MUST store files outside `wwwroot` and persist metadata in the database using integer `DocumentId` keys.
- **FR-006**: System MUST expose an authorized download/preview endpoint that validates permissions before serving files.
- **FR-007**: System MUST allow document owners to edit metadata and replace the stored file.
- **FR-008**: System MUST allow document owners to delete documents; Project Managers may delete any project document.
- **FR-009**: System MUST provide search by title, description, tags, uploader, and project and return results within 2 seconds for typical training datasets.
- **FR-010**: System MUST log document events (upload, download, delete, share) for audit reporting.
- **FR-011**: System MUST implement `IFileStorageService` abstraction and provide a `LocalFileStorageService` for training.
- **FR-012**: System MUST not expose file system paths; stored filenames MUST be GUID-based, with original filename preserved in metadata.
- **FR-013**: System MUST enforce authorization checks at service layer for all document operations (IDOR protection).
- **FR-014**: System MUST scan uploaded files for viruses/malware before storage. For the training implementation this will use a mock/stub scanner that returns a "clean" result while preserving the same scan workflow; production deployments MUST integrate a real malware scanner and configuration.

### Key Entities

- **Document**: DocumentId (int), Title, Description, Category (text), OriginalFileName, StoredFilePath, FileType (255 chars), FileSize, UploadDate, UploadedBy (UserId), ProjectId (nullable)
- **DocumentShare**: DocumentId, SharedWithUserId, SharedByUserId, SharedAt

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: 95% of uploads under 25 MB complete successfully within 30 seconds on typical network.
- **SC-002**: Document list and search return within 2 seconds for up to 500 documents.
- **SC-003**: 70% of active users upload at least one document within 3 months (business metric from stakeholder).
- **SC-004**: 90% of uploaded documents are categorized at upload time.
- **SC-005**: Zero unauthorized document accesses in audit logs for the first release.

## Assumptions

- Training environment uses LocalDB and local filesystem storage; no external cloud storage services are required.
- Malware scanning in training uses a mock/stub scanner that returns "clean" by default (Chosen: Q1: A). Production deployments MUST replace this with an operational scanner.
- Users have claims: NameIdentifier, Name, Email, Role, Department.

## Out of Scope

- Real-time collaborative editing, version history, external integrations (SharePoint, OneDrive), storage quotas, and soft-delete recovery.

## Implementation Notes

- Follow upload sequence: generate unique storage path → save file to disk → persist metadata → emit notification.
- Storage path pattern: `{userId}/{projectId or "personal"}/{guid}.{ext}` and stored outside `wwwroot` (e.g., `AppData/uploads`).
- Preview available for PDF and images; Office document preview is out-of-scope for this release.

## Clarifications

### Session 2026-04-08
- Q: When sharing a document, does the recipient become a project member or only get access to the document?  
	→ A: Only gets access to the document (no project membership change).

### Q1: Malware scanning approach (RESOLVED)

Choice recorded: **A — mock/stub scanner**. Training implementation will use a mock scanner that returns a "clean" result while exercising the same scan workflow. This choice is documented in the Assumptions and FR-014.

## READY FOR PLANNING

This spec is ready for planning following the recorded clarifications.

