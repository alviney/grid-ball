using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : Translater
{
    public Launch launch;
    
    public override void Move (JoystickEvent jEvent) {
        if (GameState.instance.characterMode == CharacterMode.Launcher) base.Move(jEvent); 
    }

    public void Aim (JoystickEvent jEvent) {
        if (GameState.instance.characterMode == CharacterMode.Launcher) launch.Aim(jEvent); 
    }
}