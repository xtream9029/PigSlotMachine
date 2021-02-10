using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameControl : Symbol
{
    //row 클래스에 있는 함수를 실행시키게금 넘겨주는 부분
    public static event Action HandlePulled = delegate { };//PullHandle에서 호출됨
    public static event Action Stop = delegate { };

    [SerializeField]//private 변수를 인스펙터에서 접근 가능하게 해주는 기능
    private Text prizeText;//점수표시

    public Text goldText;//점수표시
    public Text bettingText;
    public Text errorText;
    public Text bounsText;
    public Text DiaRedCount;
    public Text DiaBlueCount;
    public Text DiaGreenCount;
    public Text jackPot_5;
    public Text jackPot_6;
    public Text jackPot_7;
    public Text jackPot_8;
    public Text jackPot_9;
    public Text FreeSpinText;

    public PayLine p;

    public int diaBlueCount = 0;
    public int diaRedCount = 0;
    public int diaGreenCount = 0;
    public ulong jackGold_5;
    public ulong jackGold_6;
    public ulong jackGold_7;
    public ulong jackGold_8;
    public ulong jackGold_9;

    private bool jackFlag = false;
    private bool CanSpin = true;

    public InputField InputMoney;

    [SerializeField]
    protected Row[] rows;//슬롯 열형 배열,초기화를 유니티에서 해주고 있음 (객체를 집어 넣어서)

    [SerializeField]
    private Transform handle;//스핀,스탑 버튼

    public ulong prizeValue;//실제 점수값
    public static ulong goldValue = 10000;//초기 금액
    public ulong bettingGold;

    private bool resultsChecked = false;

    void Update()
    {
        //슬롯의 릴이 돌고 점수가 나오는 부분을 컨트롤 하는 부분
        if (Input.GetKeyDown(KeyCode.Return))//엔터 입력
        {
            string tmp = InputMoney.text;//배팅금액 입력
            //배팅금액은 엔터를 쳤을때에만 결정이 되기 때문에
            bettingGold = Convert.ToUInt32(tmp);//string -> int 

            //가지고 있는 금액보다 더 많은 금액을 배팅했을때 배팅이 안되는 부분을 구현
            if (bettingGold > goldValue)
            {
                GameObject.Find("Betting").transform.Find("BettingText").gameObject.SetActive(false);
                GameObject.Find("Error").transform.Find("ErrorText").gameObject.SetActive(true);
                errorText.text = "Not enough money!";
                goldText.text = "Gold:" + GetThousandCommaText(goldValue);
                CanSpin = false;//더 이상 스핀을 돌릴수 없다는 플래그
            }
            else
            {
                GameObject.Find("Betting").transform.Find("BettingText").gameObject.SetActive(true);
                GameObject.Find("Error").transform.Find("ErrorText").gameObject.SetActive(false);
                bettingText.text = "Betting gold is" + " " + GetThousandCommaText(bettingGold) + "!";
                CanSpin = true;
            }
        }

        //5개의 릴중에 하나라도 돌고 있으면 점수 안나옴
        else if (!rows[0].rowStopped || !rows[1].rowStopped || !rows[2].rowStopped || !rows[3].rowStopped || !rows[4].rowStopped)
        {
            prizeValue = 0;
            prizeText.enabled = false;//릴이 돌고 있는 동안에는 prizeText가 안보임
            goldText.enabled = true;
            goldText.text = "Gold:" + GetThousandCommaText(goldValue);
            resultsChecked = false;
        }

        //모든슬롯의 릴이 멈춰있고 결과 체크값이 거짓일때(아직 페이라인 계산을 하지 않았으므로 결과 체크값이 거짓)
        else if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped && rows[3].rowStopped && rows[4].rowStopped && !resultsChecked)
        {
            //왜 이부분이 초기화가 안되는지 모르겠음
            //초기화를 유니티에서 함 스크립트상에서 초기화한 부분은 없음
            jackPot_5.text = Convert.ToString(GetThousandCommaText(jackGold_5));
            jackPot_6.text = Convert.ToString(GetThousandCommaText(jackGold_6));
            jackPot_7.text = Convert.ToString(GetThousandCommaText(jackGold_7));
            jackPot_8.text = Convert.ToString(GetThousandCommaText(jackGold_8));
            jackPot_9.text = Convert.ToString(GetThousandCommaText(jackGold_9));

            CheckResults();//점수 계산하는 부분으로 빠짐
            //resultCheck=true로 바뀜

            if (!jackFlag)
            {
                jackGold_5 += 15;
                jackGold_6 += 16;
                jackGold_7 += 17;
                jackGold_8 += 18;
                jackGold_9 += 19;
            }

            //스핀을 돌리고난 후 상금과 현재 가진 골드를 보여주는 부분
            prizeText.enabled = true;
            if (prizeValue == 0)
            {
                prizeText.text = "Prize:" + 0;
            }
            else prizeText.text = "Prize:" + GetThousandCommaText(prizeValue);

            if (goldValue == 0)
            {
                goldText.text = "Gold:" + 0;
            }
            else goldText.text = "Gold:" + GetThousandCommaText(goldValue);

            //============================================================================================

            if (prizeValue != 0)
            {
                GameObject.Find("GameControl").transform.Find("spin").gameObject.SetActive(false);
                GameObject.Find("GameControl").transform.Find("stop").gameObject.SetActive(false);
                GameObject.Find("GameControl").transform.Find("collect").gameObject.SetActive(true);
                //prizeValue = 0;
            }

            else if (prizeValue == 0)
            {
                GameObject.Find("GameControl").transform.Find("spin").gameObject.SetActive(true);
                GameObject.Find("GameControl").transform.Find("stop").gameObject.SetActive(false);
                GameObject.Find("GameControl").transform.Find("collect").gameObject.SetActive(false);
            }

            //다이아 몬드 카운팅하여 텍스팅
            DiaRedCount.text = Convert.ToString(diaRedCount);
            DiaBlueCount.text = Convert.ToString(diaBlueCount);
            DiaGreenCount.text = Convert.ToString(diaGreenCount);
        }
    }//Update

    public string GetThousandCommaText(ulong data)
    {
        return string.Format("{0:#,###}", data);
    }

    private IEnumerator Collect()
    {
        yield return new WaitForSecondsRealtime(1);

        ulong initialMoney = goldValue;
        ulong diff = prizeValue;
        ulong targetMoney = initialMoney + diff;
        float t = 0;

        //상금을 조금이라도 탔을때 시작
        while (diff != 0)
        {
            //GameObject.Find("GameControl").transform.Find("spin").gameObject.SetActive(false);
            //GameObject.Find("GameControl").transform.Find("stop").gameObject.SetActive(false);
            //GameObject.Find("GameControl").transform.Find("collect").gameObject.SetActive(true);

            if (t >= 4 || goldValue > targetMoney)
            {
                goldText.text = "Gold:" + GetThousandCommaText(targetMoney);
                goldValue = targetMoney;
                break;
            }

            float progress = t / 2;
            long diffPerTime = (long)Mathf.Round(diff * progress);
            goldValue = (ulong)initialMoney + (ulong)diffPerTime;
            //goldValue++;

            t += Time.smoothDeltaTime;
            goldText.text = "Gold:" + GetThousandCommaText(goldValue);
            prizeValue = 0;//상금 초기화를 콜렉트 안에서 해주고 있음
            GameObject.Find("GameControl").transform.Find("spin").gameObject.SetActive(true);
            GameObject.Find("GameControl").transform.Find("stop").gameObject.SetActive(false);
            GameObject.Find("GameControl").transform.Find("collect").gameObject.SetActive(false);
            yield return null;
        }
    }//Collect

    private void OnMouseDown()
    {
        if (!CanSpin)
        {
            errorText.text = "Not enough money!";
            if (goldValue == 0)
            {
                goldText.text = "Gold:" + 0;
            }
            else goldText.text = "Gold:" + GetThousandCommaText(goldValue);
        }//더이상 스핀이 돌수 없을때

        else if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped && rows[3].rowStopped && rows[4].rowStopped)
        {//5개의 릴이 멈춰져있는지만 확인
            if (prizeValue != 0)
            {
                StartCoroutine("Collect");
            }

            else if (prizeValue == 0)
            {
                if (goldValue - bettingGold >= 0)
                {
                    if (goldValue == 0)
                    {
                        errorText.text = "Not enough money!";
                        goldText.text = "Gold:" + 0;
                        CanSpin = false;
                    }
                    else
                    {
                        goldValue -= bettingGold;
                        goldText.text = "Gold:" + GetThousandCommaText(goldValue);
                        if (CanSpin)
                            PullHandle();//이 함수 호출이후 릴이 돌게됨

                        //릴이 돌고 있는 동안 stop버튼 표시
                        GameObject.Find("GameControl").transform.Find("spin").gameObject.SetActive(false);
                        GameObject.Find("GameControl").transform.Find("stop").gameObject.SetActive(true);
                        GameObject.Find("GameControl").transform.Find("collect").gameObject.SetActive(false);
                    }
                }
            }
        }

        //릴이 돌고 있을때 강제 스톱 하는 부분
        else if (!rows[0].rowStopped && !rows[1].rowStopped && !rows[2].rowStopped && !rows[3].rowStopped && !rows[4].rowStopped)
        {
            StopHandle();//여기서 stop함수가 delegate에 등록되고 stopFlag가 바뀜

            //안전빵으로 넣어준 부분
            for (int i = 0; i < 5; i++)
            {
                rows[0].rowStopped = true;
            }
        }
    }//OnMouseDown

    private void PullHandle()
    {
        HandlePulled();
    }

    private void StopHandle()
    {
        Stop();
    }

    //페이라인의 결과를 계산하는 부분
    private void CheckResults()
    {
        //슬롯머신 돌린 결과를 3*5 2d 배열에 받아옴
        GameObject.Find("PayLine").GetComponent<PayLine>().Mapping_symbol();
        //prizeValue가 결정되는 부분
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_123();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_4();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_5();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_6();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_7();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_8();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_9();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_10();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_11();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_12();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_13();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_14();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_15();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_16();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_17();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_18();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_19();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_20();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_21();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_22();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_23();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_24();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_25();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_26();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_27();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_28();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_29();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_30();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_31();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_32();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_33();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_34();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_35();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_36();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_37();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_38();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_39();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_40();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_41();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_42();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_43();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_44();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_45();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_46();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_47();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_48();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_49();
        prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().Payline_50();
        //prizeValue += GameObject.Find("PayLine").GetComponent<PayLine>().JackPot_Prize();

        //현재 무료스핀이 돌긴 돌고 있음
        //GameObject.Find("PayLine").GetComponent<PayLine>().Bonus_TEST();

        if (PayLine.Bonus_flag)
        {
            Bonus_TEST();
        }

        //다이아 카운팅
        diaBlueCount +=GameObject.Find("PayLine").GetComponent<PayLine>().Diamond_Blue_Count();
        diaGreenCount+=GameObject.Find("PayLine").GetComponent<PayLine>().Diamond_Green_Count();
        diaRedCount+=GameObject.Find("PayLine").GetComponent<PayLine>().Diamond_Red_Count();
        
        resultsChecked = true;//결과 체크값을 참으로 바꿔줌
    }//CheckResults

    public void Bonus_TEST()
    {
        //bonusPrize = prizeValue;//현재 값이 이상하게 나옴
        StartCoroutine("Bonus_Event");//프리스핀 디스플레이
        Invoke("PullHandle", 1);//Row클래스의 Rotate함수를 1초 뒤에 실행
        //goldValue -= bettingGold;
        goldValue += (prizeValue/2);
        goldText.text = "Gold:" + GetThousandCommaText(goldValue);//이부분이 제대로 출력이 안됨
        PayLine.Bonus_flag = false;
        
    }//Bonus_TEST
   
    private IEnumerator Bonus_Event()
    {
        GameObject.Find("Text").transform.Find("FreeSpin").gameObject.SetActive(true);
        FreeSpinText.text = "Free Spin";
        yield return new WaitForSeconds(1);
        GameObject.Find("Text").transform.Find("FreeSpin").gameObject.SetActive(false);

    }//단순 무료스핀 UI 띄우기

}
