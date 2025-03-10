# Blazor/Aspire Demo

## Containers
- This demo uses Docker Desktop: https://www.docker.com/products/docker-desktop/

## Overview
### Web front-end created using Fluent Blazor components: https://www.fluentui-blazor.net/
- includes demo of my own custom FluentUI components
### Html ResizeObserver
- Demo on how to use ResizeObserver in Blazor with my custom component: https://github.com/marqdouj/HtmlResizeObserver
### Email service using MailDev/MailKit for development:
- MailDev: https://github.com/maildev/maildev
- MailKit: https://github.com/jstedfast/MailKit
#### NuGet packages that wrap MailDev/MailKit:
- https://www.nuget.org/packages/Marqdouj.Aspire.MailKit.Client/
- https://www.nuget.org/packages/Marqdouj.MailDev.Host/
### Email service using Azure for production:
- https://learn.microsoft.com/en-us/azure/communication-services/concepts/email/email-overview
### Custom exception handler in API that uses Email service to notify dev team.
### PIMS Demo: Fictional company the manages Oil/Gas pipeline integrity data.
- In-Line Inspection (ILI) data is displayed using Html Canvas and svg images.
- GPS Data is displayed using AzureMapsControl: https://github.com/arnaudleclerc/AzureMapsControl.Components
- Follow the instructions here to configure your free Azure Maps account: https://blazorhelpwebsite.com/ViewBlogPost/59
