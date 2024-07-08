#!/bin/sh
# Substitute environment variable values into servers.json
envsubst < /pgadmin4/pgpass.template > /pgadmin4/.pgpass
envsubst < /pgadmin4/servers.json.template > /pgadmin4/servers.json
cp -f /pgadmin4/.pgpass /var/lib/pgadmin/
chmod 0600 /var/lib/pgadmin/.pgpass

# Execute the original entrypoint with any passed command
exec /entrypoint.sh