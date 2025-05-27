using System.Collections;
using UnityEngine;

public class BiomeMusicManager : MonoBehaviour
{
    public enum Biome
    {
        Ice,
        Lava,
        Space
        // Add more biomes here
    }

    [System.Serializable]
    public class BiomeMusic
    {
        public Biome biome;
        public AudioClip musicClip;
    }

    public BiomeMusic[] biomeTracks;
    public float fadeDuration = 1.5f;

    private AudioSource audioSourceA;
    private AudioSource audioSourceB;
    private AudioSource currentSource;
    private Biome currentBiome;
    private int currentBiomeIndex = 0;

    void Awake()
    {
        audioSourceA = gameObject.AddComponent<AudioSource>();
        audioSourceB = gameObject.AddComponent<AudioSource>();

        audioSourceA.loop = true;
        audioSourceB.loop = true;

        audioSourceA.playOnAwake = false;
        audioSourceB.playOnAwake = false;

        currentSource = audioSourceA;
    }

    void Start()
    {
        // Immediately play Ice biome
        currentBiome = Biome.Ice;
        currentBiomeIndex = 0;
        TransitionToBiome(currentBiome);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentBiomeIndex = (currentBiomeIndex + 1) % System.Enum.GetNames(typeof(Biome)).Length;
            Biome nextBiome = (Biome)currentBiomeIndex;
            TransitionToBiome(nextBiome);
        }
    }

    public void TransitionToBiome(Biome newBiome)
    {
        if (newBiome == currentBiome) return;

        AudioClip newClip = GetClipForBiome(newBiome);
        if (newClip == null)
        {
            Debug.LogWarning($"No clip found for biome {newBiome}");
            return;
        }

        AudioSource nextSource = (currentSource == audioSourceA) ? audioSourceB : audioSourceA;
        nextSource.clip = newClip;
        nextSource.volume = 0f;
        nextSource.Play();

        StartCoroutine(CrossfadeTracks(currentSource, nextSource));
        currentSource = nextSource;
        currentBiome = newBiome;

        Debug.Log($"Switched to biome: {newBiome}");
    }

    private AudioClip GetClipForBiome(Biome biome)
    {
        foreach (var entry in biomeTracks)
        {
            if (entry.biome == biome)
                return entry.musicClip;
        }
        return null;
    }

    private IEnumerator CrossfadeTracks(AudioSource from, AudioSource to)
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            float t = timer / fadeDuration;
            from.volume = 1f - t;
            to.volume = t;
            timer += Time.deltaTime;
            yield return null;
        }
        from.Stop();
        from.volume = 0f;
        to.volume = 1f;
    }
}
