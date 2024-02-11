using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public bool IsMarked { get; private set; } = false;

    public void Mark() => IsMarked = true;
}
