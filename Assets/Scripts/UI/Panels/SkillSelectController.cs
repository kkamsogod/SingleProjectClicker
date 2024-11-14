using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelectController : MonoBehaviour
{
    public List<GameObject> skills;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button selectButton;
    [SerializeField] private ProjectileBuyPanelController projectileBuyPanelController;

    private int currentSkillIndex = 0;
    private List<GameObject> purchasedSkills = new List<GameObject>();

    private void Start()
    {
        InitializeDefaultSkill();

        nextButton.onClick.AddListener(ShowNextSkill);
        selectButton.onClick.AddListener(SelectSkill);

        projectileBuyPanelController.OnSkillPurchased += OnSkillPurchased;

        UpdateSkillDisplay();
    }

    private void InitializeDefaultSkill()
    {
        if (skills.Count > 0)
        {
            GameObject defaultSkill = skills[0];
            SkillData skillData = defaultSkill.GetComponent<SkillData>();

            if (skillData != null)
            {
                purchasedSkills.Add(defaultSkill);
                currentSkillIndex = 0;
                ProjectileManager.Instance.UpgradeProjectile(skillData.projectile);
            }
        }
    }

    private void OnSkillPurchased(RangedAttackSO purchasedProjectile)
    {
        foreach (GameObject skill in skills)
        {
            SkillData skillData = skill.GetComponent<SkillData>();
            if (skillData != null && skillData.projectile == purchasedProjectile)
            {
                if (!purchasedSkills.Contains(skill))
                {
                    purchasedSkills.Add(skill);

                    if (purchasedSkills.Count == 1)
                    {
                        currentSkillIndex = 0;
                        UpdateSkillDisplay();
                    }
                }
                break;
            }
        }
    }

    private void ShowNextSkill()
    {
        if (purchasedSkills.Count == 0) return;

        currentSkillIndex = (currentSkillIndex + 1) % purchasedSkills.Count;
        UpdateSkillDisplay();
    }

    private void UpdateSkillDisplay()
    {
        if (purchasedSkills.Count == 0)
        {
            foreach (var skill in skills)
            {
                skill.SetActive(false);
            }
            return;
        }

        for (int i = 0; i < skills.Count; i++)
        {
            skills[i].SetActive(purchasedSkills.Contains(skills[i]) && i == currentSkillIndex);
        }
    }

    private void SelectSkill()
    {
        if (purchasedSkills.Count == 0)
        {
            return;
        }

        GameObject selectedSkill = purchasedSkills[currentSkillIndex];
        SkillData selectedSkillData = selectedSkill.GetComponent<SkillData>();

        if (selectedSkillData != null)
        {
            ProjectileManager.Instance.UpgradeProjectile(selectedSkillData.projectile);
        }
    }
}