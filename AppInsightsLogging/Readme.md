# Integrate Azure Application Insights in .NET

Steps
1. Create Application Insights in Azure
2. option a. In Visual Studio Right click on project and Add->Add Application Insights Telementry. This will add Nuget packages and configurations to application
   option b. 
      i. Add Nuget Microsoft.ApplicationInsights.AspNetCore
      ii. Configure services in startup   services.AddApplicationInsightsTelemetry();
      iii. Add ApplicationInsights section in appSettings.json
3. ILogger help to load errors, warning message

Advantages using Applications Insights
a. Track all unhandler exceptions
b. Track loggings
c. Track user activity
d. Track response times
e. Track active sessions and users and more...
