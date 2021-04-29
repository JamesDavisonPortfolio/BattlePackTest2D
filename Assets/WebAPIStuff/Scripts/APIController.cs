using System;
using UnityEngine;
using RestSharp;
using SimpleJSON;
using TMPro;

public class APIController : MonoBehaviour
{
    public TMP_InputField usernameInputField;
    public TMP_Text scoreField;
    public FinalData globalData;

    // Private IP Address of webserver api
    private readonly string APIUrl = "https://rochester.cc/api/";

    void Start()
    {
        Debug.Log(String.Format("[!] API Loaded"));
        scoreField.text = "Points: " + globalData.getCurrentTotalScore().ToString();
    }

    public void OnSubmitButtonPress()
    {
        SubmitInformation(
            usernameInputField.text, 
            globalData.getCurrentTotalScore(), 
            APIUrl
        );
    }

    public void SubmitInformation(string username, int score, string APIUrl)
    {
        Debug.Log(String.Format("[?] Username: {0}, Score: {1}", username, score));

        // Send data to API
        IRestResponse POSTresponse = APIRequest(APIUrl, Method.POST, username, score);

        // If username already exists 
        if (POSTresponse.Content.Contains("player with this username already exists."))
        {
            Debug.Log(String.Format("[!] Username already exists, checking existing score"));

            // If latest score is better
            IRestResponse GETResponse = APIRequest(APIUrl + username + "/", Method.GET);
            if ((int)JSONParser(GETResponse)["score"] < score)
            {
                Debug.Log(String.Format("[!] New score is better"));

                // Update with new score
                APIRequest(APIUrl + username + "/", Method.PUT, username, score);
            }
        }
    }

    public IRestResponse APIRequest(string APIUrl, Method requestMethod, string username = null, int? score = null)
    {
        var client = new RestClient(APIUrl);
        client.Timeout = -1;
        var request = new RestRequest(requestMethod);
        request.AddHeader("Content-Type", "application/json");

        // for PUT and POST methods
        if (username != null && score != null)
        {
            string JSONData = "    {\n        \"username\": \"" + username + "\",\n        \"score\": " + Convert.ToString(score) + "\n    }";
            request.AddParameter("application/json", JSONData, ParameterType.RequestBody);
        }

        IRestResponse response = client.Execute(request);

        //Debug Information
        Debug.Log(String.Format("Content: {0}", Convert.ToString(response.Content)));
        Debug.Log(String.Format("IsSuccessful: {0}", Convert.ToString(response.IsSuccessful)));
        Debug.Log(String.Format("ResponseStatus: {0}", Convert.ToString(response.ResponseStatus)));
        Debug.Log(String.Format("StatusCode: {0} - {1}", (int)response.StatusCode, Convert.ToString(response.StatusCode)));
        Debug.Log(String.Format("ErrorMessage: {0}", Convert.ToString(response.ErrorMessage)));
        Debug.Log(String.Format("ErrorException: {0}", Convert.ToString(response.ErrorException)));

        return response;
    }

    public JSONNode JSONParser(IRestResponse response)
    {
        return JSON.Parse(response.Content); ;
    }
}
