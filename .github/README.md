# VR漆刷式繪畫
此資料庫是[邵鈞組](https://github.com/vr-paint) VR-paint_brush 的專案，使用*Unity 2019.2.21f1*  
此專案可以在VR空間中畫圖，左右手有不同的功能可供使用。  
## Link 
+ [系統使用](https://github.com/vr-paint/brush#%E7%B3%BB%E7%B5%B1%E4%BD%BF%E7%94%A8)
+ [研究方法](https://github.com/vr-paint/brush/blob/ec404d3b6a79cb90b4d631c41713c9af91efe738/.github/Readme_ResearchMethods.md)
*  [Video Link](https://youtu.be/XmyNU33L2q0 "YouTube")
***
# 系統使用

## Unity安裝說明
1. 進入[Unity download archive](https://unity3d.com/get-unity/download/archive)安裝*2019.2.21f1*版本，並使用UnityHub開啟專案。
2. 我們有另外準備[執行檔](https://drive.google.com/file/d/1uKlinXyja1ZLymo6fSq7yu2fxcNJh7AM/view?usp=sharing)使用，點開game資料夾中的draw_line執行檔案。

## VR操作準備
準備好VR頭戴式顯示器、手把控制器、基地台。  
由於是VR類型的專案，當遇到無法使用觸控板更改調色盤的時候，請參考下面的解決方法: (請以各家廠牌手把為主，此說明適用HTC VIVE Pro手把)  
1. 按下手把的選單鍵，接著進入按鍵`控制器按鍵配置`，將配置改成drawline。(上面有管理此應用程式的控制配置。若無，則直接使用安裝說明1的方式開啟專案檔)。看向畫面中間的使用中的控制器配置，將選項從預設改成自訂。
2. 手把按下修改此配置，會出現選單可改手把與配置。左邊往下滑，找到觸控板功能按旁邊的+號標誌。
3. 跳出一個選單畫面後，最上面會有觸控板跟十字鍵的功能。選擇觸控板，左側會多一個觸控板操作的設定。`位置`欄位選擇`Touchpad`，`觸控板操作`欄位的左下角打勾。
4. 最後按下面的儲存個人配置，就可以使用調色盤介面。

## 手把控制器按鍵說明
<img src="https://github.com/vr-paint/brush/blob/main/Assets/picture/explain_user/Touchpad.png" height="240"><img src="https://github.com/vr-paint/brush/blob/main/Assets/picture/explain_user/Trigger%20Grip.png" height="240">

### 右手手把操作
右手手把是畫線的，該手把上面沒有ICON功能鍵。下圖為操作圖：  
<P Align=center><img src="https://github.com/vr-paint/brush/blob/main/Assets/picture/explain_user/righthand_buttom.png" height="350">  
 
1. 扣下扳機鍵即可繪製出具有3D立體感的平面，這些平面會化成使用者作畫的線條存在在3D空間當中。
2. 使用者可以透過扣手把板機的力道決定線條的粗細，減少了調整粗細的ICON，使得使用者操作變得更加方便。

### 左手手把操作
左手手把的主要作用是繪畫功能選擇，下圖為操作圖：  
<P Align=center><img src="https://github.com/vr-paint/brush/blob/main/Assets/picture/explain_user/trigger_side.png" height="350">  
 
1. 扣下扳機鍵可以決定使用當前ICON的功能。並且藉由觸控板點擊左右兩側，可以進行功能的切換。
2. 當使用者點選進入調色盤功能後，觸控板的功能會變成可以透過觸摸不同位置，來決定色環對應位置上方的顏色。
3. 當使用者在調色盤功能中時，可以按下握持鍵進入調整色度的介面，一樣透過觸控板操作，可以變換當下顏色的色度。

***
***
### 參考文獻
```
[1]	謝其叡, 薛猷騰, 何誼庭, 黃慧緣, 呂昱辰, and 葉正聖, "陶藝與浮雕：Leap Motion結合VR之互動塑模," presented at the Computer Graphics Workshop, 台中, 2017.
[2]	C. Tseng and J.-S. Yeh, "A Kinect-based System for Virtual Sculpture," presented at the Computer Graphics Workshop 台北, 2015.
[3]	許志遙, 蔡閎鈞, 林伯儒, 邱俊澄, 呂昱辰, and 葉正聖, "以HTC Vive 為基礎 VR 3D繪本," presented at the Computer Graphics Workshop 台北, 2016.
[4]	E. Rosales, J. Rodriguez, and A. SHEFFER, "SurfaceBrush: from virtual reality drawings to manifold surfaces," ACM Trans. Graph., vol. 38, no. 4, p. Article 96, 2019.
[5]	C.-W. Chen, J.-W. Peng, C.-M. Kuo, M.-C. Hu, and Y.-C. Tseng, "Ontlus: 3d content collaborative creation via virtual reality," in International Conference on Multimedia Modeling, 2018: Springer, pp. 386-389.
[6]	D. Keefe, R. Zeleznik, and D. Laidlaw, "Drawing on Air: Input Techniques for Controlled 3D Line Illustration," IEEE Transactions on Visualization and Computer Graphics, vol. 13, no. 5, pp. 1067-1081, 2007.
[7]	S. Schkolne, M. Pruett, and P. Schröder, "Surface drawing: creating organic 3D shapes with the hand and tangible tools," presented at the Proceedings of the SIGCHI Conference on Human Factors in Computing Systems, Seattle, Washington, USA, 2001. [Online]. Available: https://doi-org.erm.lib.mcu.edu.tw/10.1145/365024.365114.
[8]	S.-H. Bae, R. Balakrishnan, and K. Singh, "ILoveSketch: as-natural-as-possible sketching system for creating 3d curve models," presented at the Proceedings of the 21st annual ACM symposium on User interface software and technology, Monterey, CA, USA, 2008. [Online]. Available: https://doi-org.erm.lib.mcu.edu.tw/10.1145/1449715.1449740.
```

### 參考資料
* [The Future of Tilt Brush](https://opensource.googleblog.com/2021/01/the-future-of-tilt-brush.html?fbclid=IwAR1vozx-rK-ldgz0Tcc2TVXNJutNq1DX1O2dpW7Z0HgNXwDjXyFr8geXPEc "Google Open Source Blog")  
* [github/googlevr/tilt-brush](https://github.com/googlevr/tilt-brush "github") 
* [Unity VR Tutorial: How To Build Tilt Brush From Scratch](https://youtu.be/eMJATZI0A7c "YouTube")  
* [github/orifmilod/KdTree-Unity3D](https://github.com/orifmilod/KdTree-Unity3D "github") 

