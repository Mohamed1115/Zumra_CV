## 1. نظرة عامة على المنتج

- **اسم المنتج**: منصة Zumra للتعلّم والتدريب  
- **النوع**: Backend API (ASP.NET Core) يخدم واجهة React Web وتطبيقات مستقبلية (موبايل، لوحة إدارة، …).  
- **وصف مختصر**:  
  منصة إدارة وتقديم دورات تدريبية (أونلاين و/أو حضوري) تتيح للمراكز/المنشآت (Facilities) إدارة المتعلمين، الدورات، الجلسات الحيّة والمسجَّلة، الواجبات، والدفعات المالية، مع نظام صلاحيات مرن وواجهة برمجة تطبيقات آمنة.

- **الهدف الأساسي**:  
  تمكين المراكز التدريبية والمدرّسين من إدارة العملية التعليمية كاملة (من التسجيل والدفع حتى إكمال الدورة) عبر نظام مركزي، مع تجربة بسيطة للمتعلّم.

---

## 2. الأهداف (Product Goals)

- **تحسين تجربة الطالب**: تسجيل بسيط، تسجيل دخول آمن (Email + Google)، الوصول السريع للدورات والمحتوى والواجبات.
- **تمكين المراكز التدريبية**: إدارة منشآت (Facilities)، مجموعات (Groups)، مدرّسين (Instructors)، طلاب، دورات، جداول، ودفعات من مكان واحد.
- **زيادة الالتزام والتفاعل**: دروس حيّة (Live) ومسجَّلة، واجبات/Tasks وتسليمات، متابعة التقدُّم.
- **قابلية التوسع**: تصميم الـ API بحيث يدعم تعدّد المنشآت، عدد كبير من المستخدمين، وإضافات مستقبلية (Mobile App، أنظمة تقارير، تكاملات أخرى).
- **أمان عالي**: مصادقة JWT، أدوار وصلاحيات دقيقة، التعامل السليم مع بيانات الدفع (Stripe) والـ OAuth (Google).

---

## 3. نطاق المنتج (Scope)

### 3.1 في النطاق (In Scope)

- **نظام حسابات ومصادقة**:
  - تسجيل مستخدم جديد مع تأكيد البريد.
  - تسجيل الدخول (Email/Username + Password).
  - استرجاع كلمة المرور عبر OTP على البريد.
  - تسجيل دخول عبر Google (External Login) وإصدار JWT.
- **إدارة المنشآت (Facilities)**:
  - إنشاء وتحديث وحذف منشآت.
  - ربط المستخدمين بالمنشأة مع أدوار (SuperAdmin, Leader, Instructor, Member).
  - Policies مخصصة لصلاحيات المنشأة.
- **إدارة الفئات والدورات (Categories & Courses)**:
  - فئات (Categories).
  - دورات (Courses) بمحتوى منظم (Sections → Lessons).
  - دفعات/مجموعات (CourseBatches) لكل دورة.
- **المحتوى التعليمي**:
  - دروس مسجَّلة (LessonRec) عبر Bunny.
  - دروس حيّة (LessonLive) عبر Jitsi.
  - إدارة Sections وLessons.
- **المهام وتسليم المهام (Tasks & Task Submissions)**:
  - إنشاء مهام للطلاب ضمن الدورات/الدروس.
  - تسليم الطالب للمهام وتتبع الحالة.
- **السلة والدفعات (Cart & Payments)**:
  - إضافة الدورات إلى سلة الشراء.
  - استخدام Stripe لمعالجة المدفوعات.
  - كوبونات (Coupons) وخصومات.
- **نظام الإشعارات عبر البريد**:
  - تأكيد البريد.
  - استرجاع كلمة المرور (OTP).
  - رسائل أخرى مرتبطة بالتسجيل والحضور (قابلة للتوسعة).
- **API مهيّأ للاستهلاك من الـ Frontend**:
  - CORS مفعّل لـ React (localhost:5173).
  - توثيق عبر OpenAPI/Scalar.

