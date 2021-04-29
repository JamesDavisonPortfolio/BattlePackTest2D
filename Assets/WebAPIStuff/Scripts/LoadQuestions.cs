using UnityEngine;
using System;
using System.Collections.Generic;
using RestSharp;
using Newtonsoft.Json;

public class LoadQuestions : MonoBehaviour
{
    public DataCarrier LevelOneDataCarrier;
    public DataCarrier LevelTwoDataCarrier;
    public DataCarrier LevelThreeDataCarrier;

    private readonly string websiteAPIURL = "https://rochester.cc/game/api/";

    private List<Question> allQuestions;

    private List<Question> levelOneQuestions = new List<Question>();
    private List<Question> levelTwoQuestions = new List<Question>();
    private List<Question> levelThreeQuestions = new List<Question>();


    void Start()
    {
        allQuestions = DeserialiseJSON(APIRequest(websiteAPIURL, Method.GET).Content);
        SortQuestions();
        AddQuestionsDataToScene(LevelOneDataCarrier, levelOneQuestions);
        AddQuestionsDataToScene(LevelTwoDataCarrier, levelTwoQuestions);
        AddQuestionsDataToScene(LevelThreeDataCarrier, levelThreeQuestions);
    }

    // Sort the Questions
    private void SortQuestions()
    {
        foreach (Question question in allQuestions)
        {
            switch (question.level)
            {
                default:
                    break;
                case 1:
                    levelOneQuestions.Add(question);
                    break;
                case 2:
                    levelTwoQuestions.Add(question);
                    break;
                case 3:
                    levelThreeQuestions.Add(question);
                    break;
            }
        }
    }

    private void AddQuestionsDataToScene(DataCarrier levelDataCarrier, List<Question> levelQuestionList) 
    {
        int count = 0;
        for (int i = 0; i < levelDataCarrier.levelQuestions.Length; i++)
        {
            levelDataCarrier.levelQuestions[i] = levelQuestionList[i].question;
            levelDataCarrier.levelInformation[i] = levelQuestionList[i].information;

            Debug.Log(String.Format("Question: {0}", i.ToString()));
            levelDataCarrier.allLevelAnswers[count] = levelQuestionList[i].correct_answer + ":y";
            levelDataCarrier.allLevelAnswers[count + 1] = levelQuestionList[i].incorrect_answer_1 + ":y";
            levelDataCarrier.allLevelAnswers[count + 2] = levelQuestionList[i].incorrect_answer_2 + ":y";
            count += 3;
        }

    }

    static public IRestResponse APIRequest(string APIUrl, Method requestMethod)
    {
        var client = new RestClient(APIUrl);
        client.Timeout = -1;
        var request = new RestRequest(requestMethod);
        request.AddHeader("Content-Type", "application/json");
        IRestResponse response = client.Execute(request);

        //Debug Information
        //Debug.Log(String.Format("Content: {0}", Convert.ToString(response.Content)));
        //Debug.Log(String.Format("IsSuccessful: {0}", Convert.ToString(response.IsSuccessful)));
        //Debug.Log(String.Format("ResponseStatus: {0}", Convert.ToString(response.ResponseStatus)));
        Debug.Log(String.Format("StatusCode: {0} - {1}", (int)response.StatusCode, Convert.ToString(response.StatusCode)));
        //Debug.Log(String.Format("ErrorMessage: {0}", Convert.ToString(response.ErrorMessage)));
        //Debug.Log(String.Format("ErrorException: {0}", Convert.ToString(response.ErrorException)));

        return response;
    }

    public static List<Question> DeserialiseJSON(string JSON)
    {
        return JsonConvert.DeserializeObject<List<Question>>(JSON);
    }
}
