FROM mcr.microsoft.com/dotnet/core/runtime:2.1-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["Wk.DotNetCore.Perf/Wk.DotNetCore.Perf.csproj", "Wk.DotNetCore.Perf/"]
RUN dotnet restore "Wk.DotNetCore.Perf/Wk.DotNetCore.Perf.csproj"
COPY . .
WORKDIR "/src/Wk.DotNetCore.Perf"
RUN dotnet build "Wk.DotNetCore.Perf.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Wk.DotNetCore.Perf.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Wk.DotNetCore.Perf.dll"]