# VR ACT – Unity Project

## Over het project

Dit is een VR prototype gemaakt in Unity rondom ACT (Acceptance and Commitment Therapy). Het project is gericht rondom stress en angst.

Het idee is om ACT niet alleen uit te leggen met audio, maar de gebruiker ook echt iets te laten doen en ervaren in VR. Denk aan rondkijken, bewegen en interactief bezig zijn.

De stijl is rustig en kleurrijk, met pastelkleuren nature aspecten. 

---

## Project installeren en openen

**Gebruik GitHub desktop om het project eerst binnen te halen**

1. Installeer GitHub desktop
2. Clone de repository via GitHub desktop
3. Laat het project op de locatie staan waar GitHub desktop hem heeft opgeslagen
4. Open het Unity project vanuit deze filelocatie (want het is een LS bestand)
5. Gebruik dezelfde unity versie met de versie waarin het project is gemaakt

**Verplaats het project niet zomaar naar een andere map.**
Als je het project vanaf een verkeerde locatie opent of bestanden los verplaatst, kan Unity bepaalde assets niet goed laden.

Maak voor de zekerheid altijd eerst een backup voordat je grote dingen aanpast of Unity/packages gaat updaten.

---

## Gebruikte technieken

* Unity
* URP
* XR Interaction Toolkit
* OpenXR
* VR - standalone headset

De code van het project staat bij de scripts in Unity.

---

## Hoe het werkt

De hoofdscene is een soort roadtrip/oefening in een auto

De auto zelf beweegt niet. Dit is bewust gedaan, omdat een bewegende auto in VR snel misselijkheid kan veroorzaken.

De gebruiker kan onder andere:

* Knoppen gebruiken
* Audio/radio starten
* Rondkijken en de omgeving ervaren
* Via een fade rustig de ervaring ingaan

Er was eerst een aparte lobby, maar deze is verwijderd. De introductie in de auto geeft al genoeg uitleg en rust.

---

## XR & interactie

Het project gebruikt een **XR Origin (Action-Based)**.

Voor knoppen wordt vooral `XR Simple Interactable` gebruikt. Interacties worden gekoppeld via het **Activated event**.

Gebruik geen `OnMouseDown`, want dit werkt niet goed voor VR interactie.

### Nieuwe knop toevoegen

1. Maak een GameObject
2. Voeg `XR Simple Interactable` toe
3. Koppel bij `Activated` de gewenste functie
4. Voeg eventueel kleurfeedback toe

---

## Belangrijkste scripts

### SpawnPoint.cs

Zet de XR Origin op de juiste startpositie nadat de tracking is geïnitialiseerd. Alleen de Y-rotatie wordt aangepast.

### FadeCube.cs

Regelt de fade aan het begin. Er zit een transparante cube om de camera die langzaam verdwijnt, dus houd hier rekening mee.

### ButtonSound.cs

Speelt audio af wanneer een interactable wordt geselecteerd en geeft korte visuele feedback.

### VRButtonSimple.cs

Regelt de belangrijkste knoplogica. Andere audio wordt eerst gestopt voordat nieuwe audio wordt afgespeeld.

### RadioManager

Beheert de audio van de radio.

---

## Graphics

De lighting is **gebaked**, omdat dynamische lighting minder goed werkte in VR.

Bij bomen en bladeren kan op afstand wat pixel/noise zichtbaar zijn. Hier is veel mee getest. Uiteindelijk is gekozen voor een rustigere low-poly/stylized stijl in plaats van heel veel detail.

Voor foliage wordt onder andere gebruikgemaakt van:

* Transparent Surface Type
* Alpha Clipping
* Leaf texture in de Base Map

---

## Bekende Unity-problemen

Het project heeft eerder problemen gehad met verkeerde oude XR packages en Unity-updates. Hierdoor konden bijvoorbeeld de XR Origin of bepaalde packages verdwijnen.

Uit `manifest.json` zijn eerder deze packages verwijderd:

```text
com.unity.feature.vr
com.unity.xr.androidxr-openxr
```

Daarna is OpenXR opnieuw correct ingesteld.

Als het project ineens veel errors geeft na een update: niet meteen alles gaan aanpassen. Controleer eerst de Unity versie, packages en OpenXR instellingen.

---

## Git & backups

Het project gebruikt Git LFS vanwege de grote Unity bestanden.

Aanrader:

* Werk via GitHub desktop
* Maak regelmatig een backup
* Update Unity niet zomaar
* Push pas wanneer het project stabiel werkt
* Verplaats niet handmatig allemaal projectbestanden

---

## Als je verder wilt werken

