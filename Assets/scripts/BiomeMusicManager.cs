using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BiomeMusicManager : MonoBehaviour {
    public enum BiomeType { 
        NONE,
        ICE,
        LAVA,
        SPACE
    }

    [SerializeField] BiomeType startBiome;
    [SerializeField] PseudoDictionary<BiomeType, AudioClip> biomeTracks;
    [SerializeField] float fadeDuration = 1.5f;

    private AudioSource audioSourceA;
    private AudioSource audioSourceB;
    private AudioSource currentSource;
    private BiomeType currentBiome;
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
        currentBiome = startBiome;
        currentBiomeIndex = 0;
        TransitionToBiome(currentBiome);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentBiomeIndex = (currentBiomeIndex + 1) % System.Enum.GetNames(typeof(BiomeType)).Length;
            BiomeType nextBiome = (BiomeType)currentBiomeIndex;
            TransitionToBiome(nextBiome);
        }
    }

    public void TransitionToBiome(BiomeType newBiome)
    {
        if (newBiome == currentBiome) return;

        AudioClip newClip = GetClipForBiome(newBiome);

        AudioSource nextSource = (currentSource == audioSourceA) ? audioSourceB : audioSourceA;
        // TODO does this crash?
        if (newClip != null) {
            nextSource.clip = newClip;
            nextSource.volume = 0f;
            nextSource.Play();
        } else {
            nextSource.Stop();
            nextSource.volume = 0f;
        }

        StartCoroutine(CrossfadeTracks(currentSource, nextSource));
        currentSource = nextSource;
        currentBiome = newBiome;

        Debug.Log($"Switched to biome: {newBiome}");
    }

    private AudioClip GetClipForBiome(BiomeType biome)
    {
        if (biomeTracks.ContainsKey(biome))
            return biomeTracks[biome];
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
