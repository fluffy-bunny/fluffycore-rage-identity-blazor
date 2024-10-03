package main

import (
	"flag"
	"fmt"
	"io/ioutil"
	"log"
	"net/http"
	"regexp"
	"strings"

	"github.com/google/uuid"
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

	// Read index.html from disk
	indexPath := fmt.Sprintf("%s/index.html", *wwwroot)
	indexContent, err := ioutil.ReadFile(indexPath)
	if err != nil {
		log.Fatalf("Failed to read index.html: %v", err)
	}

	// Generate a unique GUID for cache busting
	guid := uuid.New().String()

	// Replace <script src="..."></script> with cache-busted versions
	re := regexp.MustCompile(`<script src="([^"]+)"></script>`)
	modifiedContent := re.ReplaceAllStringFunc(string(indexContent), func(match string) string {
		return strings.Replace(match, `">`, fmt.Sprintf(`?temporaryQueryString=%s">`, guid), 1)
	})

	// Define a GET endpoint to serve the modified index.html from memory
	e.GET("/", func(c echo.Context) error {
		return c.HTML(http.StatusOK, modifiedContent)
	})

	// Print out the path to wwwroot and start the server
	fmt.Printf("Serving files from %s\n", *wwwroot)
	fmt.Printf("Server started on port %s\n", *port)
	e.Logger.Fatal(e.Start(":" + *port))
}
