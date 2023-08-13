using UnityEngine;

public static class AudioControllerHelpers
{
    public static AudioController Instance = AudioController.Instance;

    private static float lastPlayPlayerClimbTime = -100;
    private static float lastPlaySickleSpinTime = -100;
    private static float lastPlayBatLoopTime = -100;

    public static void PlayBatDeath() => AudioController.PlayAudio(Instance.batDeath);

    public static void PlayChestClose() => AudioController.PlayAudio(Instance.chestClose);

    public static void PlayChestOpen() => AudioController.PlayAudio(Instance.chestOpen);

    public static void PlayDenied() => AudioController.PlayAudio(Instance.denied);

    public static void PlayPickCoin() => AudioController.PlayAudio(Instance.pickCoin);

    public static void PlayPlayerAttack() => AudioController.PlayAudio(Instance.playerAttack);

    public static void PlayPlayerInjured() => AudioController.PlayAudio(Instance.playerInjured);

    public static void PlayPlayerJump() => AudioController.PlayAudio(Instance.playerJump);

    public static void PlaySickleThrow() => AudioController.PlayAudio(Instance.sickleThrow);

    public static void PlayUseTrashBin() => AudioController.PlayAudio(Instance.useTrashBin);

    public static void PlayBatHurt1() => AudioController.PlayAudio(Instance.batHurt1);

    public static void PlayBatHurt2() => AudioController.PlayAudio(Instance.batHurt2);

    public static void PlayRandomBatHurt()
    {
        switch ((int)Random.Range(1, 2 + 1))
        {
            case 1:
                PlayBatHurt1();
                break;

            case 2:
                PlayBatHurt2();
                break;
        }
    }

    public static void PlayBatIdle1() => AudioController.PlayAudio(Instance.batIdle1);

    public static void PlayBatIdle2() => AudioController.PlayAudio(Instance.batIdle2);

    public static void PlayBatIdle3() => AudioController.PlayAudio(Instance.batIdle3);

    public static void PlayBatIdle4() => AudioController.PlayAudio(Instance.batIdle4);

    public static void PlayBatFly() => Instance.globalAudioSource.PlayOneShot(Instance.batFly, Instance.batFlyVolumnScale);

    public static void PlayBombFuse() => AudioController.PlayAudio(Instance.bombFuse);

    public static void PlayBombExplode() => AudioController.PlayAudio(Instance.bombExplode);

    public static void PlayRandomBatIdle()
    {
        switch ((int)Random.Range(1, 4 + 1))
        {
            case 1: PlayBatIdle1(); break;
            case 2: PlayBatIdle2(); break;
            case 3: PlayBatIdle3(); break;
            case 4: PlayBatIdle4(); break;
        }
    }

    public static void PlaySickleSpin()
    {
        if (Time.time - lastPlaySickleSpinTime > Instance.spinAudioPlayInterval)
        {
            lastPlaySickleSpinTime = Time.time;
            AudioController.PlayAudio(Instance.sickleSpin);
        }
    }

    public static void PlayPlayerClimb()
    {
        if (Time.time - lastPlayPlayerClimbTime > Instance.climbAudioPlayInterval)
        {
            lastPlayPlayerClimbTime = Time.time;
            AudioController.PlayAudio(Instance.playerClimb);
        }
    }
}