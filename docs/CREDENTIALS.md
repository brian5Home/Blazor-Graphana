# Credentials

Default credentials for services in this solution. **Change these in production.**

## Grafana

| Field    | Value   |
|----------|---------|
| **URL**  | http://localhost:3000 |
| **User** | `admin` |
| **Password** | `admin` |

Set via environment in `docker-compose.yml`:
- `GF_SECURITY_ADMIN_USER`
- `GF_SECURITY_ADMIN_PASSWORD`

---

## SQL Server

| Field    | Value   |
|----------|---------|
| **Server**  | `localhost,1433` (from host) or `sqlserver` (from Docker network) |
| **Database** | `BlazorGrafanaDb` |
| **User** | `sa` |
| **Password** | `YourStrong!Passw0rd` |

Set via environment in `docker-compose.yml`:
- `MSSQL_SA_PASSWORD`

Connection string used by the API:
`Server=sqlserver;Database=BlazorGrafanaDb;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;`

---

## Other services

- **Loki**, **Tempo**, **OpenTelemetry Collector**: No built-in authentication in this setup; they are intended for use inside the Docker network or localhost.
- **Blazor App** / **API**: No login; unauthenticated access for this sample.
