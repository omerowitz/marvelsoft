# MarvelsoftTests

This project is the unit tests project for entire codebase of **MarvelsoftConsole** project and it's classes.

### Responsibility

Responsibility of this project is to assure code strictness and stability of the **MarvelsoftConsole** codebase across different test cases.

### Third-party dependencies

It relies on on two third-party dependencies:

- **NUnit** - main Unit Testing framework, popular among C# and .NET community
- **Moq** - a unit testing framework used to mimic functionality and test behavior of specific classes

---

### Unit Tests

Unit tests are written in a top-down alphabetic order by appearance of classes of the console project and are written in NUnit.

The entire test project consists of total **35 unit tests** for various test cases of the console application.

#### Naming convention

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