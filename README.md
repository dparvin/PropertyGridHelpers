# PropertyGridHelpers

PropertyGridHelpers is a .NET library designed to enhance and extend the functionality of the `PropertyGrid` control, providing custom editors, attributes, 
and utilities to facilitate property manipulation within Windows Forms applications.

## Features

- **Custom Attributes**: Define and apply custom attributes to properties for enhanced metadata representation.
- **UI Editors**: Implement specialized editors for complex property types within the PropertyGrid.
- **Type Converters**: Provide custom type converters to control property serialization and display.
- **Utilities**: Offer helper classes and methods to streamline PropertyGrid customization.

## Installation

To include PropertyGridHelpers in your project, add the following line to your `.csproj` or `.vbproj` file:

```xml
<PackageReference Include="PropertyGridHelpers" Version="1.0.1" />
```

Alternatively, use the .NET CLI:
```bash
  dotnet add package PropertyGridHelpers --version 1.0.1
```

You can also use the built in option in Visual Studio to ```Manage Nuget Packages``` for the solution or project.

## Usage
After installation, you can utilize the library by importing the necessary namespaces:

```csharp
using PropertyGridHelpers.Attributes;
using PropertyGridHelpers.UIEditors;
using PropertyGridHelpers.Converters;
```

Here's an example of applying a custom attribute to a property:

```csharp
public class SampleClass
{
    public enum sampleEnum
    {
        [EnumImage("SampleImage1")]
        [EnumText("Sample Image # 1")]
        Entry1,
        [EnumImage("SampleImage2")]
        [EnumText("Sample Image # 2")]
        Entry2
    }

    [Editor(typeof(ImageTextUIEditor<sampleEnum>), typeof(UITypeEditor))]
    [TypeConverter(typeof(EnumTextConverter<sampleEnum>))]
    public sampleEnum SampleEntries { get; set; }
}
```

For detailed usage and examples, refer to the [documentation](https://github.com/dparvin/PropertyGridHelpers/wiki).

## Contributing
Contributions are welcome! Please read the [contributing guidelines](https://github.com/dparvin/PropertyGridHelpers/blob/main/CONTRIBUTING.md) for more information.

## License
This project is licensed under the MIT License. See the [LICENSE](https://github.com/dparvin/PropertyGridHelpers/blob/main/LICENSE) file for details.

## Acknowledgments
Special thanks to the contributors and the open-source community for their invaluable support and inspiration.

### Related Locations
* [Code Coverage Report](https://dparvin.github.io/PropertyGridHelpers/)
* [Publish Package](https://www.nuget.org/packages/PropertyGridHelpers)
