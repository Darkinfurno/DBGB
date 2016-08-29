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
        DrawDiscardedAboveBelowValue,
        DrawDiscardedType,
        Peek,
        Discard,
        DiscardRandom,
        DiscardIfEvenOdd,
        DiscardIfCostZero,
        DiscardIfCostNotZero,
        DiscardOfType,
        Destroy,
        DestroyRandom,
        PassCard,
        AquireFreeCard,
        winIf
    }

    public enum MatchCondition
    {
        none,
        same,
        different
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
        allPlayers,
        playerLeft,
        playerRight
    }

    public enum TargetDeckType
    {
        availableCards,
        removedCards,
        deck
    }

    public enum ValueRange
    {
        none,
        above,
        below
    }
}
