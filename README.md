# Configuration Manager

A full-stack application for managing configuration settings with a Vue.js frontend and ASP.NET 9 backend.

## Project Overview

The Configuration Manager is a web application that allows users to:
- View and manage configuration settings
- Add new configuration items
- Filter and sort existing configurations
- Delete configuration items

The application consists of two main components:

### Backend (ASP.NET Core 9)
Located in `Service/ConfigManagerApp.API/`, the backend provides a RESTful API for managing configurations. It's built with:
- ASP.NET Core 9
- Scalar for API documentation

For detailed backend documentation, see [Service/README.md](Service/README.md)

### Frontend (Vue.js 3)
Located in `UI/config-manager-app/`, the frontend provides a user-friendly interface for interacting with the configuration management. It's built with:
- Vue.js 3
- TypeScript
- Axios for API communication

For detailed frontend documentation, see [UI/config-manager-app/README.md](UI/config-manager-app/README.md)