### 3.2 خارج النطاق (Out of Scope الآن، قابلة للإضافة لاحقًا)

- تطبيق موبايل.
- نظام تقارير/Analytics متقدّم (لوحات إحصائية).
- دعم لغات متعددة بشكل كامل على مستوى المحتوى.
- Marketplace عام لبيع الدورات بين أكثر من منشأة.

---

## 4. المستخدمون المستهدفون (User Personas)

- **مالك المنشأة (Facility SuperAdmin)**:
  - أهدافه: إدارة المنشأة بالكامل، إعداد الدورات، إضافة المدرّسين، متابعة الدخل والطلاب.
- **قائد المنشأة/البرنامج (Facility Leader)**:
  - أهدافه: إدارة المدرّسين والمجموعات، الإشراف على تنفيذ الدورات والخطط.
- **المدرّس (Instructor)**:
  - أهدافه: إنشاء وتحديث المحتوى، جدولة الجلسات، إضافة الواجبات، تقييم الطلاب.
- **الطالب (Member/User)**:
  - أهدافه: التسجيل والدفع، الانضمام للدورات، مشاهدة المحتوى، حضور الدروس الحيّة، تسليم الواجبات.
- **مسؤول تقني / Admin عام (System Admin)**:
  - أهدافه: إدارة النظام ككل، إعدادات عامة، صيانة، مراقبة مشاكل الدخول والدفع.

---

## 5. متطلبات الوظائف الأساسية (Functional Requirements)

### 5.1 إدارة الحسابات والمصادقة

- **تسجيل حساب جديد (Register)**  
  - المدخلات: `Name`, `Email`, `Password`, حقول أخرى حسب النموذج.  
  - المخرجات: رسالة نجاح + إرسال بريد تأكيد.  
  - شروط القبول:
    - البريد يجب أن يكون فريدًا.
    - كلمة المرور تلتزم بقيود Identity (طول، حروف كبيرة/صغيرة، رقم، …).
    - يتم إرسال رابط تأكيد إلى البريد عبر قالب HTML داخل `wwwroot/Templates/ConfirmEmail.html`.

- **تأكيد البريد (Email Confirmation)**  
  - عند فتح رابط التأكيد:
    - إذا الـ token صحيح → تأكيد البريد + رسالة نجاح.
    - إذا الـ token غير صحيح/منتهي → رسالة فشل مناسبة.

- **تسجيل الدخول (Login)**  
  - سيناريوهات:
    - بيانات صحيحة + بريد مؤكَّد → JWT Token + رسالة نجاح.
    - بيانات خاطئة → رسالة "Invalid username or password".
    - حساب مقفول → رسالة تبيّن مدة الإقفال المتبقية.
    - بريد غير مؤكَّد → رسالة تطلب تأكيد البريد أولاً.
  - الـ Token يحتوي على:
    - `NameIdentifier`, `Name`, `Email`, `Role(s)`, `Jti`.

- **تسجيل الدخول عبر Google (External Login)**  
  - بدء الـ flow من Endpoint `ExternalLogin`:
    - إعادة توجيه للمزوّد (Google).
  - عند العودة إلى `ExternalLoginCallback`:
    - إذا المستخدم غير موجود → يتم إنشاؤه وتعيين الدور الافتراضي.
    - ربط ExternalLogin بالمستخدم.
    - إصدار JWT وإعادة توجيه إلى `returnUrl` مع `token` و `email` و `username`.

- **استرجاع كلمة المرور (Forgot Password + OTP)**  
  - المدخل: Email.
  - إذا المستخدم موجود:
    - إنشاء OTP 6 أرقام وربطه بالبريد.
    - إرسال بريد باستخدام قالب HTML `ResetPassword.html` يعرض الأرقام بشكل جميل.
  - الرد للمستخدم لا يكشف إذا البريد مسجل أو لا (لأسباب أمان).

