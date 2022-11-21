using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InGameSetting
{
    private static bool BgMusicStatus = true;
    private static bool SoundEffectStatus = true;
    private static int Mode;

    public static bool BackgroundMusic
    {
        get 
        {
            return BgMusicStatus;
        }
        set 
        {
            BgMusicStatus = value;
        }
    }

    public static bool SoundEffect
    {
        get 
        {
            return SoundEffectStatus;
        }
        set 
        {
            SoundEffectStatus = value;
        }
    }

    public static int GameMode
    {
        get 
        {
            return Mode;
        }
        set 
        {
            Mode = value;
        }
    }

    
}