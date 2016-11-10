using System.Collections.Generic;
using OQF.Application.Tournament.ViewModels;
using OQF.Bot.Contracts;

namespace OQF.Application.Tournament.Services
{
    public interface ITournamentService
    {
        void StartTournament(IEnumerable<BotData> contestants, GameConstraints constraints);

    }
}
