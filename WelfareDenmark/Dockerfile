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

# Set password for the certificate as 1234
# I'm using Environment Variable here to simplify the .NET Core sample.
ENV certPassword 1234

# Use opnssl to generate a self signed certificate cert.pfx with password $env:certPassword
RUN openssl genrsa -des3 -passout pass:${certPassword} -out server.key 2048
RUN openssl rsa -passin pass:${certPassword} -in server.key -out server.key
RUN openssl req -sha256 -new -key server.key -out server.csr -subj '/CN=localhost'
RUN openssl x509 -req -sha256 -days 365 -in server.csr -signkey server.key -out server.crt
RUN openssl pkcs12 -export -out cert.pfx -inkey server.key -in server.crt -certfile server.crt -passout pass:${certPassword}

# Expose port 443 for the application.
EXPOSE 443
ENTRYPOINT ["dotnet", "WelfareDenmark.dll"]