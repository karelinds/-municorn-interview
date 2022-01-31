#!/bin/bash
docker stop notification_service
docker rm notification_service
docker rmi notification_service_img
sdk=$(docker images -q mcr.microsoft.com/dotnet/aspnet)
echo $sdk
docker rmi $sdk
aspnet=$(docker images -q mcr.microsoft.com/dotnet/aspnet)
docker rmi $aspnet