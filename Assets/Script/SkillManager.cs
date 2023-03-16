using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public struct SkillSlot
{
    public string name;
    public int requireSkillPoint;
    public float addition;
    public float multiplier;

    public Button skillButton;
    public RawImage Line;

    public float getValueAfterCalc(float value)
    {
        return value * multiplier + addition;
    }
}

[Serializable]
public class Skill
{
    public int currentSkillLV;
    public SkillSlot[] skillSlot;

    public bool chooseSkill(int level, ref int skillPoint)
    {

        if(level == currentSkillLV + 1 && skillSlot[currentSkillLV].requireSkillPoint <= skillPoint)
        {
            skillPoint -= skillSlot[currentSkillLV].requireSkillPoint;

            if(skillSlot[currentSkillLV].skillButton != null) { 
                skillSlot[currentSkillLV].skillButton.interactable = false;
            }

            if (skillSlot[currentSkillLV].Line != null)
            {
                skillSlot[currentSkillLV].Line.color = Color.white;
            }
            currentSkillLV++;
            return true;
        }
        return false;
    }

    public float getValueAfterCalc(float value)
    {
        if(currentSkillLV <= 0)
        {
            return value;
        }

        return skillSlot[currentSkillLV-1].getValueAfterCalc(value);
    }
}

public class SkillManager : MonoBehaviour
{

    // Start is called before the first frame update
    [Serializable]
    public struct Level
    {
        public float expForNextLv;
        public int skillPointCanGet;
    }

    [SerializeField]
    private Level[] levelList;

    [SerializeField]
    [ReadOnly]
    private float _currentExp;
    public float currentExp
    {
        get { return _currentExp; }
    }

    [SerializeField]
    [ReadOnly]
    private float _currentExpForNextLevel;
    public float currentExpForNextLevel
    {
        get { return _currentExpForNextLevel; }
    }

    [SerializeField]
    [ReadOnly]
    private int _currentLevel;
    public float currentLevel
    {
        get { return _currentLevel; }
    }

    [SerializeField]
    [ReadOnly]
    private int _currentSkillPoint;
    public float currentSkillPoint
    {
        get { return _currentSkillPoint; }
    }

    [Header("Rocket")]
    [SerializeField]
    public Skill RocketMagazineSize; // RMS
    [SerializeField]
    public Skill RocketAttackPoint; // RAP
    [SerializeField]
    public Skill RocketReloadTime; // RRT

    [Header("Landmine")]
    [SerializeField]
    public Skill LandmineMagazineSize; // LMS
    [SerializeField]
    public Skill LandmineAttackPoint;// LAP
    [SerializeField]
    public Skill LandmineRange; // LR

    [Header("Tank Ability")]
    [SerializeField]
    public Skill TankHealth; // TH
    [SerializeField]
    public Skill TankRegeneration; // TR
    [SerializeField]
    public Skill TankTowerRotation; // TTR

    void Start()
    {
        _currentSkillPoint += levelList[_currentLevel].skillPointCanGet;
    }

    // Update is called once per frame
    void Update()
    {
        _currentExpForNextLevel = levelList[_currentLevel].expForNextLv;
    }

    public void addExp(float value)
    {
        _currentExp = _currentExp + value;

        if(_currentExp <= 0f)
        {
            _currentExp = 0f;

        }else if (_currentExp >= _currentExpForNextLevel)
        {
            _currentExp = 0f;
            levelUp();
        }
    }

    void levelUp()
    {
        _currentLevel = _currentLevel + 1;
        if(_currentLevel >= levelList.Length)
        {
            _currentLevel = levelList.Length - 1;
        }
        _currentSkillPoint += levelList[_currentLevel].skillPointCanGet;
        _currentExpForNextLevel = levelList[_currentLevel].expForNextLv;
    }

    public void skillChoose(string skill)
    {
        string[] skillCommand = skill.Split(' ');
        
        if(skillCommand.Length <= 1)
        {
            return;
        }

        switch (skillCommand[0])
        {
            case "RMS":
                RocketMagazineSize.chooseSkill(Int32.Parse(skillCommand[1]), ref _currentSkillPoint);
                break;
            case "RAP":
                RocketAttackPoint.chooseSkill(Int32.Parse(skillCommand[1]), ref _currentSkillPoint);
                break;
            case "RRT":
                RocketReloadTime.chooseSkill(Int32.Parse(skillCommand[1]), ref _currentSkillPoint);
                break;

            case "LMS":
                LandmineMagazineSize.chooseSkill(Int32.Parse(skillCommand[1]), ref _currentSkillPoint);
                break;
            case "LAP":
                LandmineAttackPoint.chooseSkill(Int32.Parse(skillCommand[1]), ref _currentSkillPoint);
                break;
            case "LR":
                LandmineRange.chooseSkill(Int32.Parse(skillCommand[1]), ref _currentSkillPoint);
                break;

            case "TH":
                TankHealth.chooseSkill(Int32.Parse(skillCommand[1]), ref _currentSkillPoint);
                break;
            case "TR":
                TankRegeneration.chooseSkill(Int32.Parse(skillCommand[1]), ref _currentSkillPoint);
                break;
            case "TTR":
                TankTowerRotation.chooseSkill(Int32.Parse(skillCommand[1]), ref _currentSkillPoint);
                break;
        }
    }

}
