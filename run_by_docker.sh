#!/bin/bash
docker stop notification_service
docker rm notification_service
docker rmi notification_service_img
cd NotificationService
docker build . -t notification_service_img
docker run -d --name notification_service -p 10500:80 notification_service_img
sleep 1s
open http://localhost:10500/swagger/index.html