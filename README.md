# SRoll.Countries

A simple .NET Standard Library with a localized list of ISO-3166 country with Alpha-2, Alpha-3 and numeric codes.

For now, the following languages are available
- English
- French
- Danish

## Usage

Get the list with all the countries.
```
SRoll.Countries.Country.List
```
The Name property returns the name of the country in the current UI culture.

If needed, you can create new countries.
```
var country = new SRoll.Countries.Country("IM", "IML", 42, "Imaginationland");
```
In this case, the Name property always return the value passed in the constructor.

## Custom translations

It is possible to add new translations or to override the default translations using a custom `ITranslationProvider`
```
SRoll.Countries.Country.CustomTranslationProvider = myCustomProvider;
```

If the custom provider returns `null` for a given country/culture, then the value from the default provider is used.