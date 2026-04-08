<!--
Sync Impact Report

Version change: template -> 1.0.0

Modified principles:
- [PRINCIPLE_1_NAME] -> Training & Offline-First
- [PRINCIPLE_2_NAME] -> Security & Authorization (IDOR Prevention)
- [PRINCIPLE_3_NAME] -> Test-First Quality
- [PRINCIPLE_4_NAME] -> Abstraction & Cloud Migration
- [PRINCIPLE_5_NAME] -> Simplicity & Observability

Added sections:
- Constraints & Standards
- Development Workflow

Removed sections: none

Templates requiring review: 
- .specify/templates/plan-template.md ⚠ pending review for "Constitution Check" alignment
- .specify/templates/spec-template.md ⚠ pending review for mandatory requirements language
- .specify/templates/tasks-template.md ⚠ pending review for task gating based on constitution
- .specify/templates/constitution-template.md ✅ used as source

Follow-up TODOs:
- TODO(RATIFICATION_DATE): Original ratification date unknown — please supply or approve as part of first commit
-->

# ContosoDashboard Constitution

## Core Principles

### Training & Offline-First (NON-NEGOTIABLE)
The project MUST remain a training-first, offline-capable application. All development and feature work
MUST support running without cloud services by default (local DB, local file storage, mock auth).
Rationale: preserves reproducible training environment, reduces external dependency friction, and ensures
students can run the system locally without cloud accounts.

### Security & Authorization (IDOR Prevention)
All data access paths and resources MUST enforce authorization checks at the service layer. Any endpoint
serving files or project resources MUST validate membership/permissions before returning content. Files
MUST be stored outside web-root and served via authorized endpoints.
Rationale: prevents Insecure Direct Object Reference (IDOR) and enforces least-privilege access.

### Test-First Quality (STRONG RECOMMENDATION FOR CRITICAL PATHS)
Critical behaviors (authentication, authorization, file storage, and data migrations) MUST have automated
tests (unit + integration/contract tests). Tests for new user stories SHOULD be authored before implementation
where practical and included in every PR that modifies behavior.
Rationale: ensures regressions are detected early and educates students in test-driven practices.

### Abstraction & Cloud Migration (DESIGN CONSTRAINT)
Infrastructure dependencies (file storage, database, external services) MUST be accessed through
interface abstractions (e.g., `IFileStorageService`). Local implementations are required for training,
but swapping to cloud implementations (Azure Blob, Azure SQL) MUST NOT require changes to business logic.
Rationale: teaches dependency inversion and enables easy production migration.

### Simplicity & Observability
Favor simple, explicit solutions over speculative generality (YAGNI). Instrument critical flows with structured
logs and meaningful metrics. Logging MUST include context (user id, request id, project id) for security and debugging.
Rationale: keeps the codebase approachable for learners while enabling effective troubleshooting.

## Constraints & Standards

- **Technology**: ASP.NET Core 8.0, Blazor Server, Entity Framework Core, SQL Server LocalDB for training
- **Storage**: Local filesystem for uploaded files (outside `wwwroot`) with `IFileStorageService` abstraction
- **File upload limits**: 25 MB per file (feature guidance); block unsupported MIME types and scan for malware
- **Data model**: `DocumentId` integer keys; `FileType` supports 255 chars; `Category` stored as text
- **Performance goals**: list pages/search should respond within 2s for typical training datasets
- **Security**: mock authentication for training only — production must use proper identity providers

## Development Workflow

- All substantive changes MUST be made via PRs and include a short migration plan if they change data schemas.
- PRs that modify critical security behavior or storage MUST include tests and at least two reviewers,
	one of whom is a repository maintainer.
- The repository's CI pipeline MUST run unit and integration tests; PRs must pass CI before merge.
- The `Constitution Check` in plans (see `.specify/templates/plan-template.md`) MUST be completed before
	Phase 0 research is approved.

## Governance

Amendments to this constitution require a documented PR with the following:

- **Proposal**: Describe the change, rationale, and migration/rollout plan for any breaking behavior.
- **Review**: At least **two approvals**, including one repository maintainer (or designated steward).
- **Versioning**: Use semantic versioning for the constitution:
	- MAJOR: Breaking governance or principle removals/renames
	- MINOR: New principle added or material expansion of guidance
	- PATCH: Clarifications, wording fixes, or typo corrections
- **Migration**: Any MAJOR or MINOR change MUST include a migration checklist and test plan demonstrating
	how to remediate projects that violate the new rules.

Compliance expectations: PR authors MUST call out relevant principles in their PR description (e.g., "Conforms
to: Security & Authorization; Abstraction & Cloud Migration"). Reviewers should verify tests and CI coverage.

**Version**: 1.0.0 | **Ratified**: TODO(RATIFICATION_DATE): original ratification date unknown | **Last Amended**: 2026-04-08

