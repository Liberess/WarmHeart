public enum Monstertype
{
    MGROUND,MFLY,MNOMAL,MBOMB,MTURRET
};
public interface IState
{ 

    void OnEnter(Monster ms);

    void Update();

    void OnExit();

}