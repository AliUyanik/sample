# testproject

Basit örnek: React ana sayfa, backend `AppSettings:DisplayMessage` değerini API üzerinden gösterir.

## Yapı

- `backend/TestProject.Api` — ASP.NET Core API (`AppSettings:DisplayMessage`, `/health`, `/api/config`)
- `backend/TestProject.Text` — büyük harfleri küçük harfe çeviren yardımcı kütüphane (API `/api/config` yanıtında kullanır)
- `frontend` — React (Vite); `dist/` üretilmiş statik çıktı

## Çalıştırma

**Backend**

```bash
cd backend/TestProject.Api
dotnet run
```

- Health: `http://localhost:8080/health`
- API: `http://localhost:8080/api/config`

Konteyner / Docker imajında dinlenen port **8080** (`ASPNETCORE_HTTP_PORTS`).

**Frontend (geliştirme)**

```bash
cd frontend
npm install
npm run dev
```

Vite proxy: `/api` → `http://localhost:8080`

**Frontend build**

```bash
cd frontend
npm install
npm run build
```

## Ortam değişkenleri (Vault / deploy)

API, configuration key `AppSettings:DisplayMessage` okur. Linux konteyner / compose ortamında:

```bash
AppSettings__DisplayMessage=Vault'tan gelen metin
```

AppGraph vault binding:

| Alan | Değer |
|------|--------|
| Anahtar | `AppSettings__DisplayMessage` |
| Secret | `newproject` (örnek) |

Vault KV alan adı da **aynı** olmalı: `AppSettings__DisplayMessage`

## Nginx

Production’da React `fetch('/api/config')` kullanır; reverse proxy `/api` → API servisi (**pass-through**).
