<!--
Sync Impact Report:
- Version Change: None -> 1.0.0
- Modified Principles: All (Initial Draft)
- Added Sections: Architectural Standards
- Removed Sections: None
- Templates Requiring Updates:
  - .specify/templates/plan-template.md: ⚠ pending
  - .specify/templates/spec-template.md: ⚠ pending
  - .specify/templates/tasks-template.md: ⚠ pending
-->

# Zumra Constitution

## Core Principles

### I. Clean Architecture Strictness
The project MUST strictly adhere to Clean Architecture patterns. Dependencies MUST flow inward toward the Domain layer. The Domain layer MUST have zero dependencies on external frameworks, infrastructure, or presentation layers.

### II. Technology Stack Integrity
The backend MUST utilize ASP.NET Core and C#. Database management, querying, and migrations MUST be handled exclusively via Entity Framework Core. Bypassing EF Core for direct database mutations is strictly prohibited unless specifically justified by performance critical bottlenecks.

### III. Dual-Model Architecture (B2B & B2C)
The system is fundamentally designed to serve both Educational Centers (B2B) and individual Students (B2C). All features, domain models, and permission matrices MUST account for this dual-audience nature, ensuring secure tenancy and appropriate access controls.

### IV. Separation of Concerns
Business logic MUST reside in the Application layer (e.g., Application Services, CQRS Handlers) and NEVER in Controllers. Controllers MUST remain thin, serving only to handle HTTP routing, extract requests, and return appropriate HTTP responses.

### V. Test-Readiness
All layers MUST be designed for testability. Interfaces MUST be used to decouple external integration points (e.g., Payment gateways, CDN, Video streaming) to allow easy mocking during testing phases.

## Architectural Standards

The solution layout adheres to the following separation:
- **Domain**: Contains entities, enums, exceptions, and domain events.
- **Application**: Contains interfaces (Repositories, Services), DTOs, and core business rules.
- **Infrastructure**: Contains the EF Core `ApplicationDbContext`, Identity configurations, and implementations for external APIs (e.g., BunnyCDN, Stripe, Jitsi, MailKit).
- **Presentation**: The ASP.NET Core API layer containing Controllers and Middleware.

## Governance

This Constitution supersedes all other unstructured practices.
Any amendments to these principles require documentation, justification, and versioning updates.
All code modifications MUST be reviewed against this architectural specification. Pull requests introducing tight coupling or violating Clean Architecture logic placement MUST be blocked and revised.

**Version**: 1.0.0 | **Ratified**: 2026-04-23 | **Last Amended**: 2026-04-23
