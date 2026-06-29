# testproject

Basit örnek: React ana sayfa, backend `appsettings.json` değerini API üzerinden gösterir.

## Yapı

- `backend/TestProject.Api` — ASP.NET Core API (`AppSettings:DisplayMessage`)
- `frontend` — React (Vite); `dist/` üretilmiş statik çıktı

## Çalıştırma

**Backend**

```bash
cd backend/TestProject.Api
dotnet run
```

API: `http://localhost:5018/api/config`

**Frontend (geliştirme)**

```bash
cd frontend
npm install
npm run dev
```

**Frontend build**

```bash
cd frontend
npm install
npm run build
```

`appsettings.json` içindeki `AppSettings:DisplayMessage` değerini değiştirip API'yi yeniden başlatın; React ana sayfa bu metni gösterir.