- **التحقق من OTP (VerifyOTP)**  
  - المدخل: `Email`, `Otp`.
  - التحقق من:
    - وجود OTP.
    - صلاحية وعدم انتهاء الصلاحية.
  - في حال النجاح: تعليم OTP كمستخدم، وإرجاع نجاح مع البريد.

- **إعادة تعيين كلمة المرور (ResetPassword)**  
  - المدخل: `Email`, `Password` الجديد.
  - إنشاء token وإعادة تعيين كلمة المرور عبر Identity.
  - الرد بحالة النجاح أو الأخطاء (Weak password، الآخر).

---

### 5.2 إدارة المنشآت والصلاحيات (Facilities & Authorization)

- **الأدوار (Roles)**:
  - `FacilitySuperAdmin`, `FacilityLeader`, `FacilityInstructor`, `FacilityMember`، بالإضافة لأدوار عامة (`SD.UserRole` …).
- **السياسات (Policies)**:
  - `FacilitySuperAdmin`: فقط لمن له هذا الدور داخل المنشأة المحددة.
  - `FacilityLeader`: صلاحيات القيادة.
  - `FacilityInstructor`: صلاحيات المدرب.
  - `FacilityMember`: أي عضو في المنشأة.
- **Handler مخصص**:
  - `FacilityAuthorizationHandler` يتحقق من أن المستخدم يمتلك الدور الصحيح في المنشأة المطلوبة (مثلاً من خلال Claims/DB).

---

### 5.3 إدارة الدورات والمحتوى (Courses, Sections, Lessons)

- **الدورات (Courses)**:
  - إنشاء/تحديث/حذف دورة.
  - ربط الدورة بفئة (Category) ومنشأة.
  - حقول: عنوان، وصف، صورة غلاف، سعر، مستوى، مدة، … إلخ.

- **دفعات الدورات (CourseBatches)**:
  - تعريف دفعات مختلفة لنفس الدورة (تواريخ بداية/نهاية، سعة، سعر خاص …).

- **الأقسام والدروس (Sections & Lessons)**:
  - Sections: تقسيم الدورة إلى وحدات/محاور.
  - Lessons: دروس داخل كل Section.
  - دعم نوعين:
    - `LessonRec` (مسجَّل): رابط/ID من Bunny.
    - `LessonLive` (حيّ): رابط/ID من Jitsi مع مواعيد محددة.

---

### 5.4 إدارة المهام وتسليمها (Tasks & Submissions)

- **المهام (Tasks)**:
  - المدرب ينشئ واجبات مرتبطة بدورة/درس معيّن.
  - خصائص: عنوان، وصف، موعد نهائي، مرفقات اختيارية.

- **تسليمات المهام (TaskSubmissions)**:
  - الطالب يرفع الحل (ملف/نص/رابط).
  - حالة التسليم: Submitted, Reviewed, Graded, … إلخ.
  - المدرس يمكنه تقييم التسليم وإضافة Feedback.

---

### 5.5 السلة والمدفوعات (Cart & Payments)

- **السلة (Cart)**:
  - إضافة/إزالة دورة من السلة.
  - حفظ حالة السلة للمستخدم.

- **الدفع (Stripe)**:
  - إنشاء Sessions دفع آمنة عبر Stripe.
  - دعم العملات/الأسعار حسب إعداد المنشأة (يُحدد لاحقًا).
  - بعد الدفع:
    - توثيق العملية في النظام.
    - إعطاء صلاحية الوصول للدورة/الدفعة.

- **الكوبونات (Coupons)**:
  - إدخال كود خصم.
  - التحقق من صلاحية الكود (تاريخ صلاحية، عدد مرات استخدام، نوع الخصم).
  - تطبيق الخصم على إجمالي السعر.

---

### 5.6 البريد والإشعارات (Email Notifications)

- **إرسال البريد عبر `IEmailSender`**:
  - تأكيد البريد.
  - استرجاع كلمة المرور.
  - قابلة للتوسعة لسيناريوهات أخرى (إشعار الانضمام للدورة، تذكير بالجلسة، …).

