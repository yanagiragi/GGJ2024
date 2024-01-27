using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHand
{
    public void Slap();

    public void Resolve(bool isInputDown);
}
