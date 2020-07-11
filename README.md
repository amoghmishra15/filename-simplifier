# Filename Simplifier

<p align="center">
  <img width="250" height="250" src="https://raw.githubusercontent.com/Az-21/artwork/master/simplify/simplify.png">
</p>
<b>
<p align="center" style = "emphasis">
  A simple tool to mass-simplify (mass-rename) the filename of files and folders.
</p>
</b>

## Demo

![Demo GIF](https://raw.githubusercontent.com/Az-21/artwork/master/simplify/simplify-terminal.gif)



## Features

**Before**
```
(火) [GroupName] Generic.Isekai_ジェネリック異世界_(_2020_)_-_S01E07.[1080p].[HEVC x265 10bit][Multi-Subs] [CB67A2].(Weekly).mp4
```
**After**
```
Generic Isekai S01E07.mp4
```
* Simplifies the filename of videos downloaded via torrents. Especially geared towards simplifying the filename of anime torrent files.
* Removes some common metadata tags like '320kbps' '1080p' 'BluRay'. Supports custom blacklist.
* Removes dot dash and underscore separators. 
* Removes dash separators. `default = enabled`
* Removes text inside square brackets. `default = enabled`
* Removes text inside parentheses. `default = enabled`
* Removes extra whitespace.
* Smart capitalization (preserves words like reZero, USA). `default = enabled`
* Removes non-English characters. `default = enabled`
* Displays preview before finalizing the rename.
* Prevents two or more files ending up with the same rename.


## Requirements

* Windows: no prerequisites.
* Linux and Mac: Install python using your package manager or follow instruction on the [official website](https://www.python.org/).


## How to use


### Windows

Download the latest `simplify.exe` from the [releases](https://github.com/Az-21/filename-simplifier/releases).

**NOTE:** This file will take some 10-20 seconds to load on the first run. Furthermore, you might have to click on `run anyways` when prompted by Windows smartscreen. You can prevent the wait and confirmation if you follow the instructions in the upcoming section.


---


### Linux and Mac


[Download](https://github.com/Az-21/filename-simplifier/archive/master.zip) and extract the .zip.

\[Optional\] Or clone this repo using `git clone https://github.com/Az-21/filename-simplifier.git`

Open up your terminal where you've cloned/extracted the `simplify.py` file and run filename simplifier using

```bash
python3 simplify.py
```

---

That's it! Everything is then done using [y/n] prompts

## Customization

Press `y` when prompted with `Edit settings? (y/n): `.

Currently you can customize

* Blacklisted words
* Extensions to work on (custom extensions)
* Various flags to enable/disable the `default` actions mentioned in the features earlier.

## [Link to the Roadmap](https://github.com/Az-21/filename-simplifier/projects/1)

Features I'm currently working on. 

Suggestions are always welcomed.


# Changelog

```diff
v1.0.2
+ fixed extension capitalization (.mp4 -> .Mp4)

v1.0
+ improved title case function (now preserves words like reZero)
+ added dash separator removal
+ moved default config inside the .py (now .exe no longer requires config.json to work)
+ all config lists are now space separated
+ added option to rename everything + folders

v0.6
+ added dot and underscore separators
+ added (almost) all popular audio, video, and subtitle extensions
+ added option to convert to Title Case
+ complete revamp of blacklist removal. now uses regex
+ cleaned up prompt text. minimized user interaction
+ added safety check for 2+ files ending with the same simplified rename
+ very first .exe release

v0.5
+ refactored code
+ support for CLI settings editor
+ support for custom blacklist
+ support for custom extensions
+ foundation for dot removal and underscore removal
+ cleaned up code significantly

v0.3.2
+ fixed various bugs and vulnerabilities
- removed some ASCII art

v0.3.1
+ added some ASCII art

v0.3
+ moved user setting to a dedicated .py file
+ added support for different file formats (vid, audio, docs, custom)

v0.2
+ remove non-English characters
+ user config support to disable the removal of non-Eng characters
+ increased functionality of extra whitespace removal function ( text ) -> (text)

v0.1
+ remove everything inside square brackets
+ remove extra whitespace
+ user config to support additional filter keywords and extensions
```
