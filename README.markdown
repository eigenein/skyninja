### SkyNinja

SkyNinja (`/skˈaɪ ˈnɪn.dʒə/`) is a tool for exporting and importing [Skype](http://www.skype.com) conversations. The application is the successor of [Skype Historian](http://eigene.in/skype-historian).

#### Advantages over Skype Historian

* Does not require running and even installed Skype.
* Dramatically faster.

#### Visual Studio Project Checklist

* Verify that the project requires **.NET Framework 4.5**.
* Verify that **the namespace starts with `SkyNinja`**.
* Verify that **all warnings are treated as errors**.
* Verify that output directory is set to `..\Release` (`..\Debug`).
* Verify that **Code Analysis is turned on for all configurations**.
* Verify that **SkyNinja Ruleset** is chosen **for all configurations**.
* Verify that the assembly is **signed with the `SkyNinja.pfx`**.
* Verify that **unused references are removed**.
* Verify **`AssemblyInfo.cs`**.

#### Connector Specification

##### Input Connectors

* `skypeid://username`
* `skypedb://C:\Users\User\AppData\Roaming\Skype\user\main.db`

##### Output Connectors

* `plain://C:\Users\User\Downloads\Output`

##### Filters



##### Groupers

* `participants`
* `year-month`
* `day`

##### File Systems

###### `usual`

Usual file system.

###### `zip`

Zip archive. Accepted parameters:

* `zipEntryEncoding`

#### Cli Usage Examples

```
.\Cli
    -i skypeid://eigenein
    -o plain://C:\Users\eigenein\Downloads\SkyNinja
    -g year-month -g participants
```

```
.\Cli
    -i skypeid://eigenein
    -o plain://C:\Users\eigenein\Downloads\SkyNinja.zip
    -f zip
```

```
.\Cli
    -i skypeid://eigenein
    -o plain://C:\Users\eigenein\Downloads\SkyNinja.zip
    -f zip
    -g year-month -g day -g participants
```

```
.\Cli
    -i skypeid://eigenein
    -o plain://C:\Users\eigenein\Downloads\SkyNinja.zip?zipEntryEncoding=CP866
    -f zip
    -g year-month -g day -g participants
```
