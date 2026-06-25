# README.md – VR ACT Unity Project

---

## Projectoverzicht

Dit project is een **VR‑prototype ontwikkeld in Unity** waarin **ACT‑therapie (Acceptance and Commitment Therapy)** wordt vertaald naar een **fysieke, ervaringsgerichte VR‑omgeving**. Het project is ontstaan tijdens een stage en richt zich op **studentenwelzijn**, met aandacht voor **angst en stress**.

Het project is ontwikkeld door **Britney Krabbendam (CMGT)** en is het **eerste VR‑project**. De visuele stijl is geïnspireerd op een **Ghibli‑achtige look**, met **pastel en stylized nature assets**.

De kern van het project is dat de gebruiker **niet passief luistert**, maar **actief deelneemt** door fysieke interactie, verbeelding en reflectie. VR wordt hierbij ingezet als **ondersteunend middel**, niet als vervanging van therapie.

---

## Doel van het project

* ACT‑principes vertalen naar een **rustige VR‑omgeving**
* Focus op **lichaamsbewustzijn, acceptatie en waarden**
* Creëren van een **emotioneel veilige omgeving**
* Verminderen van **claustrofobie, overprikkeling en verwarring**
* Toegankelijk voor gebruikers met **weinig tot geen VR‑ervaring**

**Belangrijk uitgangspunt:**

> **Uitnodigen om dingen te doen, niet voorschrijven.**

---

## Concept & Therapievisie

### ACT in VR

Het project maakt gebruik van de volgende ACT‑principes:

* Acceptatie
* Mindfulness / hier‑en‑nu
* Waarden (wat is belangrijk voor mij?)
* Zelfregie

Uit gesprekken met experts kwam naar voren:

* Alleen audio of instructie is te **rigide**
* Het **fysieke lichaam** moet actief betrokken worden
* De gebruiker moet **zelf keuzes** kunnen maken
* **Reflectie** (bijv. journaling of gesprek) is essentieel na afloop

Daarom ligt de focus op:

* **Fysieke handelingen** (zitten, kijken, draaien, voelen)
* **Verbeelding via omgeving** (auto, route, haven, natuur)
* **Rustige, warme setting**

---

## Technische stack

* **Unity (VR)**
* **URP (Universal Render Pipeline)**
* **XR Interaction Toolkit (Action‑Based)**
* **OpenXR**
* Target platform: **VR‑headset (Android / Standalone, afhankelijk van build)**

---

## Projectstructuur (globaal)

* **Hoofdscene** – auto / roadtrip‑oefening
* **Omgevingen** – natuur, snelweg, horizon
* **Interacties** – knoppen, radio, teleport
* **Audio** – radio, ambient sound
Scripts staan alle code in!

> De lobby scene is verwijderd. In overleg met een psycholoog is besloten dat een aparte lobby niet nodig is, omdat de introductie in de auto voldoende rust en context biedt.

---

## Belangrijke systemen & keuzes

### XR Origin & interactie

* **XR Origin (Action‑Based)** gebruikt
* Interactie via **trigger (Activated‑event)**
* **OnMouseDown wordt niet gebruikt** (werkt niet in VR)
* XR Poke is alleen nodig voor fysieke hand‑interactie (niet toegepast)

---

### Knoppen

* Eigen script: **VRButtonSimple**
* Werking:

  * Eerst **alle andere audio stoppen** via `AudioManager`
  * Daarna **eigen audio afspelen**
* Visuele feedback:

  * Knop verandert van kleur bij hover / activatie

---

### Auto & beweging

* Auto **beweegt niet actief**

  * Bewegende auto veroorzaakte misselijkheid
* XR Origin wordt **child van de auto** bij instappen
* Instappen via knop + fade
* Radio start met vertraging via coroutine:

```csharp
StartCoroutine(PlayRadioAfterDelay(20f));
```

---

### Radio & volume

* Centrale **RadioManager** met `AudioSource`
* Volume‑draaiknop is verwijderd

  * Werd niet als natuurlijk ervaren
  * Vervangen door **drukknoppen**

---

