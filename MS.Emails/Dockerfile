FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:6.0
ENV TZ="America/Sao_Paulo"
ENV MS_EMAIL_CONNSTRING=${MS_EMAIL_CONNSTRING}
ENV MS_RABBITMQ_HOST=${MS_RABBITMQ_HOST}
ENV MS_RABBITMQ_PORT=${MS_RABBITMQ_PORT}
ENV MS_RABBITMQ_USER=${MS_RABBITMQ_USER}
ENV MS_RABBITMQ_PASSWORD=${MS_RABBITMQ_PASSWORD}
ENV MS_RABBITMQ_VHOST=${MS_RABBITMQ_VHOST}
ENV MS_SMTP_SERVER=${MS_SMTP_SERVER}
ENV MS_SMTP_PORT=${MS_SMTP_PORT}
ENV MS_SMTP_USERNAME=${MS_SMTP_USERNAME}
ENV MS_SMTP_PASSWORD=${MS_SMTP_PASSWORD}
ENV MS_EMAIL_URLBASE=${MS_EMAIL_URLBASE}
ENV MS_EMAIL_CORS_DOMAINS=${MS_EMAIL_CORS_DOMAINS}
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "MS.Emails.dll"]