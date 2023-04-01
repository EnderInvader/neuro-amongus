﻿using System.Collections;
using System.Linq;
using HarmonyLib;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Neuro.Cursor;
using Neuro.Utilities;
using UnityEngine;

namespace Neuro.Minigames.Completion.Solvers;

[MinigameSolver(typeof(VendingMinigame))]
public sealed class BuyBeverageSolver : MinigameSolver<VendingMinigame>
{
    public override IEnumerator CompleteMinigame(VendingMinigame minigame, NormalPlayerTask task)
    {
        UiElement[] uiElements = minigame.ControllerSelectable.ToArray();

        InGameCursor.Instance.MoveTo(uiElements.First(e => e.name.ToLower() == "vending_button" + minigame.targetCode[0]));
        minigame.EnterDigit(minigame.targetCode[0].ToString());

        yield return new WaitForSeconds(0.15f * DELAY_MULTIPLIER);

        InGameCursor.Instance.MoveTo(uiElements.First(e => e.name.ToLower() == "vending_button" + minigame.targetCode[1]));
        minigame.EnterDigit(minigame.targetCode[1].ToString());

        yield return new WaitForSeconds(0.15f * DELAY_MULTIPLIER);

        InGameCursor.Instance.MoveTo(uiElements.First(e => e.name.ToLower() == "admin_keypad_check"));
        minigame.AcceptDigits();
    }
}
