# VR漆刷式繪畫
draw_line專案，這是邵鈞組 VR draw 的專案，使用*Unity 2019.4.10f1*  

## Link
+ [簡介](https://github.com/jsyeh/draw_line#%E7%B0%A1%E4%BB%8B)
+ [系統使用](https://github.com/jsyeh/draw_line#%E7%B3%BB%E7%B5%B1%E4%BD%BF%E7%94%A8)
  + [Unity安裝說明](https://github.com/jsyeh/draw_line#unity%E5%AE%89%E8%A3%9D%E8%AA%AA%E6%98%8E)
  + [VR操作準備](https://github.com/jsyeh/draw_line#vr%E6%93%8D%E4%BD%9C%E6%BA%96%E5%82%99)
  + [手把控制器按鍵說明](https://github.com/jsyeh/draw_line#%E6%89%8B%E6%8A%8A%E6%8E%A7%E5%88%B6%E5%99%A8%E6%8C%89%E9%8D%B5%E8%AA%AA%E6%98%8E)
    + [右手手把操作](https://github.com/jsyeh/draw_line#%E5%8F%B3%E6%89%8B%E6%89%8B%E6%8A%8A%E6%93%8D%E4%BD%9C)
    + [左手手把操作](https://github.com/jsyeh/draw_line#%E5%B7%A6%E6%89%8B%E6%89%8B%E6%8A%8A%E6%93%8D%E4%BD%9C)
*  [Video Link](https://youtu.be/XmyNU33L2q0 "YouTube")
***
# 簡介
此專案可以在VR空間中畫圖，左右手有不同的功能可供使用。  

# 系統使用

## Unity安裝說明
1. 進入[Unity download archive](https://unity3d.com/get-unity/download/archive)安裝*2019.2.20*版本，並使用UnityHub開啟專案。
2. 我們有另外準備執行檔使用，點開game資料夾中的draw_line執行檔案。

## VR操作準備
準備好VR頭戴式顯示器、手把控制器、基地台。  
由於是VR類型的專案，當遇到無法使用觸控板更改調色盤的時候，請參考下面的解決方法: (請以各家廠牌手把為主，此說明適用HTC VIVE Pro手把)  
1. 按下手把的選單鍵，接著進入按鍵**控制器按鍵配置**，將配置改成drawline。(上面有管理此應用程式的控制配置。若無，則直接使用安裝說明1的方式開啟專案檔)。看向畫面中間的使用中的控制器配置，將選項從預設改成自訂。
2. 手把按下修改此配置，會出現選單可改手把與配置。左邊往下滑，找到觸控板功能按旁邊的+號標誌。
3. 跳出一個選單畫面後，最上面會有觸控板跟十字鍵的功能。選擇觸控板，左側會多一個觸控板操作的設定。**位置**欄位選擇Touchpad，**觸控板操作**欄位的左下角打勾。
4. 最後按下面的儲存個人配置，就可以使用調色盤介面。

## 手把控制器按鍵說明
<img src="https://github.com/jsyeh/draw_line/blob/main/Assets/picture/explain_user/Touchpad.png" height="240"><img src="https://github.com/jsyeh/draw_line/blob/main/Assets/picture/explain_user/Trigger%20Grip.png" height="240">

### 右手手把操作
右手手把是畫線的，該手把上面沒有ICON功能鍵。下圖為操作圖：  
<P Align=center><img src="https://github.com/jsyeh/draw_line/blob/main/Assets/picture/explain_user/righthand_buttom.png" height="350">  
 
1. 扣下扳機鍵即可繪製出具有3D立體感的平面，這些平面會化成使用者作畫的線條存在在3D空間當中。
2. 使用者可以透過扣手把板機的力道決定線條的粗細，減少了調整粗細的ICON，使得使用者操作變得更加方便。

### 左手手把操作
左手手把的主要作用是繪畫功能選擇，下圖為操作圖：  
<P Align=center><img src="https://github.com/jsyeh/draw_line/blob/main/Assets/picture/explain_user/trigger_side.png" height="350">  
 
1. 扣下扳機鍵可以決定使用當前ICON的功能。並且藉由觸控板點擊左右兩側，可以進行功能的切換。
2. 當使用者點選進入調色盤功能後，觸控板的功能會變成可以透過觸摸不同位置，來決定色環對應位置上方的顏色。
3. 當使用者在調色盤功能中時，可以按下握持鍵進入調整色度的介面，一樣透過觸控板操作，可以變換當下顏色的色度。

### 線帶繪畫
先將點座標以等距方式來記錄，移動手把可以依序產生中、上、下3個新的點座標。
讓產生的點相互連接，形成三角形，將四個三角形合併成平面，如圖右。
接下來只要移動手把的時候就會接連產生多個平面，最終連接的平面將會成為一整條的線帶。

<P Align=center><img src="https://github.com/jsyeh/draw_line/blob/main/Image/quad.png" height="350"> 
  
### 線帶縫合
在3D世界中進行繪畫時，會產生視覺上的誤差，導致使用者難以將兩條線帶完整的重合。為解決此問題，決定將相鄰的兩條線帶自動縫合成平面。

步驟1:  
做出一個可擴張的雙層鏈結陣列，可以將所有新增線帶的座標點都可以讀入該陣列中。
縫合之前要先進行座標點之間的距離判斷，判斷後就可以將兩條距離接近的線帶找出，為下一步的縫合做準備。
<P Align=center><img src="https://github.com/jsyeh/draw_line/blob/main/Image/%E7%B7%9A%E5%B8%B6%E9%99%A3%E5%88%97.png" height="180">    
步驟2:  
縫合部分使用的方法是將兩點合併為一點的方法。  
在找到距離相近的座標點後，根據距離算出其對應的兩個座標點的中位座標，利用此座標代替原本的最近點座標，縫合出一個完整的平面，能夠縫合兩條線帶間的空隙。   


***
參考資料:  
[1]	謝其叡, 薛猷騰, 何誼庭, 黃慧緣, 呂昱辰, and 葉正聖, "陶藝與浮雕：Leap Motion結合VR之互動塑模," presented at the Computer Graphics Workshop, 台中, 2017.   
[2]	C. Tseng and J.-S. Yeh, "A Kinect-based System for Virtual Sculpture," presented at the Computer Graphics Workshop 台北, 2015.  
[3]	許志遙, 蔡閎鈞, 林伯儒, 邱俊澄, 呂昱辰, and 葉正聖, "以HTC Vive 為基礎 VR 3D繪本," presented at the Computer Graphics Workshop 台北, 2016.  
[4]	E. Rosales, J. Rodriguez, and A. SHEFFER, "SurfaceBrush: from virtual reality drawings to manifold surfaces," ACM Trans. Graph., vol. 38, no. 4, p. Article 96, 2019.  
[5]	C.-W. Chen, J.-W. Peng, C.-M. Kuo, M.-C. Hu, and Y.-C. Tseng, "Ontlus: 3d content collaborative creation via virtual reality," in International Conference on Multimedia Modeling, 2018: Springer, pp. 386-389.  
[6]	D. Keefe, R. Zeleznik, and D. Laidlaw, "Drawing on Air: Input Techniques for Controlled 3D Line Illustration," IEEE Transactions on Visualization and Computer Graphics, vol. 13, no. 5, pp. 1067-1081, 2007.  
[7]	S. Schkolne, M. Pruett, and P. Schröder, "Surface drawing: creating organic 3D shapes with the hand and tangible tools," presented at the Proceedings of the SIGCHI   Conference on Human Factors in Computing Systems, Seattle, Washington, USA, 2001. [Online]. Available: https://doi-org.erm.lib.mcu.edu.tw/10.1145/365024.365114.  


其他參考資料:  
https://www.youtube.com/watch?v=eMJATZI0A7c&t=6300s (TiltBrush參考)    
https://github.com/orifmilod/KdTree-Unity3D   (kdtree演算法引用)  
