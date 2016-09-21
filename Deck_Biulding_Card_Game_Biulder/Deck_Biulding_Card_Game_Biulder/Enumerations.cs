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
        DrawIfAboveBelow,
        DrawDiscardedAboveBelowValue,
        DrawDiscardedType,
        DrawDiscardedTypeAboveBelowValue,
        Peek,
        Discard,
        DiscardRandom,
        DiscardOfAboveBelowValue,
        DiscardOfType,
        Destroy,
        DestroyRandom,
        PassCard,
        AquireFreeCardByValue,
        AquireFreeCardByType,
        winIf,
        endEffect
    }

    public enum CondidionToExecute
    {
        allMustMatch,
        ifAnyMatch,
        none
    }

    public enum CardSelectionType
    {
        forceAllCards,
        upToAllCards,
        SelectSpecificNumber,
        none
    }

    public enum MatchCondition
    {
        none,
        same,
        different
    }

    public enum ValueEvenOdd
    {
        none,
        even,
        odd,
        all
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

    public enum ValueRangeAboveBelow
    {
        none,
        above,
        below
    }

    public enum Special
    {
        none,
        attack,
        defend,
        oneTimeAttack
    }
}
