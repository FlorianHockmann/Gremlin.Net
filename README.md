# Gremlin.Net
Gremlin.Net is a Gremlin Server driver for .NET that is written in C#. It uses WebSockets to communicate with Gremlin Server.

[![AppVeyor](https://img.shields.io/appveyor/ci/FlorianHockmann/Gremlin-Net.svg?style=plastic)](https://ci.appveyor.com/project/FlorianHockmann/Gremlin-Net)

## Usage

```cs
var gremlinServer = new GremlinServer("localhost", 8182);
using (var gremlinClient = new GremlinClient(gremlinServer))
{
    var result =
        gremlinClient.SubmitWithSingleResultAsync<bool>("g.V().has('name', 'gremlin').hasNext()").Result;
}
```
