# Configuration Manager Application (Vue.Js 3)

## Table of Contents
- [Project Overview](#project-overview)
- [Prerequisites](#prerequisites)
- [Setup Instructions](#setup-instructions)
- [Environment Configuration](#environment-configuration)
- [Running the Application](#running-the-application)
- [Project Structure](#project-structure)
- [Available Scripts](#available-scripts)
- [GitHub Workflow](#github-workflow)

## Project Overview
This Vue.js application consumes the [Configuration Management API](https://configurationmanager-grara4f3cghrcye2.canadacentral-01.azurewebsites.net/scalar/v1) (built with ASP.NET 9) to:
- Retrieve and display configurations
- Create configuration items
- Filter and Sort configuration items
- Delete configuration items

## Prerequisites
- Node.js 18+
- Vue CLI 5+
- Access to the Configuration Manager API (running locally or deployed)


## Setup Instructions

1. **Clone the repository**
   ```bash
   git clone https://github.com/osaibi-rivaengine/ConfigurationManager.git
   cd ConfigurationManager\UI\config-manager-app
2. **Install dependencies**
   ```bash
   npm install  
3. **Check code style and fix potential errors**
   ```bash
   npm run lint --fix 
## Environment Configuration  

The application uses environment-specific configuration files to manage different settings for development and production environments.

### Development Environment (.env.development)
Create a `.env.development` file in the root directory with the following variables:
```bash
VUE_APP_API_URL=http://localhost:8081/api
```
### Production Environment (.env.production)
Create a `.env.production` file in the root directory with the following variables:
```bash
VUE_APP_API_URL=https://configurationmanager-grara4f3cghrcye2.canadacentral-01.azurewebsites.net/api
```
## Running the Application
- Start the project for development environment  
   ```bash
   npm run serve --mode=development  
- Start the project for production environment  
   ```bash
   npm run serve --mode=production  
## Project Structure
```
config-manager-app/
├── public/                 # Static files
├── src/
│   ├── assets/            # Images, fonts, and other static assets
│   ├── components/        # Reusable Vue components
│   ├── Models/            # Models files
│   ├── Provider/          # containes ConfigCLient.ts and ConfigDataProvider.ts
│   └── App.vue            # Root component
└── .env.*                 # Environment configuration files
```

## Available Scripts
- `npm run serve` - Start development server
- `npm run build` - Build for production
- `npm run lint` - Lint and fix files

## GitHub Workflow

The `.github/workflows/build_deploy_ui.yml` file automates:

1. **Build Process**:
   - Runs on: ubuntu-latest
   - Build the project using Azure action `Azure/static-web-apps-deploy@v1`.
   
2. **Deployment to Azure**:
   - Upload the dist folder to Azure State Web Apps. 

The `build_deploy_ui.yml` Workflow will be triggered for every commit to "main" branch.