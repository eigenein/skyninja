# SkyNinja

SkyNinja (`/skˈaɪ ˈnɪn.dʒə/`) is a tool for exporting and importing [Skype](http://www.skype.com) conversations. The application is the successor of [Skype Historian](http://gh.eigenein.xyz/skype-historian/).

*Though the tool is not updated for a long time please feel free to contact me should you need any missing feature – I'll try to add it for you if I have time.*

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

## Command-Line Interface

### Usage

```sh
cli -i <uri> -o <uri> [-g <name>...] [-f <name>] [--time-from <time>] [--time-to <time>]
```

### Options

```
-i --input <uri>         Input URI.
-o --output <uri>        Output URI.
-g --grouper <name>...   Groupers [default: participants].
-f --file-system <name>  Target file system [default: usual].
--time-from <time>       Include only messages sent after the specified time.
--time-to <time>         Include only messages sent before the specified time.
```

### Inputs

#### Skype ID

Locates Skype database by Skype ID (user name).

```sh
Cli
    -i skypeid://your_skype_name
    -o plain://D:\Logs
    -g year-month -g participants
```

#### Skype DB

Locates Skype database by path.

```sh
Cli
    -i skypedb://C:\Users\User\AppData\Roaming\Skype\user\main.db
    -o plain://D:\Logs
    -g year-month -g participants
```

### Outputs

#### Plain Text

Plain text files (*.txt).

```sh
Cli
    -i skypeid://your_skype_name
    -o plain://D:\Logs
    -g year-month -g participants
```

### Filters

#### Date and Time

Include only messages sent after of before the specified time. Accepted formats are UNIX timestamp and `dd-MM-yyyy HH:mm:ss` (UTC).

```sh
Cli
    -i skypeid://your_skype_name
    -o plain://D:\Logs
    --time-from "01-01-2014 00:05:33"
    --time-to "01-02-2014 00:05:53"
```

### Groupers

Grouper defines how to group all messages in output.

The following groupers are available so far:

* `participants`
* `year-month`
* `day`
 
#### Chaining Groupers

One can specify several groupers. In this case they will be applied one by one.

```sh
Cli
    -i skypeid://your_skype_name
    -o plain://D:\Logs
    -g year-month -g day -g participants
```

### File Systems

«File system» is an abstraction of a storage.

#### Usual

Messages are grouped into files and files are grouped into folders.

#### ZIP Archive

Same as usual one but compressed into a ZIP archive «on-the-fly».

```sh
Cli
    -i skypeid://your_skype_name
    -o plain://D:\Logs.zip
    -f zip
```

ZIP file name encoding can also be specified in order to be able open an archive with Windows Explorer.

```sh
.\Cli
    -i skypeid://your_skype_name
    -o plain://D:\Logs.zip?zipEntryEncoding=CP866
    -f zip
    -g year-month -g day -g participants
```
