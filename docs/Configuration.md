# Configuration

First, register the package in the ConfigureServices method of Startup.cs:

```C#
services.AddOpenCQRS(typeof(CreateProduct), typeof(GetProduct));
```

All command and query handlers will be registered automatically by passing one type per assembly.
CreateProduct is an sample command and GetProduct is a sample query.
In this scenario, commands and queries are in two different assemblies.
Both assemblies need to be registered.

### Other Pages

---

- [Installation](Installation)
- [Commands](Commands)
- [Queries](Queries)
- [Release Notes](Release-Notes)