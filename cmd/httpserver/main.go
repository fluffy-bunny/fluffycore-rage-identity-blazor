package main

import (
	"flag"
	"fmt"

	"github.com/labstack/echo/v4"
)

func main() {
	// Define command-line flags for port and wwwroot path
	port := flag.String("port", "8080", "Port to listen on")
	wwwroot := flag.String("wwwroot", "wwwroot", "Path to wwwroot directory")
	flag.Parse()

	e := echo.New()

	// Serve static files from the specified directory
	e.Static("/", *wwwroot)

	// Define a GET endpoint to serve index.html
	e.GET("/", func(c echo.Context) error {
		return c.File(fmt.Sprintf("%s/index.html", *wwwroot))
	})

	// Print out the path to wwwroot and start the server
	fmt.Printf("Serving files from %s\n", *wwwroot)
	fmt.Printf("Server started on port %s\n", *port)
	e.Logger.Fatal(e.Start(":" + *port))
}
