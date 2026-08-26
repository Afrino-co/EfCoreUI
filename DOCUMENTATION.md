# مستندات فنی EfCoreUI

## ۱. معرفی پروژه

**EfCoreUI** یک افزونه (Extension) برای Visual Studio است که رابط کاربری گرافیکی روی دستورات **Entity Framework Core CLI** (`dotnet ef`) فراهم می‌کند.

به‌جای تایپ دستی دستورات در ترمینال، توسعه‌دهنده از منوی راست‌کلیک روی پروژه در Solution Explorer استفاده می‌کند و عملیات migration و دیتابیس را از طریق دیالوگ‌های WinForms انجام می‌دهد. خروجی دستورات در پنجره **Output** ویژوال استودیو نمایش داده می‌شود.

| مورد | مقدار |
|------|--------|
| ناشر | Afrino.co |
| نسخه فعلی (VSIX) | **2.08** |
| Assembly Version | 2.0.8.0 |
| نوع خروجی | VSIX Package |
| مخزن | Visual Studio Extension (VsPackage) |

---

## ۲. تکنولوژی‌ها و وابستگی‌ها

### ۲.۱ پلتفرم و زبان

| تکنولوژی | جزئیات |
|----------|--------|
| زبان | C# |
| فریم‌ورک هدف | .NET Framework **4.7.2** |
| نوع پروژه | VSIX / Visual Studio Package (`AsyncPackage`) |
| حداقل Visual Studio | **17.0** (Visual Studio 2022) |
| محدوده نصب | Community / Professional / Enterprise — نسخه `[17.0, 19.0)` — معماری **amd64** |

### ۲.۲ Visual Studio Extensibility

| پکیج / API | نقش |
|------------|-----|
| **Microsoft.VisualStudio.SDK** (17.0) | دسترسی به سرویس‌ها، منوها، Output Window و چرخه عمر پکیج |
| **Microsoft.VSSDK.BuildTools** (17.7) | ابزارهای ساخت و بسته‌بندی VSIX |
| **EnvDTE / EnvDTE80** | دسترسی به Solution، پروژه‌ها، رویدادهای Build/Solution و Solution Explorer |
| **VSCT** (`EfCoreUiPackage.vsct`) | تعریف منو و دستورات راست‌کلیک روی Project Node |
| **Managed Package Framework (MPF)** | کلاس پایه `AsyncPackage` و ثبت پکیج |

### ۲.۳ رابط کاربری

| تکنولوژی | کاربرد |
|----------|--------|
| **Windows Forms** | فرم‌های اصلی (`Form1`, `Form3`, `ContactUsForm`) |
| **System.Drawing / System.Windows.Forms** | کنترل‌ها، اعتبارسنجی و دیالوگ‌ها |

### ۲.۴ اجرای دستورات و یکپارچگی EF Core

| تکنولوژی | نقش |
|----------|-----|
| **dotnet-ef** (ابزار سراسری CLI) | موتور واقعی اجرای migration و عملیات دیتابیس |
| **CliWrap** (3.6.4) | اجرای `dotnet ef` و capture خروجی stdout/stderr |
| **DotNetPathResolver** | یافتن خودکار مسیر `dotnet.exe` از PATH، `DOTNET_ROOT` یا مسیرهای پیش‌فرض |

پیش‌نیاز:

```bash
dotnet tool install --global dotnet-ef
```

### ۲.۵ سایر کتابخانه‌ها

| پکیج | کاربرد |
|------|--------|
| **Newtonsoft.Json** (13.0.3) | خواندن/نوشتن config و استخراج Connection String |
| **System.ComponentModel.DataAnnotations** | نمایش نام عملیات از طریق `[Display]` روی enum |

### ۲.۶ ذخیره‌سازی تنظیمات

مسیر فایل پیکربندی:

```text
.vs/EfCoreUiConfigFile.json
```

فیلدهای ذخیره‌شده (نسخه 2.08):

| فیلد | توضیح |
|------|--------|
| `StartUpProject` | آخرین پروژه Startup انتخاب‌شده |
| `MigrationProject` | آخرین پروژه Migration |
| `DbContext` | آخرین DbContext انتخاب‌شده |
| `MigrationFolder` | آخرین پوشه خروجی migration |
| `BuildConfig` | Debug یا Release |
| `CreationMethod` | StartupProject یا DesignTimeFactory |

---

