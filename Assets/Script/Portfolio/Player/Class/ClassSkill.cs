using UnityEngine;

public abstract class ClassSkill : MonoBehaviour
{
    public abstract bool Skill1();
    public abstract bool Skill2();
    public abstract bool Skill3();
    public abstract bool Skill4();
    public abstract bool Skill5();

    public abstract bool CanUseSkill1();
    public abstract bool CanUseSkill2();
    public abstract bool CanUseSkill3();
    public abstract bool CanUseSkill4();
    public abstract bool CanUseSkill5();

    public abstract void StartSkill1Cooldown();
    public abstract void StartSkill2Cooldown();
    public abstract void StartSkill3Cooldown();
    public abstract void StartSkill4Cooldown();
    public abstract void StartSkill5Cooldown();

    public abstract float Skill1Remain { get; }
    public abstract float Skill2Remain { get; }
    public abstract float Skill3Remain { get; }
    public abstract float Skill4Remain { get; }
    public abstract float Skill5Remain { get; }

    public abstract float Skill1Cooldown { get; }
    public abstract float Skill2Cooldown { get; }
    public abstract float Skill3Cooldown { get; }
    public abstract float Skill4Cooldown { get; }
    public abstract float Skill5Cooldown { get; }
}