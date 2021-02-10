using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Symbol : MonoBehaviour
{
    public enum Symbols
    {
        //잭팟 심볼
        coin1 = 0, //0
        coin2 = 1, //1
        coin3 = 2, //2

        //프리스핀 심볼
        dia_blue = 3, //3
        dia_green = 4, //4
        dia_red = 5,  //5

        //일반 심볼
        pig_green = 6, //6
        pig_blue = 7, //7
        pig_red = 8, //8
        A = 9, //9
        K = 10, //10
        Q = 11, //11
        J = 12, //12
        del = 13, //13 꼬깔
        box = 14, //14
        cart = 15, //15

        //스캐터 심볼
        bonus = 16, //16

        //와일드심볼
        wild = 17 //17 늑대
    }
}
