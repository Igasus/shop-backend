FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/sdk:6.0 AS app-build
COPY . /home/shop-backend
WORKDIR /home/shop-backend
RUN dotnet publish -c Release -o ./bin/release/net6.0
RUN dotnet dev-certs https --trust

FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/aspnet:6.0
COPY --from=app-build /home/shop-backend/bin/release/net6.0 /home/releases/shop-backend
COPY --from=app-build /root/.dotnet/corefx/cryptography/x509stores/my /root/.dotnet/corefx/cryptography/x509stores/my
WORKDIR /home/releases/shop-backend
ENV ASPNETCORE_hostBuilder:reloadConfigOnChange=false
ENTRYPOINT ["dotnet", "Shop.Api.dll"]