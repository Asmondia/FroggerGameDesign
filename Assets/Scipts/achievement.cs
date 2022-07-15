using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class achievement
{
    public int ID;
    public string description;
    public achievement(int ID, string description)
    {
        this.ID = ID;
        this.description = description;
    }
}
