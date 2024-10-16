# Function to process a Blazor project
function Process-BlazorProject {
    param (
        [string]$projectName,
        [string]$destinationPath,
        [string]$baseHref
    )

    Write-Host "Processing $projectName..."

    # Ensure the destination directory exists
    New-Item -ItemType Directory -Force -Path $destinationPath | Out-Null

    # Build and publish the Blazor project
    Set-Location $projectName
    dotnet publish -c Release -o ./publish
    Set-Location ..

    # Prepare the destination directory
    $wwwrootPath = Join-Path $destinationPath "wwwroot"
    if (Test-Path $wwwrootPath) {
        Remove-Item $wwwrootPath -Recurse -Force
    }

    # Copy the published files
    Copy-Item -Path (Join-Path $projectName "publish\wwwroot") -Destination $wwwrootPath -Recurse

    # Process the index.html file
    $indexPath = Join-Path $wwwrootPath "index.html"
    $templatePath = Join-Path $wwwrootPath "index_template.html"
    Rename-Item -Path $indexPath -NewName "index_template.html"

    # Modify the base href and add version
    $content = Get-Content -Path $templatePath -Raw
    $content = $content -replace '<base href="/" />', "<base href=`"$baseHref`" />"
    $newGuid = [Guid]::NewGuid().ToString()
    $content = $content -replace '{version}', $newGuid
    $content | Set-Content -Path $templatePath

    Write-Host "$projectName processed. New version GUID: $newGuid"
}

# Main script execution
try {
    # Process BlazorOIDCFlow
    Process-BlazorProject -projectName "BlazorOIDCFlow" `
        -destinationPath ".\cmd\httpserver\static\oidc-login-ui" `
        -baseHref "/oidc-login-ui/"

    # Process BlazorAccountManagement
    Process-BlazorProject -projectName "BlazorAccountManagement" `
        -destinationPath ".\cmd\httpserver\static\management" `
        -baseHref "/management/"

    # Build Go project
    Write-Host "Building Go project..."
    Set-Location .\cmd\httpserver
    go build .
    Set-Location ..\..

    Write-Host "All operations completed successfully."
}
catch {
    Write-Host "An error occurred: $_" -ForegroundColor Red
}
finally {
    # Ensure we're back in the original directory
    Set-Location $PSScriptRoot
}