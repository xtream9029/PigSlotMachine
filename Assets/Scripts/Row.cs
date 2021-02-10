using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : Symbol
{
    private int randomValue;
    public bool rowStopped;
    public bool stopFlag = false;

    //밑에서 부터 
    public int row_stoppedSlot1;
    public int row_stoppedSlot2;
    public int row_stoppedSlot3;

    // Start is called before the first frame update
    void Start()
    {
        int ran = Random.Range(1, 17);
        if (ran == 1)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 4.7f, 0);
        }
        else if (ran == 2)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 3.0f, 0);
        }
        else if (ran == 3)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 1.3f, 0);
        }
        else if (ran == 4)
        {
            transform.localPosition = new Vector3(transform.localPosition.x,-0.4f, 0);
        }
        else if (ran == 5)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -2.1f, 0);
        }
        else if (ran == 6)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -3.8f, 0);
        }
        else if (ran == 7)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -5.5f, 0);
        }
        else if (ran == 8)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -8.9f, 0);
        }
        else if (ran == 9)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -10.6f, 0);
        }
        else if (ran == 10)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -12.3f, 0);
        }
        else if (ran == 11)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -14.0f, 0);
        }
        else if (ran == 12)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -15.7f, 0);
        }
        else if (ran == 13)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -17.4f, 0);
        }
        else if (ran == 14)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -19.1f, 0);
        }
        else if (ran == 15)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -20.8f, 0);
        }
        rowStopped = true;

        //GameControl에서 넘겨준 함수들 from delegate
        GameControl.HandlePulled += StartRotating;
        GameControl.Stop += StopRotating;//단순 플래그처리로 릴을 멈춤
    }

    private void StopRotating()
    {
        //stop버튼을 눌렀을때 전역플래그 처리로 릴의 움직임을 강제로 정지시킴
        stopFlag = true;
        float t = transform.localPosition.y;
        if (t >= 3.0f && t <= 4.7f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 4.7f, 0);
        }
        if (t >= 1.3f && t <= 3.0f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 3.0f, 0);
        }
        if (t >= -0.4f && t <= 1.3f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 1.3f, 0);
        }
        if (t >= -2.1f && t <= -0.4f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -0.4f, 0);
        }
        if (t >= -3.8f && t <= -2.1f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -2.1f, 0);
        }
        if (t >= -5.5f && t <= -3.8f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -3.8f, 0);
        }
        if (t >= -7.2f && t <= -5.5f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -5.5f, 0);
        }
        if (t >= -8.9f && t <= -7.2f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -7.2f, 0);
        }
        if (t >= -10.6f && t <= -8.9f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -8.9f, 0);
        }
        if (t >= -12.3f && t <= -10.6f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -10.6f, 0);
        }
        if (t >= -14.0f && t <= -12.3f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -12.3f, 0);
        }
        if (t >= -15.7f && t <= -14.0f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -14.0f, 0);
        }
        if (t >= -17.4f && t <= -15.7f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -15.7f, 0);
        }
        if (t >= -19.1f && t <= -17.4f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -17.4f, 0);
        }
        if (t >= -20.8f && t <= -19.1f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -19.1f, 0);
        }
        if (t < -19.1f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -20.8f, 0);
        }
    }

    private void StartRotating()
    {
        row_stoppedSlot1 = 0;//맨밑
        row_stoppedSlot2 = 0;//중간
        row_stoppedSlot3 = 0;//맨위

        StartCoroutine("Rotate");
    }

    //PullHandle을 실행시키면 StartRotating을 거쳐서 이 함수로옴
    private IEnumerator Rotate()//코루틴
    {
        rowStopped = false;//슬롯머신의 릴이 돌게 되므로
        randomValue = Random.Range(60, 120);

        //실제로 릴이 도는 것을 구현한 부분
        for (int i=0;i<randomValue;i++)
        {
            if (transform.localPosition.y <= -20.8f)
            {
                transform.localPosition = new Vector3(transform.localPosition.x, 4.7f, 0);
            }

            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 0.4f, 0);

            if (stopFlag)
            {
                StopRotating();
                stopFlag = false;//stop버튼을 누른이후에 다시 spin버튼을 눌렀을때 릴이 돌수 있게
                break;                
                //break를 넣으면 stop이후 다시 spin을 했을때 돌지않는 릴이 생김
            }//stop 버튼을 눌렀을때

           
            yield return null;
        }//릴이 도는 부분까지(for)
       
        //심볼 결정
        float Y = transform.localPosition.y;
        if (Y>=3.0f && Y<=4.7f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 4.7f, 0);
            row_stoppedSlot1 = (int)Symbols.coin1;
            row_stoppedSlot2 = (int)Symbols.coin2;
            row_stoppedSlot3 = (int)Symbols.coin3;
        }
        if (Y >= 1.3f && Y <= 3.0f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 3.0f, 0);
            row_stoppedSlot1 = (int)Symbols.coin2;
            row_stoppedSlot2 = (int)Symbols.coin3;
            row_stoppedSlot3 = (int)Symbols.dia_blue;
        }
        if (Y >= -0.4f && Y <= 1.3f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 1.3f, 0);
            row_stoppedSlot1 = (int)Symbols.coin3;
            row_stoppedSlot2 = (int)Symbols.dia_blue;
            row_stoppedSlot3 = (int)Symbols.dia_green;
        }
        if (Y >= -2.1f && Y<= -0.4f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -0.4f, 0);
            row_stoppedSlot1 = (int)Symbols.dia_blue;
            row_stoppedSlot2 = (int)Symbols.dia_green;
            row_stoppedSlot3 = (int)Symbols.dia_red;
        }
        if (Y >= -3.8f && Y <= -2.1f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -2.1f, 0);
            row_stoppedSlot1 = (int)Symbols.dia_green;
            row_stoppedSlot2 = (int)Symbols.dia_red;
            row_stoppedSlot3 = (int)Symbols.pig_green;
        }
        if (Y >= -5.5f && Y <= -3.8f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -3.8f, 0);
            row_stoppedSlot1 = (int)Symbols.dia_red;
            row_stoppedSlot2 = (int)Symbols.pig_green;
            row_stoppedSlot3 = (int)Symbols.pig_blue;
        }
        if (Y >=-7.2f && Y <= -5.5f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -5.5f, 0);
            row_stoppedSlot1 = (int)Symbols.pig_green;
            row_stoppedSlot2 = (int)Symbols.pig_blue;
            row_stoppedSlot3 = (int)Symbols.pig_red;
        }
        if (Y >= -8.9f && Y <= -7.2f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -7.2f, 0);
            row_stoppedSlot1 = (int)Symbols.pig_blue;
            row_stoppedSlot2 = (int)Symbols.pig_red;
            row_stoppedSlot3 = (int)Symbols.A;
        }
        if (Y >= -10.6f&& Y <= -8.9f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -8.9f, 0);
            row_stoppedSlot1 = (int)Symbols.pig_red;
            row_stoppedSlot2 = (int)Symbols.A;
            row_stoppedSlot3 = (int)Symbols.K;
        }
        if (Y >= -12.3f && Y <= -10.6f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -10.6f, 0);
            row_stoppedSlot1 = (int)Symbols.A;
            row_stoppedSlot2 = (int)Symbols.K;
            row_stoppedSlot3 = (int)Symbols.Q;
        }
        if (Y >= -14.0f && Y <= -12.3f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -12.3f, 0);
            row_stoppedSlot1 = (int)Symbols.K;
            row_stoppedSlot2 = (int)Symbols.Q;
            row_stoppedSlot3 = (int)Symbols.J;
        }
        if (Y >= -15.7f && Y <= -14.0f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -14.0f, 0);
            row_stoppedSlot1 = (int)Symbols.Q;
            row_stoppedSlot2 = (int)Symbols.J;
            row_stoppedSlot3 = (int)Symbols.del;
        }
        if (Y >= -17.4f && Y <= -15.7f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -15.7f, 0);
            row_stoppedSlot1 = (int)Symbols.J;
            row_stoppedSlot2 = (int)Symbols.del;
            row_stoppedSlot3 = (int)Symbols.box;
        }
        if (Y >= -19.1f && Y <= -17.4f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -17.4f, 0);
            row_stoppedSlot1 = (int)Symbols.del;
            row_stoppedSlot2 = (int)Symbols.box;
            row_stoppedSlot3 = (int)Symbols.cart;
        }
        if (Y >= -20.8f && Y <= -19.1f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -19.1f, 0);
            row_stoppedSlot1 = (int)Symbols.box;
            row_stoppedSlot2 = (int)Symbols.cart;
            row_stoppedSlot3 = (int)Symbols.bonus;
        }
        if (Y < -19.1f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -20.8f, 0);
            row_stoppedSlot1 = (int)Symbols.cart;
            row_stoppedSlot2 = (int)Symbols.bonus;
            row_stoppedSlot3 = (int)Symbols.wild;
        }

        rowStopped = true;

    }//Rotate

    private void OnDestroy()
    {
        GameControl.HandlePulled -= StartRotating;
        GameControl.Stop -= StopRotating;
    }//OnDestroy
}