Controleer eerst:

* Of je de juiste Unity-versie gebruikt
* Of OpenXR actief staat
* Of XR Plugin Management goed staat
* Of er een controller interaction profile actief is
* Of de XR Origin aanwezig is
* Of het project vanuit de juiste filelocatie is geopend

Test veranderingen het liefst ook echt in de headset, zodat je de ervaring echt mee krijgt

## Belangrijk

Het belangrijkste aan dit project is niet om zoveel mogelijk functies toe te voegen. De VR ervaring moet vooral rustig, duidelijk en niet overweldigend blijven. Dit kan de gebruiker afschrikken.


ENGLISH

# VR ACT – Unity Project

## About the project

This is a VR prototype made in Unity based around **ACT (Acceptance and Commitment Therapy)**. The project focuses on student wellbeing, especially stress and anxiety.

The idea is not to explain ACT through audio only, but to let the user actually **do and experience things in VR**, such as looking around and moving.

The visual style is calm and colorful, with pastel colors and nature elements.

---

## Installing and opening the project

**Use GitHub Desktop to download the project.**

1. Install **GitHub Desktop**
2. Clone the repository through GitHub Desktop
3. Keep the project in the location where GitHub Desktop saved it
4. Open the Unity project **from this file location** (because it uses LFS)
5. Use the same Unity version that was used to create the project

**Do not move the project to another folder.**

If you open the project from the wrong location or move files separately, Unity may not load certain assets correctly.

It is also recommended to make a backup before making big changes or updating Unity/packages.

---

## Technologies used

* Unity
* URP
* XR Interaction Toolkit (Action-Based)
* OpenXR
* VR / standalone headset

All project code can be found in the scripts in Unity.

---

## How the experience works

The main scene is a **road trip exercise inside a car**.

The car itself does not move. This was done on purpose because a moving car can easily cause motion sickness in VR.

The user can:

* Use buttons
* Start audio/radio
* Look around and experience the environment
* Enter the experience through a slow fade

There used to be a separate lobby scene, but this was removed. The introduction inside the car already gives enough explanation and creates a calm start.

---

## XR & interaction

The project uses an **XR Origin (Action-Based)**.

Buttons mainly use `XR Simple Interactable`. Interactions are connected through the **Activated event**.

Do not use `OnMouseDown`, because this does not work properly for VR interaction.

### Adding a new button

1. Create a GameObject
2. Add `XR Simple Interactable`
3. Connect the function to `Activated`
4. Add color feedback if needed

---

## Main scripts

### SpawnPoint.cs

Places the XR Origin at the correct starting position after tracking has initialized. Only the Y rotation is changed.

### FadeCube.cs

Controls the fade at the beginning of the experience. A transparent cube is placed around the camera and slowly fades away, so keep this in mind when making changes to the camera.

### ButtonSound.cs

Plays audio when an interactable is selected and gives short visual feedback.

### VRButtonSimple.cs

Handles the main button logic. Other audio is stopped before new audio starts playing.

### RadioManager

Controls the radio audio.

---

## Graphics

The lighting is **baked**, because dynamic lighting did not work as well in VR.

Trees and leaves can look slightly pixelated/noisy from a distance. Different solutions were tested, but in the end a calmer low-poly/stylized look worked better than adding a lot of detail.

For foliage, the project uses:

* Transparent Surface Type
* Alpha Clipping
* Leaf texture in the Base Map

---

## Known Unity issues

The project has had some problems with outdated or incorrect XR packages and Unity updates. This could cause things like the XR Origin or certain packages to disappear.

These packages were previously removed from `manifest.json`:

```text
com.unity.feature.vr
com.unity.xr.androidxr-openxr
```

OpenXR was then set up again correctly.

If the project suddenly has a lot of errors after an update, **do not immediately start changing everything**. First check the Unity version, packages and OpenXR settings.

---

## Git & backups

The project uses **Git LFS** because of the large Unity files.

Recommended:

* Work through GitHub Desktop
* Make regular backups
* Do not update Unity without a reason
* Push when the project is stable
* Do not manually move project files around

---

## Continuing the project

Before continuing, check:

* You are using the correct Unity version
* OpenXR is enabled
* XR Plugin Management is set up correctly
* A controller interaction profile is enabled
* The XR Origin is present
* The project was opened from the correct file location

It is best to test changes directly in the VR headset as well.

## Important

The goal of this project is not to add as many features as possible. The VR experience should mainly stay **calm, clear and not overwhelming**.

When in doubt, test first before adding more.


Bij twijfel: eerst testen voordat je meer toevoegt.
