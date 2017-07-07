FROM microsoft/aspnetcore-build:lts

ENV ASPNETCORE_ENVIRONMENT=Production

ADD ./output /app
WORKDIR /app

ENTRYPOINT ["dotnet", "yujvidya-web.dll"]
