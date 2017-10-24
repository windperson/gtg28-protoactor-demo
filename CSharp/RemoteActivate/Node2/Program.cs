﻿// -----------------------------------------------------------------------
//  <copyright file="Program.cs" company="Asynkron HB">
//      Copyright (C) 2015-2017 Asynkron HB All rights reserved
//  </copyright>
// -----------------------------------------------------------------------

using System;
using Messages;
using Proto;
using Proto.Remote;
using ProtosReflection = Messages.ProtosReflection;

namespace Node2
{

    class Program
    {
        public static void Main(string[] args)
        {
            Serialization.RegisterFileDescriptor(ProtosReflection.Descriptor);
            var props = Actor.FromFunc(ctx =>
            {
                switch (ctx.Message)
                {
                    case HelloRequest msg:
                        Console.WriteLine("Receive HelloRequest, send back Response");
                        ctx.Respond(new HelloResponse
                        {
                            Message = "Hello from node 2",
                        });
                        break;
                    default:
                        break;
                }
                return Actor.Done;
            });

            Remote.RegisterKnownKind("hello", props);
            Remote.Start("127.0.0.1", 12000);
            Console.WriteLine("Node2 started, waiting for incoming message...\n");

            Console.ReadLine();
        }
    }
}