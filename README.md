###API.Home.Kelly

####WIP

This API will form the framework for serving between multiple databases and client apps in my home environment. Much more to come here. As a first step, I am setting up the API, a database for long term food storage items, and a client app so that we can scan in our pantry items to better track use and replacement of shelf stable foodstuffs.

I hope to eventually include inventory functionality for other home goods, most likely our library of physical books. 

This API will also serve data for [our dashboard](https://github.com/Malechus/HomeDashboard) such as weekly chore assignments.

This API will (eventually) use Windows authentication on my home domain.

This API is licensed using a standard MIT License.

This API uses calls to https://upcdatabase.org/api to lookup product UPCs.
Building this API requires a web.config file with the following:
```
<appSettings>
  <add key="UPCDatabaseToken" value="<YourAPITokenHere>"/>
</appSettings>
```