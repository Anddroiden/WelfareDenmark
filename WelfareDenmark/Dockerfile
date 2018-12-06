FROM microsoft/dotnet:sdk AS build-env2
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out
RUN dotnet ef database update


# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env2 /app/out .
COPY --from=build-env2 /app/app.db .
ENTRYPOINT ["dotnet", "WelfareDenmark.dll"]