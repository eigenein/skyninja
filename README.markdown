# SkyNinja

SkyNinja (`/skˈaɪ ˈnɪn.dʒə/`) is a tool for exporting and importing [Skype](http://www.skype.com) conversations. The application is the successor of [Skype Historian](http://eigene.in/skype-historian).

## Advantages over Skype Historian

* Does not require running and even installed Skype.
* Dramatically faster.

## Features

* Exporting of chat messages.
* Date and time filtering.
* Grouping by conversation participants and message date.
* Saving to plain text files.
* Creating ZIP archive "on the fly".

## Visual Studio Project Checklist

* Verify that the project requires **.NET Framework 4.5**.
* Verify that **namespace starts with `SkyNinja`**.
* Verify that **all warnings are treated as errors**.
* Verify that output directory is set to `..\Release` (`..\Debug`).
* Verify that **Code Analysis is turned on for all configurations**.
* Verify that **SkyNinja Ruleset** is chosen **for all configurations**.
* Verify that the assembly is **signed with the `SkyNinja.pfx`**.
* Verify that **`SkyNinja.pfx` is added as a link (not a copy)**.
* Verify that **unused references are removed**.
* Verify **`AssemblyInfo.cs`**.

## Publish Checklist

* Publish **SkyNinja** project.
* **Commit** each **`ApplicationRevision`** change.
