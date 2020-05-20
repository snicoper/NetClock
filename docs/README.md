# Documentación

Se avanza el proyecto, generar mas documentación, de momento solo son notas.

## PostgreSQL

Crear usuario `netclock` y contraseña `123456`

```bash
psql -U postgres

CREATE USER netclock WITH PASSWORD '123456' CREATEDB;
\q
```

Crear un backup `.sql`

```bash
pg_dump -U netclock NetClock >> ~/NetClock.sql
```
