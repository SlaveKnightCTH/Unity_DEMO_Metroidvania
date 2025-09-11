using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TreeNode : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler
{
    [SerializeField] private Skill_DataSO skilldata;
    [SerializeField] private string skillname;
    [SerializeField] private Image skillIcon;
    [SerializeField] private string skillLockedColorHex = "#828282";//����δ����ʱ����ɫ
    private Color lastColor;
    public bool isLearned;//�����Ƿ�ѧϰ
    public bool isLocked;//�����Ƿ����

    private void OnValidate()//inspector��屻�޸�ʱ����
    {
        if (skilldata == null)
            return;

        skillname = skilldata.skillname;
        skillIcon.sprite = skilldata.icon;
        gameObject.name = "UI_TreeNode - " + skilldata.skillname;
    }

    private void Awake()
    {
        UpdateIconColor(GetColorByHex(skillLockedColorHex));
    }

    public void Learn()
    {
        isLearned = true;

        //icon��ɫ�ı�
        UpdateIconColor(Color.white);
        //���ܹ������Ͻ�������
    }

    public bool CanBeLearned()//�ܷ�ѧϰ
    {
        if (isLearned || isLocked)
            return false;

        return true;
    }

    private void UpdateIconColor(Color color)
    {
        if (skillIcon == null)
            return;

        lastColor = skillIcon.color; 
        skillIcon.color = color;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Down");
        if (CanBeLearned())
            Learn();
        else
            Debug.Log("Can't be learn");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Enter");
        if(isLearned==false)
            UpdateIconColor(Color.white * 0.8f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(isLearned==false)
            UpdateIconColor(lastColor);
    }

    private Color GetColorByHex(string hex)
    {
        ColorUtility.TryParseHtmlString(hex, out Color color);

        return color;
    }
}