---

### 5.7 التكاملات الخارجية

- **Bunny**:
  - تخزين/بث الفيديوهات للدروس المسجَّلة.
  - حفظ معرفات الفيديو في الـ Lessons.
- **Jitsi**:
  - إنشاء/استخدام غرف للاجتماعات الحيّة (Live Lessons).
- **Stripe**:
  - معالجة مدفوعات الكورسات.
- **Google Auth**:
  - تسجيل/تسجيل دخول خارجي.

---

## 6. متطلبات غير وظيفية (Non‑Functional Requirements)

- **الأداء**:
  - استجابة أقل من 500ms في 90% من الطلبات تحت حمل عادي.
- **الأمان**:
  - جميع الـ Endpoints الحساسة محمية بـ JWT وPolicies.
  - حماية من الهجمات الشائعة (Brute Force عبر Lockout، CSRF في الـ Frontend، …).
- **القابلية للتوسع**:
  - بنية طبقية (Controllers → Services → Repositories).
  - دعم تعدد المنشآت.
- **القابلية للاختبار**:
  - فصل المنطق في Services ليسهل اختبارها بوحدات.
  - تغطية اختبارات للوحدات الحرجة (Auth, Payments, OTP, Facility policies).
- **الموثوقية**:
  - Retry على مستوى الـ DB (مفعَّل في DbContext).
  - تسجيل أخطاء عبر `ILogger`.

---

## 7. قياسات النجاح (KPIs)

- نسبة إكمال تسجيل الحساب (من أول زيارة حتى تأكيد البريد).
- نسبة نجاح تسجيل الدخول بدون أخطاء.
- نسبة إتمام الدفع بنجاح من أول محاولة.
- عدد الدورات النشطة وعدد الطلاب المسجلين.
- متوسط وقت الاستجابة للـ API.

---

## 8. خارطة طريق عالية المستوى (High‑Level Roadmap)

1. **المرحلة 1 – الأساسيات (منجزة تقريبًا)**  
   - Auth (Register/Login/Confirm/Reset).  
   - Models أساسية للدورات، المنشآت، المهام.  
   - Stripe, Bunny, Jitsi تكامل أوّلي.

2. **المرحلة 2 – تقوية التجربة**  
   - تنظيم Services بشكل أوضح (Refactor لطبقة Application).  
   - تغطية اختبارات أوسع (Register, ForgotPassword, ExternalLogin, Facility Policies).  
   - تحسين إدارة الأخطاء ورسائل الرد.

3. **المرحلة 3 – Analytics & Admin**  
   - لوحات إدارة/تقارير أساسية.  
   - Events/Logs أكثر تفصيلًا لاستخدام المستخدمين.

---

إذا حابب، أقدر أساعدك في خطوة تالية زي:  
- تحويل الـ PRD ده إلى **Backlog من User Stories** قابلة للتنفيذ.  
- أو استخراج **قائمة Endpoints منظمة** من الكود الحالي وربطها بالـ Features في الـ PRD.



## 1. Product Overview

- **Product Name**: Zumra Learning Platform (Backend API)  
- **Type**: ASP.NET Core Web API serving a React web app (and future clients such as mobile apps, admin panels, etc.).  
- **Short Description**:  
  Zumra is a training/learning management backend that enables facilities/training centers to manage learners, courses, live and recorded sessions, assignments, and payments with a flexible role/permission system and secure APIs.

- **Primary Objective**:  
  Provide a central, secure, and scalable backend to manage the full training lifecycle (from registration and payment to course completion) with a simple experience for learners and powerful tools for facilities.

---

## 2. Product Goals

