using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JT
{
    public abstract class Condition : ScriptableObject
    {
        public abstract bool IsValid();
    }
}
