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
    [SerializeField] private string skillLockedColorHex = "#828282";//技能未解锁时的颜色
    private Color lastColor;
    public bool isLearned;//技能是否学习
    public bool isLocked;//技能是否解锁

    private void OnValidate()//inspector面板被修改时调用
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

        //icon颜色改变
        UpdateIconColor(Color.white);
        //技能管理器上解锁技能
    }

    public bool CanBeLearned()//能否学习
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
