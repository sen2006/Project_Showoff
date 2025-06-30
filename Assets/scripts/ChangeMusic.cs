using UnityEngine;

public class ChangeMusic : MonoBehaviour
{
    public BiomeMusicManager musicManager;

    private void OnTriggerEnter(Collider other)
    {
        if (musicManager == null)
        {
            Debug.LogWarning("BiomeMusicManager reference not set!");
            return;
        }

        int nextIndex = ((int)musicManager.currentBiomeIndex + 1) % System.Enum.GetNames(typeof(BiomeMusicManager.Biome)).Length;
        musicManager.currentBiomeIndex++;
        BiomeMusicManager.Biome nextBiome = (BiomeMusicManager.Biome)nextIndex;
        musicManager.TransitionToBiome(nextBiome);
    }
}
