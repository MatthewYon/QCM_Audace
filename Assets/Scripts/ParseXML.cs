using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class ParseXML : MonoBehaviour
{

    // Start is called before the first frame update

    public List<List<Tuple<string,int>>> parseFile()
    {
        TextAsset txtXmlAsset = Resources.Load<TextAsset>("qcmFile");
        var doc = XDocument.Parse(txtXmlAsset.text);

        var allDict = doc.Element("root").Elements("question");
        List<List<Tuple<string,int>>> allText = new List<List<Tuple<string,int>>>();
        foreach (var oneDict in allDict)
        {
            var sTitle = oneDict.Element("title");
            var sAnswers = oneDict.Elements("answer");

            XElement element1 = sTitle;
            XElement eAnswer;
            
            List<Tuple<string,int>> content = new List<Tuple<string, int>>();
            content.Add(new Tuple<string, int>(element1.ToString().Replace("<title>", "").Replace("</title>", ""), 0));
            
            int nbAnswer = sAnswers.Count();
            
            for (int i = 0; i < nbAnswer; i++)
            {
                eAnswer = sAnswers.ElementAt(i);
                if (eAnswer.ToString().Contains("correct"))
                {
                    content.Add(new Tuple<string,int>(eAnswer.ToString().Replace("<answer correct=\"true\">", "").Replace("</answer>",""),1));
                }
                else
                {
                    content.Add(new Tuple<string,int>(eAnswer.ToString().Replace("<answer>","").Replace("</answer>",""),0));    
                }
            }
            
            allText.Add(content);
        }
        return allText;
    }
}
