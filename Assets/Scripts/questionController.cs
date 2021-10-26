using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class questionController : MonoBehaviour
{
    private List<List<Tuple<string, int>>> qcm;
    
    public TMP_Text question;
    public TMP_Text tScore;
    public List<GameObject> reponses;

    public GameObject sPanel;
    public GameObject qPanel;
    public GameObject fPanel;
    public GameObject btnValider;
    public GameObject btnContinuer;
    public GameObject tog;
    public GameObject togGroup;


    private int score = 0;
    private int nbQuestion;

    // Start is called before the first frame update
    void Start()
    {
        qcm = GetComponent<ParseXML>().parseFile();
        sPanel.SetActive(true);
        qPanel.SetActive(false);
        fPanel.SetActive(false);
        nbQuestion = qcm.Count();
        btnContinuer.SetActive(false);
    }

   public void StartQuizz()
    {
        sPanel.SetActive(false);
        qPanel.SetActive(true);
        SetQuestion();
    }

   public void Valider()
   {
       List<Tuple<string, int>> q = qcm.First();
       for (int i = 1; i < q.Count; i++)
       {
           if (q[i].Item2 == 1)
           {
               reponses[i - 1].GetComponentInChildren<Toggle>().GetComponentInChildren<Text>().color = Color.green;
               if (reponses[i - 1].GetComponentInChildren<Toggle>().isOn)
               {
                   score++;
               }
           }
           else if (reponses[i - 1].GetComponentInChildren<Toggle>().isOn)
           {
               reponses[i - 1].GetComponentInChildren<Toggle>().GetComponentInChildren<Text>().color = Color.red;
           }
       }
       foreach(var rep in reponses)
       {
           rep.GetComponentInChildren<Toggle>().interactable = false;
       }
       btnContinuer.SetActive(true);
       btnValider.SetActive(false);
   }

   public void Continuer()
   {
       qcm.RemoveAt(0);
       foreach (var rep in reponses)
       {
           Destroy(rep);
       }
       reponses.Clear();
       SetQuestion();
       btnContinuer.SetActive(false);
       btnValider.SetActive(true);
   }

   public void Quitter()
   {
       Application.Quit();
   }

    void SetQuestion()
    {
        if (qcm.Count == 0)
        {
            qPanel.SetActive(false);
            fPanel.SetActive(true);
            tScore.text = "Terminé ! \nVotre score est : \n" + score + "/"+nbQuestion;
        }
        else
        {
            int nQuestion = nbQuestion - qcm.Count() + 1;
            List<Tuple<string,int>> q = qcm.First();
            question.text = "Question " + nQuestion + " :\n" +  q.First().Item1;
            for (int i = 1; i < q.Count; i++)
            {
                reponses.Add(Instantiate(tog, togGroup.transform));
                reponses[i - 1].GetComponentInChildren<Toggle>().group = togGroup.GetComponent<ToggleGroup>();
                reponses[i-1].GetComponentInChildren<Toggle>().GetComponentInChildren<Text>().text = q[i].Item1;
                reponses[i-1].SetActive(true);
                reponses[i - 1].GetComponentInChildren<Toggle>().isOn = false;
            }
        }
    }
}