- **Improve learner experience**: Simple registration and login (Email + Google), easy access to courses, content, and assignments.
- **Empower training centers**: Manage facilities, groups, instructors, learners, courses, schedules, and payments in one place.
- **Boost engagement and completion**: Support live sessions, recorded content, assignments, and progress tracking.
- **Enable scalability**: Design APIs to support multi‑facility scenarios, high user counts, and future extensions.
- **Ensure security**: JWT auth, fine‑grained roles and policies, safe handling of payment data (Stripe) and OAuth flows (Google).

---

## 3. Scope

### 3.1 In Scope

- **Accounts & Authentication**:
  - User registration with email confirmation.
  - Email/username + password login.
  - Password reset via OTP sent to email.
  - Google external login and JWT issuance.

- **Facilities Management**:
  - Create/update/delete facilities.
  - Associate users with a facility and assign facility‑level roles (SuperAdmin, Leader, Instructor, Member).
  - Authorization policies based on facility roles.

- **Categories & Courses**:
  - Manage categories.
  - Manage courses, linked to categories and facilities.
  - Manage course batches (cohorts) for specific schedules.

- **Learning Content**:
  - Sections within a course.
  - Lessons within sections.
  - Recorded lessons (Bunny) and live lessons (Jitsi).

- **Assignments & Submissions**:
  - Instructors create tasks/assignments linked to courses/lessons.
  - Learners submit solutions; instructors review and grade.

- **Cart & Payments**:
  - Shopping cart to hold courses.
  - Stripe integration for secure payments.
  - Coupons/discount codes.

- **Email Notifications**:
  - Email confirmation.
  - Password reset (OTP + HTML template).
  - Extension‑ready for more notification scenarios.

- **Frontend Consumption**:
  - CORS configured for React (`http://localhost:5173`).
  - OpenAPI/Scalar documentation for APIs.

### 3.2 Out of Scope (for now / future candidates)

- Native mobile apps.
- Advanced analytics dashboards and reporting.
- Full multi‑language content management.
- Public marketplace of courses across multiple organizations.

---

## 4. Target Users (Personas)

- **Facility Owner (Facility SuperAdmin)**  
  - Needs: Full control over the facility, courses, staff, and financials.

- **Program/Facility Leader (Facility Leader)**  
  - Needs: Manage instructors, groups, and day‑to‑day execution of training programs.

- **Instructor**  
  - Needs: Create and manage course content, schedule sessions, create assignments, review submissions.

- **Learner/Student (Member/User)**  
  - Needs: Register and pay for courses, access content, attend live classes, submit assignments.

- **System Admin / Technical Admin**  
  - Needs: Maintain system health, configuration, and debug issues (auth, payments, integrations).

---

## 5. Functional Requirements

### 5.1 Accounts & Authentication

- **Registration (Register)**  
  - Inputs: `Name`, `Email`, `Password`, and other profile fields as defined by the DTO.  
  - Behavior:
    - Email must be unique.
    - Password must meet Identity password policy.
    - On success:
      - User is created and assigned default role (e.g., `SD.UserRole`).
      - Email confirmation token is generated.
      - Confirmation email sent using HTML template `ConfirmEmail.html` with a confirmation link.
  - Outputs:
    - Success message prompting user to check email.
    - On failure: friendly error messages with validation/Identity errors.

- **Email Confirmation (Confirm)**  
  - Inputs: `token`, `id` (user id) from the confirmation link.
  - Behavior:
    - Validate user id and token via Identity.
  - Outputs:
    - On success: email confirmed message.
    - On failure: invalid/expired token message.

- **Login (Login)**  
  - Inputs: `UserName` (username or email), `Password`, `RememberMe` flag.  
  - Behavior:
    - Validate credentials using `SignInManager`.
    - Handle scenarios:
      - Success → Generate JWT and return to client.
      - Locked out → Return message with remaining lockout time if available.
      - Not allowed (email not confirmed) → Return message indicating email confirmation is required.
      - Failure → Generic "Invalid username or password.".
  - Token Claims:
    - `NameIdentifier`, `Name`, `Email`, `Role` (comma‑separated roles), `Jti` (e.g., date‑based).

