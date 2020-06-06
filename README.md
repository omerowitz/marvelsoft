# Marvelsoft development task

This repository contains source code of the assurance test task from Marvelsoft.

The entire repository is a Visual Studio 2019+ Solution (but might work with later versions).

The solution contains three separate projects:

- **MarvelsoftConsole** - console application which parses CSV and JSON file and dumps a new processed CSV file
- **MarvelsoftGUI** - WinForms application which consumes processed file and shows it in a custom control
- **MarvelsoftTests** - Unit tests for the console application and it's classes

Both *Release* and *Debug* builds are included in the repo, and can be found in following paths:

- **Release** `/{PROJECT_NAME}/bin/Repease/netcoreapp3.1/{PROJECT_NAME}.exe`
- **Debug** `/{PROJECT_NAME}/bin/Debug/netcoreapp3.1/{PROJECT_NAME}.exe`

Where `{PROJECT_NAME}` is one of the three projects listed above.

---

### MarvelsoftConsole

This is a console application which must be ran with either *Command Prompt* or *Windows PowerShell*.

The binary name is `MarvelsoftConsole.exe` and this is a sample output of it when ran without any arguments:

```MarvelsoftConsole 1.0.0
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

Further documentation for console application [can be found here](https://github.com/omerowitz/marvelsoft/tree/master/MarvelsoftConsole).

---

### MarvelsoftGUI

This is a WinForms application built with .NET Core 3.1 and can be ran on any Windows instance with a .NET Core preinstalled.

The binary name is `MarvelsoftGUI.exe` and this is a preview of how the application looks like when executed:

![MarvelsoftGUI](https://i.imgur.com/XRmrXI3.png)

White application title container and the dark grey area is the entire custom control, along with controls on it.

Further documentation for WinForms application [can be found here](https://github.com/omerowitz/marvelsoft/tree/master/MarvelsoftGUI).

---

### MarvelsoftTests

This project is the unit tests project for entire codebase of **MarvelsoftConsole** project and it's classes.

Unit tests are written in a top-down alphabetic order by appearance of classes of the console project and are written in NUnit.

The entire test project consists of total **35 unit tests** for various test cases of the console application.

Test classes are prefixed with a `Test_` followed by an alphabetic letter i.e. `A_` followed by a class which is tested, for example:

```
Test_D_InterfaceParser
```

That test will be the fourth to run and contains tests for the `IParser` interface which uses Moq dependency to mimic a real implementation of that interface with `async/await` callbacks and a dummy test subject class.

Specific unit tests are also written in a similar manner, per test class, so for the `Test_D_InterfaceParser`, we have this order of execution:

```
InterfaceParser_A_ProcessAsync
InterfaceParser_B_ParseAsync
```

Name of a specific unit test consists of:

1. Class name or an interface on which we conduct tests upon
2. Alphabetic letter to preserve order of execution of tests
3. Method name or functionality to test, described either as a method name or a human readable description

Tests can be ran directly from Visual Studio Test Explorer, like in the following picture:

![MarvelsoftTests](https://i.imgur.com/7cEhWsQ.png)

#### Important note

Some of the tests will either need actual CSV or JSON files to conduct tests, and some tests will run the entire application to process files and create output files. All tests which create any files after execution will delete those files as soon as they are complete or reach the final test unit in those classes.

In both *Release* and *Debug* directories of the test project, you will find both `inputA.csv` and `inputB.json` files. If you want to make a test in which some of the unit tests fail, just delete those files.

Further documentation for the test project [can be found here](https://github.com/omerowitz/marvelsoft/tree/master/MarvelsoftTests).

---

### Further reading

#### Installation and usage

Download or clone this repo (preferred option) on your computer, open the `Marvelsoft.sln` with your Visual Studio to modify code or test it further.

```
$ git clone https://github.com/omerowitz/marvelsoft.git
```

#### Potential problems with latest version of Visual Studio

If you are unable to run some of the project, that might be an issue when Visual Studio modifies some of the project files and causes errors.

Recently, Microsoft released new version of Visual Studio Community 2019 Version 16.6.1 which has updates on Windows Forms Designer for .NET Core application, but it still lacks ability to edit User Controls or Custom Controls and that might cause an issue when the control cannot be built and used in the GUI project.

In order to avoid this problem, you can reset your cloned Git repository to commit `d259f6e` which is the latest stable commit with builds:

```
$ git reset --hard d259f6e
```

After this, you should rebuild the entire solution and run the application(s).