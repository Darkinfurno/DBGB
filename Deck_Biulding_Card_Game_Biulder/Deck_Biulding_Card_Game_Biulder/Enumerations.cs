using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deck_Biulding_Card_Game_Biulder
{
    // requires matching || action || peeking condition make
    public enum events
    {
        Draw,
        DrawIfEvenOdd,
        DrawIfTypesMatch,
        DrawIfType,
        Peek,
        Discard,
        DiscardRandom,
        DiscardIfEvenOdd,
        DiscardIfCostZero,
        DiscardIfCostNotZero,
        DiscardOfType,
        Destroy,
        AquireFreeCard // Figure Out how later
    }

    public enum Text
    {
        none,
        same,
        different,
    }

    public enum Value
    {
        none,
        even,
        odd
    }

    public enum Target
    {
        self,
        others,
        main,
        allPlayers
    }

    public enum TargetDeckType
    {
        availableCards,
        removedCards,
        deck
    }
}
