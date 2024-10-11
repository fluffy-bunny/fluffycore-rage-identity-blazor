package main

import (
	"flag"
	"fmt"
	"net/http"
	"os"
	"strings"

	"github.com/labstack/echo/v4"
	"github.com/labstack/echo/v4/middleware"
	"github.com/rs/xid"
)

var (
	indexRoot        string
	indexOidcLoginUI string
	indexManagement  string
)

func noCacheMiddleware(next echo.HandlerFunc) echo.HandlerFunc {
	return func(c echo.Context) error {
		if strings.HasSuffix(c.Request().URL.Path, "index.html") ||
			c.Request().URL.Path == "/" ||
			c.Request().URL.Path == "/oidc-login-ui" ||
			c.Request().URL.Path == "/management" {
			c.Response().Header().Set("Cache-Control", "no-store, no-cache, must-revalidate, max-age=0")
			c.Response().Header().Set("Pragma", "no-cache")
			c.Response().Header().Set("Expires", "0")
		}
		return next(c)
	}
}

func serveIndexFromMemory(content string) echo.HandlerFunc {
	return func(c echo.Context) error {
		return c.HTML(http.StatusOK, content)
	}
}

func createStaticUIHandler(staticMiddleware echo.MiddlewareFunc, indexContent string, rootPath string) echo.HandlerFunc {
	return func(c echo.Context) error {
		path := c.Request().URL.Path
		if path == rootPath {
			return c.HTML(http.StatusOK, indexContent)
		}

		// This is likely a file request, try to serve it statically
		err := staticMiddleware(func(c echo.Context) error {
			return nil
		})(c)
		if err == nil {
			return nil // File was found and served
		}
		return c.NoContent(http.StatusNotFound)
	}

}

func main() {
	port := flag.String("port", "7080", "Port to listen on")
	flag.Parse()

	// read env variables
	env := os.Getenv("ASPNETCORE_ENVIRONMENT")
	fmt.Printf("ASPNETCORE_ENVIRONMENT: %s\n", env)

	guid := xid.New().String()

	// Load index files into memory
	indexRootContent, err := os.ReadFile("static/index_template.html")
	if err != nil {
		panic(err)
	}
	indexOIDCLoginUI1Content, err := os.ReadFile("static/oidc-login-ui/wwwroot/index_template.html")
	if err != nil {
		panic(err)
	}
	indexManagementContent, err := os.ReadFile("static/management/wwwroot/index_template.html")
	if err != nil {
		panic(err)
	}

	// Replace {version} with guid in all index files
	indexRoot = strings.ReplaceAll(string(indexRootContent), "{version}", guid)
	indexOidcLoginUI = strings.ReplaceAll(string(indexOIDCLoginUI1Content), "{version}", guid)
	indexManagement = strings.ReplaceAll(string(indexManagementContent), "{version}", guid)

	e := echo.New()

	e.Use(middleware.Logger())
	e.Use(middleware.Recover())
	e.Use(noCacheMiddleware)

	// Serve root index.html from memory
	e.GET("/", serveIndexFromMemory(indexRoot))

	// Serve static files and handle routing for oidc-login-ui
	oidcLoginUIStatic := middleware.StaticWithConfig(middleware.StaticConfig{
		Root:   "static/oidc-login-ui/wwwroot",
		HTML5:  true,
		Browse: false,
	})

	e.GET("/oidc-login-ui/*", createStaticUIHandler(oidcLoginUIStatic, indexOidcLoginUI, "/oidc-login-ui/"))

	// Serve static files and handle routing for management
	managementStatic := middleware.StaticWithConfig(middleware.StaticConfig{
		Root:   "static/management/wwwroot",
		HTML5:  true,
		Browse: false,
	})

	e.GET("/management/*", createStaticUIHandler(managementStatic, indexManagement, "/management/"))

	// Serve other static files from the root static folder
	e.Static("/", "static")

	fmt.Printf("Server started on port %s\n", *port)
	e.Logger.Fatal(e.Start(":" + *port))
}
