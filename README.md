```mermaid
graph

    Presintation --> Core
    Presintation --> Infrastructure

    subgraph Presintation
        Controllers
        Middlewares
    end
    subgraph Core
        Application --> Domain
        subgraph Application
            UseCases,Interfaces
        end
        subgraph Domain
            Enums,Entities
        end
    end
    subgraph Infrastructure
    Services
        subgraph Persistance
        Repositories,Migrations
        end
    end
```

https://deriglazoff.github.io/Geolocation/src/Geolocation.Client/swagger.html
