﻿using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Neuro.Debugging;

[DebugWindow]
public sealed class TestDebugWindow2 : DebugWindow
{
    public override string Name => "Another test";

    public override bool ShouldShow()
    {
        return PlayerControl.LocalPlayer && TutorialManager.InstanceExists;
    }

    public override void BuildWindow()
    {
        if (GUILayout.Button("Something else"))
        {
            Minigame minigamePrefab = ShipStatus.Instance.GetComponentsInChildren<SystemConsole>().First(c => c.FreeplayOnly).MinigamePrefab;
            PlayerControl.LocalPlayer.NetTransform.Halt();
            Minigame minigame = Object.Instantiate(minigamePrefab, Camera.main!.transform, false);
            minigame.transform.localPosition = new Vector3(0f, 0f, -50f);
            minigame.Begin(null);
        }
    }
}
