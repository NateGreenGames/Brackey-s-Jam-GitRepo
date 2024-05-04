using System.Collections;
using UnityEngine;

public class IntroClipSubManager : MonoBehaviour
{
    public static IntroClipSubManager introInstance;
    public AudioSource subSpeaker;
    public AudioClip englishIntro;
    public AudioClip spanishIntro;

    private void Awake()
    {
        introInstance = this;
    }

    public IEnumerator GenerateSubtitles()
    {
        switch (SubtitleController.language)
        {
            case eSubtitleLanguage.English:
                subSpeaker.clip = englishIntro;
                subSpeaker.Play();
                SubtitleController.CreateNewSubtitle("Is this thing working ?", 2.5f);
                yield return new WaitForSeconds(2.5f);
                SubtitleController.CreateNewSubtitle("*Ahem* We don't have much time. The submarine is the last one we have, and it's in rough shape.", 4.5f);
                yield return new WaitForSeconds(4.6f);
                SubtitleController.CreateNewSubtitle("It's rusty as shit, and the navigation controls aren't fully operational.", 3.5f);
                yield return new WaitForSeconds(3.5f);
                SubtitleController.CreateNewSubtitle("Even so, you're our last hope for humanity to survive this catastrophe.", 3.8f);
                yield return new WaitForSeconds(3.8f);
                SubtitleController.CreateNewSubtitle("All you have to do is make it to the deep water facility, so focus your efforts on getting there and things should go without a hitch.", 5.6f);
                yield return new WaitForSeconds(5.6f);
                SubtitleController.CreateNewSubtitle("That said, the submarine is outfitted with an exterior light and what can best be described as a taser.", 5.3f);
                yield return new WaitForSeconds(5.3f);
                SubtitleController.CreateNewSubtitle("In the unlikely event you run into unfriendly sea life, those should do the trick.", 4.2f);
                yield return new WaitForSeconds(4.2f);
                SubtitleController.CreateNewSubtitle("Don't forget; the submarine's battery can't handle much strain,", 3.7f);
                yield return new WaitForSeconds(3.7f);
                SubtitleController.CreateNewSubtitle("so don't go turning everything on all at once or you'll trip the breaker.", 2.45f);
                yield return new WaitForSeconds(2.45f);
                SubtitleController.CreateNewSubtitle("Being a battery, you have a limited amount of power so don't burn through it - no power means no oxygen, no oxygen means game over.", 7.2f);
                yield return new WaitForSeconds(7.2f);
                SubtitleController.CreateNewSubtitle("Speaking of oxygen, mind your tank.", 2.1f);
                yield return new WaitForSeconds(2.1f);
                SubtitleController.CreateNewSubtitle("The sub's too old for an automatic electrolyser, so you'll have to manually refill it if you start running low.", 4.8f);
                yield return new WaitForSeconds(4.8f);
                SubtitleController.CreateNewSubtitle("If you for some unimaginable reason you need to LOSE some air, you can turn the valve attached to the tank to vent out oxygen.", 6f);
                yield return new WaitForSeconds(6f);
                SubtitleController.CreateNewSubtitle("Last thing you'll need t- oh. Shit. Shit! I'm out of time - you're on your own now, good luck and-", 7f);
                yield return new WaitForSeconds(7f);
                break;
            case eSubtitleLanguage.Spanish:
                subSpeaker.clip = spanishIntro;
                subSpeaker.Play();
                SubtitleController.CreateNewSubtitle("�Esto funciona?", 1.6f);
                yield return new WaitForSeconds(1.6f);
                SubtitleController.CreateNewSubtitle("*Ahem* No tenemos mucho tiempo.", 1.9f);
                yield return new WaitForSeconds(1.9f);
                SubtitleController.CreateNewSubtitle("El submarino es el �ltimo que nos queda y est� en mal estado", 2.6f);
                yield return new WaitForSeconds(2.6f);
                SubtitleController.CreateNewSubtitle("Est� oxidado hasta el carajo", 1f);
                yield return new WaitForSeconds(1f);
                SubtitleController.CreateNewSubtitle("y los controles de navegaci�n no est�n completamente operativos.", 2.55f);
                yield return new WaitForSeconds(2.55f);
                SubtitleController.CreateNewSubtitle("Aun as�, eres nuestra �ltima esperanza para que la humanidad sobreviva a esta cat�strofe.", 4.6f);
                yield return new WaitForSeconds(4.6f);
                SubtitleController.CreateNewSubtitle("Todo lo que tienes que hacer es llegar a la instalaci�n submarina,", 2.8f);
                yield return new WaitForSeconds(2.8f);
                SubtitleController.CreateNewSubtitle("as� que enfoca tus esfuerzos en llegar all�", 2.1f);
                yield return new WaitForSeconds(2.1f);
                SubtitleController.CreateNewSubtitle("y las cosas deber�an salir sin problemas.", 1.8f);
                yield return new WaitForSeconds(1.8f);
                SubtitleController.CreateNewSubtitle("Dicho esto, el submarino est� equipado con una luz exterior", 3.4f);
                yield return new WaitForSeconds(3.4f);
                SubtitleController.CreateNewSubtitle("y lo que mejor se puede describir como una pistola el�ctrica.", 3.3f);
                yield return new WaitForSeconds(3.3f);
                SubtitleController.CreateNewSubtitle("En el caso improbable de que te encuentres con", 2.1f);
                yield return new WaitForSeconds(2.1f);
                SubtitleController.CreateNewSubtitle("vida marina hostil, eso deber�a servir.", 3.1f);
                yield return new WaitForSeconds(3.1f);
                SubtitleController.CreateNewSubtitle("No olvides; la bater�a del submarino no puede manejar mucha carga,", 3.8f);
                yield return new WaitForSeconds(3.8f);
                SubtitleController.CreateNewSubtitle("as� que no enciendas todo de golpe o causaras un corto circuito.", 3.3f);
                yield return new WaitForSeconds(3.3f);
                SubtitleController.CreateNewSubtitle("Tienes una bater�a, y una cantidad limitada de energ�a,", 2.6f);
                yield return new WaitForSeconds(2.6f);
                SubtitleController.CreateNewSubtitle("as� que no la gastes toda: no energ�a significa no ox�geno, no ox�geno significa fin del juego.", 5.8f);
                yield return new WaitForSeconds(5.8f);
                SubtitleController.CreateNewSubtitle("Hablando de ox�geno, cuida tu tanque.", 2.3f);
                yield return new WaitForSeconds(2.3f);
                SubtitleController.CreateNewSubtitle("El submarino es demasiado antiguo para un electrolizador autom�tico,", 3.4f);
                yield return new WaitForSeconds(3.4f);
                SubtitleController.CreateNewSubtitle("as� que tendr�s que rellenarlo manualmente si empiezas a quedarte sin ox�geno.", 4f);
                yield return new WaitForSeconds(4f);
                SubtitleController.CreateNewSubtitle("Puedes usar la v�lvula conectada al tanque para dirigir el ox�geno al exterior del submarino,", 4.3f);
                yield return new WaitForSeconds(4.3f);
                SubtitleController.CreateNewSubtitle("si por alguna raz�n lo necesitas.", 2.2f);
                yield return new WaitForSeconds(2.2f);
                SubtitleController.CreateNewSubtitle("La �ltima cosa que necesitar�s es...", 2f);
                yield return new WaitForSeconds(2f);
                SubtitleController.CreateNewSubtitle("oh. Mierda. �Mierda!", 1.5f);
                yield return new WaitForSeconds(1.5f);
                SubtitleController.CreateNewSubtitle("Se me acaba el tiempo; est�s por tu cuenta ahora, buena suerte y...", 2.6f);
                yield return new WaitForSeconds(2.6f);
                SubtitleController.CreateNewSubtitle("*EXPLOSI�N FUERTE, luego est�tica*", 1.5f);
                yield return new WaitForSeconds(1.5f);

                break;
            default: Debug.Log("Wtf");
                break;
        }
        yield return null; 
    }
}
