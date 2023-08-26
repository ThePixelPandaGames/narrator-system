# Light-Weight Narration Kit - User Documentation

## Table of Contents

1. [Introduction](#1-introduction)
   - [Overview](#11-overview)
   - [Features](#12-features)
   - [Installation](#13-installation)

2. [Getting Started](#2-getting-started)
   - [Importing the Asset](#21-importing-the-asset)
   - [Quick Setup](#22-quick-setup)
   - [Asset Components](#23-asset-components)

3. [Usage Guide](#3-usage-guide)
   - [Main Functionality](#31-main-functionality)
   - [Configuration Options](#32-configuration-options)
   - [Example Use Cases](#33-example-use-cases)

4. [API Reference](#4-api-reference)
   - [Narration Sequence](#41-component-a-narration-sequence)
   - [Player Choice](#42-component-b-player-choice)
   - [Narration Manager](#43-component-c-narration-manager)
   - [Narration Trigger](#44-component-d-narration-trigger)
   - [Continue Narration](#45-component-e-continue-narration)

5. [Troubleshooting](#5-troubleshooting)
   - [Common Issues](#51-common-issues)
   - [FAQs](#52-faqs)

6. [Support and Contact](#6-support-and-contact)
   - [Support Channels](#61-support-channels)
   - [Contact Information](#62-contact-information)

---

## 1. Introduction

### 1.1 Overview

Welcome to the documentation for the **Light-Weight Narration Kit**! This asset kit is designed to enable you to create your own dialogue and narration systems. Whether you're a beginner or an experienced developer, this documentation will guide you through the installation, setup, and usage of the asset.

### 1.2 Features

- Code easy to understand and alter.
- Set up the narration manager only once, or decide if you want one per scene; it's your choice!
- Create dialogues and player choices directly as scriptables instead of GameObjects. You can then use them throughout the whole game.

### 1.3 Installation

To install the Light-Weight Narration Kit, follow these steps:

1. Open your Unity project.
2. Go to the Unity Asset Store and add this kit to your Asset collection.
3. Open in Unity, download and import the package.
4. Open the following folders to access all necessary files: **The Pixel Panda > Light-Weight Narration Kit**.
5. Start creating your dialogues!

---

## 2. Getting Started

### 2.1 Importing the Asset

As mentioned earlier, the Narration Manager takes UI elements as input. The Text Mesh Pro package needed for this kit may need to be imported separately.

### 2.2 Quick Setup

Here's a quick guide to getting started with the Light-Weight Narration Kit:

1. Create Narration Sequences by right-clicking in the project window and go to **Create > Scriptables > Create New Narration Sequence**.
2. Fill out the narrator/NPC's lines in the newly created narration sequence's inspector window.
3. Create Player Choices as responses to the NPC by right-clicking in the project window and going to **Create > Scriptables > Create New Player Choice**.
4. Fill out each Player Choice's text in their respective inspector windows. If a narration sequence follows, drag and drop it from your project folder into the "Next Narration Sequence" field; otherwise, the narration will end after this choice.
5. Add Player Choices to the narration sequence by drag and dropping them in the respective fields.
6. Create an empty GameObject in your scene, such as "Narration Manager". Add the "NarrationManager" script to it, setting up the necessary parameters and references as described in **Asset Components**.
7. Ensure your narration UI is set up, including a "Continue" button.
8. Add the **NarrationTrigger** script to a game object in your scene to trigger narration.
9. Add the **ContinueNarration** script to your "Continue" button.
10. Run your scene, trigger narration, and enjoy the magic!

### 2.3 Asset Components

This kit revolves around **Scriptables** for flexibility. Here are key components:

...

#### Component A: Narration Sequence

Simulates a dialogue part. Specifies what an NPC says and what the player can respond with. Optionally includes an NPC's name and a callback method triggered after the sequence ends.

#### Component B: Player Choice

Simulates player responses within a narration sequence. Also references another narration sequence for branching.

#### Component C: Narration Manager

Not a Scriptable Object, but inherits from MonoBehaviour. This allows it to be instantiated in the scene and attached to a game object. The Narration Manager manages various parameters:

- Lettering Speed: Adjusts the speed at which letters appear if "Make Letters Appear One By One" is enabled.
- Npc Text: Text area for NPC's text.
- Npc Name: Text area for NPC's name.
- Continue Button: Reference to the button that advances the narration.
- Player Choice Button Prefab: Prefab button for player choices.
- UI: Parent object holding dialogue UI.
- Player Choice Spawn Pos: Transforms for player choice button positions.
- Make Letter Appear One By One: If checked, letters appear one by one.
- Dont Destroy On Scene Change: Prevents destroying the Narration Manager on scene change.

#### Component D: Narration Trigger

Not a Scriptable Object. Initiates NPC or similar triggers for narration sequences.

#### Component E: Continue Narration

Not a Scriptable Object. Handles displaying the next sentence. Triggers on Enter key press or mouse click.

## 3. Usage Guide

### 3.1 Main Functionality

1. Create Narration Sequences by right-clicking in the project window and selecting **Create > Scriptables > Create New Narration Sequence**.
2. Fill out the dialogue in the created Narration Sequence's inspector.
3. Create Player Choices as responses by right-clicking and choosing **Create > Scriptables > Create New Player Choice**.
4. Specify text for each Player Choice. Drag a narration sequence to "Next Narration Sequence" for branching.
5. Add Player Choices to a narration sequence.
6. Create an empty GameObject, add the "NarrationManager" script, and set its parameters as detailed under **Asset Components**.
7. Set up your narration UI with a "Continue" button.
8. Attach the **NarrationTrigger** script to a GameObject to initiate narration.
9. Attach the **ContinueNarration** script to the "Continue" button.
10. Run the scene, trigger narration, and experience the magic!

### 3.2 Configuration Options

Refer to the **Asset Components** section for details on configuring the Narration Manager and other components.

### 3.3 Example Use Cases

See the included sample scenes for practical examples of using the Light-Weight Narration Kit.
The example is minimalistic, I am not a designer but a programmer LOL. Also, I used the **Fantasy Wooden GUI: Free** (https://assetstore.unity.com/packages/2d/gui/fantasy-wooden-gui-free-103811). To outline the dialogue UI.

See scene **Example 1**.

## 4. API Reference

### 4.1 Component A: Narration Sequence

A **Narration Sequence** simulates a dialogue part. It specifies what an NPC says and what the player can respond with. Optionally, you can include an NPC's name and a callback method that triggers after the sequence ends.

### 4.2 Component B: Player Choice

A **Player Choice** simulates player responses within a narration sequence. It also references another narration sequence, enabling branching paths in the dialogue.

### 4.3 Component C: Narration Manager

The **Narration Manager** is not a Scriptable Object but inherits from MonoBehaviour. This allows it to be instantiated in the scene and attached to a game object. It manages various parameters:

- **Lettering Speed:** Adjusts the speed at which letters appear if the "Make Letters Appear One By One" option is enabled.
- **Npc Text:** Text area for NPC's dialogue text.
- **Npc Name:** Text area for NPC's name.
- **Continue Button:** Reference to the button that advances the narration.
- **Player Choice Button Prefab:** Prefab button for player choices.
- **UI:** Parent object holding the dialogue UI.
- **Player Choice Spawn Pos:** Transforms for player choice button positions.
- **Make Letter Appear One By One:** If checked, letters appear one by one.
- **Dont Destroy On Scene Change:** Prevents destroying the Narration Manager on scene change.

### 4.4 Component D: Narration Trigger

The **Narration Trigger** is not a Scriptable Object. It initiates triggers for narration sequences associated with NPCs or similar events. You can choose on which event you want it to trigger. Therefore, choose the option most suitable to you in the NarrationTrigger's inspector window. Choose between:
- **OnStart:** Triggers the narration sequence when the game object with the narrationTrigger script calls its Start()-method .
- **OnTriggerEnter:** Triggers the narration sequence when something enters the game object's trigger area.
- **OnCollisionEnter:** Triggers the narration sequence when something enters the game object's collision area .
- **OnMouseClick:** Triggers the narration sequence on mouse click on the game object.
- **OnTriggerEnter2D:** Triggers the narration sequence when something enters the game object's trigger2D area .
- **OnCollisionEnter2D:** Triggers the narration sequence when something enters the game object's collision2D area.

If none of these options suits you, you can alter the script and add whatever scenario to your needs.

### 4.5 Component E: Continue Narration

The **Continue Narration** is not a Scriptable Object either. It handles displaying the next sentence of the narration. It triggers on pressing the Enter key or clicking the mouse.

## 5. Troubleshooting

### 5.1 Common Issues

- **Issue:** Dialogues are not advancing.
  - **Solution:** Ensure that the "Continue" button's reference is correctly set in the Narration Manager.

- **Issue:** Player choices are not showing up.
  - **Solution:** Verify that the Player Choice Button Prefab is assigned in the Narration Manager.

### 5.2 FAQs

- **Q:** Can I have multiple Narration Managers in one scene?
  - **A:** Yes, you can have multiple, but ensure each scene has only one active Narration Manager.

- **Q:** How do I create branching dialogues?
  - **A:** Create Player Choices in a narration sequence and link them to the desired next sequence for branching.

## 6. Support and Contact

### 6.1 Support Channels

We offer support through our Discord server, where you can connect with other users and get assistance from our team. https://discord.gg/DhbpSVGC.

### 6.2 Contact Information

For direct inquiries, you can contact us at toni.antonio.palumbo@gmail.com or visit our website at antoniopalumbo.com.

---

[End of documentation]
