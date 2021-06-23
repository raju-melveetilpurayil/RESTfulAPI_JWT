# Example of RESTful Web API with JWT Implementation in .Net Core
Implementation of JWT token in .Net Core. This projec uses Entity Framework 6, Repository, Service layer and JWT.

## Project Includes

- .Net Core 3.1 Web API
- JWT Token Implimentation 
- Generic Repository Layer
- Generic Service Layer 
- Entity Framework Data Layer
- Helper classes for Encrypt password and reader configuration settings

## Packages used
```
Microsoft.AspNetCore.Authentication.JwtBearer
System.IdentityModel.Tokens.Jwt
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SqlServer

```

## How to run?
1. Download or clone project [`https://github.com/raju-melveetilpurayil/RESTfulAPI_JWT.git`](https://github.com/raju-melveetilpurayil/RESTfulAPI_JWT.git)
2. Open in visual studio, then Build and Run
3. You need [Postman](https://www.postman.com/) or [Fiddler](https://www.telerik.com/fiddler)to test this application because there is no UI includeden in this project. 
4. You can create a user by calling `api/user/register` by passing `email` and `password` parameter *There is no model validation included in this project*
5. Call `api\user\login` by passing email and password to get the user token.
6: You can copy the user token and call `api\user\Dashboard` with token as Bearer Token in the header of the request.

## Sample Requests screens
[`api/user/register`](https://github.com/raju-melveetilpurayil/RESTfulAPI_JWT/blob/main/RESTfulAPI/images/register.PNG)

[`api/user/login`](https://github.com/raju-melveetilpurayil/RESTfulAPI_JWT/blob/main/RESTfulAPI/images/login.PNG)

[`api/user/dashboard`](https://github.com/raju-melveetilpurayil/RESTfulAPI_JWT/blob/main/RESTfulAPI/images/dashboard.PNG)


### Database Table script
Create a database `RESTful_JWT` then run below script to add `User` table

```
USE [RESTful_JWT]
GO
/****** Object:  Table [dbo].[User]    Script Date: 24/06/2021 00:17:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](150) NULL,
	[Password] [nvarchar](500) NULL,
	[DateCreated] [datetime] NULL,
 CONSTRAINT [PK_FTOIUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF,
IGNORE_DUP_KEY = OFF,
ALLOW_ROW_LOCKS = ON,
ALLOW_PAGE_LOCKS = ON, 
OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
```
