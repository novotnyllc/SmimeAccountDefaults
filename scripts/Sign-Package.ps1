Param(
	[string]$signClientSecret,
	[string]$signClientUser,
	[string]$filePath
)

$currentDirectory = split-path $MyInvocation.MyCommand.Definition

# See if we have the ClientSecret available
if([string]::IsNullOrEmpty($signClientSecret)){
	Write-Host "Client Secret not found, not signing packages"
	return;
}

nuget install SignClient -Version 0.9.0 -SolutionDir $currentDirectory\..\ -Verbosity quiet -ExcludeVersion
# Setup Variables we need to pass into the sign client tool

$appSettings = "$currentDirectory\appsettings.json"
$fileList = "$currentDirectory\filelist.txt"

$appPath = "$currentDirectory\..\packages\SignClient\tools\netcoreapp2.0\SignClient.dll"

$nupgks = ls $currentDirectory\..\*.nupkg | Select -ExpandProperty FullName


Write-Host "Submitting $filePath for signing"

dotnet $appPath 'sign' -c $appSettings -i $filePath -f $fileList -r $signClientUser -s $signClientSecret -n 'SmimeAccountDefaults' -d 'SmimeAccountDefaults' -u 'https://github.com/onovotny/SmimeAccountDefaults' 

Write-Host "Finished signing $filePath"


Write-Host "Sign-package complete"