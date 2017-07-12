FROM microsoft/aspnetcore-build:lts

ARG build_env

ENV ASPNETCORE_ENVIRONMENT=Production

ADD ./output /app
WORKDIR /app

ENTRYPOINT ["dotnet ", "yujvidya.dll"]
