# Documentación

Si avanza el proyecto, generar mas documentación, de momento solo son notas.

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

## User secrets

Copiar `compose/usersecrets/netclock-6c9ba5ff-19f7-4984-bfc9-281d3df753c2/secrets.json` a `~/.microsoft/usersecrets/`
