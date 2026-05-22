# Zumra – Database Schema

---

## 📌 Domains

| Domain   | Tables                                                                                           |
|----------|--------------------------------------------------------------------------------------------------|
| Auth     | ApplicationUser, UserImage, Otp                                                                  |
| Facility | Category, Facility, Group, UserFacility                                                          |
| Course   | Course, CourseBatches, Enrollments                                                               |
| Content  | Sections, Lessons, LessonLive, LessonRec, Tasks, TaskSubmissions, CourseContent                  |
| Payment  | PayFac, Cart, Coupon                                                                             |

---

## 🟦 Auth

### ApplicationUser
*(extends ASP.NET Identity IdentityUser)*

| Column       | Type     | Constraint |
|--------------|----------|------------|
| Id           | string   | PK         |
| Name         | string   |            |
| Email        | string   |            |
| UserName     | string   |            |
| TotalCarts   | int      |            |
| ImageId      | int?     | FK → UserImage.Id |

### UserImage

| Column    | Type   | Constraint |
|-----------|--------|------------|
| Id        | int    | PK         |
| ImageZone | string |            |
| ImagePath | string |            |
| ImageName | string |            |

### Otp

| Column     | Type     | Constraint |
|------------|----------|------------|
| Id         | int      | PK         |
| Email      | string   |            |
| OtpCode    | string   |            |
| Expiration | DateTime |            |
| IsUsed     | bool     |            |

---

## 🟣 Facility

### Category

| Column      | Type    | Constraint |
|-------------|---------|------------|
| Id          | int     | PK         |
| Name        | string  |            |
| Description | string  |            |
| ImageZone   | string  |            |
| ImagePath   | string  |            |
| ImageName   | string  |            |
| ImageUrl    | string? |            |

### Facility

| Column      | Type    | Constraint         |
|-------------|---------|--------------------|
| Id          | int     | PK                 |
| Name        | string  |                    |
| Description | string  |                    |
| Type        | string  |                    |
| ImageZone   | string  |                    |
| ImagePath   | string  |                    |
| ImageName   | string? |                    |
| ImageUrl    | string? |                    |
| Status      | string  | default: "Pending" |
| CategoryId  | int     | FK → Category.Id   |

### Group

| Column      | Type   | Constraint        |
|-------------|--------|-------------------|
| Id          | int    | PK                |
| Name        | string |                   |
| Description | string |                   |
| FacilityId  | int    | FK → Facility.Id  |

### UserFacility
*(junction table – User ↔ Facility)*

| Column     | Type         | Constraint           |
|------------|--------------|----------------------|
| UserId     | string       | FK → ApplicationUser.Id |
| FacilityId | int          | FK → Facility.Id     |
| Role       | FacilityRole | enum (SuperAdmin=0, Leader=1, Instructor=2, Member=3) |
| CreatedAt  | DateTime     |                      |

---

## 🟢 Course

### Course

| Column      | Type    | Constraint        |
|-------------|---------|-------------------|
| Id          | int     | PK                |
| Name        | string  |                   |
| Description | string  |                   |
| Cost        | int     |                   |
| Type        | string  |                   |
| CreatedAt   | string  |                   |
| ImageZone   | string  |                   |
| ImagePath   | string  |                   |
| ImageName   | string  |                   |
| ImageUrl    | string? |                   |
| GroupId     | int     | FK → Group.Id     |
| FacilityId  | int     | FK → Facility.Id  |

### CourseBatches

| Column    | Type   | Constraint      |
|-----------|--------|-----------------|
| Id        | int    | PK              |
| CourseId  | int    | FK → Course.Id  |
| Title     | string |                 |
| StartDate | string |                 |
| EndDate   | string |                 |
| Capacity  | int?   |                 |
| Status    | string |                 |

### Enrollments

| Column        | Type      | Constraint               |
|---------------|-----------|--------------------------|
| Id            | int       | PK                       |
| UserId        | string    | FK → ApplicationUser.Id  |
| CourseBatchId | int       | FK → CourseBatches.Id    |
| AccessType    | string?   | Free / Paid / Grant      |
| Status        | string    | Active / Expired / Cancelled |
| CreatedAt     | DateTime? |                          |

---

## 🟡 Content

### Sections

| Column        | Type   | Constraint               |
|---------------|--------|--------------------------|
| Id            | int    | PK                       |
| Name          | string |                          |
| Order         | int    |                          |
| CourseId      | int    | FK → Course.Id           |
| CourseBatchId | int    | FK → CourseBatches.Id    |

### Lessons

| Column          | Type      | Constraint                 |
|-----------------|-----------|----------------------------|
| Id              | int       | PK                         |
| Name            | string    |                            |
| Description     | string    |                            |
| Type            | string    | Live / Recorded / Material |
| Order           | int       |                            |
| CreatedAt       | DateTime  |                            |
| CourseId        | int       | FK → Course.Id             |
| CourseBatchId   | int       | FK → CourseBatches.Id      |
| CourseContentId | int       |                            |
| MeetingId       | int?      | FK → LessonLive.Id         |
| VideoId         | int?      | FK → LessonRec.Id          |

### LessonLive

| Column     | Type     | Constraint |
|------------|----------|------------|
| Id         | int      | PK         |
| StartTime  | DateTime |            |
| EndTime    | DateTime |            |
| MeetingUrl | string   |            |
| RoomName   | string   |            |

