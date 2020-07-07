# Filename Simplifier

A simple tool to mass-simplify (mass-rename) the filename of video, music, document, and much more.

## Features

* Simplifies the filename of videos downloaded via torrents. Especially geared towards simplifying the filename of anime torrent files.
* Removes some common tags like '1080p'. Supports custom blacklist in runtime.
* Removes non-English characters, dot separators, and underscores. Plus, flags to enable/disable them individually.
* Removes text inside square brackets.
* Removes extra whitespace, including the trailing whitespace.
* Displays preview before finalizing the rename.

**Before**
```
[GroupName] Generic Isekai 
ジェネリック異世界 (2020) - S01E07 [1080p][HEVC x265 10bit][Multi-Subs] [CB67A2] (Weekly).mp4
```
**After**
```
Generic Isekai (2020) - S01E07.mp4
```

## Requirements

Only [Python](https://www.python.org/) is required to run this script.
You can get `python3` using `choco` package manger on Windows, `brew` package manager on macOS, and your default package manager on Linux.

## How to use

First download the script using `git clone https://github.com/Az-21/filename-simplifier.git`, or just download and extract the .zip provided by github.

Open up your terminal (Win/Mac/Linux) where you've cloned/extracted the `simplify.py` file and run it using the command given below. The program will ask where the files are located. So, you can have `simplify.py` and the files you want to rename in different folders.

```bash
python simplify.py
```

That's it! Everything is then done using [y/n] prompts

## Customization

This program has built-in runtime setting customization. Press `y` when prompted with `Edit settings? (y/n): `.

Currently you can customize

* Blacklisted words
* Extensions to work on (custom extensions)
* Flag to enable/disable removal of non-English characters `(default = enabled)`
* Flag to enable/disable removal of dot separators `(default = enabled)`
* Flag to enable/disable removal of underscore separators `(default = enabled)`

## [Roadmap](https://github.com/Az-21/filename-simplifier/projects/1)

Features planned for the future

# Changelog

```diff
v2.0
+ refactored code
+ support for CLI settings editor
+ support for custom blacklist
+ support for custom extensions
+ foundation for dot removal and underscore removal
+ cleaned up code significantly

v1.2.5
+ fixed various bugs and vulnerabilities

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
