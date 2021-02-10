using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayLine : GameControl
{
    private int[,] map = new int[3, 5];
    ulong rt;
    public static bool Bonus_flag = false;
    //생성자
    public PayLine()
    {
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                map[i, j] = 0;
            }
        }
    }

    public void Mapping_symbol()
    {
        //슬롯머신 돌린 결과를 2차원 배열에 맵핑하는 함수(점수 계산을 위해)
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (i == 0)
                {
                    //high
                    map[i, j] = rows[j].row_stoppedSlot3;
                }
                else if (i == 1)
                {
                    //mid
                    map[i, j] = rows[j].row_stoppedSlot2;
                }
                else if (i == 2)
                {
                    //low
                    map[i, j] = rows[j].row_stoppedSlot1;
                }


                if (map[i, j] == (int)Symbols.bonus)
                {
                    Bonus_flag = true;
                }
            }
        }
    }

    //이 함수만 private로 할수 있음
    public ulong Calculate_Score(int[] A)
    {
        //페이라인만 체크해서 계산하는 부분
        int max_symbol = -1;
        int max_cnt = -1;

        for (int j = 0; j < 18; j++)//심볼이 0부터 
        {
            if (max_cnt < A[j])
            {
                max_cnt = A[j];
                max_symbol = j;
            }
        }

        max_cnt += A[17];//와일드심볼 추가

        ulong gold = bettingGold / 10;//배팅한 금액을 페이라인 수로 나눠줌
        ulong pv = 0;

        switch (max_symbol)
        {
            case (int)Symbols.pig_red:
                if (max_cnt == 3)
                {
                    pv += gold * 10;
                }
                else if (max_cnt == 4)
                {
                    pv += gold * 50;
                }
                else if (max_cnt == 5)
                {
                    pv += gold * 250;
                }
                break;

            case (int)Symbols.pig_blue:
                if (max_cnt == 3)
                {
                    pv += gold * 10;
                }
                else if (max_cnt == 4)
                {
                    pv += gold * 35;
                }
                else if (max_cnt == 5)
                {
                    pv += gold * 150;
                }
                break;

            case (int)Symbols.pig_green:
                if (max_cnt == 3)
                {
                    pv += gold * 10;
                }
                else if (max_cnt == 4)
                {
                    pv += gold * 30;
                }
                else if (max_cnt == 5)
                {
                    pv += gold * 100;
                }
                break;

            case (int)Symbols.del:
                if (max_cnt == 3)
                {
                    pv += gold * 8;
                }
                else if (max_cnt == 4)
                {
                    pv += gold * 25;
                }
                else if (max_cnt == 5)
                {
                    pv += gold * 80;
                }
                break;

            case (int)Symbols.box:
                if (max_cnt == 3)
                {
                    pv += gold * 5;
                }
                else if (max_cnt == 4)
                {
                    pv += gold * 20;
                }
                else if (max_cnt == 5)
                {
                    pv += gold * 50;
                }
                break;

            case (int)Symbols.cart:
                if (max_cnt == 3)
                {
                    pv += gold * 5;
                }
                else if (max_cnt == 4)
                {
                    pv += gold * 20;
                }
                else if (max_cnt == 5)
                {
                    pv += gold * 50;
                }
                break;


            case (int)Symbols.A:
                if (max_cnt == 3)
                {
                    pv += gold * 3;
                }
                else if (max_cnt == 4)
                {
                    pv += gold * 5;
                }
                else if (max_cnt == 5)
                {
                    pv += gold * 20;
                }
                break;

            case (int)Symbols.K:
                if (max_cnt == 3)
                {
                    pv += gold * 3;
                }
                else if (max_cnt == 4)
                {
                    pv += gold * 5;
                }
                else if (max_cnt == 5)
                {
                    pv += gold * 20;
                }
                break;

            case (int)Symbols.Q:
                if (max_cnt == 3)
                {
                    pv += gold * 3;
                }
                else if (max_cnt == 4)
                {
                    pv += gold * 5;
                }
                else if (max_cnt == 5)
                {
                    pv += gold * 10;
                }
                break;

            case (int)Symbols.J:
                if (max_cnt == 3)
                {
                    pv += gold * 3;
                }
                else if (max_cnt == 4)
                {
                    pv += gold * 5;
                }
                else if (max_cnt == 5)
                {
                    pv += gold * 10;
                }
                break;

        }//switch
        return pv;
    }


    public ulong Payline_123()
    {
        //■■■■■
        //■■■■■
        //■■■■■
       
        for (int i = 0; i < 3; i++)
        {
            int[] A = new int[18];
            System.Array.Clear(A, 0, 18);//c++에서 memset함수와 같은 함수

            for (int j = 0; j < 5; j++)
            {
                int k = map[i, j];
                A[k]++;
            }

            rt=Calculate_Score(A);
        }
        return rt;
    }//Payline_123

    public ulong Payline_4()
    {
        //■□□□■
        //□■□■□
        //□□■□□

        int[] m = new int[5];
        m[0] = map[0, 0];
        m[1] = map[1, 1];
        m[2] = map[2, 2];
        m[3] = map[1, 3];
        m[4] = map[0, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_5()
    {
        //□□■□□
        //□■□■□
        //■□□□■

        int[] m = new int[5];
        m[0] = map[2, 0];
        m[1] = map[1, 1];
        m[2] = map[0, 2];
        m[3] = map[1, 3];
        m[4] = map[2, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_6()
    {
        //□■■■□
        //■□□□■
        //□□□□□

        int[] m = new int[5];
        m[0] = map[1, 0];
        m[1] = map[0, 1];
        m[2] = map[0, 2];
        m[3] = map[0, 3];
        m[4] = map[1, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_7()
    {
        //□□□□□
        //■□□□■
        //□■■■□
        int[] m = new int[5];
        m[0] = map[1, 0];
        m[1] = map[2, 1];
        m[2] = map[2, 2];
        m[3] = map[2, 3];
        m[4] = map[1, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_8()
    {
        //■■□□□
        //□□■□□
        //□□□■■
        int[] m = new int[5];
        m[0] = map[0, 0];
        m[1] = map[0, 1];
        m[2] = map[1, 2];
        m[3] = map[2, 3];
        m[4] = map[2, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_9()
    {
        //□□□■■
        //□□■□□
        //■■□□□
        int[] m = new int[5];
        m[0] = map[2, 0];
        m[1] = map[2, 1];
        m[2] = map[1, 2];
        m[3] = map[0, 3];
        m[4] = map[0, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_10()
    {
        //□□□■□
        //■□■□■
        //□■□□□
        int[] m = new int[5];
        m[0] = map[1, 0];
        m[1] = map[2, 1];
        m[2] = map[1, 2];
        m[3] = map[0, 3];
        m[4] = map[1, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_11()
    {
        //□■□□□
        //■□■□■
        //□□□■□
        int[] m = new int[5];
        m[0] = map[1, 0];
        m[1] = map[0, 1];
        m[2] = map[1, 2];
        m[3] = map[2, 3];
        m[4] = map[1, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_12()
    {
        //■□□□■
        //□■■■□
        //□□□□□
        int[] m = new int[5];
        m[0] = map[0, 0];
        m[1] = map[1, 1];
        m[2] = map[1, 2];
        m[3] = map[1, 3];
        m[4] = map[0, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_13()
    {
        //□□□□□
        //□■■■□
        //■□□□■
        int[] m = new int[5];
        m[0] = map[2, 0];
        m[1] = map[1, 1];
        m[2] = map[1, 2];
        m[3] = map[1, 3];
        m[4] = map[2, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_14()
    {
        //■□■□■
        //□■□■□
        //□□□□□
        int[] m = new int[5];
        m[0] = map[0, 0];
        m[1] = map[1, 1];
        m[2] = map[0, 2];
        m[3] = map[1, 3];
        m[4] = map[0, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_15()
    {
        //□□□□□
        //□■□■□
        //■□■□■
        int[] m = new int[5];
        m[0] = map[2, 0];
        m[1] = map[1, 1];
        m[2] = map[2, 2];
        m[3] = map[1, 3];
        m[4] = map[2, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_16()
    {
        //□□■□□
        //■■□■■
        //□□□□□
        int[] m = new int[5];
        m[0] = map[1, 0];
        m[1] = map[1, 1];
        m[2] = map[0, 2];
        m[3] = map[1, 3];
        m[4] = map[1, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_17()
    {
        //□□□□□
        //■■□■■
        //□□■□□
        int[] m = new int[5];
        m[0] = map[1, 0];
        m[1] = map[1, 1];
        m[2] = map[2, 2];
        m[3] = map[1, 3];
        m[4] = map[1, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_18()
    {
        //■■□■■
        //□□□□□
        //□□■□□
        int[] m = new int[5];
        m[0] = map[0, 0];
        m[1] = map[0, 1];
        m[2] = map[2, 2];
        m[3] = map[0, 3];
        m[4] = map[0, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_19()
    {
        //□□■□□
        //□□□□□
        //■■□■■
        int[] m = new int[5];
        m[0] = map[2, 0];
        m[1] = map[2, 1];
        m[2] = map[0, 2];
        m[3] = map[2, 3];
        m[4] = map[2, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_20()
    {
        //■□□□■
        //□□□□□
        //□■■■□
        int[] m = new int[5];
        m[0] = map[0, 0];
        m[1] = map[2, 1];
        m[2] = map[2, 2];
        m[3] = map[2, 3];
        m[4] = map[0, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_21()
    {
        //□■■■□
        //□□□□□
        //■□□□■
        int[] m = new int[5];
        m[0] = map[2, 0];
        m[1] = map[0, 1];
        m[2] = map[0, 2];
        m[3] = map[0, 3];
        m[4] = map[2, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_22()
    {
        //□□■□□
        //■□□□■
        //□■□■□
        int[] m = new int[5];
        m[0] = map[1, 0];
        m[1] = map[2, 1];
        m[2] = map[0, 2];
        m[3] = map[2, 3];
        m[4] = map[1, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_23()
    {
        //□■□■□
        //■□□□■
        //□□■□□
        int[] m = new int[5];
        m[0] = map[1, 0];
        m[1] = map[0, 1];
        m[2] = map[2, 2];
        m[3] = map[0, 3];
        m[4] = map[1, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_24()
    {
        //■□■□■
        //□□□□□
        //□■□■□
        int[] m = new int[5];
        m[0] = map[0, 0];
        m[1] = map[2, 1];
        m[2] = map[0, 2];
        m[3] = map[2, 3];
        m[4] = map[0, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_25()
    {
        //□■□■□
        //□□□□□
        //■□■□■
        int[] m = new int[5];
        m[0] = map[2, 0];
        m[1] = map[0, 1];
        m[2] = map[2, 2];
        m[3] = map[0, 3];
        m[4] = map[2, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_26()
    {
        //□■□□■
        //□□■□□
        //■□□■□
        int[] m = new int[5];
        m[0] = map[2, 0];
        m[1] = map[0, 1];
        m[2] = map[1, 2];
        m[3] = map[2, 3];
        m[4] = map[0, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_27()
    {
        //■□□■□
        //□□■□□
        //□■□□■
        int[] m = new int[5];
        m[0] = map[0, 0];
        m[1] = map[2, 1];
        m[2] = map[1, 2];
        m[3] = map[0, 3];
        m[4] = map[2, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_28()
    {
        //■□□□■
        //□□■□□
        //□■□■□
        int[] m = new int[5];
        m[0] = map[0, 0];
        m[1] = map[2, 1];
        m[2] = map[1, 2];
        m[3] = map[2, 3];
        m[4] = map[0, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_29()
    {
        //□■□■□
        //□□■□□
        //■□□□■
        int[] m = new int[5];
        m[0] = map[2, 0];
        m[1] = map[0, 1];
        m[2] = map[1, 2];
        m[3] = map[0, 3];
        m[4] = map[2, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_30()
    {
        //□□■■□
        //□■□□■
        //■□□□□
        int[] m = new int[5];
        m[0] = map[2, 0];
        m[1] = map[1, 1];
        m[2] = map[0, 2];
        m[3] = map[0, 3];
        m[4] = map[2, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_31()
    {
        //■□□□□
        //□■□□■
        //□□■■□
        int[] m = new int[5];
        m[0] = map[0, 0];
        m[1] = map[1, 1];
        m[2] = map[2, 2];
        m[3] = map[2, 3];
        m[4] = map[1, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_32()
    {
        //■■□□□
        //□□□□□
        //□□■■■
        int[] m = new int[5];
        m[0] = map[0, 0];
        m[1] = map[0, 1];
        m[2] = map[2, 2];
        m[3] = map[2, 3];
        m[4] = map[2, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_33()
    {
        //□□■■■
        //□□□□□
        //■■□□□
        int[] m = new int[5];
        m[0] = map[2, 0];
        m[1] = map[2, 1];
        m[2] = map[0, 2];
        m[3] = map[0, 3];
        m[4] = map[0, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_34()
    {
        //□■□□□
        //■□□■□
        //□□■□■
        int[] m = new int[5];
        m[0] = map[1, 0];
        m[1] = map[0, 1];
        m[2] = map[2, 2];
        m[3] = map[1, 3];
        m[4] = map[2, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_35()
    {
        //□□■□■
        //■□□■□
        //□■□□□
        int[] m = new int[5];
        m[0] = map[1, 0];
        m[1] = map[2, 1];
        m[2] = map[0, 2];
        m[3] = map[1, 3];
        m[4] = map[0, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_36()
    {
        //■□■□□
        //□■□■□
        //□□□□■
        int[] m = new int[5];
        m[0] = map[0, 0];
        m[1] = map[1, 1];
        m[2] = map[0, 2];
        m[3] = map[1, 3];
        m[4] = map[2, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_37()
    {
        //□□□□■
        //□■□■□
        //■□■□□
        int[] m = new int[5];
        m[0] = map[2, 0];
        m[1] = map[1, 1];
        m[2] = map[2, 2];
        m[3] = map[1, 3];
        m[4] = map[0, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_38()
    {
        //□□□■■
        //■□□□□
        //□■■□□
        int[] m = new int[5];
        m[0] = map[1, 0];
        m[1] = map[2, 1];
        m[2] = map[2, 2];
        m[3] = map[0, 3];
        m[4] = map[0, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_39()
    {
        //■■□□□
        //□□■■□
        //□□□□■
        int[] m = new int[5];
        m[0] = map[0, 0];
        m[1] = map[0, 1];
        m[2] = map[1, 2];
        m[3] = map[1, 3];
        m[4] = map[2, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_40()
    {
        //□□□□■
        //□□■■□
        //■■□□□
        int[] m = new int[5];
        m[0] = map[2, 0];
        m[1] = map[2, 1];
        m[2] = map[1, 2];
        m[3] = map[1, 3];
        m[4] = map[0, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_41()
    {
        //□■■■■
        //□□□□□
        //■□□□□
        int[] m = new int[5];
        m[0] = map[2, 0];
        m[1] = map[0, 1];
        m[2] = map[0, 2];
        m[3] = map[0, 3];
        m[4] = map[0, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_42()
    {
        //■□□□□
        //□□□□□
        //□■■■■
        int[] m = new int[5];
        m[0] = map[0, 0];
        m[1] = map[2, 1];
        m[2] = map[2, 2];
        m[3] = map[2, 3];
        m[4] = map[2, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_43()
    {
        //□□□□■
        //□□□□□
        //■■■■□
        int[] m = new int[5];
        m[0] = map[2, 0];
        m[1] = map[2, 1];
        m[2] = map[2, 2];
        m[3] = map[2, 3];
        m[4] = map[0, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_44()
    {
        //■■■■□
        //□□□□□
        //□□□□■
        int[] m = new int[5];
        m[0] = map[0, 0];
        m[1] = map[0, 1];
        m[2] = map[0, 2];
        m[3] = map[0, 3];
        m[4] = map[2, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_45()
    {
        //□■□■□
        //■□■□■
        //□□□□□
        int[] m = new int[5];
        m[0] = map[1, 0];
        m[1] = map[0, 1];
        m[2] = map[1, 2];
        m[3] = map[0, 3];
        m[4] = map[1, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_46()
    {
        //□□□□□
        //■□■□■
        //□■□■□
        int[] m = new int[5];
        m[0] = map[1, 0];
        m[1] = map[2, 1];
        m[2] = map[1, 2];
        m[3] = map[2, 3];
        m[4] = map[1, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_47()
    {
        //■□□□□
        //□■□□□
        //□□■■■
        int[] m = new int[5];
        m[0] = map[0, 0];
        m[1] = map[1, 1];
        m[2] = map[2, 2];
        m[3] = map[2, 3];
        m[4] = map[2, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_48()
    {
        //□□■■■
        //□■□□□
        //■□□□□
        int[] m = new int[5];
        m[0] = map[2, 0];
        m[1] = map[1, 1];
        m[2] = map[0, 2];
        m[3] = map[0, 3];
        m[4] = map[0, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_49()
    {
        //■□□□□
        //□■■■■
        //□□□□□
        int[] m = new int[5];
        m[0] = map[0, 0];
        m[1] = map[1, 1];
        m[2] = map[1, 2];
        m[3] = map[1, 3];
        m[4] = map[1, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    public ulong Payline_50()
    {
        //□□□□□
        //□■■■■
        //■□□□□
        int[] m = new int[5];
        m[0] = map[2, 0];
        m[1] = map[1, 1];
        m[2] = map[1, 2];
        m[3] = map[1, 3];
        m[4] = map[1, 4];

        int[] A = new int[18];
        System.Array.Clear(A, 0, 18);

        for (int i = 0; i < 5; i++)
        {
            A[m[i]]++;
        }
        return Calculate_Score(A);
    }

    //거의 일어날 확률이 없음
    public ulong JackPot_Prize()
    {
        int cnt = 0;
        ulong j_prize = 0;
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 5; j++)
            {
                if (map[i, j] == (int)Symbols.coin1) cnt+=1;
                if (map[i, j] == (int)Symbols.coin1) cnt+=2;
                if (map[i, j] == (int)Symbols.coin1) cnt+=3;
            }
        }

        if (cnt == 3)
        {
            j_prize = bettingGold;
        }
        if (cnt == 4)
        {
            j_prize = bettingGold*3;
        }
        if (cnt == 5)
        {
            j_prize = jackGold_5;
        }
        if (cnt == 6)
        {
            j_prize = jackGold_6;
        }
        if (cnt == 7)
        {
            j_prize = jackGold_7;
        }
        if (cnt == 8)
        {
            j_prize = jackGold_8;
        }
        if (cnt == 9)
        {
            j_prize = jackGold_9;
        }
        return j_prize;
    }


    public int Diamond_Blue_Count()
    {
        int cnt = 0;
        for (int i = 0; i < 3; i++)
        {
            //5번째 릴에서 다이아 개수를 카운팅해줌
            if (map[i, 4] == (int)Symbols.dia_blue) cnt++;
        }
        return cnt;
    }

    public int Diamond_Green_Count()
    {
        int cnt = 0;
        for (int i = 0; i < 3; i++)
        {
            //5번째 릴에서 다이아 개수를 카운팅해줌
            if (map[i, 4] == (int)Symbols.dia_green) cnt++;
        }
        return cnt;
    }

    public int Diamond_Red_Count()
    {
        int cnt = 0;
        for (int i = 0; i < 3; i++)
        {
            //5번째 릴에서 다이아 개수를 카운팅해줌
            if (map[i, 4] == (int)Symbols.dia_red) cnt++;
        }
        return cnt;
    }

}
