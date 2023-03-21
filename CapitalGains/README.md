## Technical and Arquitectural Decisions
The technical architecture of the project is a substract of the principles of Clean Architecture, specifically the Onion Architecture pattern with N-Layers, which helps to achieve a separation of concerns and a more maintainable and testable codebase.

To handle the different types of stock operations, the project uses the Strategy Pattern, which allows for easy switching between different algorithms or strategies based on the operation type. This approach promotes a more modular design, making it easier to add or remove strategies as needed.

For unit testing, the Arrange-Act-Assert (AAA) pattern is used to structure test cases, which helps to ensure that each test is clear and easy to read.

Finally, Dependency Injection (DI) is leveraged in the project to manage dependencies.

## Frameworks
Console Application built with .Net Core 7.0 (New Style using top-statements)


## Instructions to compile and run

### Requirements
 - Install dotnet-sdk-7.0

From a bash or powershell console.

``` 
$ cd nubank/CapitalGains
$ dotnet build
$ dotnet run < inputTest.txt
 ```

 Expected output

```
$ dotnet run < inputTest.txt
[{"tax":0.00},{"tax":0.00},{"tax":0.00},{"tax":0.00},{"tax":3000.00},{"tax":0.00},{"tax":0.00},{"tax":3700.00},{"tax":0.00}]
```


## How to run the unit tests in the solution

From a bash or powershell console.

```
$ cd nubanktest/CapitalGains.Test
$ dotnet test
```



