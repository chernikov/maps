[T4Scaffolding.Scaffolder(Description = "Create Mapper Collection and add class inside")][CmdletBinding()]
param(        
	[string]$Project,
	[string]$CodeLanguage,
	[string[]]$TemplateFolders,
	[switch]$Force = $true
)

# Find the MapperCollection class, or create it via a template if not already present
$foundModelType = Get-ProjectType MapperCollection -Project $Project -AllowMultiple
if(!$foundModelType) { return }

$outputPath = Join-Path "Models\Mappers" "ModelMapper"
$defaultNamespace = (Get-Project $Project).Properties.Item("DefaultNamespace").Value

Add-ProjectItemViaTemplate $outputPath -Template ModelMapper `
	-Model @{ Namespace = $defaultNamespace; ModelType = $foundModelType } `
	-SuccessMessage "Create ModelMapper {0}" `
	-TemplateFolders $TemplateFolders -Project $Project -CodeLanguage $CodeLanguage -Force:$Force
	