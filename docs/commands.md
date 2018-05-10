dotnet publish -o ./output

docker-compose -f docker-compose.yml -f docker-compose.override.yml -f docker-compose.vs.debug.yml -p yugvidya_debug config

docker-compose -f docker-compose.yml -f docker-compose.override.yml -f docker-compose.vs.debug.yml -p yugvidya_debug up -d

dotnet publish -o ./output

docker build --no-cache -t yujvidya .

docker run -it --rm -p 82:80 --entrypoint bash yujvidya

docker run -it --rm -p 82:80 yujvidya

docker run -d -p 5432:5432 --name pg-db -e POSTGRES_PASSWORD=supersecret postgres

docker run -it --rm --link pg-db:postgres postgres psql -h postgres -U postgres

docker run -it -p 82:80 --link pg-db:postgres yujvidya