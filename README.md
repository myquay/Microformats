#  <img src="https://raw.githubusercontent.com/myquay/Microformats/4a2af9ec1e17704436f55693ebbcf5d0540f1593/logo.svg" alt="Microformats Logo" style="width: 30px"> Microformats .NET

**A .NET Microformats2 parser.**

This parser implements the full Microformats2 specification, including backwards compatibility with classic Microformats.

## Implementation

This parser does not restrict the property or microformat names to those explicitly mentioned in the specification. Any h-\*, p-\*, u-\*, e-\*, dt-\* will be handelled with regard to the parsing rules for that type.

## Key Features

* Compatible with both [classic microformats](http://microformats.org/wiki/Main_Page#Classic_Microformats) and [microformats2](http://microformats.org/wiki/microformats2) syntaxes.
* Targets .NET Standard 2.0 for high compatibility

## Installation

This library is distributed as a NuGet package. To install with the .NET CLI run `dotnet add package Microformats --version {version number} `

## Quick Start

### Parse a HTML string

```csharp
 var parser = new Mf2();
 var html = "...";
 var result = parser.Parse(html);
```

### Configure the parser

```csharp
var parser = new Mf2().WithOptions(o =>
{
    o.DiscoverLang = true;
    o.UpgradeClassicMicroformats = true;
    o.BaseUri = new Uri("http://example.org");
    return o;
}); ;
 var html = "...";
 var result = parser.Parse(html);
```

## Example

Here is an example parsing someone's h-card.

```csharp
 var parser = new Mf2();
 var html = "<a class=\"h-card\" href=\"{website address}\"><img alt=\"{name of person}\" src=\"{address of photo}\" /></a>";
 var result = parser.Parse(html);

 result.Items[0].Get(Props.NAME)[0]; //Access name
 result.Items[0].Get(Props.URL)[0]; //Access website
 result.Items[0].Get(Props.PHOTO)[0]; //Access photo
```

## Usage notes

* All the different Microformats (h-\*) parsed are held in the `result.Items` array.
* Access the string value for a property using the `.Get({name})` method. This returns an array of all values for that property, typically it will be just one if present.
* Some properties can have a more complex data structure which are represented by the types `MfImage`, and `MfEmbedded`. Use the generic .Get\<Type\>({name}) to access the underlying complex type.
* Unsure of the underlying type? Use the .TryGet\<T\>({name}, out T result) and fallback on the string version on failure.
* Well known properties from the spec are in the `Props` class (e.g. `Props.NAME`), these are just strings, you can pass in any non-standard property as long as it follows the p-\*, u-\* etc. pattern. (e.g. `.Get("p-some-non-standard-prop")`).

## Roadmap

- [ ] Format to JSON

## Contributing

Contributions are very welcome!

### Ways to contribute

* Fix an existing issue and submit a pull request
* Review open pull requests
* Report a new issue
* Make a suggestion/ contribute to a discussion

## Acknowledgments

The project logo is derived from the [microformats logo mark](http://microformats.org/wiki/spread-microformats) by [Rémi Prévost](http://microformats.org/wiki/User:Remi).

## License

MIT
