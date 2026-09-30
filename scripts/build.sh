#!/bin/bash

if [[ $1 != @(linux-x64|win-x64|osx-x64|osx-arm64|wasi-wasm) ]]; then
    echo "Invalid architecture: $1"
    exit 1
fi

export BUILD_KEYWORD=publish

if [[ $1 == "wasi-wasm" ]]; then
    BUILD_KEYWORD=build
fi

export PREFIX=""

if [[ ${GITHUB_REF_NAME} != "" ]]; then
    PREFIX="_$GITHUB_REF_NAME"
fi
killall dotnet
dotnet clean src/rascript-language-server.csproj
dotnet restore src/rascript-language-server.csproj
dotnet ${BUILD_KEYWORD} src/rascript-language-server.csproj -r $1 -c Release -p:AssemblyName=rascript-language-server${PREFIX}_$1 --self-contained true --verbosity quiet -consoleloggerparameters:ErrorsOnly --tl:off