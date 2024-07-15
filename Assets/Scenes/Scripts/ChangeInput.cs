using UnityEngine;
using UnityEngine.UI; // Ui �ý��� �۾�
using UnityEngine.EventSystems; // Ž���� ����
using UnityEngine.SceneManagement; // �� �̵� 
using System.Collections; // �ڷ�ƾ (�ð�����)


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
        errorMessage.text = ""; // �ʱ⿡�� �� ���ڿ��� ����

        LoginButton.onClick.AddListener(OnLoginButtonClick);
    }

    void Update()
    {
        // ��(Tab)Ű�� ������ ���� ĭ���� �̵�
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
            {
                next.Select();
            }
        }
        // ����(Enter)Ű�� ������ �α��� ��ư Ŭ��
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
            ShowLoginError("�α��� �Ǿ����ϴ�.\n�����͸� �ҷ��������Դϴ�...");
            StartCoroutine(LoadMainMenuAfterDelay(2.0f)); // 2�� ���� �� ���θ޴��� �̵�
        }
        else if (name != "unicorn" && pass != "1234")
        {
            ShowLoginError("ID�� Password�� �Է��� �߸��Ǿ����ϴ�.");
        }
        else if (name == "")
        {
            ShowLoginError("ID�� �Է����ּ���.");
        }
        else if (pass == "")
        {
            ShowLoginError("Password�� �Է����ּ���");
        }
        else if (name != "unicorn")
        {
            ShowLoginError("ID�� �Է��� �߸��Ǿ����ϴ�.");
        }
        else
        {
            ShowLoginError("Password�� �Է��� �߸��Ǿ����ϴ�.");
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
