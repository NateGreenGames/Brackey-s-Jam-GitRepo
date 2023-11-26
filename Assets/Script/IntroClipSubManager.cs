using System.Collections;
using UnityEngine;

public class IntroClipSubManager : MonoBehaviour
{
    public static IntroClipSubManager introInstance;

    private void Awake()
    {
        introInstance = this;
    }

    public IEnumerator GenerateSubtitles(int _languageIdx)
    {
        switch (_languageIdx)
        {
            case 0:
                SubtitleController.CreateNewSubtitle("Is this thing working ?", 1.5f);
                yield return new WaitForSeconds(1.5f);
                SubtitleController.CreateNewSubtitle("*Ahem* We don't have much time. The submarine is the last one we have, and it's in rough shape.", 3.7f);
                yield return new WaitForSeconds(3.7f);
                SubtitleController.CreateNewSubtitle("It's rusty as shit, and the navigation controls aren't fully operational. Even so, you're our last hope for humanity to survive this catastrophe.", 6.7f);
                yield return new WaitForSeconds(6.7f);
                SubtitleController.CreateNewSubtitle("All you have to do is make it to the deep water facility, so focus your efforts on getting there and things should go without a hitch.", 5f);
                yield return new WaitForSeconds(5f);
                SubtitleController.CreateNewSubtitle("That said, the submarine is outfitted with an exterior light and what can best be described as a taser.", 4.2f);
                yield return new WaitForSeconds(4.2f);
                break;
            case 1:
                break;
            default: Debug.Log("Wtf");
                break;
        }
        yield return null; 
    }
}
