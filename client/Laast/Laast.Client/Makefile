.DEFAULT_GOAL := local_deploy
.PHONY: publish copy_to_nas local_deploy

publish:
	dotnet publish Laast.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -o bin/publish
	
copy_to_nas:
	cp -f bin/publish/Laast.exe /Volumes/shared
	
local_deploy: publish copy_to_nas