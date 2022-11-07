using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    //�������о�̬��Ա���ԣ���ȡ��ǰѪ������̬Ѫ��ʵ������
    public static UIHealthBar Instance { get; private set; }

    //����UIͼ�ζ���MASK
    public Image mask;

    //���ñ�������¼���ֲ��ʼ����
    float originalSize;

    private void Awake()
    {
        //���þ�̬ʵ��Ϊ��ǰ�����
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //��ȡ���ֲ�ͼ��ĳ�ʼ���
       originalSize = mask.rectTransform.rect.width;
    }


    //��������������MASK���ֲ���
    public void SetValue(float value)
    {
        //���ø���MASK���ֲ�Ŀ�ȣ����Ҹ��ݲ������и���
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
