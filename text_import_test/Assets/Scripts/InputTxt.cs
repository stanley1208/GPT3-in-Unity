using OpenAI_API;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InputTxt : MonoBehaviour
{
    public TMP_InputField Target;
    private string inputTxt = "";
    string replace = "The following is a conversation with an AI assistant. The assistant is helpful, creative, clever, and very friendly.Reply in 1 line.他會使用中文回覆問題." + "\r\n\r\n" + "Human: 嗨, 你是誰?" + "\r\n" + "AI: 我是由OpenAI產生的AI. 請問您需要什麼服務呢?" + "\r\n" + "Human:";
    int count = 0;
    // path 還沒改成從檔案位置開始找
    // private string path = "C:/Users/88698/Desktop/text_import_test/Assets/Scripts/waiter_prompt.txt";
    [SerializeField]
    private string path = "";


    // 寫入
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

    public void DirSearch(string sDir)
    {
        try { 
        
            foreach(string d in Directory.GetDirectories(sDir))
            {
                foreach (string f in Directory.GetDirectories(d))
                {
                        Debug.Log(f);
                }
                // DirSearch(d);
            }
        }
        catch(System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    //結束編輯並寫入 之後可能需要寫一個再次輸入時清空欄位
    public void endedit()
    {
        /*
        string[] files= Directory.GetFiles(@"C:/Users/88698/Desktop", "waiter_prompt.txt", SearchOption.AllDirectories);
         foreach (var file in files)
         {
            try
                {

           
                path = file;
                Debug.Log(file);

            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
                continue;
            }
        }
        */
        

        inputTxt = Target.GetComponent<TMP_InputField>().text;
        if (File.Exists(path))
        {
            Debug.Log("檔案存在");
            WriteTxT(path, inputTxt);
            count++;
        }
        else if(Directory.Exists(path))
        {
            Debug.Log("目錄存在");
            WriteTxT(path, inputTxt);
            count++;
        }
        else
        {
            Debug.Log("不存在");
            Directory.CreateDirectory(path);
            WriteTxT(path, inputTxt);
            count++;
        }
        
        if (count > 3)
        {
            File.WriteAllText(path, replace);
            inputTxt = Target.GetComponent<TMP_InputField>().text;
            WriteTxT(path, inputTxt);
            count = 0;
        }
        Debug.Log(count);
        //DirSearch("C:/Users/88698/Desktop/text_import_test");
        

    }

}

