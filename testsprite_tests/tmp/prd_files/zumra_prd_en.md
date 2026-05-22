## Product Overview

- **Product Name**: Zumra Learning Platform (Backend API)
- **Type**: ASP.NET Core Web API serving a React web app and future clients.
- **Short Description**:
  Zumra is a training/learning management backend that enables facilities/training centers to manage learners, courses, live and recorded sessions, assignments, and payments with a flexible role/permission system and secure APIs.

- **Primary Objective**:
  Provide a central, secure, and scalable backend to manage the full training lifecycle (from registration and payment to course completion) with a simple experience for learners and powerful tools for facilities.

## Product Goals

- Improve learner experience with simple registration/login and easy access to courses, content, and assignments.
- Empower training centers to manage facilities, groups, instructors, learners, courses, schedules, and payments in one place.
- Boost engagement and completion using live sessions, recorded content, assignments, and progress tracking.
- Enable scalability for multi‑facility, high‑traffic scenarios and future extensions.
- Ensure security with JWT auth, roles/policies, and safe handling of payments and OAuth flows.

## Scope

### In Scope

- Accounts & Authentication (register, login, email confirmation, password reset via OTP, Google login with JWT).
- Facilities management with facility‑scoped roles and authorization policies.
- Categories & Courses, including course batches (cohorts).
- Sections & Lessons (recorded via Bunny, live via Jitsi).
- Tasks/Assignments and learner submissions with review/grading.
- Cart & Payments (Stripe) with coupons/discounts.
- Email notifications for confirmation and password reset (HTML templates).
- API consumption by frontend with CORS and OpenAPI/Scalar docs.

### Out of Scope (for now)

- Native mobile apps.
- Advanced analytics dashboards.
- Full multi‑language content management.
- Public multi‑org course marketplace.

## Target Users

- Facility Owner (SuperAdmin)
- Program/Facility Leader
- Instructor
- Learner/Student
- System/Technical Admin

## Key Functional Areas

- **Auth**: Identity‑based user management, JWT issuance, Google OAuth, OTP‑based reset.
- **Facilities & Roles**: Facility‑level roles and custom authorization handler.
- **Learning Content**: Structured courses → sections → lessons (live/recorded).
- **Assignments**: Task creation, submissions, and grading.
- **Commerce**: Cart, coupons, Stripe payments, and enrolment granting.
- **Notifications**: Email flows driven by templates.

