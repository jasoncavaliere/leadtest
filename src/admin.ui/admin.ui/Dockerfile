#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
ARG buildVersion=1.0.0-alpha
ARG buildConfiguration=Release
WORKDIR /src
COPY ["admin.ui/admin.ui.csproj", "admin.ui/"]
RUN dotnet restore "admin.ui/admin.ui.csproj"
COPY . .
WORKDIR "/src/admin.ui"

RUN ["sh", "-c", "dotnet build \"admin.ui.csproj\" -c ${buildConfiguration} -o /app/build /property:Version=\"${buildVersion}\"" ]

FROM build AS publish
ARG buildVersion=1.0.0-alpha
ARG buildConfiguration=Release
RUN dotnet publish "admin.ui.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "admin.ui.dll"]