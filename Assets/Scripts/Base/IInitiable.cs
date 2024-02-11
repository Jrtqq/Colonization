using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInitiable
{
    void Init(GameObject instantiator, Vector3 instantiatePosition);
}
