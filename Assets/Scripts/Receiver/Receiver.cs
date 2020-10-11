using UnityEngine;

public class Receiver : Translater   
{
    public PathCreater pathCreater;

    public override void Move (JoystickEvent jEvent) {
        if (GameState.instance.characterMode == CharacterMode.Receiver) base.Move(jEvent); 
    }

    public void Path(JoystickEvent jEvent) {
        if (GameState.instance.characterMode == CharacterMode.Receiver) pathCreater.Create(jEvent);
    }
}