### LessonRec

| Column       | Type      | Constraint |
|--------------|-----------|------------|
| Id           | int       | PK         |
| VideoUrl     | string    |            |
| Duration     | float     | in minutes |
| VideoSize    | long?     | in bytes   |
| VideoFormat  | string?   |            |
| VideoQuality | string?   |            |
| UploadedAt   | DateTime? |            |
| IsProcessed  | bool      |            |

### Tasks

| Column          | Type     | Constraint          |
|-----------------|----------|---------------------|
| Id              | int      | PK                  |
| SectionId       | int      | FK → Sections.Id    |
| Title           | string   |                     |
| Description     | string   |                     |
| Type            | string   | Assignment / Quiz / Practice |
| FormUrl         | string?  |                     |
| MaxScore        | int      |                     |
| Deadline        | DateTime |                     |
| CourseContentId | int      | FK → CourseContent.Id |

### TaskSubmissions

| Column        | Type     | Constraint              |
|---------------|----------|-------------------------|
| Id            | int      | PK                      |
| TaskId        | int      | FK → Tasks.Id           |
| UserId        | string   | FK → ApplicationUser.Id |
| SubmissionUrl | string   |                         |
| SubmissionAt  | DateTime |                         |
| Status        | string   | Submitted / Late        |

### CourseContent

| Column        | Type   | Constraint               |
|---------------|--------|--------------------------|
| Id            | int    | PK                       |
| CourseId      | int    | FK → Course.Id           |
| CourseBatchId | int    | FK → CourseBatches.Id    |
| SectionId     | int    | FK → Sections.Id         |
| ContentType   | string | Lesson / Task            |
| ContentId     | int?   | polymorphic FK           |
| CourseOrder   | int    |                          |

---

## 🔴 Payment

### PayFac

| Column          | Type     | Constraint              |
|-----------------|----------|-------------------------|
| Id              | int      | PK                      |
| UserId          | string   | FK → ApplicationUser.Id |
| FacilityId      | int      | FK → Facility.Id        |
| Status          | string   |                         |
| PaymentDate     | DateTime |                         |
| StripeSessionId | string   |                         |
| Amount          | decimal  |                         |

### Cart

| Column     | Type   | Constraint              |
|------------|--------|-------------------------|
| Id         | int    | PK                      |
| UserId     | string | FK → ApplicationUser.Id |
| BookId     | int    |                         |
| Quantity   | int    |                         |
| TotalPrice | int    |                         |
| CouponId   | int?   | FK → Coupon.Id          |

### Coupon

| Column         | Type     | Constraint |
|----------------|----------|------------|
| Id             | int      | PK         |
| Code           | string   |            |
| DiscountAmount | int      |            |
| ExpiryDate     | DateTime |            |
| IsActive       | bool     |            |

---

## 🔗 Relationships Summary

| From               | To                 | Type         | Via / FK                        |
|--------------------|--------------------|--------------|---------------------------------|
| ApplicationUser    | UserImage          | Many → One   | ApplicationUser.ImageId         |
| ApplicationUser    | UserFacility       | One → Many   | UserFacility.UserId             |
| ApplicationUser    | Enrollments        | One → Many   | Enrollments.UserId              |
| ApplicationUser    | TaskSubmissions    | One → Many   | TaskSubmissions.UserId          |
| ApplicationUser    | PayFac             | One → Many   | PayFac.UserId                   |
| ApplicationUser    | Cart               | One → Many   | Cart.UserId                     |
| Category           | Facility           | One → Many   | Facility.CategoryId             |
| Facility           | UserFacility       | One → Many   | UserFacility.FacilityId         |
| Facility           | Group              | One → Many   | Group.FacilityId                |
| Facility           | Course             | One → Many   | Course.FacilityId               |
| Facility           | PayFac             | One → Many   | PayFac.FacilityId               |
| Group              | Course             | One → Many   | Course.GroupId                  |
| Course             | CourseBatches      | One → Many   | CourseBatches.CourseId          |
| Course             | Sections           | One → Many   | Sections.CourseId               |
| Course             | Lessons            | One → Many   | Lessons.CourseId                |
| Course             | CourseContent      | One → Many   | CourseContent.CourseId          |
| CourseBatches      | Sections           | One → Many   | Sections.CourseBatchId          |
| CourseBatches      | Lessons            | One → Many   | Lessons.CourseBatchId           |
| CourseBatches      | Enrollments        | One → Many   | Enrollments.CourseBatchId       |
| CourseBatches      | CourseContent      | One → Many   | CourseContent.CourseBatchId     |
| Sections           | Tasks              | One → Many   | Tasks.SectionId                 |
| Sections           | CourseContent      | One → Many   | CourseContent.SectionId         |
| Lessons            | LessonLive         | One → One    | Lessons.MeetingId               |
| Lessons            | LessonRec          | One → One    | Lessons.VideoId                 |
| Tasks              | TaskSubmissions    | One → Many   | TaskSubmissions.TaskId          |
| CourseContent      | Lessons            | One → One    | polymorphic (ContentType=Lesson)|
| CourseContent      | Tasks              | One → One    | polymorphic (ContentType=Task)  |
| Cart               | Coupon             | Many → One   | Cart.CouponId                   |
