### Sky Ninja

Sky Ninja (`/skˈaɪ ˈnɪn.dʒə/`) is a [Skype](http://www.skype.com) history exporting application. The application is the successor of the [Skype Historian](http://eigene.in/skype-historian).

#### Visual Studio Project Checklist

* Verify that the project requires **.NET Framework 4 Client Profile**.
* Verify that **the namespace starts with `SkyNinja`**.
* Verify that **all warnings are treated as errors**.
* Verify that output directory is set to `..\Release` (`..\Debug`).
* Verify that **Code Analysis is turned on for all configurations**.
* Verify that **SkyNinja Ruleset** is chosen **for all configurations**.
* Verify that the assembly is **signed with the `SkyNinja.pfx`**.
* Verify that **`SkyNinja.pfx` is added as a link (not a copy)**.
* Verify that **unused references are removed**.
* Verify **`AssemblyInfo.cs`**.
