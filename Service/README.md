# Configuration Management API Documentation ( ASP.NET 9 )

## Table of Contents
1. [Prerequisites](#prerequisites)
2. [Project Setup](#project-setup)
5. [Running the Application](#running-the-application)
6. [OpenAPI and Scalar API references](#openapi-and-scalar-api-references)
7. [GitHub Workflow](#github-workflow)

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [VS Code](https://code.visualstudio.com/download) 
- [Postman](https://www.postman.com/) (optional)
- [Git](https://git-scm.com/downloads)

## Project Setup

1. Clone the repository:
   ```bash
   git clone https://github.com/Osaibi-RivaEngine/configuration-manager.git
   cd ConfigurationManager\Service\ConfigManagerApp.API
2. Restore and Build API project:
   ```bash
   dotnet restore
   dotnet build
## Running the Application
1. Using Visual Studio:
   - Right click on "ConfigManagerApp.API" project and select Set as startup project
   - Press F5 or Debug to Start Debugging

2. Using CLI  
**Run the API project with HTTPS:**  
By default, .NET CLI will pick the first launch profile, which is Http by default. In order to run it with Https, copy and paste the following command in your terminal. For more details, please vist microsoft [docs](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-7.0#development-and-launchsettingsjson)
    ```bash
    dotnet watch --launch-profile https
    or
    dotnet run --launch-profile https   
## OpenAPI and Scalar API references
Scalar is a modern, customizable API documentation viewer designed as an alternative to Swagger UI. It works with standard OpenAPI/Swagger specifications but provides enhanced UX and customization options.  
Follow these steps to use Scalar API reference to test APIs:  
1. Run your application
    ```bash
    dotnet watch --launch-profile https
    or
    dotnet run --launch-profile https   
2. Navigate to:  
    ```bash
    https://localhost:7025/scalar/v1
3. Test an endpoint  
   a. Select any endpoint to expand it  
   b. Click on "Test request"  
   c. Fill in any required parameters  
   d. Click "Execute"  

Scalar provides samples in different language on how to consume the provided APIs. Feel free to try those samples. 

## GitHub Workflow

The `.github/workflows/build_deploy_api.yml` file automates:

1. **Build Process**:
   - Runs on: ubuntu-latest
   - Restores dependencies
   - Builds the solution
   - Runs tests

2. **Deployment to Azure**:
   - Deploys to: ConfigurationManager in Azure App Services using publish profile that is stored in GitHub secrets with key: AZURE_WEBAPP_PUBLISH_PROFILE  

The `build_deploy_api.yml` Workflow will be triggered for every commit to "main" branch.