- **Google External Login**  
  - Start flow (`ExternalLogin`):
    - Input: `provider` (e.g., "google"), optional `returnUrl`.
    - Behavior: Configure external auth properties and challenge Google.
  - Callback (`ExternalLoginCallback`):
    - Use `SignInManager.GetExternalLoginInfoAsync`.
    - Extract email and display name from external claims.
    - If user does not exist:
      - Create new `ApplicationUser` with:
        - Generated username.
        - Name from provider.
        - Email and `EmailConfirmed = true`.
      - Assign default role.
      - Link external login.
    - If exists:
      - Ensure external login is linked.
    - Generate JWT.
    - If `returnUrl` is non‑root:
      - Redirect with query params `token`, `email`, `username`.
    - Else:
      - Return JSON with token and user info.

- **Forgot Password (ForgotPassword)**  
  - Inputs: `Email`.
  - Behavior:
    - If user exists:
      - Generate 6‑digit OTP.
      - Save OTP in DB (via `IRepository<Otp>` + `IUnitOfWork`).
      - Load `ResetPassword.html` template, insert OTP digits into placeholders.
      - Send email with OTP.
    - Response should NOT reveal whether the email exists (for security).
  - Outputs:
    - Generic success message ("If the email exists, a code has been sent").

- **Verify OTP (VerifyOTP)**  
  - Inputs: `Email`, `Otp`.
  - Behavior:
    - Fetch OTP entry for email and code.
    - Validate existence and non‑expiry.
    - Mark OTP as used and persist.
  - Outputs:
    - Success message indicating OTP verified and user can now reset password.
    - Failure messages for invalid or expired OTP.

- **Reset Password (ResetPassword)**  
  - Inputs: `Email`, `Password` (new password) via `ResetPasswordRequest`.
  - Behavior:
    - Find user by email.
    - Generate password reset token.
    - Reset password using Identity.
  - Outputs:
    - On success: password reset success message.
    - On failure: validation/Identity errors.

- **Resend Confirmation Email (ResendconfirmEmail)**  
  - Inputs: `email`.
  - Behavior:
    - If user exists and email not confirmed:
      - Generate new email confirmation token.
      - Build confirmation link.
      - Send email using `ConfirmEmail.html` template.
    - If already confirmed: return message indicating so.
  - Outputs:
    - Success/failure messages for resend.

---

### 5.2 Facilities & Authorization

- **Roles**:
  - Facility‑scoped roles: `FacilitySuperAdmin`, `FacilityLeader`, `FacilityInstructor`, `FacilityMember`.
  - General roles (e.g., `SD.UserRole`) for global permissions.

- **Policies** (configured in `Program.cs`):
  - `FacilitySuperAdmin`: requires `FacilityRole.SuperAdmin`.
  - `FacilityLeader`: requires `FacilityRole.Leader`.
  - `FacilityInstructor`: requires `FacilityRole.Instructor`.
  - `FacilityMember`: requires `FacilityRole.Member`.
  - `FacilityAdmin`: requires role `SD.FacilitySuperAdmin` or `SD.FacilityLeader`.

- **Custom Authorization Handler**:
  - `FacilityAuthorizationHandler`:
    - Evaluates whether the current user has the required facility role for the resource being accessed (based on claims/DB).

---

### 5.3 Courses & Content (Courses, Sections, Lessons)

- **Courses**:
  - CRUD for courses linked to:
    - Facility.
    - Category.
  - Core fields: title, description, cover image, price, level, duration, etc.

- **Course Batches (CourseBatches)**:
  - Represent cohorts or scheduled runs of a course.
  - Fields: start/end dates, capacity, batch‑specific pricing, etc.

- **Sections & Lessons**:
  - `Sections`: logical grouping of lessons in a course.
  - `Lessons`:
    - Attached to a section.
    - Support:
      - **Recorded lessons (LessonRec)**: Bunny video ID/URL.
      - **Live lessons (LessonLive)**: Jitsi room/link, scheduled times.

