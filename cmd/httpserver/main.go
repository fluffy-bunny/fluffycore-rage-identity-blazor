package main

import (
	"log"
	"net/http"
)

const (
	port = ":8099"
)

func main() {
	// Serve static files from the "static" directory
	fs := http.FileServer(http.Dir("./publish/wwwroot"))
	http.Handle("/", fs)

	// Start the server on port 8080
	log.Println("Listening on port", port)
	err := http.ListenAndServe(port, nil)
	if err != nil {
		log.Fatal(err)
	}
}
