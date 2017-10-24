package main

import (
	"fmt"
	"time"

	console "github.com/AsynkronIT/goconsole"
	"github.com/AsynkronIT/protoactor-go/remote"
	"github.com/windperson/gtg28-protoactor-demo/golang/remoteactivate/messages"
)

func main() {

	timeout := 5 * time.Second
	remote.Start("127.0.0.1:8081")

	fmt.Println("Start Node1 (Go)")

	actorPidResp, _ := remote.SpawnNamed("127.0.0.1:12000", "remote", "hello", timeout)
	res, _ := actorPidResp.Pid.RequestFuture(&messages.HelloRequest{}, timeout).Result()
	response := res.(*messages.HelloResponse)
	fmt.Printf("Response from remote %v", response.Message)

	console.ReadLine()
}
