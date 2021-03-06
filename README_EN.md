## 日本語版READMEは[こちら](README.md)

# CenterDistanceCounter

This is a custom counter for [CountersPlus](https://github.com/Caeden117/CountersPlus) that displays the average of the absolute and relative distance between the center of the slashed notes and the slashed point in cm.

![sample](Images/sample.png)

## Dependent Mods

- [BSIPA](https://bsmg.github.io/BeatSaber-IPA-Reloaded/)
- [BeatSaberMarkupLanguage](https://github.com/monkeymanboy/BeatSaberMarkupLanguage)(BSML)
- [SiraUtil](https://github.com/Auros/SiraUtil)
- **[CounterPlus](https://github.com/rakkyo150/CountersPlus)(This is NOT the original version)**

## How to install
1. download CenterDistanceCounter.dll from [Releases](https://github.com/rakkyo150/CenterDistanceCounter/releases)
2. Add CenterDistanceCounter.dll to the Plugins folder under the Beat Saber installation folder.

For the Steam version of Beat Saber, the location of the Plugin folder is<br>
C:\Program Files (x86)\Steam\steamapps\common\Beat Saber\Plugins
is the default. <br>
Just in case.

## Settings

Settings can be changed from the CounterPlus settings screen in the game. <br>
If you want to adjust the values more accuratly, you can overwrite<br>
Beat Saber\UserData\CenterDistanceCounter.json<br>
with the new values.

The following is a description of each setting item and its contents. <br>
|Item|Content|
|:---|:---|
|SeparateSaber|Whether to separate left and right sabers or not|
|DecimalPrecision|How many decimal places to display|
|CounterType|Selectable from both absolute and relative distance, absolute distance only, or relative distance only|
|EnableLabel|Whether to display "Center Distance Counter" above the counter|
|LabelFontSize|What is the font size of the "Center Distance Counter" displayed above the counter|
|FigureFontSize|What is the font size of the counter|
|OffsetX|What is the x-axis position of the counter|
|OffsetY|What is the y-axis position of the counter|
|OffsetZ|What is the z-axis position of the counter|
