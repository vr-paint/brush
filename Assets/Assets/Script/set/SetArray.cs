using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetArray : MonoBehaviour
{
    public int I;
    public int J;
    public void SetI(int i){
        I=i;
    }
    public void SetJ(int j){
        J=j;
    }
   public int GetI(){
       return I;
   } 
   public int GetJ(){
       return J;
   }
}
