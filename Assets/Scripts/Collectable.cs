using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField]
    private SoundID sound = SoundID.None;

    public virtual void Collect() // Play an effect sound when collected
    {
        AudioManager.Instance.PlayEffect(sound);
    }
}
