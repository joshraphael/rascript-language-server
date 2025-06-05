RATOOLS_VERSION := v1.15.1

reset:
	rm -rf RATools/ && git submodule update --init --recursive && cd RATools/ && git checkout tags/${RATOOLS_VERSION}

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

build: reset modify
	dotnet build