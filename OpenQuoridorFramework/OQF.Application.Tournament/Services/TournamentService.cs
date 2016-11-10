using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using NetMQ;
using NetMQ.Sockets;
using OQF.Application.Tournament.Utils;
using OQF.Application.Tournament.ViewModels;
using OQF.Bot.Contracts;

namespace OQF.Application.Tournament.Services
{
    public class TournamentService : ITournamentService
    {
        public void StartTournament(IEnumerable<BotData> contestants, GameConstraints constraints)
        {
            var botpathOne =
                @"C:\Users\Alexander\Documents\Quoridor\OpenQuoridorFramework\Bots\SimpleWalkingBot\bin\Debug\SimpleWalkingBot.dll";
            var botPathTwo =
                @"C:\Users\Alexander\Documents\Quoridor\OpenQuoridorFramework\Bots\SimpleWalkingBot\bin\Debug\SimpleWalkingBot.dll";

            StartMatch(botpathOne, botPathTwo);
        }

        private void StartMatch(string bottomBotPath, string topBotPath)
        {
            var bottomBotProcess = CreateWrapperProcessForBot();
            var topBotProcess = CreateWrapperProcessForBot();

            bottomBotProcess.Start();
            topBotProcess.Start();

            StartCommunication(bottomBotPath, topBotPath);
        }

        private Process CreateWrapperProcessForBot()
        {
            var path = @"..\..\..\OQF.Application.BotWrapper\bin\Debug\OQF.Application.BotWrapper.exe";
            var info =
                new ProcessStartInfo()

                {
                    FileName = path,
                    UseShellExecute = false,
                    WorkingDirectory = Path.GetDirectoryName(path)
                };
            return new Process() {StartInfo = info};
        }

        private void StartCommunication(string bottomPath, string topPath)
        {
            using (var socket = new RequestSocket("tcp://localhost:5555"))
            {
                socket.SendMoreFrame("bottom").SendMoreFrame(MessageTypes.LoadDll.ToString()).SendFrame(bottomPath);
                if (!CheckAnswer(socket.ReceiveFrameString()))
                    return;

                socket.SendMoreFrame("top").SendMoreFrame(MessageTypes.LoadDll.ToString()).SendFrame(topPath);
                if (!CheckAnswer(socket.ReceiveFrameString()))
                    return;
                
                socket.SendMoreFrame("bottom").SendMoreFrame(MessageTypes.Init.ToString()).SendFrame(string.Empty);
                if (!CheckAnswer(socket.ReceiveFrameString()))
                    return;

                socket.SendMoreFrame("top").SendMoreFrame(MessageTypes.Init.ToString()).SendFrame(string.Empty);
                if (!CheckAnswer(socket.ReceiveFrameString()))
                    return;

                //socket.SendFrame("FirstMove");

                while (true)
                {
                }
            }
        }

        private bool CheckAnswer(string answer)
        {
            if (answer.Equals("Error"))
            {
                Console.WriteLine("bot init failed");
                return false;
            }

            return true;
        }
    }
}