---

### 5.4 Tasks & Submissions

- **Tasks (Assignments)**:
  - Created by instructors and linked to:
    - Course or specific lesson.
  - Fields: title, description, due date, optional attachments/links.

- **Task Submissions**:
  - Learner submits solution (file, text, link, etc.).
  - States: e.g., `Submitted`, `Reviewed`, `Graded`.
  - Instructors can review, grade, and add feedback.

---

### 5.5 Cart & Payments

- **Cart**:
  - Add/remove courses from user’s cart.
  - Persist cart for each authenticated user.

- **Payments (Stripe)**:
  - Create Stripe sessions for checkout.
  - Support multiple prices/currencies (to be specified in configuration).
  - On payment success:
    - Record transaction details.
    - Grant user access/enrolment to the purchased course/batch.

- **Coupons**:
  - Input: coupon code.
  - Validate:
    - Active period.
    - Usage limits.
    - Discount type/value.
  - Apply discount to cart total.

---

### 5.6 Email & Notifications

- **Email Sending (`IEmailSender`)**:
  - Used for:
    - Email confirmation.
    - Forgot password/OTP.
  - Uses HTML templates in `wwwroot/Templates`.
  - Extensible for:
    - Enrollment confirmations.
    - Session reminders.
    - Other system notifications.

---

### 5.7 External Integrations

- **Bunny (Video CDN)**:
  - Store and stream recorded lessons.
  - Keep Bunny video identifiers in lesson entities.

- **Jitsi (Live Sessions)**:
  - Manage live rooms/meetings for `LessonLive`.
  - Store room info/links per live lesson.

- **Stripe**:
  - Payment processing for courses.
  - Secure checkout with tokens/sessions handled on Stripe side.

- **Google Auth**:
  - OAuth2 login with Google as external provider.
  - Onboard new users and link Google accounts to existing users.

---

## 6. Non‑Functional Requirements

- **Performance**:
  - ≤ 500ms response time for 90% of API calls under normal load.

- **Security**:
  - JWT‑secured endpoints for all protected operations.
  - Fine‑grained authorization via policies and handlers.
  - Lockout policies to mitigate brute force login attempts.
  - No user enumeration in auth error messages.

- **Scalability**:
  - Layered architecture (Controllers → Application Services → Repositories).
  - Multi‑facility design.
  - Database resilience using `EnableRetryOnFailure`.

- **Testability**:
  - Controllers depend on interfaces (UserManager, SignInManager, repositories, services, etc.) → easily mockable.
  - Unit test coverage for critical flows (Auth, OTP, Payments, Facility roles).

- **Reliability & Logging**:
  - Structured logging using `ILogger`.
  - Clear error handling and HTTP status codes.
  - DB initialization via `IDBInitializer` on startup.

---

## 7. Key Success Metrics (KPIs)

- Registration funnel completion rate (visit → registered → email confirmed).
- Login success rate vs. login error/lockout rate.
- Payment success rate on first attempt.
- Active courses and active learners per facility.
- Average API response time and error rate.

---

## 8. High‑Level Roadmap

1. **Phase 1 – Core Platform (Mostly Implemented)**  
   - User registration, login, email confirmation, reset password.  
   - Core entities for facilities, courses, sections, lessons, tasks.  
   - Initial integrations: Stripe, Bunny, Jitsi.

2. **Phase 2 – Hardening & UX Improvement**  
   - Refactor service layer (cleaner separation, smaller controllers).  
   - Extend unit tests to cover more auth and learning flows.  
   - Improve error messages and validation responses.

3. **Phase 3 – Analytics & Admin Experience**  
   - Admin dashboards and basic reporting (enrollments, revenue, activity).  
   - More detailed logging and monitoring.  

If you want, I can now turn this PRD into a **set of concrete user stories / tickets** (with acceptance criteria) ready to put into a backlog (Jira/Trello, etc.).