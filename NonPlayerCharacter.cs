using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    //�Ի�����ʾʱ��
    public float displayTime = 4.0f;
    
    //��ȡ�Ի������
    public GameObject dialogbox;
    //��ʱ��������ʱ�ı�����ʾ��ʱ��
    float timerDiaplay;

    //������Ϸ��������ȡTMP�ؼ�
    public GameObject dlgTxtProGameObject;
    //������Ϸ��������
    TextMeshProUGUI _tmTxtBox;
    //���ñ����洢��ǰҳ��
    int _currentPage = 1;
    //������ҳ������
    int _totalPages;


    // Start is called before the first frame update
    void Start()
    {   
        //����ʾ�Ի���
        dialogbox.SetActive(false);
        //���ü�ʱ��������
        timerDiaplay = -1.0f;

        //��ȡ�Ի������
        _tmTxtBox =dlgTxtProGameObject.GetComponent<TextMeshProUGUI>();

}

    // Update is called once per frame
    //��Update�е���ʱ
    private void Awake()
    {
        
    }
    void Update()
    {
        //��ȡ�Ի�������жԻ�������ҳ��
        _totalPages = _tmTxtBox.textInfo.pageCount;
        //�������ʱδ����
        if (timerDiaplay >= 0.0f)
        {
            //��ҳ����
            //����û����룬SPACE������ʱ����
            if(Input.GetKeyUp(KeyCode.Space))
            {
                //����������ҳ�����ҳ
                if(_currentPage<_totalPages)
                {
                    _currentPage++;
                }
                //����ص���һҳ
                else
                {
                    _currentPage = 1;
                }
                //����������ʾ��ҳ��
                _tmTxtBox.pageToDisplay =_currentPage;
            }
            //��ʼ����ʱ
            timerDiaplay-=Time.deltaTime;
            //�������ʱ���������ضԻ���
            if (timerDiaplay < 0.0f)
            {
                dialogbox.SetActive(false);
            }
        }
        
    }
    //��ʾ�ı��򷽷�
    public void DisplayDialog()
    {
        //���ü�ʱ��
        timerDiaplay = displayTime;
       //��ʾ�Ի���
       dialogbox.SetActive(true);
    }
}
