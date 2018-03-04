using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameCtrl : MonoBehaviour
{
    public enum GameMode { Train, Test, Campaign };
    public GameMode mode;
    public enum IntelMode { AI, Human, None };
    public IntelMode intelli;
    public enum CamMode { Free, Track, POV, Follow, Top, Lock };
    public CamMode Camy;
    public enum COMode { Random, Slice };
    public COMode CrossOver;
    public enum SelMode { Percent, Top2 };
    public SelMode selMode;

    public bool randomizeIniWeights, saved, saveTheData, campAdd, slowMode;
    public int day, mLaps = 2, camp, mDays;
    public Vector2 attempt;
    public GameObject forrest, startSp, t, aR, cFailed, cSucc, Ff;
    public Text vT, fT, camButt, dT, lpT, bfT, wT;
    public Slider oS;
    public Button SB, snEB;
    public InputField nfF;
    public Image sI, pI, cI;
    //public ForrestCTRL F;

    public RectTransform[] rT;
    public Text[] inp;
    public float[][] attIniData;
    public string[] attIniString;
    float mutationRate = 1f;
    float mutationProb = .05f;
    public float highestAvg;
    public string highestFitBrain;
    public List<string> loadedBrains;
    // public List<ForrestCTRL> startFatt = new List<ForrestCTRL>();
    //  public List<ForrestCTRL> allFatt = new List<ForrestCTRL>();
    Image[] aRs;
    menuParameters mP;

    // 1, 2, 3, 4, 5
    GameObject[] graphs = new GameObject[2];
    int graphWidth = 860;
    float graphBarWidth;
    List<float> fits = new List<float>();
    List<float> avgs = new List<float>();

    Vector3[] camStarts = new Vector3[2];


    // Use this for initialization
    void Start()
    {
        menuParameters.Check4MenuParam();

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        campAdd = true;

        // 
        if (GameObject.Find("menuParams"))
        {
            mP = GameObject.Find("menuParams").GetComponent<menuParameters>();

            mode = mP.mode;
            CrossOver = mP.CO;
            selMode = mP.SEL;
            randomizeIniWeights = mP.randomizeWeights;
            attempt.y = mP.attempts;
            mLaps = mP.laps;
            slowMode = mP.slowMode;

            // 
            if (mode == GameMode.Test)
            {
                attempt.y = mP.brains.Count;
            }
            else if (mode == GameMode.Campaign)
            {
                attempt.y = 1;
            }
            // 
            if (mP.brains.Count > 0)
            {
                // 
                for (int i = 0; i < mP.brains.Count; i++)
                {
                    loadedBrains.Add(mP.brains[i]);
                }
            }


            // 
            MapBuilder lb = gameObject.AddComponent<MapBuilder>();

            // 
            if (mode == GameMode.Test || mode == GameMode.Train)
            {
                lb.saveFile = mP.lvlData;
                lb.reverse = mP.reverse;
            }
            else
            {
                mP.lvlName = "Campaign Course #" + (mP.campaignProgress + 1);
                lb.saveFile = (mP.campaignProgress < 4) ? mP.campaign[(int)Mathf.Floor(mP.campaignProgress)] : "FINAL";
                lb.reverse = (mP.campaignProgress % 1) != 0;
            }

        }

        // 
        graphs[0] = GameObject.Find("FitnessGraph");
        graphs[1] = GameObject.Find("AverageGraph");

        // This will disable the UI if in race mode
        rT[0].gameObject.SetActive(mode == GameMode.Test || mode == GameMode.Campaign);
        rT[1].gameObject.SetActive(mode == GameMode.Train);

        // Set the appropriate version number on the top left of screen
        vT.text = "v" + Application.version;

        // Set all of the input texts to "?" instead of the default "New Text" :)
        for (int i = 0; i < inp.Length; i++)
        {
            inp[i].text = "?";
        }

        // This will hide the starting marker
        startSp.GetComponent<Renderer>().enabled = false;

        // This grabs a ref to the camera's starting position & rotation
        camStarts[0] = Camera.main.transform.position;
        camStarts[1] = Camera.main.transform.eulerAngles;

        // This sets the text for the control & camera mode buttons
        //ctButt.text = intelli.ToString();
        camButt.text = Camy.ToString();

        // attIniData is Attempt Initializer, we first need to define how many brains this ini will hold, it will hold our max attempt count 
        attIniData = new float[(int)attempt.y][];

        // 
        attIniString = new string[(int)attempt.y];

        // then every brian will hold 29 values, in our case floats, so we'll loop through every brain & set their value size to hold 29 floats, which will be a fully randomized brain
        for (int i = 0; i < attIniData.Length; i++)
        {
            attIniData[i] = new float[29];
        }

        // 
        if (loadedBrains == null || loadedBrains.Count < 1)
        {
            // 
            if (randomizeIniWeights)
            {
                RandomizeWholeDay();
            }
        }
        else
        {
            SetWholeDay();
        }

    }

    void GenGraph(GameObject who, string high, List<float> eval, float max)
    {
        Color32[] gCol = new Color32[]
        {
            new Color32(161,255,171,255),
            new Color32(255,161,161,255),
            new Color32(255,252,161,255),
        };

        for (int i = 0; i < Mathf.Round(graphWidth / graphBarWidth); i++)
        {
            GameObject t = new GameObject();
            t.transform.SetParent(who.transform);
            t.AddComponent<Image>();
            RectTransform rt = t.GetComponent<RectTransform>();
            rt.transform.localPosition = Vector3.zero + (Vector3.right * (i * graphBarWidth));
            rt.transform.localScale = Vector3.one;
            rt.pivot = Vector2.zero;
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.zero;
            rt.localEulerAngles = Vector3.zero;
            rt.sizeDelta = new Vector2(graphBarWidth, (eval[i] / max) * 130);

            Image im = rt.GetComponent<Image>();

            if (eval[i] == max)
            {
                im.color = gCol[2];
            }
            else if (i == 0 || eval[i] > eval[i - 1])
            {
                im.color = gCol[0];
            }
            else
            {
                im.color = gCol[1];
            }

        }

        Text disp = who.GetComponentInChildren<Text>();
        disp.transform.SetAsLastSibling();
        disp.text = high + ": " + Mathf.Round(max * 1000) / 1000;

    }

    // 
    void Update()
    {

        // Will load the menu if power is pressed
        if (Input.GetAxis("Power") > 0)
        {
            GoToMenu();
        }

        // Restart the scene when we press restart
        if (Input.GetAxis("Restart") > 0)
        {
            RestartRoom();
        }

        // Check to see if there are any active Forrests
        // bool activeFs = allFatt.Count > 0;

        // If so then perform this 
        //if (activeFs)
        {
            //  F = allFatt[ReturnHighest(allFatt)];
            //   t.transform.position = new Vector3(F.transform.position.x, 3.5f, F.transform.position.z);
            //  oS.value = F.movement;
            //  fT.text = mP.lvlName + (mP.reverse ? "(R)" : string.Empty) + "\n" + mP.CO + "-" + mP.SEL + "\n" + Mathf.Round(F.fitness * 1000) / 1000;
            //    lpT.text = ((slowMode) ? attempt.x + 1 + "/" + attempt.y + "\n" : "") + F.lap.x + "/" + F.lap.y;
            // wT.text = "" + F.myName + "\n" + mP.lvlName + (mP.reverse ? "(R)" : string.Empty) + "\n" + Mathf.Round(F.fitness * 1000) / 1000;

            // 
            for (int i = 0; i < inp.Length; i++)
            {
                try
                {
                    //       inp[i].text = "" + Mathf.Round(F.inp[i] * 100) / 100;
                }
                catch
                {
                    inp[i].text = "?";
                }
            }

            // 
            if (Camy == CamMode.Lock)
            {
                Camera.main.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
                // Camera.main.transform.position = new Vector3(F.transform.position.x, 50, F.transform.position.z);
            }
            else if (Camy == CamMode.Top)
            {
                //  Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, Quaternion.Euler(new Vector3(90, F.transform.eulerAngles.y, 0)), Time.deltaTime * 10);
                //   Camera.main.transform.position = new Vector3(F.transform.position.x, 25, F.transform.position.z);
            }
            else if (Camy == CamMode.Free)
            {
                Camera.main.transform.position = new Vector3(Mathf.Clamp(Camera.main.transform.position.x + Input.GetAxis("Horizontal"), -100, 100), Mathf.Clamp(Camera.main.transform.position.y + (Input.GetAxis("Mouse ScrollWheel") * -10), 5, 100), Mathf.Clamp(Camera.main.transform.position.z + Input.GetAxis("Vertical"), -100, 100));
            }
            else if (Camy == CamMode.Track)
            {
                Camera.main.transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Mouse ScrollWheel") * 10, Input.GetAxis("Vertical")));
                Camera.main.transform.position = new Vector3(Mathf.Clamp(Camera.main.transform.position.x, -100, 100), Mathf.Clamp(Camera.main.transform.position.y, 2, 100), Mathf.Clamp(Camera.main.transform.position.z, -100, 100));
                //    Camera.main.transform.LookAt(F.transform);
            }
            else if (Camy == CamMode.POV)
            {
                //    Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, F.transform.localRotation, Time.deltaTime * 10);
                //  Camera.main.transform.position = F.transform.position + (Vector3.up / 2);
            }
            else if (Camy == CamMode.Follow)
            {
                //  if (Vector3.Distance(Camera.main.transform.position, F.transform.position) > 3)
                {
                    Camera.main.transform.Translate(Vector3.forward * .15f);
                }

                //  Camera.main.transform.LookAt(F.transform.position + (Vector3.up * .5f));
            }
        }
    }

    public void ImStuck()
    {
        // F.fitness = 0;
        //  F.Freeze();
    }

    void GoToMenu()
    {
        SceneManager.LoadScene("menu");
    }

    /// <summary>
    /// restarts the room of course
    /// </summary>
    public void RestartRoom()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    /// <summary>
    /// This will update the Active Run Badges to reflect the accurate number of attempts left
    /// </summary>
    public void UpdateActiveRunBadges()
    {
        if (!slowMode)
        {
            // 
            //   if (allFatt.Count < aRs.Length)
            {
                SetActiveRunBadges(Color.black);

                // 
                // for (int i = 0; i < allFatt.Count; i++)
                {
                    //      aRs[i].color = Color.white;
                }
            }
        }
    }

    /// <summary>
    /// This will turn all active run badges back to the inputted color
    /// </summary>
    void SetActiveRunBadges(Color col)
    {
        // 
        aRs = aR.GetComponentsInChildren<Image>();

        // 
        foreach (Image im in aRs)
        {
            im.color = col;
        }

    }

    /// <summary>
    /// This will set each brain configuration for the whole day randomly
    /// </summary>
    void SetWholeDay()
    {
        if (mode == GameMode.Train)
        {
            attIniData = ParseBrain2(attIniData, true);
        }
        else
        {
            attIniData = ParseBrain2(attIniData, false);
        }
    }

    /// <summary>
    /// This will parse the brain from a string into a float[]
    /// </summary>
    /// <param name="size"></param>
    /// <param name="randomize"></param>
    /// <returns></returns>
    float[] ParseBrain1(string data, int size)
    {
        float[] ret = new float[size];

        // set each brain 
        for (int j = 0; j < ret.Length; j++)
        {
            ret[j] = float.Parse(data.Split(',')[j]);
        }

        return ret;
    }

    /// <summary>
    /// This will parse the brain from a string into a float[][]
    /// </summary>
    /// <param name="size"></param>
    /// <param name="randomize"></param>
    /// <returns></returns>
    float[][] ParseBrain2(float[][] size, bool randomize)
    {
        float[][] data = size;

        // set each brain 
        for (int i = 0; i < data.Length; i++)
        {
            int p = (randomize) ? Random.Range(0, loadedBrains.Count) : i;
            for (int j = 0; j < data[i].Length; j++)
            {
                data[i][j] = float.Parse(loadedBrains[p].Split(',')[j]);
            }
        }

        return data;
    }

    /// <summary>
    /// This will set each brain configuration for the whole day randomly
    /// </summary>
    void RandomizeWholeDay()
    {
        // set each brain for Data
        for (int i = 0; i < attIniData.Length; i++)
        {
            for (int j = 0; j < attIniData[i].Length; j++)
            {
                attIniData[i][j] = RandomWeight();
            }
        }
    }

    /// <summary>
    /// Use this function to get a random weight value
    /// </summary>
    /// <returns></returns>
    float RandomWeight()
    {
        return Random.Range(-4f, 4f);
    }

    /// <summary>
    /// This will spawn Forrest at the start marker
    /// </summary>
    /// 

    /// <summary>
    /// Resets the camera back to the topdown position
    /// </summary>
    public void ResetCamy()
    {
        Camera.main.transform.parent = null;
        Camera.main.transform.position = camStarts[0];
        Camera.main.transform.eulerAngles = camStarts[1];
    }

    /// <summary>
    /// Toggles the control from human to AI
    /// </summary>
    public void ToggleControl()
    {
        intelli = intelli == IntelMode.Human ? IntelMode.AI : IntelMode.Human;
        //ctButt.text = intelli.ToString();
    }

    /// <summary>
    /// Toggles the camera from top to follow
    /// </summary>
    public void ToggleCamera()
    {
        int enumL = System.Enum.GetValues(typeof(CamMode)).Length - 1;

        Camy = (Camy != (CamMode)enumL ? Camy += 1 : (CamMode)0);
        camButt.text = Camy.ToString();
    }

    public void ReverseNTest()
    {
        mP.reverse = mP.reverse ? false : true;
        RestartRoom();
    }
}
