FROM node:alpine

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["TwitterStatisticApp/TwitterStatisticApp.csproj", "TwitterStatisticApp/"]
COPY ["TwitterStatisticApp.Application/TwitterStatisticApp.Application.csproj", "TwitterStatisticApp.Application/"]
COPY ["TwitterStatisticApp.Infra.Data/TwitterStatisticApp.Infra.Data.csproj", "TwitterStatisticApp.Infra.Data/"]
COPY ["TwitterStatisticApp.Domain/TwitterStatisticApp.Domain.csproj", "TwitterStatisticApp.Domain/"]
RUN dotnet restore "TwitterStatisticApp/TwitterStatisticApp.csproj"
COPY . .
WORKDIR "/src/TwitterStatisticApp"
RUN dotnet build "TwitterStatisticApp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TwitterStatisticApp.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TwitterStatisticApp.dll"]