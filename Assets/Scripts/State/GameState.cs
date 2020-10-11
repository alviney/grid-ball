using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterMode { Launcher, Receiver }

public class GameState : MonoBehaviour
{
    public static GameState instance;

    public CharacterMode characterMode;

    private void Awake() {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this);
    }

    public void ToggleMode() {
        if (characterMode == CharacterMode.Launcher) characterMode = CharacterMode.Receiver;
        else characterMode = CharacterMode.Launcher;
    }
}
