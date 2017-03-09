# Gremlin.Net
Gremlin.Net is a cross-platform Gremlin Server driver for .NET that is written in C#. It uses WebSockets to communicate with Gremlin Server.

[![Travis](https://img.shields.io/travis/FlorianHockmann/Gremlin.Net/glvSupport.svg)](https://travis-ci.org/FlorianHockmann/Gremlin.Net)

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

## API Reference

The API reference can be found here: [https://florianhockmann.github.io/Gremlin.Net/api/Gremlin.Net.html](https://florianhockmann.github.io/Gremlin.Net/api/Gremlin.Net.html)
