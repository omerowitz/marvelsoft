# MarvelsoftConsole

This is a console application which must be ran with either *Command Prompt* or *Windows PowerShell*.

### Responsibility

Application's main responsibility is to consume one CSV file and one "broken" JSON file, process them concurrently, pile them up together and create a brand new CSV output file with processed data. In that manner, the application has two modes of operation:

1. Asynchronous (default) - while it processes each file, in the same time it appends a new line into the *output file*, concurrently
2. Synchronous (optional) - it conducts processing in the same manner as the first operation, but it waits the application to create a full list of processed records to pile up, and when done, it uses **CsvHelper** against that set of records to create the output file - depending on underlying architecture and size of files, this operation CAN sometimes be a bit faster than the asynchronous operation.

Worth noting, another difference between the two modes is that the asynchronous mode opens a `StreamWriter` **once** inside the `Bootstrap.cs` and passes it two each parsers as a reference, thus reducing any potential conflicts when updating the same file.

---

### Dependencies (third-party)

The application uses three different third-party frameworks:

- **CommandLineParser** - parses passed command-line arguments and maps them to a model-class `Models/CommandLineOptions.cs`
- **CsvHelper** - a library used for reading, parsing, processing and writing CSV files with ease
- **Newtonsoft.Json** - a JSON parser, serializer & deserializer library used to parse the input JSON file

---

### Code structure

Entire application relies on the .NET Core 3.1+ Framework and it consists of the following custom class-components:

- `Program.cs` - main entry point for CLIs
- `Runner.cs` - application runner which parses command-line arguments and can be ran without a *Command Line Interface*
- `Application.cs` - it consumes parsed CLI arguments and passes them to a bootstrapper component which runs the actual application
- `Bootstrap.cs` - also consumes parsed arguments and then ties all further components to run the application workflow
- `Helpers/` - contains various helper utilities and classes:
  - `Helpers/FileHelper.cs` - checks if files exist, if they are of proper types, throws exceptions of few occasions
  - `Helpers/FileReader.cs` - asynchronously reads a file into a `FileStream` and contains helper methods and converts it's payload into a `byte[]` array
  - `Helpers/CsvOutputSerializer.cs` - since the **CsvHelper** lacks ability to parse a single row of consumed CSV file, or if done in a manner that it iterates through each CSV record to create an output, would be too much of an overhead work, so this helper consumes an instance of a model-class `CsvOutput` and then returns a single serialized CSV row as a string, later appended to a file
  - `Helpers/Summary.cs` - just a helper class containing various static methods to print various texts in the `Console`.
