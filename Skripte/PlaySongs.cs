using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class PlaySongs : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] audioClips;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(PlayAudioSequentially());
    }

    IEnumerator PlayAudioSequentially()
    {
        yield return null; // Execution pauses here and continues the next frame

        //1.Loop through each AudioClip
        for (int i = 0; i < audioClips.Length; i++)
        {
            //2.Assign current AudioClip to audiosource
            audioSource.clip = audioClips[i];

            //3.Play Audio
            audioSource.Play();

            //4. Delay playing next song till the current finishes
            yield return new WaitForSeconds(audioClips[i].length);

            //5. Go back to #2 and play the next audio in the audioClips array
        }
    }
}
