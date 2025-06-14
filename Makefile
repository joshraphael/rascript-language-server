RATOOLS_VERSION := v1.15.1

reset:
	rm -rf bin/
	rm -rf obj/
	rm -rf RATools/ && git submodule update --init --recursive && cd RATools/ && git checkout tags/${RATOOLS_VERSION}
	dotnet restore

modify:
	rm -rf RATools/Tests
	rm -rf RATools/Core/UI
	rm -rf RATools/Core/Tests
	rm -rf RATools/Core/Core/Tests
	rm -rf RATools/Source/ViewModels
	rm -rf RATools/Source/Views
	rm -rf RATools/Core/Database
	rm -rf RATools/Source/Services
	rm -rf RATools/Source/Properties/Settings.Designer.cs
	rm -rf RATools/Core/Tools
	rm -rf RATools/Source/App.xaml.cs
	rm -rf RATools/Source/AssemblyInfo.cs
	rm -rf RATools/Source/rascript-cli/Program.cs
	sed -i '1iusing Timer = System.Timers.Timer;' RATools/Core/Core/Source/Services/Impl/TimerService.cs

run: reset modify
	dotnet run

build: reset modify build-linux-x64 build-win-x64 build-osx-x64 build-osx-arm64

build-linux-x64:
	dotnet publish -r linux-x64 -p:PublishSingleFile=true,AssemblyName=rascript-language-server-linux-x64 --self-contained true

build-win-x64:
	dotnet publish -r win-x64 -p:PublishSingleFile=true,AssemblyName=rascript-language-server-win-x64 --self-contained true

build-osx-x64:
	dotnet publish -r osx-x64 -p:PublishSingleFile=true,AssemblyName=rascript-language-server-osx-x64 --self-contained true

build-osx-arm64:
	dotnet publish -r osx-arm64 -p:PublishSingleFile=true,AssemblyName=rascript-language-server-osx-arm64 --self-contained true