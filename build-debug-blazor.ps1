cd BlazorOIDCFlow
dotnet publish -c Debug -o ./publish.Debug
cd ..
cd .\BlazorAccountManagement
dotnet publish -c Debug -o ./publish.Debug
cd ..
 
go build ./cmd/httpserver/
