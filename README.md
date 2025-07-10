# **Pokemon viewer web application**
A full-stack Vue.js + C# .NET web application for searching and and retrieving Pokemon data from the Pokemon API.

You can find the official documentation of the Pokemon API here: https://pokeapi.co/docs/v2


![Alt text](https://cdn.europosters.eu/image/750/59768.jpg)

# Description
After login users can search Pokemons by name or id. After hitting the Submit button (using valid parameter) the details (name, sprite, height, weight, abilities, types) of the Pokemon are displayed.
If the Pokemon wasn't requested before, it gets added to the database. Otherwise we only make changes to the PokemonRequest table, which acts like a log table (logs the requested pokemon ids, and the user data).
A Pokemon can have one or two types. There are 18 official types, which are stored in the Type table. The types of the requested pokemons are stored in the PokemonTypes table. 
A Pokemon can have a maximum of three abilities. The PokemonAbilities table stores the abilities associated with a Pokemon. There are more than 300 official abilities so we add the abilities of the Pokemon to the Ability table. One ability can belong to many Pokemons so we only add the new abilities to the Ability table .

![Alt text](https://i.postimg.cc/xdvvLdG1/draw-SQL-image-export-2025-07-09.png)



# Tech stack
**Frontend:** Vue 3 (Vite, Composition API, Vitest)  
**Backend:** ASP .NET Core RESTful API API (C#), Entity Framework Core  
**Authentication:** Auth0 (JWT-based)  
**Database:** SQL  

# Setup

Visual Studio 2022 is required to start the application locally.
OR
In terminal from the PokemonApp.Server folder by these commands:
```
dotnet ef database update
dotnet run --launch-profile "https"
```

By default, the backend API is hosted at: **https://localhost:55481/**

> Note: Don't forget to change the connection string in the appsettings.json accordingly.

# API Testing
#### Postman testing:
The GET endpoint is available at: https://localhost:7222/api/pokemon/pikachu (you need to run the application from VS for this)
The endpoint requires a Bearer token, which needs to be passed in the Authorization header. 
Bearer tokens can be requested by using the following cURL command: (replace AUTH0DOMAIN, CLIENTID AND CLIENTSECRET with the original values).

```
curl --request POST \
  --url AUTH0DOMAIN/oauth/token \
  --header 'content-type: application/json' \
  --data '{
    "client_id":CLIENTID,
    "client_secret":CLIENTSECRET,
    "audience":"https://localhost:7222/api/pokemon/",
    "grant_type":"client_credentials",
     "scope":"read:pokemon"
  }'
```

#### Testing from frontend:
Testing the endpoint from the frontend, you don't need to get a token, it's handled automatically by Auth0. Only logged in  users (either with Auth0 or with Google Account) can interact with the endpoint. 
> Note: There's a known issue with logging in with Google using Auth0. It's recommended to try using incognito mode ot temporarily disabling browser extensions when trying to login with Google.

# Testing
To run the frontend tests you need to run this from the pokemonapp.client folder:
```
npm run test
```