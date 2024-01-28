using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHand
{
    public void Slap(int playerIndex);

    public void Resolve(bool isInputDown);

    public void EnableInput();
}
