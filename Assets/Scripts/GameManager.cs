using Assets.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour

{
    #region Variables

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI _headerLabel;
    [SerializeField] private TextMeshProUGUI _descriptionLabel;
    [SerializeField] private TextMeshProUGUI _choicesLabel;
    [SerializeField] private TextMeshProUGUI _locationLabel;
    [SerializeField] private Button _menuButton;
    [SerializeField] private Image _oldImage;

    [Header("Initial Setup")]
    [SerializeField] private Step _greetStep;

    [Header("External Components")]
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private string _menuSceneName;
    [SerializeField] private string _winSceneName;
    [SerializeField] private string _gameOverSceneName;

    private Step _currentStep;
    private Step _winStep;
    private Step _finishStep;
    #endregion


    #region Unity lifecycle

    private void Start()
    {
        _menuButton.onClick.AddListener(MenuButtonClicked);
        SetCurrentStep(_greetStep);
    }

    private void Update()
    {
        CheckGameOver();
        CheckGameWin();
        int choiceIndex = GetPressedButtonIndex();
        if (!IsIndexValid(choiceIndex))
            return;
        SetCurrentStep(choiceIndex);

        if (_currentStep.DebugHeaderText.Contains("Win"))
        {  _sceneLoader.LoadScene(_winSceneName);
            return;
        }
        if (_currentStep.DebugHeaderText.Contains("Finish"))
        {  _sceneLoader.LoadScene(_gameOverSceneName);
            return;
        }
    }
    
    //

    #endregion


    #region Private method
    private bool IsIndexValid(int choiceIndex) =>
        choiceIndex >= 0;
    private int GetPressedButtonIndex()
    {
        int pressedButtonIndex = NumButtonHelper.GetPressedButtonIndex();
        return pressedButtonIndex - 1;
    }

    private void SetCurrentStep(int choiceIndex)
    {
        if (_currentStep.Choices.Length <= choiceIndex)
            return;

        Step nextStep = _currentStep.Choices[choiceIndex];
        SetCurrentStep(nextStep);
    }

    private void SetCurrentStep(Step step)
    {
        if (step == null)
            return;

        _currentStep = step;

        _headerLabel.text = step.DebugHeaderText;
        _descriptionLabel.text = step.DescriptionText;
        _choicesLabel.text = step.ChoicesText;
        _locationLabel.text = step.LocationText;
         _oldImage.sprite = step.MyImage;
    }

    private void MenuButtonClicked()
    {
        _sceneLoader.LoadScene(_menuSceneName);
    }
    
    private void CheckGameOver()
    {
        if (!Input.GetKeyDown(KeyCode.KeypadEnter))
            return;
        
        if (_currentStep.Choices.Length == 0);
        _sceneLoader.LoadScene(_gameOverSceneName);
    }

    private void CheckGameWin()
    {   
        if(!Input.GetKeyDown(KeyCode.Space))
            return;
        
        if (_currentStep.Choices.Length == 1 ) ;
        _sceneLoader.LoadScene(_winSceneName);
    }

    #endregion
}