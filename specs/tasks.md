# Tasks: Document Upload & Management

## Phase 1: Setup (Shared Infrastructure)
- [ ] T001 Create/verify AppData/uploads directory for local file storage
- [ ] T002 Add IFileStorageService interface to Services/
- [ ] T003 Add LocalFileStorageService implementation to Services/
- [ ] T004 Add mock malware scanner service to Services/
- [ ] T005 Update DI container in Program.cs to register new services

## Phase 2: Foundational (Data, Models, Security)
- [ ] T006 Add Document and DocumentShare entities to Models/
- [ ] T007 Add EF Core migrations for new entities
- [ ] T008 Add DocumentService to Services/ for upload, metadata, sharing, and audit logic
- [ ] T009 Add authorization checks to DocumentService (IDOR protection)
- [ ] T010 Add logging for document events (upload, download, delete, share)

## Phase 3: User Story 1 - Upload Document (P1)
- [ ] T011 Add upload modal/component to Pages/Documents.razor
- [ ] T012 Implement file selection, metadata form, and upload progress UI
- [ ] T013 Integrate upload logic with DocumentService and IFileStorageService
- [ ] T014 Validate file type, size, and scan with mock malware scanner
- [ ] T015 Persist metadata and file, show success/error messages
- [ ] T016 Add unit/integration tests for upload flow and validation
- [ ] T012 Implement DocumentController POST /api/documents endpoint in ContosoDashboard/Controllers/DocumentController.cs
  - Depends on: T011 (DocumentService)
  - Note: Include comprehensive error handling for file size limits and unsupported types

## Phase 4: User Story 2 - Browse, Search & Preview (P1)
- [ ] T017 Add document list/table to Pages/Documents.razor
- [ ] T018 Implement sorting, filtering, and search by title, description, tags, uploader, project
- [ ] T019 Add preview endpoint/controller for PDF/images (authorized)
- [ ] T020 Add preview UI to open PDF/image in browser
- [ ] T021 Add tests for search, filter, and preview

## Phase 5: User Story 3 - Share, Edit Metadata, Delete (P2)
- [ ] T022 Add sharing UI to select users/teams and send notifications
- [ ] T023 Implement DocumentShare logic in DocumentService
- [ ] T024 Add edit metadata and replace file UI/actions
- [ ] T025 Implement delete with confirmation and permanent removal
- [ ] T026 Add tests for sharing, edit, and delete flows

## Phase 6: Polish & Cross-Cutting Concerns
- [ ] T027 Add audit log/reporting for document events
- [ ] T028 Update dashboard widgets (recent documents, document count)
- [ ] T029 Update documentation and quickstart
- [ ] T030 Final checklist review and code cleanup
