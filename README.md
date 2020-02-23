# Simple Spreadsheet Application





# Introduction:
This application allows user to create/update/calculating the total values in the spreadsheet using command line interface 

# Use Case Examples
1) create a 10x5 spreadsheet
enter command:C 10 5
0   1   2   3   4   5   6   7   8   9
========================================

----------------------------------------

----------------------------------------

----------------------------------------

----------------------------------------

----------------------------------------

2) update cell [1,1] to 2
enter command:N 1 1 2
0   1   2   3   4   5   6   7   8   9
========================================

----------------------------------------
    2
----------------------------------------

----------------------------------------

----------------------------------------

----------------------------------------

3) update cell [2,3] to 10 
enter command:N 2 3 10
0   1   2   3   4   5   6   7   8   9
========================================

----------------------------------------
    2
----------------------------------------

----------------------------------------
        10
----------------------------------------

----------------------------------------

4) update cell [4,2] to 5
enter command:N 4 2 5
0   1   2   3   4   5   6   7   8   9
========================================

----------------------------------------
    2
----------------------------------------
                5
----------------------------------------
        10
----------------------------------------

----------------------------------------

5) calculate sum ranged from [1,1] to [4,3], and update to [0,0]
enter command:S 1 1 4 2 0 0
0   1   2   3   4   5   6   7   8   9
========================================
7
----------------------------------------
    2
----------------------------------------
                5
----------------------------------------
        10
----------------------------------------

----------------------------------------

# Development environment and Library
IDE: Microsoft Visual Studio Community 2017
Framework: .NET Framework 4.5
Container: Castle Windsor 5.0.0.0

 
# Solution Structure
The Solution contains the following projects
The solution contains the following projects
1    CommonModel: Contains general data model such as common interfaces that describes command, worksheet
2    SpreadsheetApp: the main console app that configured as a startup project of the solution.
3    SpreadsheetCommand: contains commands classes that perform basic functions such as create/update/sum worksheet
4    SpreadsheetManager: contains collection model that stores a list of command and methods to print spreadsheet
5    SpreadsheetModel: contains the Simple spreadsheet model.

# Build and run the application in visual studio: 
Build the SpreadsheetApp solution and run SpreadsheetApp project.


# Design Overview
## IoC Container
The application is written in C# .NET 4.5. The projet uses the Inversion of Control technique decouple the dependencies, and  
to allow easy extension of the new features. The dependencies will be registered into Castle Windsor IoC Container. 
They will be resolved automatically by the container. (see https://github.com/castleproject/Windsor)

## Simple Spreadsheet model
The simple spreadsheet model is to provide simple functions to update/access cell values by specifying  X and Y coordinate.  
The cell access mechanism is optimized using Cantor pairing algorithm for fast access.
(see https://en.wikipedia.org/wiki/Pairing_function). 


## Spreadsheet command
All spreadsheet commands classes comply to SpreadsheetCommand interface. This allows IoC container to register and resolve 
the required commands to be used in this application.

