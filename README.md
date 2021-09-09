# Filename Simplifier
<p align="center">
  <img width="250" height="250" src="https://i.imgur.com/yNhvFMr.png">
</p>
<b>
<p align="center" style = "emphasis">
  A simple tool to mass-simplify (mass-rename) the filename of files and folders.
</p>
</b>

[![Codacy Badge](https://api.codacy.com/project/badge/Grade/9f400677d2c1436585e179cb8d3d885c)](https://app.codacy.com/gh/Az-21/filename-simplifier?utm_source=github.com&utm_medium=referral&utm_content=Az-21/filename-simplifier&utm_campaign=Badge_Grade_Settings)

<a href="https://github.com/Az-21/filename-simplifier/blob/main/LICENSE" alt="GPL 3.0">
        <img src="https://img.shields.io/github/license/Az-21/filename-simplifier?style=for-the-badge" /></a>
<a href="" alt="C#10">
        <img src="https://img.shields.io/badge/Built%20With-C%20Sharp-%23630094?style=for-the-badge&logo=c-sharp" /></a>
<a href="" alt=".NET6">
        <img src="https://img.shields.io/badge/Built%20On-.NET6-%234E2ACD?style=for-the-badge&logo=dotnet" /></a>
        
🚧 We're currently working on a re-implementation in C#. See the `Releases` for the currently working legacy Python version.

Source code of legacy python version: https://github.com/Az-21/filename-simplifier-legacy

# CLI Documentation

## Download

* Click on 'Releases' and download the latest version of simplify
* Extract the files. We recommend placing it in `C:\src\simplify`
* **NOTE** Do not place these files under `C:\Program Files` or a folder which requires admin privileges

## Adding simplify to PATH (Optional)

* Search 'Edit the system environment variables' from the start menu
* Click on 'Environment Variables'
* Under the 'User variables' (top panel), select `Path` and click 'Edit'
* Now, click on 'New' and provide the address of the folder `simplify`
* Example: `C:\src\simplify`

## Running the program

* If you have added the program to PATH
  * Run the program by calling `simplify` in your terminal.

* If you have not added the program to PATH
  * `Shift` + `Right Click` in the `simplify` folder and select 'Open powershell window here' or 'Open cmd window here'
  * Run the program by calling `.\simplify.exe` in your terminal.
  * **NOTE** Further documentation assumes you have added the program to PATH
  * Replace `simplify` with `.\simplify.exe` to achieve the same effect

## Simplifying

**Step 1: Perform a preview run**
```cmd
simplify 'C:\\PathOfLibrary'
```

**Step 2: Perform a permanent run**
```cmd
simplify 'C:\\PathOfLibrary' --rename
```

## Include Folders

By default, folders are not included in rename. To include folders in rename, pass `--includefolders` flag

**Step 1: Perform a preview run**
```cmd
simplify 'C:\\PathOfLibrary' --includefolders
```

**Step 2: Perform a permanent run**
```cmd
simplify 'C:\\PathOfLibrary' --includefolders --rename
```

# Editing Behavior of Simplification (Config)

* Configuration file can be found in `config\config.json`
* Open this file with Notepad (or your IDE).
* Edit the parameters to turn features ON or OFF

## LibraryPath

* This is the default location of your library.
* If you do not pass a directory in the CLI, this address will be considered.
* This field is useful if you want to regularly simplify your media library by running `simplify --rename`

## GetAllDirectories

* `true`: crawl all the subfolders. Default.
* `false`: do not crawl the subfolders and only rename the top level contents.

## Extensions

* This is a **comma-separated** list of extension which will be renamed.
* Files without these extensions will be skipped.

## Blacklist

* This is a **comma-separated** list of words and characters which will be removed.
* As of now, this list is case-sensitive. We're working towards a case-insensitive blacklist.

## RemoveCurvedBracket

* `true`: remove everything contained within parentheses `()`
* `false`: keep text inside parentheses

**Before**
```
Name of Movie (x256 OPUS).mkv
```

**After**
```
Name of Movie.mkv
```

## RemoveSquareBracket

* `true`: remove everything contained within square brackets `[]`
* `false`: keep text inside square brackets

**Before**
```
Name of Movie [HEVC Dolby].mkv
```

**After**
```
Name of Movie.mkv
```

## IsCliFriendly and CliSeparator

* `true`: convert spaces in filename with `CliSeparator`
* `false`: retain spaces in filename
* This setting is made for people who hate spaces in folders and filenames
* `CliSeparator` is a string, so you are not restricted to a single character.

**Before**
```
Name of Movie.mkv
```

**After** [CliSeparator = "-"]
```
Name-of-Movie.mkv
```

