using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public PurificationManager purificationManager;

    private string[][] questionData;

    public TextMeshProUGUI questionText;

    public TextMeshProUGUI[] answerText;

    public TextMeshProUGUI[] checkMarks;

    public Button[] buttons;

    public GameObject quizPanel;

    public List<int> suffleNumbers;

    public int answer;

    public bool isExact;

    public QuizManager()
    {
        questionData = new string[][]
        {
            new string[] {"1 + 1 = ?", "2", "1", "-1", "3", "4", "0"},
            new string[] {"2 + 3 = ?", "5", "6", "3", "1", "4", "0"},
            new string[] {"5 - 4 = ?", "1", "2", "3", "4", "0", "-1"},
            new string[] {"9 - 5 = ?", "4", "5", "3", "6", "2", "8"},
            new string[] {"5 + 8 = ?", "13", "10", "3", "11", "12", "14"},
            new string[] {"1 + 2 = ?", "3", "1", "0", "2", "4", "5"}
        };
    }

    private void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int buttonIndex = i; 

            buttons[i].onClick.AddListener(() => OnButtonClick(buttonIndex));
        }

        for(int i = 2; i <= 6; i++)
        {
            suffleNumbers.Add(i);
        }
    }


    private void OnButtonClick(int buttonIndex)
    {
        if(buttonIndex == answer)
        {
            Debug.Log("정답");
            checkMarks[buttonIndex].text = "O";
            checkMarks[buttonIndex].color = Color.green;

            isExact = true;

           
        }
        else
        {
            Debug.Log("오답");
            checkMarks[buttonIndex].text = "X";
            checkMarks[buttonIndex].color = Color.red;

            isExact = false;
        }

        StartCoroutine(DelayedSetQuiz());
    }

    private IEnumerator DelayedSetQuiz()
    {
        yield return new WaitForSeconds(1f);

        // 퀴즈 창을 닫고 보상을 준다.
        quizPanel.SetActive(false);
        if(isExact)
        {
            purificationManager.Purify();
        }
        //SetQuiz();
    }

    public void ActivePurification()
    {
        quizPanel.SetActive(true);
        SetRandomNumbers();
        SetQuiz();
    }

    private void SetQuiz()
    {
        ResetCheckMark();
        int index = Random.Range(0, questionData.Length);
        questionText.text = GetQuestion(index);
        SetWrongAnswers(index);
        SetAnswer(index);
    }

    private void ResetCheckMark()
    {
        for(int i = 0; i < checkMarks.Length; i++)
        {
            checkMarks[i].text = "";
        }
    }

    private void SetAnswer(int index)
    {
        answer = Random.Range(0, buttons.Length);

        answerText[answer].text = questionData[index][1];

    }

    private void SetWrongAnswers(int index)
    {
        for(int i = 0; i < buttons.Length; i++)
        {
            answerText[i].text = questionData[index][suffleNumbers[i]];
        }
    }

    private void SetRandomNumbers()
    {
        System.Random rng = new System.Random();
        int n = suffleNumbers.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            int temp = suffleNumbers[k];
            suffleNumbers[k] = suffleNumbers[n];
            suffleNumbers[n] = temp;
        }
    }

    private string GetQuestion(int index)
    {
        return questionData[index][0];
    }
    private string GetCorrectAnswer(int index)
    {
        return questionData[index][1]; // 정답 가져오기
    }

    private string[] GetWrongAnswers(int index)
    {
        string[] wrongAnswers = new string[6];

        // 오답 가져오기
        for (int i = 0; i < 6; i++)
        {
            wrongAnswers[i] = questionData[index][i + 2];
        }

        return wrongAnswers;
    }
}
