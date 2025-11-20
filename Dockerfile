
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS="http://+:8080;http://+:8081"
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["MemberService.API/MemberService.API.csproj", "MemberService.API/"]
COPY ["MemberService.BO/MemberService.BO.csproj", "MemberService.BO/"]
COPY ["MemberService.DAO/MemberService.DAO.csproj", "MemberService.DAO/"]
COPY ["MemberService.Repository/MemberService.Repository.csproj", "MemberService.Repository/"]
COPY ["MemberService.Service/MemberService.Service.csproj", "MemberService.Service/"]

RUN dotnet restore "MemberService.API/MemberService.API.csproj"

COPY . .
WORKDIR "/src/MemberService.API"
RUN dotnet build "./MemberService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MemberService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

USER app
ENTRYPOINT ["dotnet", "MemberService.API.dll"]