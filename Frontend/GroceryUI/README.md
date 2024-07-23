# GroceryUI

This project is a web application for managing a grocery store. It consists of an Angular frontend and an ASP.NET 6 backend.

## Getting Started

Before running the project, make sure you have the following dependencies installed:

- Node.js (version v18.16.0)
- Angular CLI (version 15.0.0)
- .NET SDK (version 6.0)

1. Navigate to the project directory
   cd GroceryUI
2. Install the required frontend dependencies:
    npm install
3. Update the backend server URL:

4. Open each service file in the src/app/services directory.
Look for the server URL and replace it with the appropriate value. (e.g., http://localhost:5024)->with your url of backend on which your backend is running

5. Start the frontend 
   ng serve

# Backend Setup
Open the backend solution file (GroceryAPI.sln) in Visual Studio or your preferred code editor.

Configure the database connection string:

Open the appsettings.json file.
Locate the "DefaultConnection" property and update it with the connection string for your database.
Build the backend solution.

Run the backend server:

Press F5 or use the debugging tools of your chosen development environment to run the application.
Note: Make sure the backend server is running on http://localhost:5024 etc.
# Adding Product Images
When an admin adds a product through the "Add Product" feature, they need to follow these steps:

Place the product image in the src/assets/Images folder.

Use the relative path of the image in the "Image URL" section of the product form, following this format:
    ../../../assets/Images/productimagename.jpeg

# Dependencies
This project relies on the following dependencies:

@angular/animations: ^15.2.0
@angular/common: ^15.2.0
@angular/compiler: ^15.2.0
@angular/core: ^15.2.0
@angular/forms: ^15.2.0
@angular/platform-browser: ^15.2.0
@angular/platform-browser-dynamic: ^15.2.0
@angular/router: ^15.2.0
@auth0/angular-jwt: ^5.1.2
@ng-bootstrap/ng-bootstrap: ^14.2.0
@popperjs/core: ^2.11.6
bootstrap: ^5.2.3
ng-angular-popup: ^0.3.1
ngx-pagination: ^6.0.3
rxjs: ~7.8.0
tslib: ^2.3.0
zone.js: ~0.12.0
.NET SDK (version 6.0)
Please ensure that these dependencies are installed before running the project.


## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.


