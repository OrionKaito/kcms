FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src/kcms
WORKDIR /src/KCMS.Domain
WORKDIR /src/KCMS.Infrastructure
WORKDIR /src
COPY ["kcms/kcms.csproj", "./kcms"]
COPY ["KCMS.Domain/KCMS.Domain.csproj", "./KCMS.Domain"]
COPY ["KCMS.Infrastructure/KCMS.Infrastructure.csproj", "./KCMS.Infrastructure"]
RUN dotnet restore "./kcms/kcms.csproj"
COPY . .
RUN dotnet build "kcms/kcms.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "kcms/kcms.csproj" -c Release -o /app/publish
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN rm -rf appsettings.json
# COPY . .
# WORKDIR /app/kcms
RUN ls
# RUN dotnet dev-certs https --trust
# RUN dotnet sdk check
ENV ASPNETCORE_URLS http://0.0.0.0:80
EXPOSE 80
# To enable development, set env like below
# ENV ASPNETCORE_ENVIRONMENT Development
ENTRYPOINT ["dotnet", "kcms.dll"]