# 研究方法(ResearchMethods)
## 線帶繪畫
先將點座標以等距方式來記錄，移動手把可以依序產生中、上、下3個新的點座標。讓產生的點相互連接，形成三角形，將四個三角形合併成平面。
接下來只要移動手把的時候就會接連產生多個平面，最終連接的平面將會成為一整條的線帶。  
[Unity - Mesh](https://docs.unity3d.com/ScriptReference/Mesh.html)  
<P Align=center><img src="https://github.com/jsyeh/draw_line/blob/main/Image/quad.png" height="250"> 
  
  手把上的三個向量:forward、up、right 向量,當使用 forward 與 up向量做外積,可以形成延展平面所需的 Bi-Normal 向量,當使用者拖曳手把畫線時,能夠產生對應的等距座標點,在座標點產生的當下就可連接點與點產生四個三角形組成的平面。  
  當使用者拖曳手把畫線時,就能夠運用此向量來延展平面,畫出所需的立體線帶。順帶一提,手把上的 right 向量就是延展平面所需的 Bi-Normal 向量,因此直接使用 right 向量仍可以畫出相同的結果。
  
***
## 線帶縫合
在3D世界中進行繪畫時，會產生視覺上的誤差，導致使用者難以將兩條線帶完整的重合。為解決此問題，決定將相鄰的兩條線帶自動縫合成平面。

步驟1:  
做出一個可擴張的雙層鏈結陣列，可以將所有新增線帶的座標點都可以讀入該陣列中。
縫合之前要先進行座標點之間的距離判斷，判斷後就可以將兩條距離接近的線帶找出，為下一步的縫合做準備。
<P Align=center><img src="https://github.com/jsyeh/draw_line/blob/main/Image/%E7%B7%9A%E5%B8%B6%E9%99%A3%E5%88%97.png" height="180">   
   
步驟2:  
縫合部分使用的方法是將兩點合併為一點的方法。  
在找到距離相近的座標點後，根據距離算出其對應的兩個座標點的中位座標，利用此座標代替原本的最近點座標，縫合出一個完整的平面，能夠縫合兩條線帶間的空隙。   
<P Align=center><img src="https://github.com/jsyeh/draw_line/blob/main/Image/%E7%B8%AB%E5%90%883.png" height="180"> 
