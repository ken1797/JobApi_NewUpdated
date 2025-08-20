# Jobs API 

Solution for the interview test

## Quick Start
```bash
# 1) Restore
dotnet restore

# 2) Run Web API
cd src/Teknorix.JobsApi.WebApi
dotnet run
```

The API will create a local SQLite DB `jobs.db` with seed Departments & Locations.

## Auth
POST `/api/auth/login`
- Users:
  - admin / admin123 (Admin role)
  - viewer / viewer123 (Viewer role)
Use the returned JWT as `Authorization: Bearer <token>`

## Endpoints (per spec)
- POST `/api/v1/jobs` -> 201 Created (Admin only)
- PUT `/api/v1/jobs/{id}` -> 200 OK (Admin only)
- POST `/api/jobs/list` -> paged list (Anonymous)
- GET `/api/v1/jobs/{id}` -> details (Anonymous)
- Departments: POST/PUT/GET `/api/v1/departments`
- Locations: POST/PUT/GET `/api/v1/locations`

## Tests
```bash
dotnet test
```

## Db Script
Running SQL manually, see `db/schema.sql`
