using System;
using System.Collections.Generic;
using System.Reflection;
using NetMQ;
using NetMQ.Sockets;
using OQF.Application.Tournament.Utils;
using OQF.Bot.Contracts;
using OQF.Utils;
using Serilog;
using Serilog.Core;

namespace OQF.Application.BotWrapper
{
    class Program
    {
        private static Logger log = new LoggerConfiguration().WriteTo.RollingFile("log-{Date}.txt").CreateLogger();

        static void Main(string[] args)
        {
            var listener = new CommandListener(new MessageHandler());

            listener.Listen();
        }
    }

    public class CommandListener
    {
        private readonly Tuple<IQuoridorBot, string> botInfo;

        private readonly MessageHandler messageHandler;

        private static Logger log = new LoggerConfiguration().WriteTo.RollingFile("log-{Date}.txt").CreateLogger();

        public CommandListener(MessageHandler handler)
        {
            this.messageHandler = handler;
        }

        public void Listen()
        {
            using (var socket = new ResponseSocket("tcp://*:5555"))
            {
                while (true)
                {
                    //Messageformat: command|argument1;argument2...
                    var msg = socket.ReceiveMultipartStrings().ToArray();
                    var msgType = (MessageTypes) Enum.Parse(typeof(MessageTypes), msg[1]);
                    log.Information($"got message with header:{msg[1]} and content:{msg[2]}");

                    var handler = this.messageHandler.GetHandler(msgType, socket);
                    handler.Handle(botInfo, msg[2]);
                }
            }
        }

    }

    public class MessageHandler
    {
        public IResponseHandler GetHandler(MessageTypes messageType, ResponseSocket socket)
        {
            switch (messageType)
            {
                case MessageTypes.LoadDll:
                    return new LoadDllHandler(socket);
                case MessageTypes.Init:
                    return new InitHandler(socket);
                case MessageTypes.DoNextMove:
                    return new NextMoveHandler(socket);
                default:
                    throw new NotImplementedException();
            }
        }
    }

    public class InitHandler : IResponseHandler
    {
        public InitHandler(ResponseSocket socket)
        {
            throw new NotImplementedException();
        }

        public void Handle(Tuple<IQuoridorBot, string> botInfo, string message)
        {
            throw new NotImplementedException();
        }
    }

    public class NextMoveHandler : IResponseHandler
    {
        public NextMoveHandler(ResponseSocket socket)
        {
    
        }

        public void Handle(Tuple<IQuoridorBot, string> botInfo, string message)
        {
            throw new NotImplementedException();
        }
    }

    public class LoadDllHandler : IResponseHandler
    {
        private ResponseSocket socket;

        public LoadDllHandler(ResponseSocket socket)
        {
            this.socket = socket;
        }

        public void Handle(Tuple<IQuoridorBot, string> botInfo, string message)
        {
            var botAssembly = Assembly.LoadFile(message);

            botInfo = BotLoader.LoadBot(botAssembly);

            socket.SendFrame("ok");
            
        }
    }

    public interface IResponseHandler
    {
        void Handle(Tuple<IQuoridorBot, string> bots, string message);
    }
}