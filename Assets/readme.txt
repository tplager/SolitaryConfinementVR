# VR Experience of Solitary Confinement

## Summary

This project was originally created by myself, Trenton Plager, as an intern, to assist in the research of Professors Ying Zhu and Douglas Blackmon of Georgia State University in conjuction with the National Science Foundation's Research Education Opportunity for Undergraduates (NSF REU) at Georgia State. The topic of this NSF REU program was Immersive Media. The project is intended to both bring awareness to the conditions that prison inmates in solitary confinement face, and as a comparison against 360 degree video, 2D video, pictures, and a physical experience, to determine which of these media experiences produce more emotional reaction in a viewer's or player's reactions. 

## Details:

* The project was created using Unity 2018.2.1 - a compatible version is required to edit
* The project was tested using an HTC Vive Virtual Reality Headset

## Scene Information:

There are multiple scenes that have been edited or used for testing in the project

*Testing Scenes:* 
* [Testing Scene](Scenes\TestingScene.unity) - used for testing lighting and placement of models in prefabs without overhead of entire scene 
* [Resonance Audio Demo](PluginFolders\ResonanceAudio\Demos\Scenes\ResonanceAudioDemo.unity) - used to examine the effects of the Resonance Audio Package
* [SteamVR Interaction Sample Scene](PluginFolders\SteamVR\InteractionSystem\Samples\Interactions_Example.unity) - used to examine the various interactions that can be used with SteamVR and to test these interactions 

*Project Scenes:*
* [Cell Scene](Scenes\CellScene.unity) - the scene that contains the solitary cell and much of the prison 
* [Outdoor Scene](Scenes\OutdoorScene.unity) - the scene that contains the objects for the outdoor scene such as the the fences and gates
* [Project Scene](Scenes\OutdorScene.unity) - contains all of the objects in both the Cell scene and the Outdoor scene - only used as a container

## Quick Start

To get a quick idea of the contents of the scenes, you can open the Project Scene. However, the Project Scene's settings are not exactly the same as it is primarily intended for use as a container and visualization tool. 

To get an idea of the functionallity of the project, open the Cell Scene and hit play to enter play mode. This will start the ~8 min. sequence of the experience. To decrease the time of the experience, open the Time Manager object in the Unity hierarchy and edit the values in the "SECONDS_TILL..." fields. By default, they are, in this order: 30, 30, 120, 240, 60. 

## Known Bugs

There are a few bugs that I know of and just didn't have time to figure out how to fix. The first is that when used with the butter, while holding the tray up, 2 buttered bread models will instantiate. This doesn't impact the experience, but it is possible for it to break immersion of course. The milk and cereal bowl also do this while holding the tray. The only other bug has to do with the grip posing system. Keep in mind that I haven't encountered this bug for a while, but if you edit the grip poses be sure to move only the hand object containing everything and the individual finger joints. Anything other than that will mess up the position of the hand and your hand models will always be slightly offset. 
## Documentation

Documentation can be found in the documentation file included in the Assets directory of this project and in the various scripts themselves. 

## Further Support

If you have any further questions not answered by the documentation file or this readme, feel free to contact me at tlp6760@g.rit.edu. I will be happy to answer any questions I can. 