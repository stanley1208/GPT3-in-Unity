using OpenAI_API;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class OpenAITest : MonoBehaviour
{
    // path 還沒改成從檔案位置開始找
    [SerializeField]
    private string path = "";
    public TMP_Text text;

    [SerializeField]
    //private string apikey;
    public int Max_tokens = 16;
    [Range(0, 1)]
    public double Temperature = 0.1;
    [Range(0, 1)]
    public double Top_p = 1;
    [Range(0, 1)]
    public int NumOutputs=1;
    [Range(0, 1)]
    public double PresencePenalty = 1;
    [Range(0, 1)]
    public double FrequencyPenalty = 1;
    [Range(0, 1)]
    public int LogProbs = 1;


    public bool Echo = false;
    
    public string[] StopSequences;


    public EngineEnum engine;

    public enum EngineEnum
    {
        Ada,
        Babbage,
        Curie,
        Davinci
    }

    //寫入
    private void WriteTxT(string p, string t)
    {
        try
        {
            StreamWriter sw = File.AppendText(p);
            sw.WriteLine(t);
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }
        catch (System.Exception e)
        {
            Debug.LogError("Write Error" + e.Message);
        }
    }

    private Engine GetEngine(EngineEnum e)
    {
        switch (e)
        {
            case EngineEnum.Ada:
                return Engine.Ada;
            case EngineEnum.Babbage:
                return Engine.Babbage;
            case EngineEnum.Curie:
                return Engine.Curie;
            case EngineEnum.Davinci:
                return Engine.Davinci;
        }
        return Engine.Default;
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    //執行GPT-3
    public void runGPT()
    {
        var task = StartAsync();
    }

    async Task StartAsync()
    {
        // string[] files = Directory.GetFiles(@"C:/Users/88698/Desktop", "waiter_prompt.txt", SearchOption.AllDirectories);

        /*
        foreach (var file in files)
        {
            try {
                path = file;
                Debug.Log(file);
            }
            catch(System.Exception e)
            {
                Debug.Log(e.Message);
                continue;
            }
        }
        */
        
        //讀取
        if (File.Exists(path) == false)
        {
            Debug.LogError("txt missing: " + path);
        }
        var txt = File.ReadAllText(path);


        //api金鑰

        //var apikey = "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";


        //if (apikey == "")
        //{
           // Debug.Log("Need apikey");
        //}


        //訓練模組設定
        // var api = new OpenAI_API.OpenAIAPI(apikey,engine: "text-davinci-002");
        string prompt = txt;
        OAIEngine.Instance.Api.UsingEngine = GetEngine(engine);
        var c = new CompletionRequest(prompt, Max_tokens, Temperature, Top_p, NumOutputs, PresencePenalty, FrequencyPenalty, LogProbs, Echo, StopSequences);
        var result = await OAIEngine.Instance.Api.Completions.CreateCompletionsAsync(c);
        /*
        result = await api.Completions.CreateCompletionAsync(
            prompt,
            temperature: 0.9,
            max_tokens: 150,
            top_p: 1,
            numOutputs: 10,
            frequencyPenalty: 0.6,
            presencePenalty: 0,
            logProbs: 0
            );
        */
        if (result == null)
        {
            result = await OAIEngine.Instance.Api.Completions.CreateCompletionsAsync(c);
        }


        //輸出
        text.text = result.ToString();

        //text.text = result.Completions[0].Text;
        
        //寫入文件
        WriteTxT(path, result.ToString() + "Human:");
        //WriteTxT(path, result.Completions[0].Text); 

    }
}