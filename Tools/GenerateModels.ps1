dotnet tool restore
$OUTPUT_PATH = Join-Path $PSScriptRoot "..\Models\ContentTypes"
dotnet tool run KontentModelGenerator -p "9f25588c-7478-01ce-a4c1-90a6f971ba9c" -o $OUTPUT_PATH -n "Kentico.Kontent.Statiq.Lumen.Models"