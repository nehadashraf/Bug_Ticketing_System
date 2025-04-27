# Bug Ticketing System

## Overview
The **Bug Ticketing System** is a web application designed to help software teams manage bugs and issues in their projects. The system enables users (Managers, Developers, and Testers) to track, report, and resolve bugs efficiently. Users can create, view, and manage bugs, handle user accounts, and manage attachments related to bugs.

## Key Features
- **User Management**: Register, authenticate, and manage users with different roles (Manager, Developer, Tester).
- **Project Management**: Create and view projects, with each project containing multiple bugs.
- **Bug Management**: Report and manage bugs with details, assignees, and attachments.
- **File Management**: Upload and manage attachments related to bugs, such as images or logs.
- **User-Bug Relationships**: Assign and unassign users (Managers, Developers, Testers) to bugs.

## API Endpoints

### User Management
- **Register User**: Create a new user account.
  - `POST /api/users/register`
- **Login User**: Authenticate user and provide a token.
  - `POST /api/users/login`

### Project Management
- **Create Project**: Add a new project.
  - `POST /api/projects`
- **Get All Projects**: List all projects.
  - `GET /api/projects`
- **Get Project Details**: View specific project information and bugs.
  - `GET /api/projects/:id`

### Bug Management
- **Create Bug**: Report a new bug.
  - `POST /api/bugs`
- **Get All Bugs**: List all bugs.
  - `GET /api/bugs`
- **Get Bug Details**: View detailed info on a specific bug.
  - `GET /api/bugs/:id`

### User-Bug Relationships
- **Assign User to Bug**: Assign a user to a bug.
  - `POST /api/bugs/:id/assignees`
- **Remove User from Bug**: Unassign a user from a bug.
  - `DELETE /api/bugs/:id/assignees/:userId`

### File Management
- **Upload Attachment**: Add an attachment to a bug.
  - `POST /api/bugs/:id/attachments`
- **Get Attachments for Bug**: Retrieve all attachments for a bug.
  - `GET /api/bugs/:id/attachments`
- **Delete Attachment**: Remove an attachment from a bug.
  - `DELETE /api/bugs/:id/attachments/:attachmentId`

## Technologies Used
- **Backend**: ASP.NET Core
- **Database**: SQL Server
- **Authentication**: JWT (JSON Web Tokens)
