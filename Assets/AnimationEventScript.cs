using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventScript : MonoBehaviour
{
    public UIManager manager;
    public void HideBossHpBar()
    {
        manager.HideBossHpBar();
    }
}
