using System.Collections;
using UnityEngine;

public class IntroClipSubManager : MonoBehaviour
{
    public static IntroClipSubManager introInstance;

    private void Awake()
    {
        introInstance = this;
    }

    public IEnumerator GenerateSubtitles()
    {
        switch (SubtitleController.language)
        {
            case eSubtitleLanguage.English:
                Debug.Log("Whoopsies");
                SubtitleController.CreateNewSubtitle("Is this thing working ?", 1.5f);
                yield return new WaitForSeconds(1.5f);
                SubtitleController.CreateNewSubtitle("*Ahem* We don't have much time. The submarine is the last one we have, and it's in rough shape.", 3.7f);
                yield return new WaitForSeconds(3.7f);
                SubtitleController.CreateNewSubtitle("It's rusty as shit, and the navigation controls aren't fully operational.", 2.6f);
                yield return new WaitForSeconds(2.6f);
                SubtitleController.CreateNewSubtitle("Even so, you're our last hope for humanity to survive this catastrophe.", 3f);
                yield return new WaitForSeconds(3f);
                SubtitleController.CreateNewSubtitle("All you have to do is make it to the deep water facility, so focus your efforts on getting there and things should go without a hitch.", 5f);
                yield return new WaitForSeconds(5f);
                SubtitleController.CreateNewSubtitle("That said, the submarine is outfitted with an exterior light and what can best be described as a taser.", 4.2f);
                yield return new WaitForSeconds(4.2f);
                SubtitleController.CreateNewSubtitle("In the unlikely event you run into unfriendly sea life, those should do the trick.", 4.2f);
                yield return new WaitForSeconds(4.2f);
                SubtitleController.CreateNewSubtitle("Don't forget; the submarine's battery can't handle much strain,", 3.7f);
                yield return new WaitForSeconds(5.5f);
                SubtitleController.CreateNewSubtitle("so don't go turning everything on all at once or you'll trip the breaker.", 2.45f);
                yield return new WaitForSeconds(5.5f);
                SubtitleController.CreateNewSubtitle("Being a battery, you have a limited amount of power so don't burn through it - no power means no oxygen, no oxygen means game over.", 6.8f);
                yield return new WaitForSeconds(6.8f);
                SubtitleController.CreateNewSubtitle("Speaking of oxygen, mind your tank.", 1.5f);
                yield return new WaitForSeconds(1.5f);
                SubtitleController.CreateNewSubtitle("The sub's too old for an automatic electrolyser, so you'll have to manually refill it if you start running low.", 4.2f);
                yield return new WaitForSeconds(4.2f);
                SubtitleController.CreateNewSubtitle("If you for some unimaginable reason you need to LOSE some air, you can turn the valve attached to the tank to vent out oxygen.", 6f);
                yield return new WaitForSeconds(6f);
                SubtitleController.CreateNewSubtitle("Last thing you'll need t- oh. Shit. Shit! I'm out of time - you're on your own now, good luck and-", 6f);
                yield return new WaitForSeconds(6f);
                break;
            case eSubtitleLanguage.Spanish:
                break;
            default: Debug.Log("Wtf");
                break;
        }
        yield return null; 
    }
}
