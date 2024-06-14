#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
#EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RoadDefectsService.Presentation.Web/RoadDefectsService.Presentation.Web.csproj", "RoadDefectsService.Presentation.Web/"]
COPY ["RoadDefectsService.Core.Application/RoadDefectsService.Core.Application.csproj", "RoadDefectsService.Core.Application/"]
COPY ["RoadDefectsService.Core.Domain/RoadDefectsService.Core.Domain.csproj", "RoadDefectsService.Core.Domain/"]
COPY ["RoadDefectsService.Infrastructure.DinkToPdf/RoadDefectsService.Infrastructure.DinkToPdf.csproj", "RoadDefectsService.Infrastructure.DinkToPdf/"]
COPY ["RoadDefectsService.Infrastructure.Identity/RoadDefectsService.Infrastructure.Identity.csproj", "RoadDefectsService.Infrastructure.Identity/"]
COPY ["RoadDefectsService.Infrastructure.SMTP/RoadDefectsService.Infrastructure.SMTP.csproj", "RoadDefectsService.Infrastructure.SMTP/"]
RUN dotnet restore "./RoadDefectsService.Presentation.Web/RoadDefectsService.Presentation.Web.csproj"
COPY . .
WORKDIR "/src/RoadDefectsService.Presentation.Web"
RUN dotnet build "./RoadDefectsService.Presentation.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RoadDefectsService.Presentation.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["RoadDefectsService.Presentation.Web/Libs/", "Libs/"]
ENTRYPOINT ["dotnet", "RoadDefectsService.Presentation.Web.dll"]
