### SkyNinja

SkyNinja (`/skˈaɪ ˈnɪn.dʒə/`) &ndash; export and import [Skype](http://www.skype.com) conversations. The application is the successor of [Skype Historian](http://eigene.in/skype-historian).

#### Visual Studio Project Checklist

* Verify that the project requires **.NET Framework 4.5**.
* Verify that **the namespace starts with `SkyNinja`**.
* Verify that **all warnings are treated as errors**.
* Verify that output directory is set to `..\Release` (`..\Debug`).
* Verify that **Code Analysis is turned on for all configurations**.
* Verify that **SkyNinja Ruleset** is chosen **for all configurations**.
* Verify that the assembly is **signed with the `SkyNinja.pfx`**.
* Verify that **`SkyNinja.pfx` is added as a link (not a copy)**.
* Verify that **unused references are removed**.
* Verify **`AssemblyInfo.cs`**.

#### Connector Specification

##### Input Connectors

* `skypeid://username`
* `skypedb://C:\Users\User\AppData\Roaming\Skype\user\main.db`

##### Output Connectors

* `plain://C:\Users\User\Downloads\Output`
