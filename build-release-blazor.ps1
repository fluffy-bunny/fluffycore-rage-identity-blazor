cd BlazorOIDCFlow
dotnet publish -c Release -o ./publish
cd ..
cd .\BlazorAccountManagement
dotnet publish -c Release -o ./publish
cd ..
 
go build ./cmd/httpserver/
