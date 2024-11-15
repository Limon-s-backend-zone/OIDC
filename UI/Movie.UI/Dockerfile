FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["../UI/Movie.UI/Movie.UI.csproj", "../UI/Movie.UI/"]
RUN dotnet restore "../UI/Movie.UI/Movie.UI.csproj"
COPY . .
WORKDIR "/src/../UI/Movie.UI"
RUN dotnet build "Movie.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Movie.UI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Movie.UI.dll"]
