package main

import (
	"fmt"
	"runtime"

	"github.com/AsynkronIT/goconsole"
	"github.com/AsynkronIT/protoactor-go/actor"
	"github.com/AsynkronIT/protoactor-go/remote"
	"github.com/windperson/gtg28-protoactor-demo/golang/remoteactivate/messages"
)

type helloActor struct{}

func (*helloActor) Receive(ctx actor.Context) {
	switch ctx.Message().(type) {
	case *messages.HelloRequest:
		fmt.Println("Receive HelloRequest, send back Response")
		ctx.Respond(&messages.HelloResponse{
			Message: "Hello from node 2 (Go)",
		})
	}
}

func newHelloActor() actor.Actor {
	return &helloActor{}
}

func init() {
	remote.Register("hello", actor.FromProducer(newHelloActor))
}

func main() {
	runtime.GOMAXPROCS(runtime.NumCPU())

	remote.Start("127.0.0.1:12000")
	fmt.Println("Node2(Go) started, waiting for incoming message...\n")

	console.ReadLine()
}
