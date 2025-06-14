#!/bin/bash

if [[ $1 != @(linux-x64|win-x64|osx-x64|osx-arm64) ]]; then
    echo "Invalid architecture: $1"
    exit 1
fi

export PREFIX=""

if [[ ${GITHUB_REF_NAME} != "" ]]; then
    PREFIX="-$GITHUB_REF_NAME"
fi

dotnet publish -r $1 -p:PublishSingleFile=true,AssemblyName=rascript-language-server-$1$PREFIX --self-contained true