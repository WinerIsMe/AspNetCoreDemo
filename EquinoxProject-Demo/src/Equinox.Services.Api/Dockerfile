#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Equinox.Services.Api/Equinox.Services.Api.csproj", "src/Equinox.Services.Api/"]
COPY ["src/Equinox.Infra.CrossCutting.IoC/Equinox.Infra.CrossCutting.IoC.csproj", "src/Equinox.Infra.CrossCutting.IoC/"]
COPY ["src/Equinox.Domain/Equinox.Domain.csproj", "src/Equinox.Domain/"]
COPY ["src/Equinox.Infra.CrossCutting.Bus/Equinox.Infra.CrossCutting.Bus.csproj", "src/Equinox.Infra.CrossCutting.Bus/"]
COPY ["src/Equinox.Domain.Core/Equinox.Domain.Core.csproj", "src/Equinox.Domain.Core/"]
COPY ["src/Equinox.Infra.Data/Equinox.Infra.Data.csproj", "src/Equinox.Infra.Data/"]
COPY ["src/Equinox.Infra.CrossCutting.Identity/Equinox.Infra.CrossCutting.Identity.csproj", "src/Equinox.Infra.CrossCutting.Identity/"]
COPY ["src/Equinox.Application/Equinox.Application.csproj", "src/Equinox.Application/"]
RUN dotnet restore "src/Equinox.Services.Api/Equinox.Services.Api.csproj"
COPY . .
WORKDIR "/src/src/Equinox.Services.Api"
RUN dotnet build "Equinox.Services.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Equinox.Services.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Equinox.Services.Api.dll"]