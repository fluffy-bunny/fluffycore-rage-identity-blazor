
cd BlazorOIDCFlow
dotnet publish -c Release -o ./publish
cd ..
# Delete existing wwwroot folder
Remove-Item -Path ./cmd/httpserver/static/oidc-login-ui/wwwroot -Recurse -Force

# Copy new wwwroot folder
Copy-Item -Path ./BlazorOIDCFlow/publish/wwwroot -Destination ./cmd/httpserver/static/oidc-login-ui/wwwroot -Recurse

# Rename index.html to index_template.html
Rename-Item -Path ./cmd/httpserver/static/oidc-login-ui/wwwroot/index.html -NewName index_template.html

# Modify base href in index_template.html
$content = Get-Content -Path ./cmd/httpserver/static/oidc-login-ui/wwwroot/index_template.html -Raw
$content = $content -replace '<base href="/" />', '<base href="/oidc-login-ui/" />'
$content | Set-Content -Path ./cmd/httpserver/static/oidc-login-ui/wwwroot/index_template.html


cd .\BlazorAccountManagement
dotnet publish -c Release -o ./publish
cd ..


# Delete existing wwwroot folder
Remove-Item -Path ./cmd/httpserver/static/management/wwwroot -Recurse -Force

# Copy new wwwroot folder
Copy-Item -Path ./BlazorAccountManagement/publish/wwwroot -Destination ./cmd/httpserver/static/management/wwwroot -Recurse

# Rename index.html to index_template.html
Rename-Item -Path ./cmd/httpserver/static/management/wwwroot/index.html -NewName index_template.html

# Modify base href in index_template.html
$content = Get-Content -Path ./cmd/httpserver/static/management/wwwroot/index_template.html -Raw
$content = $content -replace '<base href="/" />', '<base href="/management/" />'
$content | Set-Content -Path ./cmd/httpserver/static/management/wwwroot/index_template.html

cd .\cmd/httpserver/
go build .
cd ..
cd ..