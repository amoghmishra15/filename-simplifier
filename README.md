# Filename Simplifier

<p align="center">
  <img width="250" height="250" src="https://raw.githubusercontent.com/Az-21/artwork/master/simplify/simplify.png">
</p>
<b>
<p align="center" style = "emphasis">
  A simple tool to mass-simplify (mass-rename) the filename of videos downloaded via torrents.
</p>
</b>


## Features

**Before**
```
(火) [GroupName] Generic.Isekai_ジェネリック異世界_(_2020_)_-_S01E07.[1080p].[HEVC x265 10bit][Multi-Subs] [CB67A2].(Weekly).mp4
```
**After**
```
Generic Isekai (2020) - S01E07.mp4
```
* Simplifies the filename of videos downloaded via torrents. Especially geared towards simplifying the filename of anime torrent files.
* Removes some common metadata tags like '320kbps' '1080p' 'BluRay'. Supports custom blacklist.
* Removes dot separators, and underscores.
* Removes text inside square brackets.
* Removes extra whitespace.
* Converts to 'Title Case' and removes non-English characters. This can be disabled.
* Displays preview before finalizing the rename.
* Prevents two or more files ending with the same rename.


## Requirements

* Windows: no prerequisites.
* Linux and Mac: [Python 3](https://www.python.org/).


## How to use


### Windows

Download and the latest `simplify.exe` from the [releases].

---


### Linux and Mac


[Download](https://github.com/Az-21/filename-simplifier/archive/master.zip) and extract the .zip provided by github.

\[Optional\] Or clone this repo using `git clone https://github.com/Az-21/filename-simplifier.git`

Open up your terminal cloned/extracted the `simplify.py` file and run it using the command given below. The program will ask where the files are located. So, you can have `simplify.py` and the files you want to rename in different folders.

```bash
python3 simplify.py
```

---

That's it! Everything is then done using [y/n] prompts

## Customization

This program has built-in runtime setting customization. Press `y` when prompted with `Edit settings? (y/n): `.

Currently you can customize

* Blacklisted words
* Extensions to work on (custom extensions)
* Flag to enable/disable removal of non-English characters `(default = enabled)`
* Flag to enable/disable title case `(default = disabled)`

## [Link to the Roadmap](https://github.com/Az-21/filename-simplifier/projects/1)

Features I'm currently working on. 

Suggestions are always welcomed.


# Changelog

```diff
v2.1
+ added dot and underscore separators
+ added (almost) all popular audio, video, and subtitle extensions
+ added option to convert to Title Case
+ complete revamp of blacklist removal. now uses regex
+ cleaned up prompt text. minimized user interaction
+ added safety check for 2+ files ending with the same simplified rename
+ very first .exe release

v2.0
+ refactored code
+ support for CLI settings editor
+ support for custom blacklist
+ support for custom extensions
+ foundation for dot removal and underscore removal
+ cleaned up code significantly

v1.2.5
+ fixed various bugs and vulnerabilities
- removed some ASCII art

v1.2.1
+ added some ASCII art

v1.2.0
+ moved user setting to a dedicated .py file
+ added support for different file formats (vid, audio, docs, custom)

v1.1
+ remove non-English characters
+ user config support to disable the removal of non-Eng characters
+ increased functionality of extra whitespace removal function ( text ) -> (text)

v1.0
+ remove everything inside square brackets
+ remove extra whitespace
+ user config to support additional filter keywords and extensions
```
