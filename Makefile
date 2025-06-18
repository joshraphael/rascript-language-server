SHELL := /bin/bash
RATOOLS_VERSION := v1.15.1

reset:
	rm -rf src/bin/
	rm -rf src/obj/
	rm -rf src/RATools/ && git submodule update --init --recursive && cd src/RATools/ && git checkout tags/${RATOOLS_VERSION}
	dotnet restore src/rascript-language-server.csproj

modify:
	rm -rf src/RATools/Tests
	rm -rf src/RATools/Core/UI
	rm -rf src/RATools/Core/Tests
	rm -rf src/RATools/Core/Core/Tests
	rm -rf src/RATools/Source/ViewModels
	rm -rf src/RATools/Source/Views
	rm -rf src/RATools/Core/Database
	rm -rf src/RATools/Source/Services
	rm -rf src/RATools/Source/Properties/Settings.Designer.cs
	rm -rf src/RATools/Core/Tools
	rm -rf src/RATools/Source/App.xaml.cs
	rm -rf src/RATools/Source/AssemblyInfo.cs
	rm -rf src/RATools/Source/rascript-cli/Program.cs
	sed -i '1iusing Timer = System.Timers.Timer;' src/RATools/Core/Core/Source/Services/Impl/TimerService.cs

run: reset modify
	dotnet run --project src/rascript-language-server.csproj

build: reset modify build-linux-x64 build-win-x64 build-osx-x64 build-osx-arm64

build-linux-x64:
	./scripts/build.sh linux-x64

build-win-x64:
	./scripts/build.sh win-x64

build-osx-x64:
	./scripts/build.sh osx-x64

build-osx-arm64:
	./scripts/build.sh osx-arm64