## ۳. ساختار پروژه

```text
EfCoreUI/
├── EfCoreUi.sln
├── EfCoreUI.vsix
├── README.md
├── DOCUMENTATION.md
└── EfCoreUi/
    ├── EfCoreUiPackage.cs
    ├── AllCommand.cs
    ├── Form1.cs
    ├── Form3.cs
    ├── ContactUsForm.cs
    ├── PreServeService.cs
    ├── MemoryParameter.cs
    ├── Services/
    │   ├── DotNetPathResolver.cs
    │   ├── EfCommandRunner.cs
    │   ├── EfCommandBuilder.cs
    │   ├── EfCommandOptions.cs
    │   ├── EfCoreUiConfigStore.cs
    │   ├── ConnectionStringExtractor.cs
    │   └── OutputWindowHelper.cs
    └── ...
```

---

## ۴. معماری و جریان اجرا

```text
[راست‌کلیک روی Project]
        │
        ▼
[منوی Entity Framework Core]
        │
        ▼
[AllCommand — async handler]
        │
        ▼
[Form1 — UI و validation]
        │
        ├── EfCommandBuilder.Build()
        └── EfCommandRunner.ExecuteAsync()
                │
                ├── DotNetPathResolver
                ├── CliWrap
                └── OutputWindowHelper
```

### ۴.۱ لایه Services

| کلاس | مسئولیت |
|------|---------|
| `DotNetPathResolver` | یافتن `dotnet.exe` |
| `EfCommandBuilder` | ساخت رشته آرگومان `ef ...` |
| `EfCommandRunner` | اجرای دستور و نمایش خروجی |
| `EfCoreUiConfigStore` | خواندن/نوشتن `.vs/EfCoreUiConfigFile.json` |
| `ConnectionStringExtractor` | استخراج Connection String از JSON |
| `OutputWindowHelper` | ایجاد Output Pane و لاگ |

### ۴.۲ تشخیص پروژه‌ها

**Startup Project:** `OutputType` ≠ Library و بدون reference تست.

**Migration Project:** پروژه‌هایی که Reference به `Microsoft.EntityFrameworkCore` دارند، یا حداقل یک `DbContext` مشتق از:

- `Microsoft.EntityFrameworkCore.DbContext`
- `System.Data.Entity.DbContext`

---

## ۵. قابلیت‌ها

| عملیات | دستور معادل CLI |
|--------|-----------------|
| Add Migration | `dotnet ef migrations add` |
| Remove Last Migration | `dotnet ef migrations remove --force` |
| Generate SQL Script | `dotnet ef migrations script` |
| Update Database | `dotnet ef database update` |
| Drop Database | `dotnet ef database drop --force` |

---

## ۶. Changelog نسخه 2.08

### رفع باگ

- حذف مسیر ثابت `C:\Program Files\dotnet\dotnet.exe` — جستجوی خودکار dotnet
- رفع deadlock بالقوه در `AllCommand` (حذف `.Result`)
- فرم بعد از خطا بسته نمی‌شود؛ دکمه Execute غیرفعال و پس از خطا دوباره فعال می‌شود
- اصلاح connection string در Update Database (استفاده از `comboBoxConnection` به‌جای migration combo)

### بهبود

- استخراج منطق به لایه `Services/`
- اجرای دستورات با **CliWrap** به‌جای `Process` خام
- handlerهای async با try/catch و log به Output Window
- فیلتر دقیق‌تر DbContext (بدون false positive از `.Contains("dbcontext")`)
- فیلتر Migration Project بر اساس EF Core reference
- config گسترش‌یافته در `.vs/EfCoreUiConfigFile.json`

### manifest و packaging

- Identity VSIX: `EfCoreUI.ebc275fe-8195-4ca0-a424-8e13475dc26d`
- پشتیبانی VS 2022 و 2025: `[17.0, 19.0)`
- نصب روی Community، Professional و Enterprise
- همگام‌سازی AssemblyVersion با VSIX
- حذف `Form2` (کد مرده) و وابستگی‌های unused

---

## ۷. پیش‌نیازها و بیلد

1. Visual Studio 2022+ با workload **Visual Studio extension development**
2. `dotnet-ef` نصب‌شده به‌صورت global tool
3. باز کردن `EfCoreUi.sln` و Debug با Experimental Instance

---

*این مستند بر اساس سورس‌کد نسخه 2.08 تهیه شده است.*
