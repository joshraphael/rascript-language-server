#!/bin/bash

if [[ $1 != @(linux-x64|win-x64|osx-x64|osx-arm64) ]]; then
    echo "Invalid architecture: $1"
    exit 1
fi

export PREFIX=""

if [[ ${GITHUB_REF_NAME} != "" ]]; then
    PREFIX="_$GITHUB_REF_NAME"
fi

dotnet publish src/rascript-language-server.csproj -r $1 -p:PublishSingleFile=true,AssemblyName=rascript-language-server${PREFIX}_$1 --self-contained true