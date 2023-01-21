using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using UnityEngine.Events;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    #region STARTING_VARIABLE
    public static GameManager Instance { get; private set; }

    [SerializeField]
    private TMP_Text _scoreText, _endScoreText,_highScoreText;

    private int score;
    

    [SerializeField]
    private Animator _scoreAnimator;

    [SerializeField]
    private AnimationClip _scoreClip;

    [SerializeField]
    private GameObject _endPanel;

    [SerializeField]
    private Image _soundImage;

    [SerializeField]
    private Sprite _activeSoundSprite, _inactiveSoundSprite;

    [SerializeField]
    private int totalScoreTargets;

    [SerializeField]
    public int currentTargetIndex;
    #endregion

    #region MONOBEHAVIOUR
    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        ColorChanged += OnColorChanged;
    }

    private void OnDisable()
    {
        ColorChanged -= OnColorChanged;
    }

    private void Start()
    {
        AudioManager.Instance.AddButtonSound();

        StartCoroutine(IStartGame());
    }
    #endregion

    #region UI
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(Constants.DATA.MAIN_MENU_SCENE);
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToggleSound()
    {
        bool sound = (PlayerPrefs.HasKey(Constants.DATA.SETTINGS_SOUND) ? PlayerPrefs.GetInt(Constants.DATA.SETTINGS_SOUND)
            : 1) == 1;
        sound = !sound;
        PlayerPrefs.SetInt(Constants.DATA.SETTINGS_SOUND, sound ? 1 : 0);
        _soundImage.sprite = sound ? _activeSoundSprite : _inactiveSoundSprite;
        AudioManager.Instance.ToggleSound();
    }

    public void UpdateScore()
    {
        score++;
        _scoreText.text = score.ToString();
        _scoreAnimator.Play(_scoreClip.name, -1, 0f);

        if (score % 2 == 0)
        {
            CurrentColorId++;
        }
    }

    #endregion

    #region GAEM_START
    public void EndGame()
    {
        StartCoroutine(IEndGame());
    }

    [SerializeField] private Animator bestAnimator;
    [SerializeField] private AnimationClip bestClip;
    private IEnumerator IEndGame()
    {
        GameEnded?.Invoke();
        hasGameEnded = true;
        _scoreText.gameObject.SetActive(false);

        yield return MoveCamera(new Vector3(cameraStartPos.x, -cameraStartPos.y, cameraStartPos.z));

        _endPanel.SetActive(true);
        _endScoreText.text = score.ToString();
        _endPanel.GetComponent<Image>().color = CurrentColor;

        bool sound = (PlayerPrefs.HasKey(Constants.DATA.SETTINGS_SOUND) ?
          PlayerPrefs.GetInt(Constants.DATA.SETTINGS_SOUND) : 1) == 1;
        _soundImage.sprite = sound ? _activeSoundSprite : _inactiveSoundSprite;

        int highScore = PlayerPrefs.HasKey(Constants.DATA.HIGH_SCORE) ? PlayerPrefs.GetInt(Constants.DATA.HIGH_SCORE) : 0;
        if (score > highScore)
        {
            _highScoreText.text = "NEW BEST";
            highScore = score;
            PlayerPrefs.SetInt(Constants.DATA.HIGH_SCORE, highScore);
            bestAnimator.Play(bestClip.name, -1, 0f);
        }
        else
        {
            _highScoreText.text = "BEST " + highScore.ToString();
        }
    }


    public static event Action<int> UpdateScoreColor;
   

    [SerializeField] private Vector3 cameraStartPos, cameraEndPos;
    [SerializeField] private float timeToMoveCamara;

    private bool hasGameEnded;
    public static event Action GameStarted, GameEnded;

    private IEnumerator IStartGame()
    {
        hasGameEnded = false;
        Camera.main.transform.position = cameraStartPos;
        _scoreText.gameObject.SetActive(false);

        yield return MoveCamera(cameraEndPos);

        _scoreText.gameObject.SetActive(true);
        score = 0;
        _scoreText.text = score.ToString();
        _scoreAnimator.Play(_scoreClip.name, -1, 0f);

        StartCoroutine(ISpawnObstacle());
        CurrentColorId = 0;
        GameStarted?.Invoke();
    }

    private IEnumerator MoveCamera(Vector3 endPos)
    {
        var fixedUpdate = new WaitForFixedUpdate();
        Transform cameraTransform = Camera.main.transform;
        float timeElapsed = 0;
        Vector3 startPos = cameraTransform.position;
        Vector3 offset = endPos - startPos;
        float speed = 1 / timeToMoveCamara;
        while(timeElapsed < 1f)
        {
            timeElapsed += speed * Time.fixedDeltaTime;
            cameraTransform.position = startPos + timeElapsed * offset;
            yield return fixedUpdate;
        }

        cameraTransform.position = endPos;
    }
    #endregion

    #region OBSTACLE_SPAWING
    [SerializeField] private GameObject obstaclePrefab, scorePrefab;
    [SerializeField] private float obstacleSpawnTime;
    
    private IEnumerator ISpawnObstacle()
    {
        float spawnTime;

        while (!hasGameEnded)
        {
            Instantiate(obstaclePrefab);

            if(UnityEngine.Random.Range(0,2) == 0)
            {
                Instantiate(scorePrefab);
            }

            spawnTime = obstacleSpawnTime * UnityEngine.Random.Range(2, 5) * 0.5f;
            yield return new WaitForSeconds(spawnTime);
        }
    }
    #endregion

    #region COLOR_CHANGE
    [SerializeField] private List<Color> colors;
    [HideInInspector] public Color CurrentColor => colors[CurrentColorId];
    [HideInInspector] public UnityAction<Color> ColorChanged;
    
    private int currentColorId;
    
    private int CurrentColorId
    {
        get { return currentColorId; }
        set
        {
            currentColorId = value % colors.Count;
            ColorChanged?.Invoke(CurrentColor);
        }
    }

    [SerializeField] private Camera main;

    private void OnColorChanged(Color col)
    {
        main.backgroundColor = col;
    }

    #endregion

}