- `Models/` - contains model-classes on which certain sets of data are mapped to, i.e. command-line arguments will be mapped to `Models/CommandLineOptions.cs` 
- `Interfaces/` - contains a sole interface `IParser.cs` which holds the contract on how to implement a specific Parser
- `Parsers/` - contains implementation of interfaces used in this application:
  - `Parsers/BaseParser.cs` - abstract class which implements the base `IParser.cs` interface and implements it's methods as `virtual` so they can be overridden by classes which use this class as their parent. Also, it creates instances of properties to be used, such as: a `FileReader` and `StreamWriter` instances, a reference to `CsvOutput` - a list which will contain all processed records, and a `SemaphoreSlim` instance which will handle concurrency between our processes.
  - `Parsers/CsvParser.cs` - it extends the previous class and overrides methods: `ProcessAsync()` and `ParseAsync()`. The entire CSV file payload (represented as a `byte[]` array) is passed into a `MemoryStream` which is then passed into the **CsvHelper** library which parses all records. The library may throw an exception if a specific row is not parsable, so the method `GetRecordsAsync()` catches any exception occurring there and notifies user in the console that those records are not parsable. Then, when `ParseAsync()` is called, it uses `SemaphoreSlim` to await next async task, and if and when ready, it will conduct either asynchronous file update (mode #1) or **synchronous** file creation.
  - `Parsers/JsonParser.cs` - works a bit differently than `CsvParser`, but uses the same methods and extends upon `BaseParser` too. This parser uses the pre-prepared file payload represented as a `byte[]` array, converts it into a string, then we split that string with a new-line character, derived from `Environment.NewLine` property, and pass those further to `ProcessAsync()` which then iterates each line and calling asynchronous method `ParseAsync()`, passing each line and iterating index. Parsing, or deserialization of each JSON line, occurs in `ParseAsync()` itself, catching any JSON-related exceptions, and appending new results into the `CsvOutput`. Main difference between the two parsers is that the `JsonParser` handles deserialization in `ParseAsync()`, whilst `CsvParser` does that in the `private` method `GetRecordsAsync()`.
- `Exceptions/` - contains a sole `FileErrorException` class which will be thrown by the `FileHelper` or some other place.

---

### Running and testing

As noted previously, the application will accept one badly-constructed JSON in which each line represents a valid JSON object, but the entire file itself is not a valid JSON representation, and a CSV file who's delimiter is expected to be a comma (`,`).

Application's main build binary for both release versions is: `MarvelsoftConsole.exe`.

This is the output of the program (below) if you run it without required arguments, which are: `-j` for JSON file and `-c` - the CSV file:

```
Copyright (C) 2020 MarvelsoftConsole

ERROR(S):
  Required option 'j, json' is missing.
  Required option 'c, csv' is missing.

  -j, --json      Required. JSON input filename

  -c, --csv       Required. CSV input filename

  -o, --output    (Default: output.csv) Output filename (defaults to: output.csv)

  -s, --sync      (Default: false) Whether to write file synchronously after processing is done

  -w, --wait      (Default: false) Wait user to press ENTER after the process is done to exit the application

  --help          Display this help screen.

  --version       Display version information.
```

As presented above, you can pass `-o` to specify a new output filename, BUT it must end with an extension `.csv`, nothing more and nothing less.

The `-s` parameter or syncing will conduct all operations concurrently, but the output file will be created after all records are processed from both files.

Parameter `-w` is intended for testing purposes only; basically it waits user interaction with hitting ENTER key on their keyboard to exit the program after the process is done.

#### Error messages in the console

Following errors may be shown in the console:

1. If the `-j` file is not a JSON file (not checked against it's mime-type, but it's extension)
2. If the `-c` file is not a CSV file (same rule as above)
3. If one of the provided files do not exist on the filesystem
4. If one of the provided files is empty and has zero-length
5. If both provided files are empty (this could have been redone with file hash checking, but for now it only checks their filenames against each other)

**Important note:** You can provide a full path to one of the files, but those paths will be truncated as soon as the CLI arguments are passed, and we do this so we could ensure proper format of the final output CSV file. Expected format is:

```
Filename;Id;Quantity;Price
"input-csv-file";"ID-from-CSV";762;7.62
"input-json-file";"ID-from-JSON";556;5.56
```

Also note that *header field* is not included in the output file and is not implemented at all.

##### Real-life test now

Both releases contain `inputA.csv` and `inputB.json` files, respectively, so you can `cd` into `MarvelsoftConsole/bin/Release/netcoreapp3.1`  and run:

```
$ MarvelsoftConsole.exe -j inputB.csv -c inputA.csv -o new-output.csv -w
```

This will process both files and create a new file `new-output.csv` and will wait for your interaction after the process is done, and will run with the asynchronous mode by updating the output file concurrently.

In order to verify output file integrity, you can open it either with MS Excel or with **MarvelsoftGUI** program.

## Other information

The command-line parser accepts both shorthand and named parameters, prefixed with `-` for shorthand ones and `--` for named ones, respectively.

If you run two or more instances of the application using the same output file, you might crash either application by causing a conflict as each instance of the application will open it's `StreamWriter` against that file and producing invalid results in that file or crashing entirely.

When using `-w` parameter and when forgetting or missing to hit ENTER key on your keyboard, if you run another instance of the application against the same **files** or against the same output file, you might crash the application(s). 