public interface IState
{ 

    void OnEnter(Monster ms);

    void Update();

    void OnExit();

}