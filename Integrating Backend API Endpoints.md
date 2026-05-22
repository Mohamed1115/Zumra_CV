# Chat Conversation

Note: _This is purely the output of the chat conversation and does not contain any raw data, codebase snippets, etc. used to generate the output._

### User Input

@[/home/mohamed/Code/Front/finaly-project2-main/Zumra_V1.pdf] @[/home/mohamed/Code/Front/finaly-project2-main/v1.json] دي ال endpoint بتعت الباك عايز اخلي مشروع الفرونت ده يشتغل عليهم   و يتوصل صح بيهم

*Listed directory [finaly-project2-main](file:///home/mohamed/Code/Front/finaly-project2-main) *

*Viewed [package.json](file:///home/mohamed/Code/Front/finaly-project2-main/package.json) *

*User accepted the command `head -n 50 /home/mohamed/Code/Front/finaly-project2-main/v1.json`*

*Checked command status*

*Listed directory [src](file:///home/mohamed/Code/Front/finaly-project2-main/src) *

*Listed directory [services](file:///home/mohamed/Code/Front/finaly-project2-main/src/services) *

*Listed directory [viewpages](file:///home/mohamed/Code/Front/finaly-project2-main/src/viewpages) *

*Listed directory [Loginpage](file:///home/mohamed/Code/Front/finaly-project2-main/src/Loginpage) *

*Viewed [App.jsx](file:///home/mohamed/Code/Front/finaly-project2-main/src/App.jsx) *

*Viewed [Api.js](file:///home/mohamed/Code/Front/finaly-project2-main/src/services/Api.js) *

*Viewed [Login.jsx](file:///home/mohamed/Code/Front/finaly-project2-main/src/Loginpage/Login.jsx) *

*Grep searched codebase*

*User accepted the command `python3 -c "import json; data = json.load(open('v1.json')); print('\n'.join(['%s: %s' % (k, ','.join(v.keys())) for k, v in data.get('paths', {}).items()]))"`*

*Viewed [HomePage.jsx](file:///home/mohamed/Code/Front/finaly-project2-main/src/viewpages/HomePage.jsx) *

*Viewed [CoursePage.jsx](file:///home/mohamed/Code/Front/finaly-project2-main/src/viewpages/CoursePage.jsx) *

*Edited relevant file*

### Planner Response

I have reviewed the `v1.json` file which contains the Swagger/OpenAPI specifications for your backend, and I've checked the current structure of the frontend app, specifically how `src/services/Api.js`, `HomePage.jsx`, and `CoursePage.jsx` are currently utilizing mock data and configuration.

I've created an [Implementation Plan](file:///home/mohamed/.gemini/antigravity/brain/65f46692-83cb-4d5c-a0be-aa73762ea894/implementation_plan.md) to outline the systematic approach required to fully connect the frontend to the backend REST APIs. Please review the plan in the artifacts panel to the right. 

Before we proceed with the execution, please let me know:
1. **Would you like me to connect all pages immediately, or should we start incrementally** (e.g., configuring `Api.js`, enabling and verifying real Authentication, and then moving to the `HomePage.jsx`)?
2. **Is your backend currently running on `http://localhost:5000`** so that we can test these changes live when I make them? 

Let me know what you think, and once you approve, I'll start executing the plan!

### User Input

ابدا جزئية جزئية علشان متتلغبطش

*Edited relevant file*

*Edited relevant file*

*Edited relevant file*

*Edited relevant file*

*Edited relevant file*

*Edited relevant file*

*Edited relevant file*

### Planner Response

تمام جداً، أنا نفذت التعديلات خطوة خطوة زي ما اتفقنا:

1. **تحديث الـ API Service (`src/services/Api.js`)**:
   - قفلت الـ `MOCK_MODE` (خليته `false`). وبكده كل الـ endpoints بتاعة تسجيل الدخول والتسجيل (`/Auth/Account/Login`, `/Auth/Account/Register`) بقوا شغالين على الباك إند الحقيقي.
   - ضفت دوال جديدة بتكلم المسميات اللي في الـ Swagger file (`v1.json`) وهي: `getAllCourses`, `getCourseById`, `getAllFacilities`,  `getFacilityById`. ده كله باستخدام المسار الحقيقي `http://localhost:5000`.

2. **توصيل الصفحة الرئيسية (`HomePage.jsx`)**:
   - شلت البيانات الوهمية (Mock data) للـ Popular Courses والـ Facilities.
   - خليت الصفحة تسحبهم مباشرة من الـ Backend باستخدام الـ APIs اللي عملناها، مع إضافة علامات التحميل (Loading state) عشان تظهر شكل احترافي لحد ما الداتا ترجع من السيرفر.

3. **توصيل صفحة الكورس (`CoursePage.jsx`)**:
   - خليت الصفحة تسحب بيانات الكورس الفعلي باستخدام الـ ID بتاعه من الرابط (`useParams`).
   - دمجت البيانات اللي جاية من الباك إند مع بعض الخصائص الوهمية القديمة (زي معلومات الـ Instructor الثابتة أو الفيديوهات) عشان الـ UI ميبُظش لو الباك إند لسه مرجعش التفاصيل دي كلها في الـ Endpoint ده.

**الخطوة الجاية ليك:**
شغل مشروع الـ Backend بتاعك بحيث يكون شغال على `http://localhost:5000` وافتح مشروع الـ React وجرب تعمل تسجيل دخول وتشوف كورسات الصفحة الرئيسية. لو حابب نوصل أي صفحة كمان (زي Facility Profile أو All Courses) عرفني!

### User Input

بص عايزك تلغيها وتعملها حاجة احسن و مودرن

*User accepted the command `npm install react-hot-toast`*

*Grep searched codebase*

*Viewed [Signup.jsx](file:///home/mohamed/Code/Front/finaly-project2-main/src/Loginpage/Signup.jsx) *

*Viewed [OTP.jsx](file:///home/mohamed/Code/Front/finaly-project2-main/src/Loginpage/OTP.jsx) *

*Viewed [Forgetpassword.jsx](file:///home/mohamed/Code/Front/finaly-project2-main/src/Loginpage/Forgetpassword.jsx) *

*Viewed [Resetpassword.jsx](file:///home/mohamed/Code/Front/finaly-project2-main/src/Loginpage/Resetpassword.jsx) *

*Viewed [Confirm.jsx](file:///home/mohamed/Code/Front/finaly-project2-main/src/Loginpage/Confirm.jsx) *

*Viewed [Resendemail.jsx](file:///home/mohamed/Code/Front/finaly-project2-main/src/Loginpage/Resendemail.jsx) *

*User accepted the command `node -e "
const fs = require('fs');
const path = require('path');

const dir = './src/Loginpage';
const files = fs.readdirSync(dir).filter(f => f.endsWith('.jsx'));

for (const file of files) {
  const filePath = path.join(dir, file);
  let content = fs.readFileSync(filePath, 'utf8');
  if (content.includes('alert(')) {
     if (!content.includes('from \\'react-hot-toast\\'') && !content.includes('from \\"react-hot-toast\\"')) {
        content = 'import toast from \\"react-hot-toast\\";\\n' + content;
     }

     content = content.replace(/alert\\(\"Logged in successfully(.*)\"\\)/g, 'toast.success(\"Logged in successfully$1\")');
     content = content.replace(/alert\\(\"تم إنشاء الحساب بنجاح!(.*)\"\\)/g, 'toast.success(\"تم إنشاء الحساب بنجاح!$1\")');
     content = content.replace(/alert\\(\"تم التحقق من OTP بنجاح(.*)\"\\)/g, 'toast.success(\"تم التحقق من OTP بنجاح$1\")');
     content = content.replace(/alert\\(\"تم إرسال رابط إعادة تعيين(.*)\"\\)/g, 'toast.success(\"تم إرسال رابط إعادة تعيين$1\")');
     content = content.replace(/alert\\(\"تم تغيير كلمة المرور(.*)\"\\)/g, 'toast.success(\"تم تغيير كلمة المرور$1\")');
     content = content.replace(/alert\\(\"تم تأكيد الحساب بنجاح(.*)\"\\)/g, 'toast.success(\"تم تأكيد الحساب بنجاح$1\")');
     content = content.replace(/alert\\(\"تم إرسال بريد تأكيد جديد(.*)\"\\)/g, 'toast.success(\"تم إرسال بريد تأكيد جديد$1\")');
     
     content = content.replace(/alert\\(/g, 'toast.error(');
     
     fs.writeFileSync(filePath, content, 'utf8');
     console.log('Updated ' + file);
  }
}
"`*

*Edited relevant file*

*Checked command status*

*Edited relevant file*

### User Input

انت هنجت كده حاول تاني

*Edited relevant file*

### User Input

حاول تاني

### Planner Response

هخلي كل ملف يتعمل منفصل بدون scripts. خليني أبدأ:

*Edited relevant file*

*Edited relevant file*

*Edited relevant file*

*Edited relevant file*

*Edited relevant file*

*Edited relevant file*

*Edited relevant file*

### Planner Response

تمام! ✅ خلصنا كل الملفات بنجاح. إليك ما تم فعله:

## ما تم تنفيذه

استبدلت جميع `alert()` الـ browser القبيحة في **6 صفحات** بـ `toast` notifications أحدث وأجمل:

| الملف | الحالة |
|---|---|
| `Login.jsx` | ✅ تم |
| `Signup.jsx` | ✅ تم |
| `OTP.jsx` | ✅ تم |
| `Forgetpassword.jsx` | ✅ تم |
| `Resetpassword.jsx` | ✅ تم |
| `Confirm.jsx` | ✅ تم |
| `Resendemail.jsx` | ✅ تم |

الآن:
- ✅ الرسائل الناجحة (زي "Logged in successfully") بتظهر كـ **toast أخضر** في أعلى الشاشة
- ❌ رسائل الخطأ بتظهر كـ **toast أحمر**
- في `App.jsx` ضفنا `<Toaster position="top-center" />` يشتغل لكل الصفحات

جرب تعمل Login دلوقتي هتشوف الفرق!

### User Input

/usr/bin/npm run dev

> my-react-app@0.0.0 dev
> vite

node:internal/errors:496
    ErrorCaptureStackTrace(err);
    ^

Error: EMFILE: too many open files, watch '/home/mohamed/Code/Front/finaly-project2-main/vite.config.js'
    at FSWatcher.<computed> (node:internal/fs/watchers:247:19)
    at Object.watch (node:fs:2418:34)
    at createFsWatchInstance (file:///home/mohamed/Code/Front/finaly-project2-main/node_modules/vite/dist/node/chunks/dep-827b23df.js:52160:17)
    at setFsWatchListener (file:///home/mohamed/Code/Front/finaly-project2-main/node_modules/vite/dist/node/chunks/dep-827b23df.js:52207:15)
    at NodeFsHandler._watchWithNodeFs (file:///home/mohamed/Code/Front/finaly-project2-main/node_modules/vite/dist/node/chunks/dep-827b23df.js:52362:14)
    at NodeFsHandler._handleFile (file:///home/mohamed/Code/Front/finaly-project2-main/node_modules/vite/dist/node/chunks/dep-827b23df.js:52426:23)
    at NodeFsHandler._addToNodeFs (file:///home/mohamed/Code/Front/finaly-project2-main/node_modules/vite/dist/node/chunks/dep-827b23df.js:52668:21)
    at async file:///home/mohamed/Code/Front/finaly-project2-main/node_modules/vite/dist/node/chunks/dep-827b23df.js:53663:21
    at async Promise.all (index 1)
Emitted 'error' event on FSWatcher instance at:
    at FSWatcher._handleError (file:///home/mohamed/Code/Front/finaly-project2-main/node_modules/vite/dist/node/chunks/dep-827b23df.js:53859:10)
    at NodeFsHandler._addToNodeFs (file:///home/mohamed/Code/Front/finaly-project2-main/node_modules/vite/dist/node/chunks/dep-827b23df.js:52676:18)
    at async file:///home/mohamed/Code/Front/finaly-project2-main/node_modules/vite/dist/node/chunks/dep-827b23df.js:53663:21
    at async Promise.all (index 1) {
  errno: -24,
  syscall: 'watch',
  code: 'EMFILE',
  path: '/home/mohamed/Code/Front/finaly-project2-main/vite.config.js',
  filename: '/home/mohamed/Code/Front/finaly-project2-main/vite.config.js'
}

Node.js v18.19.1

Process finished with exit code 1



### Planner Response

ده مش error في الكود - ده Linux system limit مش كافي للـ file watchers. نحله بأمر واحد:

*User accepted the command `echo fs.inotify.max_user_watches=524288 | sudo tee -a /etc/sysctl.conf && sudo sysctl -p && ulimit -n 65536`*

### Planner Response

محتاج تكتب الـ password بتاع الـ sudo، ادخله في الـ terminal. أو جرب الأسهل:

*User accepted the command `ulimit -n 65536 && npm run dev`*

### Planner Response

المشكلة أعمق. الـ system محتاج تغيير الـ `inotify` limit. المحادثة مازالت شغالة من قبل كمان — اللي بيسبب المشكلة هو إن في عمليات كتير فاضية شاغلة الـ file handles. 

المشكلة مش هنقدر نحلها بدون password. **اعمل الخطوات دي يدوياً في الـ terminal:**

```bash
# الخطوة 1: اكتب الـ password بتاعك
sudo sysctl -w fs.inotify.max_user_watches=524288
sudo sysctl -w fs.inotify.max_user_instances=512

# الخطوة 2: بعد كده شغل الـ dev server
npm run dev
```

أو لو مش عايز تكتب password، جرب:

```bash
# أقفل كل عمليات node الموجودة الأول
pkill -f node
# بعدين
npm run dev
```

---

**سبب المشكلة:** الـ Linux بيحد عدد الـ files اللي ممكن تتراقب في نفس الوقت. لازم نرفع الـ limit من الـ system settings عشان Vite يقدر يشتغل.

### User Input

شوف كده المشاكل دي

### Planner Response

في مشكلتين واضحين في الصورة وكلهم **من الـ Backend مش الـ Frontend:**

---

## 🔴 المشكلة 1: CORS Error

```
Access to fetch at 'http://localhost:5000/Api/Course' from origin 
'http://localhost:5173' has been blocked by CORS policy: 
No 'Access-Control-Allow-Origin' header is present
```

**السبب:** الـ Backend مش مسموحله يرد على requests من `localhost:5173` (الـ Frontend). 

**الحل في الـ Backend** (في `Program.cs`):
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// وبعدين
app.UseCors("AllowFrontend");
```

---

## 🔴 المشكلة 2: 500 على Register

```
Failed to load resource: the server responded with a status of 500
```

**السبب:** Server-side bug — غالباً مشكلة في الـ database أو validation في الـ Backend.

**الحل:** افتح logs الـ Backend وشوف الـ exception الحقيقي.

---

## ✅ حل مؤقت للـ Frontend

لحد ما تحل الـ CORS من الـ Backend، ممكن تضيف proxy في الـ Vite config:

*Viewed [vite.config.js](file:///home/mohamed/Code/Front/finaly-project2-main/vite.config.js) *

*Edited relevant file*

*Edited relevant file*

### Planner Response

تمام! خليني أوضح إيه اللي عملته:

## التعديلات

### 1. `vite.config.js` - ضفت proxy لكل الـ API routes
الآن كل request من الـ Frontend بيتوجه أوتوماتيك للـ Backend:
- `/Auth/*` → `http://localhost:5000/Auth/*`
- `/Api/*` → `http://localhost:5000/Api/*`
- `/api/*` → `http://localhost:5000/api/*`

هكذا CORS مش هيظهر خالص لأن الـ Proxy بيعمل الـ request من نفس الـ server.

### 2. `Api.js` - الـ BASE_URL بقت فاضية في الـ dev
بدل ما الـ Frontend يكلم `localhost:5000` مباشرة (وييجي CORS)، دلوقتي بيكلم نفسه (`localhost:5173`) والـ Vite proxy بيعمل الميزان.

---

## ⚠️ مشكلة الـ 500 على Register
دي مشكلة **Backend** مش عندي فيها حل من الـ Frontend. افتح logs الـ Backend وشوف الـ exception لأنها غالباً:
- مشكلة في الـ Database connection
- Validation error في الـ model

جرب شغل الـ dev server تاني وشوف لو CORS اتحل.

### User Input

2026-04-23 20:26:03.864 | warn: Microsoft.AspNetCore.DataProtection.Repositories.FileSystemXmlRepository[60]
2026-04-23 20:26:03.864 |       Storing keys in a directory '/app/.aspnet/DataProtection-Keys' that may not be persisted outside of the container. Protected data will be unavailable when container is destroyed. For more information go to https://aka.ms/aspnet/dataprotectionwarning
2026-04-23 20:26:04.723 | warn: Microsoft.EntityFrameworkCore.Model.Validation[30000]
2026-04-23 20:26:04.723 |       No store type was specified for the decimal property 'Amount' on entity type 'PayFac'. This will cause values to be silently truncated if they do not fit in the default precision and scale. Explicitly specify the SQL server column type that can accommodate all the values in 'OnModelCreating' using 'HasColumnType', specify precision and scale using 'HasPrecision', or configure a value converter using 'HasConversion'.
2026-04-23 20:26:06.658 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:06.658 |       Executed DbCommand (541ms) [Parameters=[], CommandType='Text', CommandTimeout='60']
2026-04-23 20:26:06.658 |       CREATE DATABASE [ZumraDB];
2026-04-23 20:26:06.991 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:06.991 |       Executed DbCommand (330ms) [Parameters=[], CommandType='Text', CommandTimeout='60']
2026-04-23 20:26:06.991 |       IF SERVERPROPERTY('EngineEdition') <> 5
2026-04-23 20:26:06.991 |       BEGIN
2026-04-23 20:26:06.991 |           ALTER DATABASE [ZumraDB] SET READ_COMMITTED_SNAPSHOT ON;
2026-04-23 20:26:06.991 |       END;
2026-04-23 20:26:07.039 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.039 |       Executed DbCommand (11ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.039 |       SELECT 1
2026-04-23 20:26:07.046 | info: Microsoft.EntityFrameworkCore.Migrations[20411]
2026-04-23 20:26:07.046 |       Acquiring an exclusive lock for migration application. See https://aka.ms/efcore-docs-migrations-lock for more information if this takes too long.
2026-04-23 20:26:07.091 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.091 |       Executed DbCommand (40ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.091 |       DECLARE @result int;
2026-04-23 20:26:07.091 |       EXEC @result = sp_getapplock @Resource = '__EFMigrationsLock', @LockOwner = 'Session', @LockMode = 'Exclusive';
2026-04-23 20:26:07.091 |       SELECT @result
2026-04-23 20:26:07.209 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.209 |       Executed DbCommand (18ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.209 |       IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
2026-04-23 20:26:07.209 |       BEGIN
2026-04-23 20:26:07.209 |           CREATE TABLE [__EFMigrationsHistory] (
2026-04-23 20:26:07.209 |               [MigrationId] nvarchar(150) NOT NULL,
2026-04-23 20:26:07.209 |               [ProductVersion] nvarchar(32) NOT NULL,
2026-04-23 20:26:07.209 |               CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
2026-04-23 20:26:07.209 |           );
2026-04-23 20:26:07.209 |       END;
2026-04-23 20:26:07.217 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.217 |       Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.217 |       SELECT 1
2026-04-23 20:26:07.218 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.218 |       Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.218 |       SELECT OBJECT_ID(N'[__EFMigrationsHistory]');
2026-04-23 20:26:07.242 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.242 |       Executed DbCommand (22ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.242 |       SELECT [MigrationId], [ProductVersion]
2026-04-23 20:26:07.242 |       FROM [__EFMigrationsHistory]
2026-04-23 20:26:07.242 |       ORDER BY [MigrationId];
2026-04-23 20:26:07.255 | info: Microsoft.EntityFrameworkCore.Migrations[20402]
2026-04-23 20:26:07.255 |       Applying migration '20260423180929_InitialSqlServer'.
2026-04-23 20:26:07.389 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.389 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.389 |       CREATE TABLE [AspNetRoles] (
2026-04-23 20:26:07.389 |           [Id] nvarchar(450) NOT NULL,
2026-04-23 20:26:07.389 |           [Name] nvarchar(256) NULL,
2026-04-23 20:26:07.389 |           [NormalizedName] nvarchar(256) NULL,
2026-04-23 20:26:07.389 |           [ConcurrencyStamp] nvarchar(max) NULL,
2026-04-23 20:26:07.389 |           CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
2026-04-23 20:26:07.389 |       );
2026-04-23 20:26:07.391 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.391 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.391 |       CREATE TABLE [Categories] (
2026-04-23 20:26:07.391 |           [Id] int NOT NULL IDENTITY,
2026-04-23 20:26:07.391 |           [Name] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.391 |           [Description] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.391 |           [ImageZone] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.391 |           [ImagePath] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.391 |           [ImageName] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.391 |           [ImageUrl] nvarchar(max) NULL,
2026-04-23 20:26:07.391 |           CONSTRAINT [PK_Categories] PRIMARY KEY ([Id])
2026-04-23 20:26:07.391 |       );
2026-04-23 20:26:07.393 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.393 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.393 |       CREATE TABLE [Coupons] (
2026-04-23 20:26:07.393 |           [Id] int NOT NULL IDENTITY,
2026-04-23 20:26:07.393 |           [Code] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.393 |           [DiscountAmount] int NOT NULL,
2026-04-23 20:26:07.393 |           [ExpiryDate] datetime2 NOT NULL,
2026-04-23 20:26:07.393 |           [IsActive] bit NOT NULL,
2026-04-23 20:26:07.393 |           CONSTRAINT [PK_Coupons] PRIMARY KEY ([Id])
2026-04-23 20:26:07.393 |       );
2026-04-23 20:26:07.396 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.396 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.396 |       CREATE TABLE [LessonLives] (
2026-04-23 20:26:07.396 |           [Id] int NOT NULL IDENTITY,
2026-04-23 20:26:07.396 |           [StartTime] datetime2 NOT NULL,
2026-04-23 20:26:07.396 |           [EndTime] datetime2 NOT NULL,
2026-04-23 20:26:07.396 |           [MeetingUrl] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.396 |           [RoomName] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.396 |           CONSTRAINT [PK_LessonLives] PRIMARY KEY ([Id])
2026-04-23 20:26:07.396 |       );
2026-04-23 20:26:07.398 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.398 |       Executed DbCommand (3ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.398 |       CREATE TABLE [LessonRecs] (
2026-04-23 20:26:07.398 |           [Id] int NOT NULL IDENTITY,
2026-04-23 20:26:07.398 |           [VideoUrl] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.398 |           [Duration] real NOT NULL,
2026-04-23 20:26:07.398 |           [VideoSize] bigint NULL,
2026-04-23 20:26:07.398 |           [VideoFormat] nvarchar(max) NULL,
2026-04-23 20:26:07.398 |           [VideoQuality] nvarchar(max) NULL,
2026-04-23 20:26:07.398 |           [UploadedAt] datetime2 NULL,
2026-04-23 20:26:07.398 |           [IsProcessed] bit NOT NULL,
2026-04-23 20:26:07.398 |           CONSTRAINT [PK_LessonRecs] PRIMARY KEY ([Id])
2026-04-23 20:26:07.398 |       );
2026-04-23 20:26:07.402 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.402 |       Executed DbCommand (3ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.402 |       CREATE TABLE [Otps] (
2026-04-23 20:26:07.402 |           [Id] int NOT NULL IDENTITY,
2026-04-23 20:26:07.402 |           [Email] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.402 |           [OtpCode] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.402 |           [Expiration] datetime2 NOT NULL,
2026-04-23 20:26:07.402 |           [IsUsed] bit NOT NULL,
2026-04-23 20:26:07.402 |           CONSTRAINT [PK_Otps] PRIMARY KEY ([Id])
2026-04-23 20:26:07.402 |       );
2026-04-23 20:26:07.404 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.405 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.405 |       CREATE TABLE [UserImages] (
2026-04-23 20:26:07.405 |           [Id] int NOT NULL IDENTITY,
2026-04-23 20:26:07.405 |           [ImageZone] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.405 |           [ImagePath] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.405 |           [ImageName] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.405 |           CONSTRAINT [PK_UserImages] PRIMARY KEY ([Id])
2026-04-23 20:26:07.405 |       );
2026-04-23 20:26:07.409 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.409 |       Executed DbCommand (4ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.409 |       CREATE TABLE [AspNetRoleClaims] (
2026-04-23 20:26:07.409 |           [Id] int NOT NULL IDENTITY,
2026-04-23 20:26:07.409 |           [RoleId] nvarchar(450) NOT NULL,
2026-04-23 20:26:07.409 |           [ClaimType] nvarchar(max) NULL,
2026-04-23 20:26:07.409 |           [ClaimValue] nvarchar(max) NULL,
2026-04-23 20:26:07.409 |           CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
2026-04-23 20:26:07.409 |           CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
2026-04-23 20:26:07.409 |       );
2026-04-23 20:26:07.413 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.413 |       Executed DbCommand (4ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.413 |       CREATE TABLE [Facilities] (
2026-04-23 20:26:07.413 |           [Id] int NOT NULL IDENTITY,
2026-04-23 20:26:07.413 |           [Name] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.413 |           [Description] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.413 |           [Type] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.413 |           [ImageZone] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.413 |           [ImagePath] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.413 |           [ImageName] nvarchar(max) NULL,
2026-04-23 20:26:07.413 |           [ImageUrl] nvarchar(max) NULL,
2026-04-23 20:26:07.413 |           [Status] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.413 |           [CategoryId] int NOT NULL,
2026-04-23 20:26:07.413 |           CONSTRAINT [PK_Facilities] PRIMARY KEY ([Id]),
2026-04-23 20:26:07.413 |           CONSTRAINT [FK_Facilities_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id]) ON DELETE CASCADE
2026-04-23 20:26:07.413 |       );
2026-04-23 20:26:07.417 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.417 |       Executed DbCommand (4ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.417 |       CREATE TABLE [AspNetUsers] (
2026-04-23 20:26:07.417 |           [Id] nvarchar(450) NOT NULL,
2026-04-23 20:26:07.417 |           [Name] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.417 |           [TotalCarts] int NOT NULL,
2026-04-23 20:26:07.417 |           [ImageId] int NULL,
2026-04-23 20:26:07.417 |           [UserName] nvarchar(256) NULL,
2026-04-23 20:26:07.417 |           [NormalizedUserName] nvarchar(256) NULL,
2026-04-23 20:26:07.417 |           [Email] nvarchar(256) NULL,
2026-04-23 20:26:07.417 |           [NormalizedEmail] nvarchar(256) NULL,
2026-04-23 20:26:07.417 |           [EmailConfirmed] bit NOT NULL,
2026-04-23 20:26:07.417 |           [PasswordHash] nvarchar(max) NULL,
2026-04-23 20:26:07.417 |           [SecurityStamp] nvarchar(max) NULL,
2026-04-23 20:26:07.417 |           [ConcurrencyStamp] nvarchar(max) NULL,
2026-04-23 20:26:07.417 |           [PhoneNumber] nvarchar(max) NULL,
2026-04-23 20:26:07.417 |           [PhoneNumberConfirmed] bit NOT NULL,
2026-04-23 20:26:07.417 |           [TwoFactorEnabled] bit NOT NULL,
2026-04-23 20:26:07.417 |           [LockoutEnd] datetimeoffset NULL,
2026-04-23 20:26:07.417 |           [LockoutEnabled] bit NOT NULL,
2026-04-23 20:26:07.417 |           [AccessFailedCount] int NOT NULL,
2026-04-23 20:26:07.417 |           CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id]),
2026-04-23 20:26:07.417 |           CONSTRAINT [FK_AspNetUsers_UserImages_ImageId] FOREIGN KEY ([ImageId]) REFERENCES [UserImages] ([Id])
2026-04-23 20:26:07.417 |       );
2026-04-23 20:26:07.420 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.420 |       Executed DbCommand (3ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.420 |       CREATE TABLE [Groups] (
2026-04-23 20:26:07.420 |           [Id] int NOT NULL IDENTITY,
2026-04-23 20:26:07.420 |           [Name] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.420 |           [Description] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.420 |           [FacilityId] int NOT NULL,
2026-04-23 20:26:07.420 |           CONSTRAINT [PK_Groups] PRIMARY KEY ([Id]),
2026-04-23 20:26:07.420 |           CONSTRAINT [FK_Groups_Facilities_FacilityId] FOREIGN KEY ([FacilityId]) REFERENCES [Facilities] ([Id]) ON DELETE CASCADE
2026-04-23 20:26:07.420 |       );
2026-04-23 20:26:07.424 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.424 |       Executed DbCommand (4ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.424 |       CREATE TABLE [AspNetUserClaims] (
2026-04-23 20:26:07.424 |           [Id] int NOT NULL IDENTITY,
2026-04-23 20:26:07.424 |           [UserId] nvarchar(450) NOT NULL,
2026-04-23 20:26:07.424 |           [ClaimType] nvarchar(max) NULL,
2026-04-23 20:26:07.424 |           [ClaimValue] nvarchar(max) NULL,
2026-04-23 20:26:07.424 |           CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
2026-04-23 20:26:07.424 |           CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
2026-04-23 20:26:07.424 |       );
2026-04-23 20:26:07.428 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.428 |       Executed DbCommand (4ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.428 |       CREATE TABLE [AspNetUserLogins] (
2026-04-23 20:26:07.429 |           [LoginProvider] nvarchar(450) NOT NULL,
2026-04-23 20:26:07.429 |           [ProviderKey] nvarchar(450) NOT NULL,
2026-04-23 20:26:07.429 |           [ProviderDisplayName] nvarchar(max) NULL,
2026-04-23 20:26:07.429 |           [UserId] nvarchar(450) NOT NULL,
2026-04-23 20:26:07.429 |           CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
2026-04-23 20:26:07.429 |           CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
2026-04-23 20:26:07.429 |       );
2026-04-23 20:26:07.434 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.434 |       Executed DbCommand (5ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.434 |       CREATE TABLE [AspNetUserRoles] (
2026-04-23 20:26:07.434 |           [UserId] nvarchar(450) NOT NULL,
2026-04-23 20:26:07.434 |           [RoleId] nvarchar(450) NOT NULL,
2026-04-23 20:26:07.434 |           CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
2026-04-23 20:26:07.434 |           CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
2026-04-23 20:26:07.434 |           CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
2026-04-23 20:26:07.434 |       );
2026-04-23 20:26:07.437 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.437 |       Executed DbCommand (3ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.437 |       CREATE TABLE [AspNetUserTokens] (
2026-04-23 20:26:07.437 |           [UserId] nvarchar(450) NOT NULL,
2026-04-23 20:26:07.437 |           [LoginProvider] nvarchar(450) NOT NULL,
2026-04-23 20:26:07.437 |           [Name] nvarchar(450) NOT NULL,
2026-04-23 20:26:07.437 |           [Value] nvarchar(max) NULL,
2026-04-23 20:26:07.437 |           CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
2026-04-23 20:26:07.437 |           CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
2026-04-23 20:26:07.437 |       );
2026-04-23 20:26:07.441 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.441 |       Executed DbCommand (3ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.441 |       CREATE TABLE [Carts] (
2026-04-23 20:26:07.441 |           [Id] int NOT NULL IDENTITY,
2026-04-23 20:26:07.441 |           [UserId] nvarchar(450) NOT NULL,
2026-04-23 20:26:07.441 |           [BookId] int NOT NULL,
2026-04-23 20:26:07.441 |           [Quantity] int NOT NULL,
2026-04-23 20:26:07.441 |           [TotalPrice] int NOT NULL,
2026-04-23 20:26:07.441 |           [CouponId] int NULL,
2026-04-23 20:26:07.441 |           CONSTRAINT [PK_Carts] PRIMARY KEY ([Id]),
2026-04-23 20:26:07.441 |           CONSTRAINT [FK_Carts_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
2026-04-23 20:26:07.441 |           CONSTRAINT [FK_Carts_Coupons_CouponId] FOREIGN KEY ([CouponId]) REFERENCES [Coupons] ([Id])
2026-04-23 20:26:07.441 |       );
2026-04-23 20:26:07.445 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.445 |       Executed DbCommand (4ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.445 |       CREATE TABLE [PayFacs] (
2026-04-23 20:26:07.445 |           [Id] int NOT NULL IDENTITY,
2026-04-23 20:26:07.445 |           [UserId] nvarchar(450) NOT NULL,
2026-04-23 20:26:07.445 |           [FacilityId] int NOT NULL,
2026-04-23 20:26:07.445 |           [status] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.445 |           [PaymentDate] datetime2 NOT NULL,
2026-04-23 20:26:07.445 |           [StripeSessionId] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.445 |           [Amount] decimal(18,2) NOT NULL,
2026-04-23 20:26:07.445 |           CONSTRAINT [PK_PayFacs] PRIMARY KEY ([Id]),
2026-04-23 20:26:07.445 |           CONSTRAINT [FK_PayFacs_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
2026-04-23 20:26:07.445 |           CONSTRAINT [FK_PayFacs_Facilities_FacilityId] FOREIGN KEY ([FacilityId]) REFERENCES [Facilities] ([Id]) ON DELETE CASCADE
2026-04-23 20:26:07.445 |       );
2026-04-23 20:26:07.449 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.449 |       Executed DbCommand (4ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.449 |       CREATE TABLE [UserFacilities] (
2026-04-23 20:26:07.449 |           [UserId] nvarchar(450) NOT NULL,
2026-04-23 20:26:07.449 |           [FacilityId] int NOT NULL,
2026-04-23 20:26:07.449 |           [Role] int NOT NULL,
2026-04-23 20:26:07.449 |           [CreatedAt] datetime2 NOT NULL,
2026-04-23 20:26:07.449 |           CONSTRAINT [PK_UserFacilities] PRIMARY KEY ([UserId], [FacilityId]),
2026-04-23 20:26:07.449 |           CONSTRAINT [FK_UserFacilities_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
2026-04-23 20:26:07.449 |           CONSTRAINT [FK_UserFacilities_Facilities_FacilityId] FOREIGN KEY ([FacilityId]) REFERENCES [Facilities] ([Id]) ON DELETE CASCADE
2026-04-23 20:26:07.449 |       );
2026-04-23 20:26:07.453 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.453 |       Executed DbCommand (4ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.453 |       CREATE TABLE [Courses] (
2026-04-23 20:26:07.453 |           [Id] int NOT NULL IDENTITY,
2026-04-23 20:26:07.453 |           [Name] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.453 |           [Description] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.453 |           [Cost] int NOT NULL,
2026-04-23 20:26:07.453 |           [Type] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.453 |           [CreatedAt] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.453 |           [ImageZone] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.453 |           [ImagePath] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.453 |           [ImageName] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.453 |           [ImageUrl] nvarchar(max) NULL,
2026-04-23 20:26:07.453 |           [GroupId] int NOT NULL,
2026-04-23 20:26:07.453 |           [FacilityId] int NOT NULL,
2026-04-23 20:26:07.453 |           CONSTRAINT [PK_Courses] PRIMARY KEY ([Id]),
2026-04-23 20:26:07.453 |           CONSTRAINT [FK_Courses_Facilities_FacilityId] FOREIGN KEY ([FacilityId]) REFERENCES [Facilities] ([Id]) ON DELETE NO ACTION,
2026-04-23 20:26:07.453 |           CONSTRAINT [FK_Courses_Groups_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [Groups] ([Id]) ON DELETE NO ACTION
2026-04-23 20:26:07.453 |       );
2026-04-23 20:26:07.458 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.458 |       Executed DbCommand (5ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.458 |       CREATE TABLE [CourseBatches] (
2026-04-23 20:26:07.458 |           [Id] int NOT NULL IDENTITY,
2026-04-23 20:26:07.458 |           [CourseId] int NOT NULL,
2026-04-23 20:26:07.458 |           [Title] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.458 |           [StartDate] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.458 |           [EndDate] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.458 |           [Capacity] int NULL,
2026-04-23 20:26:07.458 |           [Status] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.458 |           CONSTRAINT [PK_CourseBatches] PRIMARY KEY ([Id]),
2026-04-23 20:26:07.458 |           CONSTRAINT [FK_CourseBatches_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]) ON DELETE CASCADE
2026-04-23 20:26:07.458 |       );
2026-04-23 20:26:07.462 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.462 |       Executed DbCommand (4ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.462 |       CREATE TABLE [Favorites] (
2026-04-23 20:26:07.462 |           [UserId] nvarchar(450) NOT NULL,
2026-04-23 20:26:07.462 |           [CourseId] int NOT NULL,
2026-04-23 20:26:07.462 |           [CreatedAt] datetime2 NOT NULL,
2026-04-23 20:26:07.462 |           CONSTRAINT [PK_Favorites] PRIMARY KEY ([UserId], [CourseId]),
2026-04-23 20:26:07.462 |           CONSTRAINT [FK_Favorites_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
2026-04-23 20:26:07.462 |           CONSTRAINT [FK_Favorites_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]) ON DELETE CASCADE
2026-04-23 20:26:07.462 |       );
2026-04-23 20:26:07.466 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.466 |       Executed DbCommand (4ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.466 |       CREATE TABLE [Enrollments] (
2026-04-23 20:26:07.466 |           [Id] int NOT NULL IDENTITY,
2026-04-23 20:26:07.466 |           [UserId] nvarchar(450) NOT NULL,
2026-04-23 20:26:07.466 |           [CourseBatchId] int NOT NULL,
2026-04-23 20:26:07.466 |           [AccessType] nvarchar(max) NULL,
2026-04-23 20:26:07.466 |           [Status] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.466 |           [CreatedAt] datetime2 NULL,
2026-04-23 20:26:07.466 |           CONSTRAINT [PK_Enrollments] PRIMARY KEY ([Id]),
2026-04-23 20:26:07.466 |           CONSTRAINT [FK_Enrollments_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
2026-04-23 20:26:07.466 |           CONSTRAINT [FK_Enrollments_CourseBatches_CourseBatchId] FOREIGN KEY ([CourseBatchId]) REFERENCES [CourseBatches] ([Id]) ON DELETE CASCADE
2026-04-23 20:26:07.466 |       );
2026-04-23 20:26:07.471 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.471 |       Executed DbCommand (5ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.471 |       CREATE TABLE [Lessons] (
2026-04-23 20:26:07.471 |           [Id] int NOT NULL IDENTITY,
2026-04-23 20:26:07.471 |           [Name] nvarchar(200) NOT NULL,
2026-04-23 20:26:07.471 |           [Description] nvarchar(1000) NOT NULL,
2026-04-23 20:26:07.471 |           [Type] nvarchar(50) NOT NULL,
2026-04-23 20:26:07.471 |           [Order] int NOT NULL,
2026-04-23 20:26:07.471 |           [CreatedAt] datetime2 NOT NULL,
2026-04-23 20:26:07.471 |           [CourseId] int NOT NULL,
2026-04-23 20:26:07.471 |           [CourseBatchId] int NOT NULL,
2026-04-23 20:26:07.471 |           [CourseContentId] int NOT NULL,
2026-04-23 20:26:07.471 |           [MeetingId] int NULL,
2026-04-23 20:26:07.471 |           [VideoId] int NULL,
2026-04-23 20:26:07.471 |           CONSTRAINT [PK_Lessons] PRIMARY KEY ([Id]),
2026-04-23 20:26:07.471 |           CONSTRAINT [FK_Lessons_CourseBatches_CourseBatchId] FOREIGN KEY ([CourseBatchId]) REFERENCES [CourseBatches] ([Id]) ON DELETE NO ACTION,
2026-04-23 20:26:07.471 |           CONSTRAINT [FK_Lessons_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]) ON DELETE NO ACTION,
2026-04-23 20:26:07.471 |           CONSTRAINT [FK_Lessons_LessonLives_MeetingId] FOREIGN KEY ([MeetingId]) REFERENCES [LessonLives] ([Id]) ON DELETE SET NULL,
2026-04-23 20:26:07.471 |           CONSTRAINT [FK_Lessons_LessonRecs_VideoId] FOREIGN KEY ([VideoId]) REFERENCES [LessonRecs] ([Id]) ON DELETE SET NULL
2026-04-23 20:26:07.471 |       );
2026-04-23 20:26:07.474 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.474 |       Executed DbCommand (4ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.475 |       CREATE TABLE [Sections] (
2026-04-23 20:26:07.475 |           [Id] int NOT NULL IDENTITY,
2026-04-23 20:26:07.475 |           [Name] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.475 |           [Order] int NOT NULL,
2026-04-23 20:26:07.475 |           [CourseId] int NOT NULL,
2026-04-23 20:26:07.475 |           [CourseBatchId] int NOT NULL,
2026-04-23 20:26:07.475 |           CONSTRAINT [PK_Sections] PRIMARY KEY ([Id]),
2026-04-23 20:26:07.475 |           CONSTRAINT [FK_Sections_CourseBatches_CourseBatchId] FOREIGN KEY ([CourseBatchId]) REFERENCES [CourseBatches] ([Id]) ON DELETE NO ACTION,
2026-04-23 20:26:07.475 |           CONSTRAINT [FK_Sections_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]) ON DELETE NO ACTION
2026-04-23 20:26:07.475 |       );
2026-04-23 20:26:07.479 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.479 |       Executed DbCommand (4ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.479 |       CREATE TABLE [CourseContents] (
2026-04-23 20:26:07.479 |           [Id] int NOT NULL IDENTITY,
2026-04-23 20:26:07.479 |           [CourseId] int NOT NULL,
2026-04-23 20:26:07.479 |           [CourseBatchId] int NOT NULL,
2026-04-23 20:26:07.479 |           [SectionId] int NOT NULL,
2026-04-23 20:26:07.479 |           [ContentType] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.479 |           [ContentId] int NULL,
2026-04-23 20:26:07.479 |           [CourseOrder] int NOT NULL,
2026-04-23 20:26:07.479 |           CONSTRAINT [PK_CourseContents] PRIMARY KEY ([Id]),
2026-04-23 20:26:07.479 |           CONSTRAINT [FK_CourseContents_CourseBatches_CourseBatchId] FOREIGN KEY ([CourseBatchId]) REFERENCES [CourseBatches] ([Id]) ON DELETE NO ACTION,
2026-04-23 20:26:07.479 |           CONSTRAINT [FK_CourseContents_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]) ON DELETE NO ACTION,
2026-04-23 20:26:07.479 |           CONSTRAINT [FK_CourseContents_Sections_SectionId] FOREIGN KEY ([SectionId]) REFERENCES [Sections] ([Id]) ON DELETE NO ACTION
2026-04-23 20:26:07.479 |       );
2026-04-23 20:26:07.482 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.482 |       Executed DbCommand (3ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.482 |       CREATE TABLE [Tasks] (
2026-04-23 20:26:07.482 |           [Id] int NOT NULL IDENTITY,
2026-04-23 20:26:07.482 |           [SectionId] int NOT NULL,
2026-04-23 20:26:07.482 |           [Title] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.482 |           [Description] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.482 |           [Type] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.482 |           [FormUrl] nvarchar(max) NULL,
2026-04-23 20:26:07.482 |           [MaxScore] int NOT NULL,
2026-04-23 20:26:07.482 |           [Deadline] datetime2 NOT NULL,
2026-04-23 20:26:07.482 |           [CourseContentId] int NOT NULL,
2026-04-23 20:26:07.482 |           CONSTRAINT [PK_Tasks] PRIMARY KEY ([Id]),
2026-04-23 20:26:07.482 |           CONSTRAINT [FK_Tasks_Sections_SectionId] FOREIGN KEY ([SectionId]) REFERENCES [Sections] ([Id]) ON DELETE CASCADE
2026-04-23 20:26:07.482 |       );
2026-04-23 20:26:07.486 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.486 |       Executed DbCommand (4ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.486 |       CREATE TABLE [TaskSubmissions] (
2026-04-23 20:26:07.486 |           [Id] int NOT NULL IDENTITY,
2026-04-23 20:26:07.486 |           [TaskId] int NOT NULL,
2026-04-23 20:26:07.486 |           [UserId] nvarchar(450) NOT NULL,
2026-04-23 20:26:07.486 |           [SubmissionUrl] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.486 |           [SubmissionAt] datetime2 NOT NULL,
2026-04-23 20:26:07.486 |           [Status] nvarchar(max) NOT NULL,
2026-04-23 20:26:07.486 |           CONSTRAINT [PK_TaskSubmissions] PRIMARY KEY ([Id]),
2026-04-23 20:26:07.486 |           CONSTRAINT [FK_TaskSubmissions_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
2026-04-23 20:26:07.486 |           CONSTRAINT [FK_TaskSubmissions_Tasks_TaskId] FOREIGN KEY ([TaskId]) REFERENCES [Tasks] ([Id]) ON DELETE CASCADE
2026-04-23 20:26:07.486 |       );
2026-04-23 20:26:07.489 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.489 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.489 |       CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
2026-04-23 20:26:07.491 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.491 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.491 |       CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
2026-04-23 20:26:07.493 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.493 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.493 |       CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
2026-04-23 20:26:07.495 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.495 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.495 |       CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
2026-04-23 20:26:07.497 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.497 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.497 |       CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
2026-04-23 20:26:07.499 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.499 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.499 |       CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
2026-04-23 20:26:07.501 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.502 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.502 |       CREATE INDEX [IX_AspNetUsers_ImageId] ON [AspNetUsers] ([ImageId]);
2026-04-23 20:26:07.504 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.504 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.504 |       CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
2026-04-23 20:26:07.506 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.506 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.506 |       CREATE INDEX [IX_Carts_CouponId] ON [Carts] ([CouponId]);
2026-04-23 20:26:07.508 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.508 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.508 |       CREATE INDEX [IX_Carts_UserId] ON [Carts] ([UserId]);
2026-04-23 20:26:07.510 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.510 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.510 |       CREATE INDEX [IX_CourseBatches_CourseId] ON [CourseBatches] ([CourseId]);
2026-04-23 20:26:07.513 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.513 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.513 |       CREATE INDEX [IX_CourseContents_CourseBatchId] ON [CourseContents] ([CourseBatchId]);
2026-04-23 20:26:07.515 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.515 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.515 |       CREATE INDEX [IX_CourseContents_CourseId] ON [CourseContents] ([CourseId]);
2026-04-23 20:26:07.517 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.517 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.517 |       CREATE INDEX [IX_CourseContents_SectionId] ON [CourseContents] ([SectionId]);
2026-04-23 20:26:07.520 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.520 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.520 |       CREATE INDEX [IX_Courses_FacilityId] ON [Courses] ([FacilityId]);
2026-04-23 20:26:07.522 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.522 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.522 |       CREATE INDEX [IX_Courses_GroupId] ON [Courses] ([GroupId]);
2026-04-23 20:26:07.524 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.524 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.524 |       CREATE INDEX [IX_Enrollments_CourseBatchId] ON [Enrollments] ([CourseBatchId]);
2026-04-23 20:26:07.526 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.526 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.526 |       CREATE INDEX [IX_Enrollments_UserId] ON [Enrollments] ([UserId]);
2026-04-23 20:26:07.528 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.528 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.528 |       CREATE INDEX [IX_Facilities_CategoryId] ON [Facilities] ([CategoryId]);
2026-04-23 20:26:07.531 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.531 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.531 |       CREATE INDEX [IX_Favorites_CourseId] ON [Favorites] ([CourseId]);
2026-04-23 20:26:07.533 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.533 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.533 |       CREATE INDEX [IX_Groups_FacilityId] ON [Groups] ([FacilityId]);
2026-04-23 20:26:07.535 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.535 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.535 |       CREATE INDEX [IX_Lessons_CourseBatchId] ON [Lessons] ([CourseBatchId]);
2026-04-23 20:26:07.537 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.537 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.537 |       CREATE INDEX [IX_Lessons_CourseId] ON [Lessons] ([CourseId]);
2026-04-23 20:26:07.540 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.540 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.540 |       CREATE UNIQUE INDEX [IX_Lessons_MeetingId] ON [Lessons] ([MeetingId]) WHERE [MeetingId] IS NOT NULL;
2026-04-23 20:26:07.542 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.542 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.542 |       CREATE UNIQUE INDEX [IX_Lessons_VideoId] ON [Lessons] ([VideoId]) WHERE [VideoId] IS NOT NULL;
2026-04-23 20:26:07.544 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.544 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.545 |       CREATE INDEX [IX_PayFacs_FacilityId] ON [PayFacs] ([FacilityId]);
2026-04-23 20:26:07.547 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.547 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.547 |       CREATE INDEX [IX_PayFacs_UserId] ON [PayFacs] ([UserId]);
2026-04-23 20:26:07.549 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.549 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.549 |       CREATE INDEX [IX_Sections_CourseBatchId] ON [Sections] ([CourseBatchId]);
2026-04-23 20:26:07.552 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.552 |       Executed DbCommand (3ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.552 |       CREATE INDEX [IX_Sections_CourseId] ON [Sections] ([CourseId]);
2026-04-23 20:26:07.555 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.555 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.555 |       CREATE INDEX [IX_Tasks_SectionId] ON [Tasks] ([SectionId]);
2026-04-23 20:26:07.557 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.557 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.557 |       CREATE INDEX [IX_TaskSubmissions_TaskId] ON [TaskSubmissions] ([TaskId]);
2026-04-23 20:26:07.559 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.559 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.559 |       CREATE INDEX [IX_TaskSubmissions_UserId] ON [TaskSubmissions] ([UserId]);
2026-04-23 20:26:07.562 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.562 |       Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.562 |       CREATE INDEX [IX_UserFacilities_FacilityId] ON [UserFacilities] ([FacilityId]);
2026-04-23 20:26:07.585 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.585 |       Executed DbCommand (22ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.585 |       INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
2026-04-23 20:26:07.585 |       VALUES (N'20260423180929_InitialSqlServer', N'9.0.0');
2026-04-23 20:26:07.613 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.613 |       Executed DbCommand (10ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.613 |       DECLARE @result int;
2026-04-23 20:26:07.613 |       EXEC @result = sp_releaseapplock @Resource = '__EFMigrationsLock', @LockOwner = 'Session';
2026-04-23 20:26:07.613 |       SELECT @result
2026-04-23 20:26:07.845 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:07.845 |       Executed DbCommand (3ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:07.845 |       SELECT CASE
2026-04-23 20:26:07.845 |           WHEN EXISTS (
2026-04-23 20:26:07.845 |               SELECT 1
2026-04-23 20:26:07.845 |               FROM [AspNetRoles] AS [a]) THEN CAST(1 AS bit)
2026-04-23 20:26:07.845 |           ELSE CAST(0 AS bit)
2026-04-23 20:26:07.845 |       END
2026-04-23 20:26:08.041 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:08.041 |       Executed DbCommand (27ms) [Parameters=[@__normalizedName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:08.041 |       SELECT TOP(1) [a].[Id], [a].[ConcurrencyStamp], [a].[Name], [a].[NormalizedName]
2026-04-23 20:26:08.041 |       FROM [AspNetRoles] AS [a]
2026-04-23 20:26:08.041 |       WHERE [a].[NormalizedName] = @__normalizedName_0
2026-04-23 20:26:08.249 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:08.249 |       Executed DbCommand (13ms) [Parameters=[@p0='?' (Size = 450), @p1='?' (Size = 4000), @p2='?' (Size = 256), @p3='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:08.249 |       SET IMPLICIT_TRANSACTIONS OFF;
2026-04-23 20:26:08.249 |       SET NOCOUNT ON;
2026-04-23 20:26:08.249 |       INSERT INTO [AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName])
2026-04-23 20:26:08.249 |       VALUES (@p0, @p1, @p2, @p3);
2026-04-23 20:26:08.287 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:08.287 |       Executed DbCommand (24ms) [Parameters=[@__normalizedName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:08.287 |       SELECT TOP(1) [a].[Id], [a].[ConcurrencyStamp], [a].[Name], [a].[NormalizedName]
2026-04-23 20:26:08.287 |       FROM [AspNetRoles] AS [a]
2026-04-23 20:26:08.287 |       WHERE [a].[NormalizedName] = @__normalizedName_0
2026-04-23 20:26:08.304 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:08.304 |       Executed DbCommand (10ms) [Parameters=[@p0='?' (Size = 450), @p1='?' (Size = 4000), @p2='?' (Size = 256), @p3='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:08.304 |       SET IMPLICIT_TRANSACTIONS OFF;
2026-04-23 20:26:08.304 |       SET NOCOUNT ON;
2026-04-23 20:26:08.304 |       INSERT INTO [AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName])
2026-04-23 20:26:08.304 |       VALUES (@p0, @p1, @p2, @p3);
2026-04-23 20:26:08.308 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:08.308 |       Executed DbCommand (2ms) [Parameters=[@__normalizedName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:08.308 |       SELECT TOP(1) [a].[Id], [a].[ConcurrencyStamp], [a].[Name], [a].[NormalizedName]
2026-04-23 20:26:08.308 |       FROM [AspNetRoles] AS [a]
2026-04-23 20:26:08.308 |       WHERE [a].[NormalizedName] = @__normalizedName_0
2026-04-23 20:26:08.321 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:08.321 |       Executed DbCommand (11ms) [Parameters=[@p0='?' (Size = 450), @p1='?' (Size = 4000), @p2='?' (Size = 256), @p3='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:08.321 |       SET IMPLICIT_TRANSACTIONS OFF;
2026-04-23 20:26:08.321 |       SET NOCOUNT ON;
2026-04-23 20:26:08.321 |       INSERT INTO [AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName])
2026-04-23 20:26:08.321 |       VALUES (@p0, @p1, @p2, @p3);
2026-04-23 20:26:08.511 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:08.511 |       Executed DbCommand (4ms) [Parameters=[@__normalizedUserName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:08.511 |       SELECT TOP(1) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-23 20:26:08.511 |       FROM [AspNetUsers] AS [a]
2026-04-23 20:26:08.511 |       WHERE [a].[NormalizedUserName] = @__normalizedUserName_0
2026-04-23 20:26:08.529 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:08.529 |       Executed DbCommand (7ms) [Parameters=[@__normalizedEmail_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:08.529 |       SELECT TOP(2) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-23 20:26:08.529 |       FROM [AspNetUsers] AS [a]
2026-04-23 20:26:08.529 |       WHERE [a].[NormalizedEmail] = @__normalizedEmail_0
2026-04-23 20:26:08.682 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:08.682 |       Executed DbCommand (10ms) [Parameters=[@p0='?' (Size = 450), @p1='?' (DbType = Int32), @p2='?' (Size = 4000), @p3='?' (Size = 256), @p4='?' (DbType = Boolean), @p5='?' (DbType = Int32), @p6='?' (DbType = Boolean), @p7='?' (DbType = DateTimeOffset), @p8='?' (Size = 4000), @p9='?' (Size = 256), @p10='?' (Size = 256), @p11='?' (Size = 4000), @p12='?' (Size = 4000), @p13='?' (DbType = Boolean), @p14='?' (Size = 4000), @p15='?' (DbType = Int32), @p16='?' (DbType = Boolean), @p17='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:08.682 |       SET IMPLICIT_TRANSACTIONS OFF;
2026-04-23 20:26:08.682 |       SET NOCOUNT ON;
2026-04-23 20:26:08.682 |       INSERT INTO [AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [ImageId], [LockoutEnabled], [LockoutEnd], [Name], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TotalCarts], [TwoFactorEnabled], [UserName])
2026-04-23 20:26:08.682 |       VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15, @p16, @p17);
2026-04-23 20:26:08.702 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:08.702 |       Executed DbCommand (16ms) [Parameters=[@__normalizedEmail_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:08.702 |       SELECT TOP(2) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-23 20:26:08.702 |       FROM [AspNetUsers] AS [a]
2026-04-23 20:26:08.702 |       WHERE [a].[NormalizedEmail] = @__normalizedEmail_0
2026-04-23 20:26:08.730 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:08.730 |       Executed DbCommand (5ms) [Parameters=[@__normalizedRoleName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:08.730 |       SELECT TOP(2) [a].[Id], [a].[ConcurrencyStamp], [a].[Name], [a].[NormalizedName]
2026-04-23 20:26:08.730 |       FROM [AspNetRoles] AS [a]
2026-04-23 20:26:08.730 |       WHERE [a].[NormalizedName] = @__normalizedRoleName_0
2026-04-23 20:26:08.793 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:08.794 |       Executed DbCommand (3ms) [Parameters=[@__p_0='?' (Size = 450), @__p_1='?' (Size = 450)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:08.794 |       SELECT TOP(1) [a].[UserId], [a].[RoleId]
2026-04-23 20:26:08.794 |       FROM [AspNetUserRoles] AS [a]
2026-04-23 20:26:08.794 |       WHERE [a].[UserId] = @__p_0 AND [a].[RoleId] = @__p_1
2026-04-23 20:26:08.801 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:08.801 |       Executed DbCommand (2ms) [Parameters=[@__normalizedRoleName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:08.801 |       SELECT TOP(2) [a].[Id], [a].[ConcurrencyStamp], [a].[Name], [a].[NormalizedName]
2026-04-23 20:26:08.801 |       FROM [AspNetRoles] AS [a]
2026-04-23 20:26:08.801 |       WHERE [a].[NormalizedName] = @__normalizedRoleName_0
2026-04-23 20:26:08.849 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:08.849 |       Executed DbCommand (28ms) [Parameters=[@__normalizedUserName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:08.849 |       SELECT TOP(1) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-23 20:26:08.849 |       FROM [AspNetUsers] AS [a]
2026-04-23 20:26:08.849 |       WHERE [a].[NormalizedUserName] = @__normalizedUserName_0
2026-04-23 20:26:08.854 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:08.854 |       Executed DbCommand (2ms) [Parameters=[@__normalizedEmail_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:08.854 |       SELECT TOP(2) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-23 20:26:08.854 |       FROM [AspNetUsers] AS [a]
2026-04-23 20:26:08.854 |       WHERE [a].[NormalizedEmail] = @__normalizedEmail_0
2026-04-23 20:26:08.933 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:26:08.933 |       Executed DbCommand (34ms) [Parameters=[@p0='?' (Size = 450), @p1='?' (Size = 450), @p19='?' (Size = 450), @p2='?' (DbType = Int32), @p3='?' (Size = 4000), @p20='?' (Size = 4000), @p4='?' (Size = 256), @p5='?' (DbType = Boolean), @p6='?' (DbType = Int32), @p7='?' (DbType = Boolean), @p8='?' (DbType = DateTimeOffset), @p9='?' (Size = 4000), @p10='?' (Size = 256), @p11='?' (Size = 256), @p12='?' (Size = 4000), @p13='?' (Size = 4000), @p14='?' (DbType = Boolean), @p15='?' (Size = 4000), @p16='?' (DbType = Int32), @p17='?' (DbType = Boolean), @p18='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:26:08.933 |       SET NOCOUNT ON;
2026-04-23 20:26:08.933 |       INSERT INTO [AspNetUserRoles] ([RoleId], [UserId])
2026-04-23 20:26:08.933 |       VALUES (@p0, @p1);
2026-04-23 20:26:08.933 |       UPDATE [AspNetUsers] SET [AccessFailedCount] = @p2, [ConcurrencyStamp] = @p3, [Email] = @p4, [EmailConfirmed] = @p5, [ImageId] = @p6, [LockoutEnabled] = @p7, [LockoutEnd] = @p8, [Name] = @p9, [NormalizedEmail] = @p10, [NormalizedUserName] = @p11, [PasswordHash] = @p12, [PhoneNumber] = @p13, [PhoneNumberConfirmed] = @p14, [SecurityStamp] = @p15, [TotalCarts] = @p16, [TwoFactorEnabled] = @p17, [UserName] = @p18
2026-04-23 20:26:08.933 |       OUTPUT 1
2026-04-23 20:26:08.933 |       WHERE [Id] = @p19 AND [ConcurrencyStamp] = @p20;
2026-04-23 20:26:08.967 | DBInitializer executed successfully ✅
2026-04-23 20:26:09.152 | warn: Microsoft.AspNetCore.DataProtection.KeyManagement.XmlKeyManager[35]
2026-04-23 20:26:09.152 |       No XML encryptor configured. Key {b47aa919-e19f-4284-b87b-b5b3443c9e07} may be persisted to storage in unencrypted form.
2026-04-23 20:26:09.166 | warn: Microsoft.AspNetCore.Hosting.Diagnostics[15]
2026-04-23 20:26:09.166 |       Overriding HTTP_PORTS '8080' and HTTPS_PORTS ''. Binding to values defined by URLS instead 'http://+:8080'.
2026-04-23 20:26:09.201 | info: Microsoft.Hosting.Lifetime[14]
2026-04-23 20:26:09.201 |       Now listening on: http://[::]:8080
2026-04-23 20:26:09.201 | info: Microsoft.Hosting.Lifetime[0]
2026-04-23 20:26:09.201 |       Application started. Press Ctrl+C to shut down.
2026-04-23 20:26:09.201 | info: Microsoft.Hosting.Lifetime[0]
2026-04-23 20:26:09.201 |       Hosting environment: Development
2026-04-23 20:26:09.201 | info: Microsoft.Hosting.Lifetime[0]
2026-04-23 20:26:09.201 |       Content root path: /app
2026-04-23 20:26:27.205 | warn: Microsoft.AspNetCore.HttpsPolicy.HttpsRedirectionMiddleware[3]
2026-04-23 20:26:27.205 |       Failed to determine the https port for redirect.
2026-04-23 20:28:00.381 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:28:00.381 |       Executed DbCommand (1ms) [Parameters=[@__normalizedUserName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:28:00.381 |       SELECT TOP(1) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-23 20:28:00.381 |       FROM [AspNetUsers] AS [a]
2026-04-23 20:28:00.381 |       WHERE [a].[NormalizedUserName] = @__normalizedUserName_0
2026-04-23 20:28:00.383 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:28:00.383 |       Executed DbCommand (1ms) [Parameters=[@__normalizedEmail_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:28:00.383 |       SELECT TOP(2) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-23 20:28:00.383 |       FROM [AspNetUsers] AS [a]
2026-04-23 20:28:00.383 |       WHERE [a].[NormalizedEmail] = @__normalizedEmail_0
2026-04-23 20:28:00.386 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:28:00.386 |       Executed DbCommand (1ms) [Parameters=[@__normalizedUserName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:28:00.386 |       SELECT TOP(1) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-23 20:28:00.386 |       FROM [AspNetUsers] AS [a]
2026-04-23 20:28:00.386 |       WHERE [a].[NormalizedUserName] = @__normalizedUserName_0
2026-04-23 20:28:00.559 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:28:00.559 |       Executed DbCommand (3ms) [Parameters=[@__user_Id_0='?' (Size = 450)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:28:00.559 |       SELECT [a].[Id], [a].[ClaimType], [a].[ClaimValue], [a].[UserId]
2026-04-23 20:28:00.559 |       FROM [AspNetUserClaims] AS [a]
2026-04-23 20:28:00.559 |       WHERE [a].[UserId] = @__user_Id_0
2026-04-23 20:28:00.644 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:28:00.644 |       Executed DbCommand (41ms) [Parameters=[@__userId_0='?' (Size = 450)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:28:00.644 |       SELECT [a0].[Name]
2026-04-23 20:28:00.644 |       FROM [AspNetUserRoles] AS [a]
2026-04-23 20:28:00.644 |       INNER JOIN [AspNetRoles] AS [a0] ON [a].[RoleId] = [a0].[Id]
2026-04-23 20:28:00.644 |       WHERE [a].[UserId] = @__userId_0
2026-04-23 20:28:00.650 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:28:00.650 |       Executed DbCommand (2ms) [Parameters=[@__normalizedName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:28:00.650 |       SELECT TOP(1) [a].[Id], [a].[ConcurrencyStamp], [a].[Name], [a].[NormalizedName]
2026-04-23 20:28:00.650 |       FROM [AspNetRoles] AS [a]
2026-04-23 20:28:00.650 |       WHERE [a].[NormalizedName] = @__normalizedName_0
2026-04-23 20:28:00.671 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:28:00.671 |       Executed DbCommand (5ms) [Parameters=[@__role_Id_0='?' (Size = 450)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:28:00.671 |       SELECT [a].[ClaimType], [a].[ClaimValue]
2026-04-23 20:28:00.671 |       FROM [AspNetRoleClaims] AS [a]
2026-04-23 20:28:00.671 |       WHERE [a].[RoleId] = @__role_Id_0
2026-04-23 20:28:00.700 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:28:00.700 |       Executed DbCommand (3ms) [Parameters=[@__userId_0='?' (Size = 450)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:28:00.700 |       SELECT [a0].[Name]
2026-04-23 20:28:00.700 |       FROM [AspNetUserRoles] AS [a]
2026-04-23 20:28:00.700 |       INNER JOIN [AspNetRoles] AS [a0] ON [a].[RoleId] = [a0].[Id]
2026-04-23 20:28:00.700 |       WHERE [a].[UserId] = @__userId_0
2026-04-23 20:37:21.495 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:37:21.495 |       Executed DbCommand (2ms) [Parameters=[@__normalizedUserName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:37:21.495 |       SELECT TOP(1) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-23 20:37:21.495 |       FROM [AspNetUsers] AS [a]
2026-04-23 20:37:21.495 |       WHERE [a].[NormalizedUserName] = @__normalizedUserName_0
2026-04-23 20:37:21.498 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:37:21.498 |       Executed DbCommand (2ms) [Parameters=[@__normalizedEmail_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:37:21.498 |       SELECT TOP(2) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-23 20:37:21.498 |       FROM [AspNetUsers] AS [a]
2026-04-23 20:37:21.498 |       WHERE [a].[NormalizedEmail] = @__normalizedEmail_0
2026-04-23 20:37:21.501 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:37:21.501 |       Executed DbCommand (2ms) [Parameters=[@__normalizedUserName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:37:21.501 |       SELECT TOP(1) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-23 20:37:21.501 |       FROM [AspNetUsers] AS [a]
2026-04-23 20:37:21.501 |       WHERE [a].[NormalizedUserName] = @__normalizedUserName_0
2026-04-23 20:37:21.672 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:37:21.672 |       Executed DbCommand (1ms) [Parameters=[@__user_Id_0='?' (Size = 450)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:37:21.672 |       SELECT [a].[Id], [a].[ClaimType], [a].[ClaimValue], [a].[UserId]
2026-04-23 20:37:21.672 |       FROM [AspNetUserClaims] AS [a]
2026-04-23 20:37:21.672 |       WHERE [a].[UserId] = @__user_Id_0
2026-04-23 20:37:21.674 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:37:21.674 |       Executed DbCommand (1ms) [Parameters=[@__userId_0='?' (Size = 450)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:37:21.674 |       SELECT [a0].[Name]
2026-04-23 20:37:21.674 |       FROM [AspNetUserRoles] AS [a]
2026-04-23 20:37:21.674 |       INNER JOIN [AspNetRoles] AS [a0] ON [a].[RoleId] = [a0].[Id]
2026-04-23 20:37:21.674 |       WHERE [a].[UserId] = @__userId_0
2026-04-23 20:37:21.675 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:37:21.675 |       Executed DbCommand (1ms) [Parameters=[@__normalizedName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:37:21.675 |       SELECT TOP(1) [a].[Id], [a].[ConcurrencyStamp], [a].[Name], [a].[NormalizedName]
2026-04-23 20:37:21.675 |       FROM [AspNetRoles] AS [a]
2026-04-23 20:37:21.675 |       WHERE [a].[NormalizedName] = @__normalizedName_0
2026-04-23 20:37:21.676 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:37:21.676 |       Executed DbCommand (1ms) [Parameters=[@__role_Id_0='?' (Size = 450)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:37:21.676 |       SELECT [a].[ClaimType], [a].[ClaimValue]
2026-04-23 20:37:21.676 |       FROM [AspNetRoleClaims] AS [a]
2026-04-23 20:37:21.676 |       WHERE [a].[RoleId] = @__role_Id_0
2026-04-23 20:37:21.683 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:37:21.683 |       Executed DbCommand (2ms) [Parameters=[@__userId_0='?' (Size = 450)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:37:21.683 |       SELECT [a0].[Name]
2026-04-23 20:37:21.683 |       FROM [AspNetUserRoles] AS [a]
2026-04-23 20:37:21.683 |       INNER JOIN [AspNetRoles] AS [a0] ON [a].[RoleId] = [a0].[Id]
2026-04-23 20:37:21.683 |       WHERE [a].[UserId] = @__userId_0
2026-04-23 20:46:20.519 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:46:20.519 |       Executed DbCommand (2ms) [Parameters=[@__normalizedUserName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:46:20.519 |       SELECT TOP(1) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-23 20:46:20.519 |       FROM [AspNetUsers] AS [a]
2026-04-23 20:46:20.519 |       WHERE [a].[NormalizedUserName] = @__normalizedUserName_0
2026-04-23 20:46:20.522 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:46:20.522 |       Executed DbCommand (3ms) [Parameters=[@__normalizedEmail_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:46:20.522 |       SELECT TOP(2) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-23 20:46:20.522 |       FROM [AspNetUsers] AS [a]
2026-04-23 20:46:20.522 |       WHERE [a].[NormalizedEmail] = @__normalizedEmail_0
2026-04-23 20:46:20.525 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:46:20.525 |       Executed DbCommand (2ms) [Parameters=[@__normalizedUserName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:46:20.525 |       SELECT TOP(1) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-23 20:46:20.525 |       FROM [AspNetUsers] AS [a]
2026-04-23 20:46:20.525 |       WHERE [a].[NormalizedUserName] = @__normalizedUserName_0
2026-04-23 20:46:20.693 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:46:20.693 |       Executed DbCommand (3ms) [Parameters=[@__user_Id_0='?' (Size = 450)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:46:20.693 |       SELECT [a].[Id], [a].[ClaimType], [a].[ClaimValue], [a].[UserId]
2026-04-23 20:46:20.693 |       FROM [AspNetUserClaims] AS [a]
2026-04-23 20:46:20.693 |       WHERE [a].[UserId] = @__user_Id_0
2026-04-23 20:46:20.696 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:46:20.696 |       Executed DbCommand (2ms) [Parameters=[@__userId_0='?' (Size = 450)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:46:20.696 |       SELECT [a0].[Name]
2026-04-23 20:46:20.696 |       FROM [AspNetUserRoles] AS [a]
2026-04-23 20:46:20.696 |       INNER JOIN [AspNetRoles] AS [a0] ON [a].[RoleId] = [a0].[Id]
2026-04-23 20:46:20.696 |       WHERE [a].[UserId] = @__userId_0
2026-04-23 20:46:20.698 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:46:20.698 |       Executed DbCommand (2ms) [Parameters=[@__normalizedName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:46:20.698 |       SELECT TOP(1) [a].[Id], [a].[ConcurrencyStamp], [a].[Name], [a].[NormalizedName]
2026-04-23 20:46:20.698 |       FROM [AspNetRoles] AS [a]
2026-04-23 20:46:20.698 |       WHERE [a].[NormalizedName] = @__normalizedName_0
2026-04-23 20:46:20.701 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:46:20.701 |       Executed DbCommand (2ms) [Parameters=[@__role_Id_0='?' (Size = 450)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:46:20.701 |       SELECT [a].[ClaimType], [a].[ClaimValue]
2026-04-23 20:46:20.701 |       FROM [AspNetRoleClaims] AS [a]
2026-04-23 20:46:20.701 |       WHERE [a].[RoleId] = @__role_Id_0
2026-04-23 20:46:20.706 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-23 20:46:20.706 |       Executed DbCommand (2ms) [Parameters=[@__userId_0='?' (Size = 450)], CommandType='Text', CommandTimeout='30']
2026-04-23 20:46:20.706 |       SELECT [a0].[Name]
2026-04-23 20:46:20.706 |       FROM [AspNetUserRoles] AS [a]
2026-04-23 20:46:20.706 |       INNER JOIN [AspNetRoles] AS [a0] ON [a].[RoleId] = [a0].[Id]
2026-04-23 20:46:20.706 |       WHERE [a].[UserId] = @__userId_0
2026-04-24 01:04:54.582 | info: Microsoft.Hosting.Lifetime[0]
2026-04-24 01:04:54.582 |       Application is shutting down...
2026-04-24 15:39:35.454 | warn: Microsoft.AspNetCore.DataProtection.Repositories.FileSystemXmlRepository[60]
2026-04-24 15:39:35.455 |       Storing keys in a directory '/app/.aspnet/DataProtection-Keys' that may not be persisted outside of the container. Protected data will be unavailable when container is destroyed. For more information go to https://aka.ms/aspnet/dataprotectionwarning
2026-04-24 15:39:36.303 | warn: Microsoft.EntityFrameworkCore.Model.Validation[30000]
2026-04-24 15:39:36.303 |       No store type was specified for the decimal property 'Amount' on entity type 'PayFac'. This will cause values to be silently truncated if they do not fit in the default precision and scale. Explicitly specify the SQL server column type that can accommodate all the values in 'OnModelCreating' using 'HasColumnType', specify precision and scale using 'HasPrecision', or configure a value converter using 'HasConversion'.
2026-04-24 15:39:37.646 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:39:37.646 |       Executed DbCommand (29ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-24 15:39:37.646 |       SELECT 1
2026-04-24 15:39:37.692 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:39:37.692 |       Executed DbCommand (23ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-24 15:39:37.692 |       SELECT OBJECT_ID(N'[__EFMigrationsHistory]');
2026-04-24 15:39:37.728 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:39:37.728 |       Executed DbCommand (16ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-24 15:39:37.728 |       SELECT [MigrationId], [ProductVersion]
2026-04-24 15:39:37.728 |       FROM [__EFMigrationsHistory]
2026-04-24 15:39:37.728 |       ORDER BY [MigrationId];
2026-04-24 15:39:38.352 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:39:38.352 |       Executed DbCommand (23ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
2026-04-24 15:39:38.352 |       SELECT CASE
2026-04-24 15:39:38.352 |           WHEN EXISTS (
2026-04-24 15:39:38.352 |               SELECT 1
2026-04-24 15:39:38.352 |               FROM [AspNetRoles] AS [a]) THEN CAST(1 AS bit)
2026-04-24 15:39:38.352 |           ELSE CAST(0 AS bit)
2026-04-24 15:39:38.352 |       END
2026-04-24 15:39:38.354 | DBInitializer executed successfully ✅
2026-04-24 15:39:38.567 | warn: Microsoft.AspNetCore.Hosting.Diagnostics[15]
2026-04-24 15:39:38.567 |       Overriding HTTP_PORTS '8080' and HTTPS_PORTS ''. Binding to values defined by URLS instead 'http://+:8080'.
2026-04-24 15:39:38.709 | info: Microsoft.Hosting.Lifetime[14]
2026-04-24 15:39:38.709 |       Now listening on: http://[::]:8080
2026-04-24 15:39:38.709 | info: Microsoft.Hosting.Lifetime[0]
2026-04-24 15:39:38.709 |       Application started. Press Ctrl+C to shut down.
2026-04-24 15:39:38.713 | info: Microsoft.Hosting.Lifetime[0]
2026-04-24 15:39:38.713 |       Hosting environment: Development
2026-04-24 15:39:38.713 | info: Microsoft.Hosting.Lifetime[0]
2026-04-24 15:39:38.713 |       Content root path: /app
2026-04-24 15:43:14.043 | warn: Microsoft.AspNetCore.HttpsPolicy.HttpsRedirectionMiddleware[3]
2026-04-24 15:43:14.043 |       Failed to determine the https port for redirect.
2026-04-24 15:43:14.403 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:43:14.403 |       Executed DbCommand (41ms) [Parameters=[@__normalizedUserName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:43:14.403 |       SELECT TOP(1) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-24 15:43:14.403 |       FROM [AspNetUsers] AS [a]
2026-04-24 15:43:14.403 |       WHERE [a].[NormalizedUserName] = @__normalizedUserName_0
2026-04-24 15:43:14.428 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:43:14.428 |       Executed DbCommand (12ms) [Parameters=[@__normalizedEmail_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:43:14.428 |       SELECT TOP(2) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-24 15:43:14.428 |       FROM [AspNetUsers] AS [a]
2026-04-24 15:43:14.428 |       WHERE [a].[NormalizedEmail] = @__normalizedEmail_0
2026-04-24 15:43:14.548 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:43:14.548 |       Executed DbCommand (1ms) [Parameters=[@__normalizedUserName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:43:14.548 |       SELECT TOP(1) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-24 15:43:14.548 |       FROM [AspNetUsers] AS [a]
2026-04-24 15:43:14.548 |       WHERE [a].[NormalizedUserName] = @__normalizedUserName_0
2026-04-24 15:43:14.697 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:43:14.697 |       Executed DbCommand (3ms) [Parameters=[@__user_Id_0='?' (Size = 450)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:43:14.697 |       SELECT [a].[Id], [a].[ClaimType], [a].[ClaimValue], [a].[UserId]
2026-04-24 15:43:14.697 |       FROM [AspNetUserClaims] AS [a]
2026-04-24 15:43:14.697 |       WHERE [a].[UserId] = @__user_Id_0
2026-04-24 15:43:14.738 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:43:14.738 |       Executed DbCommand (30ms) [Parameters=[@__userId_0='?' (Size = 450)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:43:14.738 |       SELECT [a0].[Name]
2026-04-24 15:43:14.738 |       FROM [AspNetUserRoles] AS [a]
2026-04-24 15:43:14.738 |       INNER JOIN [AspNetRoles] AS [a0] ON [a].[RoleId] = [a0].[Id]
2026-04-24 15:43:14.738 |       WHERE [a].[UserId] = @__userId_0
2026-04-24 15:43:14.766 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:43:14.766 |       Executed DbCommand (16ms) [Parameters=[@__normalizedName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:43:14.766 |       SELECT TOP(1) [a].[Id], [a].[ConcurrencyStamp], [a].[Name], [a].[NormalizedName]
2026-04-24 15:43:14.766 |       FROM [AspNetRoles] AS [a]
2026-04-24 15:43:14.766 |       WHERE [a].[NormalizedName] = @__normalizedName_0
2026-04-24 15:43:14.795 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:43:14.795 |       Executed DbCommand (4ms) [Parameters=[@__role_Id_0='?' (Size = 450)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:43:14.795 |       SELECT [a].[ClaimType], [a].[ClaimValue]
2026-04-24 15:43:14.795 |       FROM [AspNetRoleClaims] AS [a]
2026-04-24 15:43:14.795 |       WHERE [a].[RoleId] = @__role_Id_0
2026-04-24 15:43:14.817 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:43:14.817 |       Executed DbCommand (2ms) [Parameters=[@__userId_0='?' (Size = 450)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:43:14.817 |       SELECT [a0].[Name]
2026-04-24 15:43:14.817 |       FROM [AspNetUserRoles] AS [a]
2026-04-24 15:43:14.817 |       INNER JOIN [AspNetRoles] AS [a0] ON [a].[RoleId] = [a0].[Id]
2026-04-24 15:43:14.817 |       WHERE [a].[UserId] = @__userId_0
2026-04-24 15:43:17.534 | fail: Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddleware[1]
2026-04-24 15:43:17.534 |       An unhandled exception has occurred while executing the request.
2026-04-24 15:43:17.534 |       Microsoft.AspNetCore.Routing.Matching.AmbiguousMatchException: The request matched multiple endpoints. Matches: 
2026-04-24 15:43:17.534 |       
2026-04-24 15:43:17.534 |       Zumra.Controllers.CourseController.GetCourses (Zumra)
2026-04-24 15:43:17.534 |       Zumra.Controllers.CourseController.GetMyEnrollments (Zumra)
2026-04-24 15:43:17.534 |          at Microsoft.AspNetCore.Routing.Matching.DefaultEndpointSelector.ReportAmbiguity(Span`1 candidateState)
2026-04-24 15:43:17.534 |          at Microsoft.AspNetCore.Routing.Matching.DefaultEndpointSelector.ProcessFinalCandidates(HttpContext httpContext, Span`1 candidateState)
2026-04-24 15:43:17.534 |          at Microsoft.AspNetCore.Routing.Matching.DefaultEndpointSelector.Select(HttpContext httpContext, Span`1 candidateState)
2026-04-24 15:43:17.534 |          at Microsoft.AspNetCore.Routing.Matching.DfaMatcher.MatchAsync(HttpContext httpContext)
2026-04-24 15:43:17.534 |          at Microsoft.AspNetCore.Routing.EndpointRoutingMiddleware.Invoke(HttpContext httpContext)
2026-04-24 15:43:17.534 |          at Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddlewareImpl.Invoke(HttpContext context)
2026-04-24 15:43:30.961 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:43:30.961 |       Executed DbCommand (2ms) [Parameters=[@__normalizedUserName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:43:30.961 |       SELECT TOP(1) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-24 15:43:30.961 |       FROM [AspNetUsers] AS [a]
2026-04-24 15:43:30.961 |       WHERE [a].[NormalizedUserName] = @__normalizedUserName_0
2026-04-24 15:43:30.963 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:43:30.963 |       Executed DbCommand (2ms) [Parameters=[@__normalizedEmail_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:43:30.963 |       SELECT TOP(2) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-24 15:43:30.963 |       FROM [AspNetUsers] AS [a]
2026-04-24 15:43:30.963 |       WHERE [a].[NormalizedEmail] = @__normalizedEmail_0
2026-04-24 15:43:30.967 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:43:30.967 |       Executed DbCommand (2ms) [Parameters=[@__normalizedUserName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:43:30.967 |       SELECT TOP(1) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-24 15:43:30.967 |       FROM [AspNetUsers] AS [a]
2026-04-24 15:43:30.967 |       WHERE [a].[NormalizedUserName] = @__normalizedUserName_0
2026-04-24 15:43:31.045 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:43:31.045 |       Executed DbCommand (1ms) [Parameters=[@__normalizedUserName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:43:31.045 |       SELECT TOP(1) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-24 15:43:31.045 |       FROM [AspNetUsers] AS [a]
2026-04-24 15:43:31.045 |       WHERE [a].[NormalizedUserName] = @__normalizedUserName_0
2026-04-24 15:43:31.047 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:43:31.047 |       Executed DbCommand (1ms) [Parameters=[@__normalizedEmail_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:43:31.047 |       SELECT TOP(2) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-24 15:43:31.047 |       FROM [AspNetUsers] AS [a]
2026-04-24 15:43:31.047 |       WHERE [a].[NormalizedEmail] = @__normalizedEmail_0
2026-04-24 15:43:31.173 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:43:31.173 |       Executed DbCommand (41ms) [Parameters=[@p17='?' (Size = 450), @p0='?' (DbType = Int32), @p1='?' (Size = 4000), @p18='?' (Size = 4000), @p2='?' (Size = 256), @p3='?' (DbType = Boolean), @p4='?' (DbType = Int32), @p5='?' (DbType = Boolean), @p6='?' (DbType = DateTimeOffset), @p7='?' (Size = 4000), @p8='?' (Size = 256), @p9='?' (Size = 256), @p10='?' (Size = 4000), @p11='?' (Size = 4000), @p12='?' (DbType = Boolean), @p13='?' (Size = 4000), @p14='?' (DbType = Int32), @p15='?' (DbType = Boolean), @p16='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:43:31.173 |       SET IMPLICIT_TRANSACTIONS OFF;
2026-04-24 15:43:31.173 |       SET NOCOUNT ON;
2026-04-24 15:43:31.173 |       UPDATE [AspNetUsers] SET [AccessFailedCount] = @p0, [ConcurrencyStamp] = @p1, [Email] = @p2, [EmailConfirmed] = @p3, [ImageId] = @p4, [LockoutEnabled] = @p5, [LockoutEnd] = @p6, [Name] = @p7, [NormalizedEmail] = @p8, [NormalizedUserName] = @p9, [PasswordHash] = @p10, [PhoneNumber] = @p11, [PhoneNumberConfirmed] = @p12, [SecurityStamp] = @p13, [TotalCarts] = @p14, [TwoFactorEnabled] = @p15, [UserName] = @p16
2026-04-24 15:43:31.173 |       OUTPUT 1
2026-04-24 15:43:31.173 |       WHERE [Id] = @p17 AND [ConcurrencyStamp] = @p18;
2026-04-24 15:43:38.579 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:43:38.579 |       Executed DbCommand (3ms) [Parameters=[@__normalizedUserName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:43:38.579 |       SELECT TOP(1) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-24 15:43:38.579 |       FROM [AspNetUsers] AS [a]
2026-04-24 15:43:38.579 |       WHERE [a].[NormalizedUserName] = @__normalizedUserName_0
2026-04-24 15:43:38.583 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:43:38.583 |       Executed DbCommand (3ms) [Parameters=[@__normalizedEmail_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:43:38.583 |       SELECT TOP(2) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-24 15:43:38.583 |       FROM [AspNetUsers] AS [a]
2026-04-24 15:43:38.583 |       WHERE [a].[NormalizedEmail] = @__normalizedEmail_0
2026-04-24 15:43:38.588 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:43:38.588 |       Executed DbCommand (2ms) [Parameters=[@__normalizedUserName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:43:38.588 |       SELECT TOP(1) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-24 15:43:38.588 |       FROM [AspNetUsers] AS [a]
2026-04-24 15:43:38.588 |       WHERE [a].[NormalizedUserName] = @__normalizedUserName_0
2026-04-24 15:43:38.745 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:43:38.745 |       Executed DbCommand (1ms) [Parameters=[@__normalizedUserName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:43:38.745 |       SELECT TOP(1) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-24 15:43:38.745 |       FROM [AspNetUsers] AS [a]
2026-04-24 15:43:38.745 |       WHERE [a].[NormalizedUserName] = @__normalizedUserName_0
2026-04-24 15:43:38.746 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:43:38.746 |       Executed DbCommand (1ms) [Parameters=[@__normalizedEmail_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:43:38.746 |       SELECT TOP(2) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-24 15:43:38.746 |       FROM [AspNetUsers] AS [a]
2026-04-24 15:43:38.746 |       WHERE [a].[NormalizedEmail] = @__normalizedEmail_0
2026-04-24 15:43:38.759 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:43:38.759 |       Executed DbCommand (12ms) [Parameters=[@p17='?' (Size = 450), @p0='?' (DbType = Int32), @p1='?' (Size = 4000), @p18='?' (Size = 4000), @p2='?' (Size = 256), @p3='?' (DbType = Boolean), @p4='?' (DbType = Int32), @p5='?' (DbType = Boolean), @p6='?' (DbType = DateTimeOffset), @p7='?' (Size = 4000), @p8='?' (Size = 256), @p9='?' (Size = 256), @p10='?' (Size = 4000), @p11='?' (Size = 4000), @p12='?' (DbType = Boolean), @p13='?' (Size = 4000), @p14='?' (DbType = Int32), @p15='?' (DbType = Boolean), @p16='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:43:38.759 |       SET IMPLICIT_TRANSACTIONS OFF;
2026-04-24 15:43:38.759 |       SET NOCOUNT ON;
2026-04-24 15:43:38.759 |       UPDATE [AspNetUsers] SET [AccessFailedCount] = @p0, [ConcurrencyStamp] = @p1, [Email] = @p2, [EmailConfirmed] = @p3, [ImageId] = @p4, [LockoutEnabled] = @p5, [LockoutEnd] = @p6, [Name] = @p7, [NormalizedEmail] = @p8, [NormalizedUserName] = @p9, [PasswordHash] = @p10, [PhoneNumber] = @p11, [PhoneNumberConfirmed] = @p12, [SecurityStamp] = @p13, [TotalCarts] = @p14, [TwoFactorEnabled] = @p15, [UserName] = @p16
2026-04-24 15:43:38.759 |       OUTPUT 1
2026-04-24 15:43:38.759 |       WHERE [Id] = @p17 AND [ConcurrencyStamp] = @p18;
2026-04-24 15:43:54.463 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:43:54.463 |       Executed DbCommand (2ms) [Parameters=[@__normalizedUserName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:43:54.463 |       SELECT TOP(1) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-24 15:43:54.463 |       FROM [AspNetUsers] AS [a]
2026-04-24 15:43:54.463 |       WHERE [a].[NormalizedUserName] = @__normalizedUserName_0
2026-04-24 15:43:54.466 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:43:54.466 |       Executed DbCommand (2ms) [Parameters=[@__normalizedEmail_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:43:54.466 |       SELECT TOP(2) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-24 15:43:54.466 |       FROM [AspNetUsers] AS [a]
2026-04-24 15:43:54.466 |       WHERE [a].[NormalizedEmail] = @__normalizedEmail_0
2026-04-24 15:43:54.469 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:43:54.469 |       Executed DbCommand (2ms) [Parameters=[@__normalizedUserName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:43:54.469 |       SELECT TOP(1) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-24 15:43:54.469 |       FROM [AspNetUsers] AS [a]
2026-04-24 15:43:54.469 |       WHERE [a].[NormalizedUserName] = @__normalizedUserName_0
2026-04-24 15:43:54.606 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:43:54.606 |       Executed DbCommand (1ms) [Parameters=[@__normalizedUserName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:43:54.606 |       SELECT TOP(1) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-24 15:43:54.606 |       FROM [AspNetUsers] AS [a]
2026-04-24 15:43:54.606 |       WHERE [a].[NormalizedUserName] = @__normalizedUserName_0
2026-04-24 15:43:54.607 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:43:54.607 |       Executed DbCommand (1ms) [Parameters=[@__normalizedEmail_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:43:54.607 |       SELECT TOP(2) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-24 15:43:54.607 |       FROM [AspNetUsers] AS [a]
2026-04-24 15:43:54.607 |       WHERE [a].[NormalizedEmail] = @__normalizedEmail_0
2026-04-24 15:43:54.623 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:43:54.623 |       Executed DbCommand (15ms) [Parameters=[@p17='?' (Size = 450), @p0='?' (DbType = Int32), @p1='?' (Size = 4000), @p18='?' (Size = 4000), @p2='?' (Size = 256), @p3='?' (DbType = Boolean), @p4='?' (DbType = Int32), @p5='?' (DbType = Boolean), @p6='?' (DbType = DateTimeOffset), @p7='?' (Size = 4000), @p8='?' (Size = 256), @p9='?' (Size = 256), @p10='?' (Size = 4000), @p11='?' (Size = 4000), @p12='?' (DbType = Boolean), @p13='?' (Size = 4000), @p14='?' (DbType = Int32), @p15='?' (DbType = Boolean), @p16='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:43:54.623 |       SET IMPLICIT_TRANSACTIONS OFF;
2026-04-24 15:43:54.623 |       SET NOCOUNT ON;
2026-04-24 15:43:54.623 |       UPDATE [AspNetUsers] SET [AccessFailedCount] = @p0, [ConcurrencyStamp] = @p1, [Email] = @p2, [EmailConfirmed] = @p3, [ImageId] = @p4, [LockoutEnabled] = @p5, [LockoutEnd] = @p6, [Name] = @p7, [NormalizedEmail] = @p8, [NormalizedUserName] = @p9, [PasswordHash] = @p10, [PhoneNumber] = @p11, [PhoneNumberConfirmed] = @p12, [SecurityStamp] = @p13, [TotalCarts] = @p14, [TwoFactorEnabled] = @p15, [UserName] = @p16
2026-04-24 15:43:54.623 |       OUTPUT 1
2026-04-24 15:43:54.623 |       WHERE [Id] = @p17 AND [ConcurrencyStamp] = @p18;
2026-04-24 15:43:54.629 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:43:54.629 |       Executed DbCommand (2ms) [Parameters=[@__user_Id_0='?' (Size = 450)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:43:54.629 |       SELECT [a].[Id], [a].[ClaimType], [a].[ClaimValue], [a].[UserId]
2026-04-24 15:43:54.629 |       FROM [AspNetUserClaims] AS [a]
2026-04-24 15:43:54.629 |       WHERE [a].[UserId] = @__user_Id_0
2026-04-24 15:43:54.632 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:43:54.632 |       Executed DbCommand (2ms) [Parameters=[@__userId_0='?' (Size = 450)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:43:54.632 |       SELECT [a0].[Name]
2026-04-24 15:43:54.632 |       FROM [AspNetUserRoles] AS [a]
2026-04-24 15:43:54.632 |       INNER JOIN [AspNetRoles] AS [a0] ON [a].[RoleId] = [a0].[Id]
2026-04-24 15:43:54.632 |       WHERE [a].[UserId] = @__userId_0
2026-04-24 15:43:54.635 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:43:54.635 |       Executed DbCommand (2ms) [Parameters=[@__normalizedName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:43:54.635 |       SELECT TOP(1) [a].[Id], [a].[ConcurrencyStamp], [a].[Name], [a].[NormalizedName]
2026-04-24 15:43:54.635 |       FROM [AspNetRoles] AS [a]
2026-04-24 15:43:54.635 |       WHERE [a].[NormalizedName] = @__normalizedName_0
2026-04-24 15:43:54.638 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:43:54.638 |       Executed DbCommand (2ms) [Parameters=[@__role_Id_0='?' (Size = 450)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:43:54.638 |       SELECT [a].[ClaimType], [a].[ClaimValue]
2026-04-24 15:43:54.638 |       FROM [AspNetRoleClaims] AS [a]
2026-04-24 15:43:54.638 |       WHERE [a].[RoleId] = @__role_Id_0
2026-04-24 15:43:54.643 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:43:54.643 |       Executed DbCommand (2ms) [Parameters=[@__userId_0='?' (Size = 450)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:43:54.643 |       SELECT [a0].[Name]
2026-04-24 15:43:54.643 |       FROM [AspNetUserRoles] AS [a]
2026-04-24 15:43:54.643 |       INNER JOIN [AspNetRoles] AS [a0] ON [a].[RoleId] = [a0].[Id]
2026-04-24 15:43:54.643 |       WHERE [a].[UserId] = @__userId_0
2026-04-24 15:58:28.219 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:58:28.219 |       Executed DbCommand (1ms) [Parameters=[@__normalizedUserName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:58:28.219 |       SELECT TOP(1) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-24 15:58:28.219 |       FROM [AspNetUsers] AS [a]
2026-04-24 15:58:28.219 |       WHERE [a].[NormalizedUserName] = @__normalizedUserName_0
2026-04-24 15:58:28.221 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:58:28.221 |       Executed DbCommand (1ms) [Parameters=[@__normalizedEmail_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:58:28.221 |       SELECT TOP(2) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-24 15:58:28.221 |       FROM [AspNetUsers] AS [a]
2026-04-24 15:58:28.221 |       WHERE [a].[NormalizedEmail] = @__normalizedEmail_0
2026-04-24 15:58:28.276 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:58:28.276 |       Executed DbCommand (19ms) [Parameters=[@p0='?' (Size = 450), @p1='?' (DbType = Int32), @p2='?' (Size = 4000), @p3='?' (Size = 256), @p4='?' (DbType = Boolean), @p5='?' (DbType = Int32), @p6='?' (DbType = Boolean), @p7='?' (DbType = DateTimeOffset), @p8='?' (Size = 4000), @p9='?' (Size = 256), @p10='?' (Size = 256), @p11='?' (Size = 4000), @p12='?' (Size = 4000), @p13='?' (DbType = Boolean), @p14='?' (Size = 4000), @p15='?' (DbType = Int32), @p16='?' (DbType = Boolean), @p17='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:58:28.276 |       SET IMPLICIT_TRANSACTIONS OFF;
2026-04-24 15:58:28.276 |       SET NOCOUNT ON;
2026-04-24 15:58:28.276 |       INSERT INTO [AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [ImageId], [LockoutEnabled], [LockoutEnd], [Name], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TotalCarts], [TwoFactorEnabled], [UserName])
2026-04-24 15:58:28.276 |       VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15, @p16, @p17);
2026-04-24 15:58:28.300 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:58:28.300 |       Executed DbCommand (5ms) [Parameters=[@__normalizedRoleName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:58:28.300 |       SELECT TOP(2) [a].[Id], [a].[ConcurrencyStamp], [a].[Name], [a].[NormalizedName]
2026-04-24 15:58:28.300 |       FROM [AspNetRoles] AS [a]
2026-04-24 15:58:28.300 |       WHERE [a].[NormalizedName] = @__normalizedRoleName_0
2026-04-24 15:58:28.348 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:58:28.348 |       Executed DbCommand (4ms) [Parameters=[@__p_0='?' (Size = 450), @__p_1='?' (Size = 450)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:58:28.348 |       SELECT TOP(1) [a].[UserId], [a].[RoleId]
2026-04-24 15:58:28.348 |       FROM [AspNetUserRoles] AS [a]
2026-04-24 15:58:28.348 |       WHERE [a].[UserId] = @__p_0 AND [a].[RoleId] = @__p_1
2026-04-24 15:58:28.355 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:58:28.355 |       Executed DbCommand (2ms) [Parameters=[@__normalizedRoleName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:58:28.355 |       SELECT TOP(2) [a].[Id], [a].[ConcurrencyStamp], [a].[Name], [a].[NormalizedName]
2026-04-24 15:58:28.355 |       FROM [AspNetRoles] AS [a]
2026-04-24 15:58:28.355 |       WHERE [a].[NormalizedName] = @__normalizedRoleName_0
2026-04-24 15:58:28.369 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:58:28.369 |       Executed DbCommand (2ms) [Parameters=[@__normalizedUserName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:58:28.369 |       SELECT TOP(1) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-24 15:58:28.369 |       FROM [AspNetUsers] AS [a]
2026-04-24 15:58:28.369 |       WHERE [a].[NormalizedUserName] = @__normalizedUserName_0
2026-04-24 15:58:28.371 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:58:28.371 |       Executed DbCommand (2ms) [Parameters=[@__normalizedEmail_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:58:28.371 |       SELECT TOP(2) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-24 15:58:28.371 |       FROM [AspNetUsers] AS [a]
2026-04-24 15:58:28.371 |       WHERE [a].[NormalizedEmail] = @__normalizedEmail_0
2026-04-24 15:58:28.416 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:58:28.416 |       Executed DbCommand (11ms) [Parameters=[@p0='?' (Size = 450), @p1='?' (Size = 450), @p19='?' (Size = 450), @p2='?' (DbType = Int32), @p3='?' (Size = 4000), @p20='?' (Size = 4000), @p4='?' (Size = 256), @p5='?' (DbType = Boolean), @p6='?' (DbType = Int32), @p7='?' (DbType = Boolean), @p8='?' (DbType = DateTimeOffset), @p9='?' (Size = 4000), @p10='?' (Size = 256), @p11='?' (Size = 256), @p12='?' (Size = 4000), @p13='?' (Size = 4000), @p14='?' (DbType = Boolean), @p15='?' (Size = 4000), @p16='?' (DbType = Int32), @p17='?' (DbType = Boolean), @p18='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:58:28.416 |       SET NOCOUNT ON;
2026-04-24 15:58:28.416 |       INSERT INTO [AspNetUserRoles] ([RoleId], [UserId])
2026-04-24 15:58:28.416 |       VALUES (@p0, @p1);
2026-04-24 15:58:28.416 |       UPDATE [AspNetUsers] SET [AccessFailedCount] = @p2, [ConcurrencyStamp] = @p3, [Email] = @p4, [EmailConfirmed] = @p5, [ImageId] = @p6, [LockoutEnabled] = @p7, [LockoutEnd] = @p8, [Name] = @p9, [NormalizedEmail] = @p10, [NormalizedUserName] = @p11, [PasswordHash] = @p12, [PhoneNumber] = @p13, [PhoneNumberConfirmed] = @p14, [SecurityStamp] = @p15, [TotalCarts] = @p16, [TwoFactorEnabled] = @p17, [UserName] = @p18
2026-04-24 15:58:28.416 |       OUTPUT 1
2026-04-24 15:58:28.416 |       WHERE [Id] = @p19 AND [ConcurrencyStamp] = @p20;
2026-04-24 15:58:29.429 | fail: Zumra.Controllers.AccountController[0]
2026-04-24 15:58:29.429 |       Error during registration for email mmm810813@gmail.com
2026-04-24 15:58:29.429 |       System.Net.Mail.SmtpException: The SMTP server requires a secure connection or the client was not authenticated. The server response was: 5.7.0 Authentication Required. For more information, go to
2026-04-24 15:58:29.429 |          at System.Net.Mail.MailCommand.CheckResponse(SmtpStatusCode statusCode, String response)
2026-04-24 15:58:29.429 |          at System.Net.Mail.MailCommand.EndSend(IAsyncResult result)
2026-04-24 15:58:29.429 |          at System.Net.Mail.SendMailAsyncResult.SendMailFromCompleted(IAsyncResult result)
2026-04-24 15:58:29.429 |       --- End of stack trace from previous location ---
2026-04-24 15:58:29.429 |          at System.Net.Mail.SendMailAsyncResult.End(IAsyncResult result)
2026-04-24 15:58:29.429 |          at System.Net.Mail.SmtpClient.SendMailCallback(IAsyncResult result)
2026-04-24 15:58:29.429 |       --- End of stack trace from previous location ---
2026-04-24 15:58:29.429 |          at Zumra.Controllers.AccountController.Register(RegisterRequest vm) in /src/Zumra/Controllers/AccountController.cs:line 201
2026-04-24 15:58:47.518 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:58:47.518 |       Executed DbCommand (2ms) [Parameters=[@__normalizedUserName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:58:47.518 |       SELECT TOP(1) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-24 15:58:47.518 |       FROM [AspNetUsers] AS [a]
2026-04-24 15:58:47.518 |       WHERE [a].[NormalizedUserName] = @__normalizedUserName_0
2026-04-24 15:58:47.520 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:58:47.520 |       Executed DbCommand (2ms) [Parameters=[@__normalizedEmail_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:58:47.520 |       SELECT TOP(2) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-24 15:58:47.521 |       FROM [AspNetUsers] AS [a]
2026-04-24 15:58:47.521 |       WHERE [a].[NormalizedEmail] = @__normalizedEmail_0
2026-04-24 15:58:47.523 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:58:47.523 |       Executed DbCommand (2ms) [Parameters=[@__normalizedUserName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:58:47.523 |       SELECT TOP(1) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-24 15:58:47.523 |       FROM [AspNetUsers] AS [a]
2026-04-24 15:58:47.523 |       WHERE [a].[NormalizedUserName] = @__normalizedUserName_0
2026-04-24 15:58:47.673 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:58:47.674 |       Executed DbCommand (1ms) [Parameters=[@__user_Id_0='?' (Size = 450)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:58:47.674 |       SELECT [a].[Id], [a].[ClaimType], [a].[ClaimValue], [a].[UserId]
2026-04-24 15:58:47.674 |       FROM [AspNetUserClaims] AS [a]
2026-04-24 15:58:47.674 |       WHERE [a].[UserId] = @__user_Id_0
2026-04-24 15:58:47.675 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:58:47.675 |       Executed DbCommand (1ms) [Parameters=[@__userId_0='?' (Size = 450)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:58:47.675 |       SELECT [a0].[Name]
2026-04-24 15:58:47.675 |       FROM [AspNetUserRoles] AS [a]
2026-04-24 15:58:47.675 |       INNER JOIN [AspNetRoles] AS [a0] ON [a].[RoleId] = [a0].[Id]
2026-04-24 15:58:47.675 |       WHERE [a].[UserId] = @__userId_0
2026-04-24 15:58:47.677 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:58:47.677 |       Executed DbCommand (1ms) [Parameters=[@__normalizedName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:58:47.677 |       SELECT TOP(1) [a].[Id], [a].[ConcurrencyStamp], [a].[Name], [a].[NormalizedName]
2026-04-24 15:58:47.677 |       FROM [AspNetRoles] AS [a]
2026-04-24 15:58:47.677 |       WHERE [a].[NormalizedName] = @__normalizedName_0
2026-04-24 15:58:47.678 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:58:47.679 |       Executed DbCommand (1ms) [Parameters=[@__role_Id_0='?' (Size = 450)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:58:47.679 |       SELECT [a].[ClaimType], [a].[ClaimValue]
2026-04-24 15:58:47.679 |       FROM [AspNetRoleClaims] AS [a]
2026-04-24 15:58:47.679 |       WHERE [a].[RoleId] = @__role_Id_0
2026-04-24 15:58:47.682 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 15:58:47.682 |       Executed DbCommand (1ms) [Parameters=[@__userId_0='?' (Size = 450)], CommandType='Text', CommandTimeout='30']
2026-04-24 15:58:47.682 |       SELECT [a0].[Name]
2026-04-24 15:58:47.682 |       FROM [AspNetUserRoles] AS [a]
2026-04-24 15:58:47.682 |       INNER JOIN [AspNetRoles] AS [a0] ON [a].[RoleId] = [a0].[Id]
2026-04-24 15:58:47.682 |       WHERE [a].[UserId] = @__userId_0
2026-04-24 15:58:47.794 | fail: Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddleware[1]
2026-04-24 15:58:47.794 |       An unhandled exception has occurred while executing the request.
2026-04-24 15:58:47.794 |       Microsoft.AspNetCore.Routing.Matching.AmbiguousMatchException: The request matched multiple endpoints. Matches: 
2026-04-24 15:58:47.794 |       
2026-04-24 15:58:47.794 |       Zumra.Controllers.CourseController.GetCourses (Zumra)
2026-04-24 15:58:47.794 |       Zumra.Controllers.CourseController.GetMyEnrollments (Zumra)
2026-04-24 15:58:47.794 |          at Microsoft.AspNetCore.Routing.Matching.DefaultEndpointSelector.ReportAmbiguity(Span`1 candidateState)
2026-04-24 15:58:47.794 |          at Microsoft.AspNetCore.Routing.Matching.DefaultEndpointSelector.ProcessFinalCandidates(HttpContext httpContext, Span`1 candidateState)
2026-04-24 15:58:47.794 |          at Microsoft.AspNetCore.Routing.Matching.DefaultEndpointSelector.Select(HttpContext httpContext, Span`1 candidateState)
2026-04-24 15:58:47.794 |          at Microsoft.AspNetCore.Routing.Matching.DfaMatcher.MatchAsync(HttpContext httpContext)
2026-04-24 15:58:47.794 |          at Microsoft.AspNetCore.Routing.EndpointRoutingMiddleware.Invoke(HttpContext httpContext)
2026-04-24 15:58:47.794 |          at Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddlewareImpl.Invoke(HttpContext context)
2026-04-24 16:02:40.171 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 16:02:40.171 |       Executed DbCommand (19ms) [Parameters=[@__normalizedUserName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 16:02:40.171 |       SELECT TOP(1) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-24 16:02:40.171 |       FROM [AspNetUsers] AS [a]
2026-04-24 16:02:40.171 |       WHERE [a].[NormalizedUserName] = @__normalizedUserName_0
2026-04-24 16:02:40.173 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 16:02:40.173 |       Executed DbCommand (2ms) [Parameters=[@__normalizedEmail_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 16:02:40.173 |       SELECT TOP(2) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-24 16:02:40.173 |       FROM [AspNetUsers] AS [a]
2026-04-24 16:02:40.173 |       WHERE [a].[NormalizedEmail] = @__normalizedEmail_0
2026-04-24 16:02:40.183 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 16:02:40.183 |       Executed DbCommand (7ms) [Parameters=[@p0='?' (Size = 450), @p1='?' (DbType = Int32), @p2='?' (Size = 4000), @p3='?' (Size = 256), @p4='?' (DbType = Boolean), @p5='?' (DbType = Int32), @p6='?' (DbType = Boolean), @p7='?' (DbType = DateTimeOffset), @p8='?' (Size = 4000), @p9='?' (Size = 256), @p10='?' (Size = 256), @p11='?' (Size = 4000), @p12='?' (Size = 4000), @p13='?' (DbType = Boolean), @p14='?' (Size = 4000), @p15='?' (DbType = Int32), @p16='?' (DbType = Boolean), @p17='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 16:02:40.183 |       SET IMPLICIT_TRANSACTIONS OFF;
2026-04-24 16:02:40.183 |       SET NOCOUNT ON;
2026-04-24 16:02:40.183 |       INSERT INTO [AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [ImageId], [LockoutEnabled], [LockoutEnd], [Name], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TotalCarts], [TwoFactorEnabled], [UserName])
2026-04-24 16:02:40.183 |       VALUES (@p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15, @p16, @p17);
2026-04-24 16:02:40.186 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 16:02:40.186 |       Executed DbCommand (2ms) [Parameters=[@__normalizedRoleName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 16:02:40.186 |       SELECT TOP(2) [a].[Id], [a].[ConcurrencyStamp], [a].[Name], [a].[NormalizedName]
2026-04-24 16:02:40.186 |       FROM [AspNetRoles] AS [a]
2026-04-24 16:02:40.186 |       WHERE [a].[NormalizedName] = @__normalizedRoleName_0
2026-04-24 16:02:40.189 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 16:02:40.189 |       Executed DbCommand (2ms) [Parameters=[@__p_0='?' (Size = 450), @__p_1='?' (Size = 450)], CommandType='Text', CommandTimeout='30']
2026-04-24 16:02:40.189 |       SELECT TOP(1) [a].[UserId], [a].[RoleId]
2026-04-24 16:02:40.189 |       FROM [AspNetUserRoles] AS [a]
2026-04-24 16:02:40.189 |       WHERE [a].[UserId] = @__p_0 AND [a].[RoleId] = @__p_1
2026-04-24 16:02:40.192 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 16:02:40.192 |       Executed DbCommand (2ms) [Parameters=[@__normalizedRoleName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 16:02:40.192 |       SELECT TOP(2) [a].[Id], [a].[ConcurrencyStamp], [a].[Name], [a].[NormalizedName]
2026-04-24 16:02:40.192 |       FROM [AspNetRoles] AS [a]
2026-04-24 16:02:40.192 |       WHERE [a].[NormalizedName] = @__normalizedRoleName_0
2026-04-24 16:02:40.194 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 16:02:40.194 |       Executed DbCommand (2ms) [Parameters=[@__normalizedUserName_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 16:02:40.194 |       SELECT TOP(1) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-24 16:02:40.194 |       FROM [AspNetUsers] AS [a]
2026-04-24 16:02:40.194 |       WHERE [a].[NormalizedUserName] = @__normalizedUserName_0
2026-04-24 16:02:40.197 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 16:02:40.197 |       Executed DbCommand (2ms) [Parameters=[@__normalizedEmail_0='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 16:02:40.197 |       SELECT TOP(2) [a].[Id], [a].[AccessFailedCount], [a].[ConcurrencyStamp], [a].[Email], [a].[EmailConfirmed], [a].[ImageId], [a].[LockoutEnabled], [a].[LockoutEnd], [a].[Name], [a].[NormalizedEmail], [a].[NormalizedUserName], [a].[PasswordHash], [a].[PhoneNumber], [a].[PhoneNumberConfirmed], [a].[SecurityStamp], [a].[TotalCarts], [a].[TwoFactorEnabled], [a].[UserName]
2026-04-24 16:02:40.197 |       FROM [AspNetUsers] AS [a]
2026-04-24 16:02:40.197 |       WHERE [a].[NormalizedEmail] = @__normalizedEmail_0
2026-04-24 16:02:40.202 | info: Microsoft.EntityFrameworkCore.Database.Command[20101]
2026-04-24 16:02:40.202 |       Executed DbCommand (2ms) [Parameters=[@p0='?' (Size = 450), @p1='?' (Size = 450), @p19='?' (Size = 450), @p2='?' (DbType = Int32), @p3='?' (Size = 4000), @p20='?' (Size = 4000), @p4='?' (Size = 256), @p5='?' (DbType = Boolean), @p6='?' (DbType = Int32), @p7='?' (DbType = Boolean), @p8='?' (DbType = DateTimeOffset), @p9='?' (Size = 4000), @p10='?' (Size = 256), @p11='?' (Size = 256), @p12='?' (Size = 4000), @p13='?' (Size = 4000), @p14='?' (DbType = Boolean), @p15='?' (Size = 4000), @p16='?' (DbType = Int32), @p17='?' (DbType = Boolean), @p18='?' (Size = 256)], CommandType='Text', CommandTimeout='30']
2026-04-24 16:02:40.202 |       SET NOCOUNT ON;
2026-04-24 16:02:40.202 |       INSERT INTO [AspNetUserRoles] ([RoleId], [UserId])
2026-04-24 16:02:40.202 |       VALUES (@p0, @p1);
2026-04-24 16:02:40.202 |       UPDATE [AspNetUsers] SET [AccessFailedCount] = @p2, [ConcurrencyStamp] = @p3, [Email] = @p4, [EmailConfirmed] = @p5, [ImageId] = @p6, [LockoutEnabled] = @p7, [LockoutEnd] = @p8, [Name] = @p9, [NormalizedEmail] = @p10, [NormalizedUserName] = @p11, [PasswordHash] = @p12, [PhoneNumber] = @p13, [PhoneNumberConfirmed] = @p14, [SecurityStamp] = @p15, [TotalCarts] = @p16, [TwoFactorEnabled] = @p17, [UserName] = @p18
2026-04-24 16:02:40.202 |       OUTPUT 1
2026-04-24 16:02:40.202 |       WHERE [Id] = @p19 AND [ConcurrencyStamp] = @p20;
2026-04-24 16:02:40.973 | fail: Zumra.Controllers.AccountController[0]
2026-04-24 16:02:40.973 |       Error during registration for email mmm810813@gmai.com
2026-04-24 16:02:40.973 |       System.Net.Mail.SmtpException: The SMTP server requires a secure connection or the client was not authenticated. The server response was: 5.7.0 Authentication Required. For more information, go to
2026-04-24 16:02:40.973 |          at System.Net.Mail.MailCommand.CheckResponse(SmtpStatusCode statusCode, String response)
2026-04-24 16:02:40.973 |          at System.Net.Mail.MailCommand.EndSend(IAsyncResult result)
2026-04-24 16:02:40.973 |          at System.Net.Mail.SendMailAsyncResult.SendMailFromCompleted(IAsyncResult result)
2026-04-24 16:02:40.973 |       --- End of stack trace from previous location ---
2026-04-24 16:02:40.973 |          at System.Net.Mail.SendMailAsyncResult.End(IAsyncResult result)
2026-04-24 16:02:40.973 |          at System.Net.Mail.SmtpClient.SendMailCallback(IAsyncResult result)
2026-04-24 16:02:40.973 |       --- End of stack trace from previous location ---
2026-04-24 16:02:40.973 |          at Zumra.Controllers.AccountController.Register(RegisterRequest vm) in /src/Zumra/Controllers/AccountController.cs:line 201  دي الل log بتعت الباك

### Planner Response

قرأت الـ logs كلها، في **مشكلتين واضحين في الـ Backend**:

---

## 🔴 المشكلة 1: SMTP غلط (سبب 500 على Register)

```
SmtpException: The SMTP server requires a secure connection or the client was not authenticated.
5.7.0 Authentication Required
```

**السبب:** الـ Backend بيحاول يبعت email تأكيد بعد التسجيل لكن الـ SMTP credentials غلطة أو مفيش credentials أصلاً.

**الحل** في `appsettings.json` في الـ Backend:
```json
"EmailSettings": {
  "SmtpHost": "smtp.gmail.com",
  "SmtpPort": 587,
  "SmtpUser": "your-email@gmail.com",
  "SmtpPass": "your-app-password",
  "EnableSsl": true
}
```

> ⚠️ لو بتستخدم Gmail، لازم تعمل **App Password** مش الـ password الأصلي. روح على: Google Account → Security → 2-Step Verification → App Passwords

---

## 🔴 المشكلة 2: Routing Conflict (سبب 500 على /Api/Course)

```
AmbiguousMatchException: The request matched multiple endpoints:
- CourseController.GetCourses
- CourseController.GetMyEnrollments
```

**السبب:** في الـ `CourseController` في الـ Backend، عندك route اتكرر. الاتنين بيعملوا `GET /Api/Course` بدون تمييز.

**الحل** في الـ Backend في `CourseController.cs` — غيّر route الـ `GetMyEnrollments`:

```csharp
// قبل
[HttpGet]
public async Task<IActionResult> GetMyEnrollments() { ... }

// بعد - أضف route مختلف
[HttpGet("my-enrollments")]
public async Task<IActionResult> GetMyEnrollments() { ... }
```

---

## ✅ ملخص

| المشكلة | المصدر | الحل |
|---|---|---|
| 500 على Register | SMTP credentials في Backend | إعداد Gmail App Password |
| 500 على /Api/Course | Route conflict في CourseController | تغيير route الـ GetMyEnrollments |

**كلهم Backend issues مش Frontend.** محتاج تعدل في مشروع الـ .NET. عايز أساعدك تلاقي الملفات دي وتعدلها؟