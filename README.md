# Gremlin.Net
Gremlin.Net is a cross-platform Gremlin Server driver for .NET that is written in C#. It uses WebSockets to communicate with Gremlin Server.

[![AppVeyor](https://img.shields.io/appveyor/ci/FlorianHockmann/Gremlin-Net.svg?style=plastic)](https://ci.appveyor.com/project/FlorianHockmann/Gremlin-Net) [![Coveralls](https://img.shields.io/coveralls/FlorianHockmann/Gremlin.Net.svg?style=plastic)](https://coveralls.io/r/FlorianHockmann/Gremlin.Net)

## Getting Started

Install the [NuGet package](https://www.nuget.org/packages/Gremlin.Net/).

## Usage

```cs
var gremlinServer = new GremlinServer("localhost", 8182);
using (var gremlinClient = new GremlinClient(gremlinServer))
{
    var result =
        await gremlinClient.SubmitWithSingleResultAsync<bool>("g.V().has('name', 'gremlin').hasNext()");
}
```
