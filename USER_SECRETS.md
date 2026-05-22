# إعدادات الأمان (User Secrets)

تم نقل جميع المعلومات الحساسة من `appsettings.json` إلى **User Secrets** لحماية البيانات السرية.

## ما تم نقله:
1. **Connection String**: سلسلة الاتصال بقاعدة البيانات
2. **Email Password**: كلمة مرور البريد الإلكتروني
3. **JWT Key**: مفتاح تشفير الـ JWT
4. **Google Client ID**: معرف تطبيق Google
5. **Google Client Secret**: المفتاح السري لـ Google

## كيفية عرض الأسرار المحفوظة:
```bash
dotnet user-secrets list
```

## كيفية تعديل أو إضافة سر جديد:
```bash
dotnet user-secrets set "المسار:الخاصية" "القيمة"
```

مثال:
```bash
dotnet user-secrets set "Jwt:Key" "new-secret-key-here"
```

## كيفية حذف سر:
```bash
dotnet user-secrets remove "المسار:الخاصية"
```

## موقع تخزين الأسرار:
- **Linux/macOS**: `~/.microsoft/usersecrets/<user_secrets_id>/secrets.json`
- **Windows**: `%APPDATA%\Microsoft\UserSecrets\<user_secrets_id>\secrets.json`

## ملاحظات مهمة:
⚠️ **User Secrets** مناسبة فقط لبيئة التطوير (Development).
⚠️ في بيئة الإنتاج (Production)، استخدم:
  - **Azure Key Vault**
  - **AWS Secrets Manager**
  - **Environment Variables** على السيرفر

✅ الآن ملف `appsettings.json` آمن ويمكن رفعه على Git بدون مشاكل.
