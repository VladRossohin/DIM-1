#### How to create and publish a new data base object (steps with pics)

:one: [Add new object into data base project](https://ibb.co/8xdd9q9)

:two: [Start publishing your data base locally](https://ibb.co/QMgXTDW)

:three: [Connect to sql local server](https://ibb.co/PFPN26C)

:heavy_exclamation_mark: to connect to data base as SA user you need to enable sa user and sql authentication, please follow this [guide](https://www.youtube.com/watch?v=Ckb-YoHsuOE)

:heavy_exclamation_mark: to add seed data `automaticaly` create post-deployment script and set appropriate `build action`

:four: [If you get an error investigate and try to resolve it](https://ibb.co/Tvh1C0T)

:five: [After publishing check you data base in SQL Management Studio ](https://ibb.co/QcT7ccC)

#### How to update model from database

After you `successfully` published a **`new data table, view, stored procedure or change any entity in data base`** in project right-click anywhere on the design surface, and select Update Model from Database.
In the **`Update Wizard`**, set **`Connection`**, select the **`Refresh`** tab and then select what you want to update/add. Click **`Finish`**.
Follow the example [here](https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/database-first-development/changing-the-database) if you have questions.