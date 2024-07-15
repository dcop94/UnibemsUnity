using UnityEngine;
using UnityEngine.UI; // Ui 시스템 작업
using UnityEngine.EventSystems; // 탐색을 제어
using UnityEngine.SceneManagement; // 씬 이동 
using System.Collections; // 코루틴 (시간제어)


public class ChangeInput : MonoBehaviour
{
    EventSystem system; 

    public Text errorMessage;
    public InputField username;
    public InputField password;
    public Button LoginButton;

    public GameObject[] canvas;

    
    void Start()
    {
        system = EventSystem.current;
        errorMessage.text = ""; // 초기에는 빈 문자열로 설정

        LoginButton.onClick.AddListener(OnLoginButtonClick);
    }

    void Update()
    {
        // 탭(Tab)키를 누르면 다음 칸으로 이동
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
            {
                next.Select();
            }
        }
        // 엔터(Enter)키를 누르면 로그인 버튼 클릭
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
                LoginButton.onClick.Invoke();
        }
    }

    void OnLoginButtonClick()
    {
        checkValidation();
    }

    public void checkValidation()
    {
        string name = username.text;
        string pass = password.text;

        if (name == "unicorn" && pass == "1234")
        {
            ShowLoginError("로그인 되었습니다.\n데이터를 불러오는중입니다...");
            StartCoroutine(LoadMainMenuAfterDelay(2.0f)); // 2초 지연 후 메인메뉴로 이동
        }
        else if (name != "unicorn" && pass != "1234")
        {
            ShowLoginError("ID와 Password의 입력이 잘못되었습니다.");
        }
        else if (name == "")
        {
            ShowLoginError("ID를 입력해주세요.");
        }
        else if (pass == "")
        {
            ShowLoginError("Password를 입력해주세요");
        }
        else if (name != "unicorn")
        {
            ShowLoginError("ID의 입력이 잘못되었습니다.");
        }
        else
        {
            ShowLoginError("Password의 입력이 잘못되었습니다.");
        }
    }

    IEnumerator LoadMainMenuAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("mainMenu");
    }

    public void ShowLoginError(string message)
    {
        errorMessage.text = message;
    }

}
