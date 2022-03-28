# _Bakery_

#### By _**Tiffany Rodrigo**_

#### Many to many ralationship with authentication using Identity in C# ASP.Net Core MVC

## Technologies Used
* _C#_
* _.Net 5.0_
* _ASP.Net Core_
* _Razor_
* _MySQL_
* _EntityFramework Core_
* _CSS_
* _Linq_
* _HtmlHelper_



## Description

An application with user authentication and a many-to-many relationship. Here are the features  in the application:

* _The application should have user authentication. A user should be able to log in and log out. Only logged in users should have create, update and delete functionality. All users should be able to have read functionality._
* _There should be a many-to-many relationship between Treats and Flavors. A treat can have many flavors (such as sweet, savory, spicy, or creamy) and a flavor can have many treats. For instance, the "sweet" flavor could include chocolate croissants, cheesecake, and so on._
* _A user should be able to navigate to a splash page that lists all treats and flavors. Users should be able to click on an individual treat or flavor to see all the treats/flavors that belong to it._


._


_

## Setup/Installation Requirements
* _Install the MySQL Community Server & MySQL Workbench_
* _Install .NET5_
* _Clone this repository to your desktop._
* _Import sql database :
* _Add a new file called appsettings.json in the project's production folder and store the following { "ConnectionStrings": { "DefaultConnection": "Server=localhost;Port=3306;database=[NAME-OF-THE-DATABASE-YOU-CREATED-ABOVE];uid=root;pwd=[YOUR-PASSWORD-HERE];" } }_
* _Create your own database by Navigating to the Bakery directory in terminal run the following command:_
  * _Run "dotnet restore" in Terminal then hit enter_
  * _Run "dotnet build" to Terminal_
  * _Run "dotnet ef migration add InitialCreate"_
  * _Run "dotnet ef database update"_
  * _Run dotnet run_
  * _Visit http://localhost:5000_


## License

_MIT_

Copyright (c) _Mar_ 27th 2022_ _Tiffany Rodrigo_




