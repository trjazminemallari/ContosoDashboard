Key Decisions
- Local file storage: Use AppData/uploads outside wwwroot for security and offline training. Pattern: {userId}/{projectId or "personal"}/{guid}.{ext}
- IFileStorageService abstraction: Enables future migration to Azure Blob Storage with no business logic changes.
- Mock malware scanner: Training implementation uses a stub; production must integrate a real scanner.
- Authorization: All file access via authorized endpoints, never direct file paths. Service-layer checks for IDOR prevention.
- Document sharing: Sharing grants access only to the document, not project membership.

Alternatives Considered
- Direct file storage in wwwroot: Rejected for security (would expose files without auth checks).
- No abstraction for storage: Would block cloud migration and violate constitution.
- Real malware scanner in training: Too complex for offline/trainee setup; stub is sufficient for learning.

References
- Stakeholder doc: StakeholderDocs/document-upload-and-management-feature.md
- Constitution: .specify/memory/constitution.md
