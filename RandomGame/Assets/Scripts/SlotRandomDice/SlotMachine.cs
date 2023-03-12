using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachine : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputBetAmount;          // 배팅 금액
    [SerializeField]
    private Image imageBetAmount;                   // 배팅 금액 색상 변경용
    [SerializeField]
    private TextMeshProUGUI textCredits;            //  플레이어 소지 금액
    [SerializeField]
    private TextMeshProUGUI textFirstReel;          // 첫 번째 릴
    [SerializeField]
    private TextMeshProUGUI textSecondReel;         // 두 번째 릴
    [SerializeField]
    private TextMeshProUGUI textThirdReel;          // 세 번째 릴
    [SerializeField]
    private TextMeshProUGUI textResult;             // 실행 결과 출력

    private float spinDuration = 0.2f;              // 릴 굴리기 지속 시간
    private float elapsedTime = 0;                  // 숫자 선택 지연 시간
    private bool isStartSpin = false;               // 작동 확인
    private int credits = 10000;                    // 플레이어 소지 금액

    private bool isFirstReelSpinned = false;
    private bool isSecondReelSpinned = false;
    private bool isThirdReelSpinned = false;

    private int firstReelResult = 0;
    private int secondReelResult = 0;
    private int thirdReelResult = 0;

    private List<int> weightReelPoll;               // 릴이 등장하는 숫자의 확률 조잦 리스트
    private int zeroProbability = 30;               // 0이 나올 확률 30%

    private void Awake()
    {
        weightReelPoll = new List<int>(100);
        // zeroProbability 개수인 30개 만큼 0으로 채원준다.
        for(int i = 0; i < zeroProbability; ++i)
        {
            weightReelPoll.Add(0);
        }

        // 0이 나올 확률이 30%이기 때문에 1 ~ 9 가 나올 확룰이 70%
        // 7.7777 = ( 100 - 30 ) / 9;
        int remaining_values_prob = (100 - zeroProbability) / 9;

        for(int i = 0; i < 10; ++ i)
        {
            for(int j = 0; j < remaining_values_prob; ++j)
            {
                weightReelPoll.Add(i);
            }
        }

        // 확인 용
        //for(int i = 0; i < weightReelPoll.Count; ++i)
        //{
        //    Debug.Log(weightReelPoll[i]);
        //}
    }
    private void Update()
    {
        if (!isStartSpin) return;

        elapsedTime += Time.deltaTime;
        int random_spinResult = Random.Range(0, 10);

        if(!isFirstReelSpinned)
        {
            firstReelResult = random_spinResult;

            if(elapsedTime >= spinDuration)
            {
                int weight_random = Random.Range(0, weightReelPoll.Count);
                firstReelResult = (int)weightReelPoll[weight_random];

                isFirstReelSpinned = true;
                elapsedTime = 0;
            }
        }
        else if (!isSecondReelSpinned)
        {
            secondReelResult = random_spinResult;

            if (elapsedTime >= spinDuration)
            {
                isSecondReelSpinned = true;
                elapsedTime = 0;
            }
        }
        else if (!isThirdReelSpinned)
        {
            thirdReelResult= random_spinResult;

            if (elapsedTime >= spinDuration)
            {
                int weight_random = Random.Range(0, weightReelPoll.Count);
                thirdReelResult = (int)weightReelPoll[weight_random];

                if((firstReelResult == secondReelResult) && (thirdReelResult != firstReelResult))
                {
                    if (thirdReelResult < firstReelResult) thirdReelResult = firstReelResult - 1;
                    if (thirdReelResult > firstReelResult) thirdReelResult = firstReelResult + 1;
                }

                isStartSpin = false;
                elapsedTime = 0;
                isFirstReelSpinned = false;
                isSecondReelSpinned = false;
                isThirdReelSpinned = false;

                CheckBet();
            }
        }

        textFirstReel.text = firstReelResult.ToString("D1");
        textSecondReel.text = secondReelResult.ToString("D1");
        textThirdReel.text = thirdReelResult.ToString("D1");
    }

    public void OnClickPull()
    {
        OnMessage(Color.white, string.Empty);

        if (inputBetAmount.text.Trim().Equals(""))
        {
            OnMessage(Color.red, "Please Fill Bet Amound");
            return;
        }

        int parse = int.Parse(inputBetAmount.text);

        if(credits - parse >= 0)
        {
            credits -= parse;
            textCredits.text = $"Credits : {credits}";

            isStartSpin = true;
        }
        else
        {
            OnMessage(Color.red, "You don't have enough money.");
        }
    }
    private void CheckBet()
    {
        int betAmount = int.Parse(inputBetAmount.text);


        if(firstReelResult == secondReelResult&& secondReelResult == thirdReelResult)
        {
            credits += betAmount * 100;
            //credits += int.Parse(inputBetAmount.text) * 100;
            //textCredits.text = $"Credits : {credits}";

            textResult.text = "Jackpot!! { betAmount * 100}";
        }
        else if(firstReelResult == 0 && thirdReelResult == 0)
        {
            credits += (int)(betAmount * 0.5f);
            textResult.text = $"There are two 0! You Win! {betAmount * 0.5f}";
        }
        else if(firstReelResult == secondReelResult)
        {
            textResult.text = "Oh... My.. jackpot!!..";
        }
        else if(firstReelResult == thirdReelResult)
        {
            credits += betAmount * 2;
            textResult.text = $"There are two same number! You Win!! {betAmount * 2}";

        }
        else
        {
            textResult.text = "YOU LOSE";
        }

        textCredits.text = $"Credits : {credits}";
    }

    private void OnMessage(Color color, string msg)
    {
        imageBetAmount.color = color;
        textResult.text = msg;  
    }
}