### Fade‑in effect (grijs → normaal)

**Huidige oplossing:**

* Geen UI‑overlay
* Cube rondom de camera
* URP **Unlit Transparent** materiaal
* Cube is child van de camera
* Script verlaagt alpha na 5 seconden

**Effect:**

* Scene start volledig grijs
* Beeld bloeit langzaam open

---

## Graphics & optimalisatie

### Bomen & foliage

Probleem:

* Pixelated / noisy bladeren, vooral op afstand

Oplossing:

* Surface Type: **Transparent**
* **Alpha Clipping** aan
* Juiste leaf‑texture in Base Map

Wat is geprobeerd:

* Verschillende URP‑shaders
* Unlit vs Lit
* Meerdere foliage‑assets
* Bomen verwijderen / vervangen
* Verschillende LOD‑instellingen

Conclusie:

* Pixelnoise blijft deels aanwezig in VR
* Bewuste keuze gemaakt voor **rust boven detail**

Low‑poly assets zijn getest en toegepast waar mogelijk, omdat complexe foliage visueel en technisch slecht werkt in VR.

---

### Lighting

* Lighting is **gebaked**
* Dynamische lighting gaf problemen in VR

---

## Bekende problemen & lessons learned

### Unity / XR‑issues

Oorzaken:

* Wifi‑uitval tijdens updates
* Verouderde of verkeerde XR‑packages

Gevolgen:

* Extreme laadtijden (10–12 minuten)
* Missing XR Origin
* Compile‑errors

Verwijderd uit `manifest.json`:

```text
com.unity.feature.vr
com.unity.xr.androidxr-openxr
```

Vervangen door correcte OpenXR‑setup.

---

### Build‑problemen

* Random build‑errors
* Shader‑hangs
* Soms alleen opgelost door:

  * Terug naar een vorige versie
  * Nieuw project aanmaken

---

## GitHub & backups

Problemen:

* Grote bestanden
* Push‑errors

Oplossingen:

* **Git LFS** gebruikt
* Nieuwe repository aangemaakt

**Advies:**

* Dagelijkse backups maken
* Pas pushen als het project stabiel is
* Niet zomaar Unity‑updates installeren

---

## Ontwerpprincipes

* **Rust is key**
* Warm welkom (geen harde overgangen)
* Eén knop per handeling
* Minimaal controllergebruik
* Geen claustrofobische ruimtes

---

## How to continue this project

### 1. Start veilig

* Maak altijd eerst een **backup**
* Gebruik dezelfde **Unity‑versie**
* Open het project **zonder upgraden**
* Test XR direct in de headset

---

### 2. XR & Unity‑setup controleren

* XR Plugin Management actief
* OpenXR correct ingesteld
* Minstens één controller‑interaction profile
* XR Origin (Action‑Based) aanwezig

---

### 3. Interacties uitbreiden (knoppen)

Stappen:

1. Nieuw GameObject maken
2. `XR Simple Interactable` toevoegen
3. `Activated` koppelen aan functie
4. Visuele feedback toevoegen

Gebruik **geen `OnMouseDown`**.

---

## Code‑uitleg (belangrijkste scripts)

### SpawnPoint.cs

* Wacht 0.1 seconde zodat XR‑tracking eerst initialiseert
* Zet daarna positie en **alleen Y‑rotatie** van XR Origin

---

### FadeCube.cs

* Cube als child van camera
* Transparant URP‑materiaal
* Alpha fade naar 0

Resultaat: rustige visuele overgang zonder UI.

---

### ButtonSound.cs

* Gebruikt `XRBaseInteractable`
* Reageert op `selectEntered`
* Speelt geluid + korte kleurfeedback

---

### VRButtonSimple.cs

* Centrale knoplogica
* Stopt eerst alle audio
* Speelt daarna eigen audio
* Zorgt dat maar **één knop tegelijk actief** is

---

## Laatste advies

Dit project is **proces‑gedreven**, niet feature‑gedreven.

Meer functies toevoegen ≠ betere ervaring.

Test altijd met echte gebruikers en bewaak **rust en veiligheid**.

*Einde README*
