using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] List<ESoundType> eSoundTypes = new List<ESoundType>();

    public void Play()
    {
        int soundCnt = eSoundTypes.Count;
        if (soundCnt > 0)
        {
            for (int i = 0; i < soundCnt; i++)
            {
                AudioManager.instance.PlaySound(eSoundTypes[i]);
            }
        }
    }

}
