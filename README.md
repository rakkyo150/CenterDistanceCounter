## The English version of the README is [here](README_EN.md)

# CenterDistanceCounter

斬ったノーツの中心と切断点の絶対的距離・相対的距離の平均をcm単位で表示する[CountersPlus](https://github.com/Caeden117/CountersPlus)のカスタムカウンターです。

![サンプル](Images/sample.png)

## 依存MOD

- [BSIPA](https://bsmg.github.io/BeatSaber-IPA-Reloaded/)
- [BeatSaberMarkupLanguage](https://github.com/monkeymanboy/BeatSaberMarkupLanguage)(BSML)
- [SiraUtil](https://github.com/Auros/SiraUtil)
- **[CounterPlus](https://github.com/rakkyo150/CountersPlus)(オリジナルバージョンではありません)**

## インストール方法
1. [Releases](https://github.com/rakkyo150/CenterDistanceCounter/releases)からCenterDistanceCounter.dllをダウンロード
2. CenterDistanceCounter.dllをBeat Saberのインストールフォルダ下のPluginsフォルダに追加

Steam版Beat Saberの場合、Pluginフォルダの場所は<br>
C:\Program Files (x86)\Steam\steamapps\common\Beat Saber\Plugins<br>
がデフォルトになっています。<br>
念のため。

## 設定

設定はゲーム内のCounterPlusの設定画面から変更できます。<br>
より設定値を細かく決めたい場合、<br>
Beat Saber\UserData\CenterDistanceCounter.json<br>
から値を書き換えて上書き保存してください。

以下それぞれの設定項目とその内容についてです。<br>
|項目|説明|
|:---|:---|
|SeparateSaber|左右のセイバーに分けて表示するか否か|
|DecimalPrecision|小数点以下何桁までを表示するか|
|CounterType|絶対値と相対値の両方、絶対値のみ、相対値のみから選択可|
|EnableLabel|カウンターの上に表示される"Center Distance Counter"の表示をするかどうか|
|LabelFontSize|カウンターの上に表示される"Center Distance Counter"のフォントサイズをどうするか|
|FigureFontSize|カウンターのフォントサイズをどうするか|
|OffsetX|カウンターのx軸方向の位置をどうするか|
|OffsetY|カウンターのy軸方向の位置をどうするか|
|OffsetZ|カウンターのz軸方向の位置をどうするか|

