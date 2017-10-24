// -----------------------------------------------------------------------
//  <copyright file="Program.cs" company="Asynkron HB">
//      Copyright (C) 2015-2017 Asynkron HB All rights reserved
//  </copyright>
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Messages;
using Proto.Remote;
using ProtosReflection = Messages.ProtosReflection;

namespace Node1
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            Serialization.RegisterFileDescriptor(ProtosReflection.Descriptor);
            Remote.Start("127.0.0.1", 12001);

            Console.WriteLine("Start Node1(C#), press any key to proceed\n");
            Console.ReadKey();

            var actorPidResponse = await Remote.SpawnNamedAsync("127.0.0.1:12000", "remote", "hello", TimeSpan.FromSeconds(10));
            var res = await actorPidResponse.Pid.RequestAsync<HelloResponse>(new HelloRequest { });

            Console.WriteLine(res.Message);
            Console.ReadLine();
        }
    }
}