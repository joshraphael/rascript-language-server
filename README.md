# rascript-language-server

# RAScript Language Server

Provides language support for the [RATools](https://github.com/Jamiras/RATools) scripting language conforming to Microsofts [Language Server Protocol](https://microsoft.github.io/language-server-protocol/)

[![GitHub License](https://img.shields.io/github/license/joshraphael/rascript-language-server)](https://github.com/joshraphael/rascript-language-server/blob/main/LICENSE)
[![pipeline](https://github.com/joshraphael/rascript-language-server/actions/workflows/release.yaml/badge.svg)](https://github.com/joshraphael/rascript-language-server/actions/workflows/release.yaml)
[![GitHub Tag](https://img.shields.io/github/v/tag/joshraphael/rascript-language-server)](https://github.com/joshraphael/rascript-language-server/tags)
[![GitHub repo size](https://img.shields.io/github/repo-size/joshraphael/rascript-language-server)](https://github.com/joshraphael/rascript-language-server/archive/main.zip)

## Requirements
- dotnet
- Make

## Quickstart
To start, download and unzip the latest binary release for your operating system [here](https://github.com/joshraphael/rascript-language-server/releases/latest). Point your language server client to the location of the downloaded binary, example:

Linux/MacOS:
```text
/home/joshraphael/rascript-language-server_v0.0.1_linux-x64
```

Windows:
```text
C:\Users\joshraphael\rascript-language-server_v0.0.1_win-x64.exe
```

## Feature Highlights
- Syntax Highlighting - Custom RAScript syntax highlighting using TextMate.
- Function navigation - Jump to a functions defintion.
- Code Completion - Completion results appear for symbols as you type.
- Hover Info - Documentation appears when you hover over a function.