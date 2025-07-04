using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class GamePresenter : IGamePresenter
{
    private IGameView _view;
    private IGameModel _model;
    public GamePresenter(IGameView view, IGameModel model)
    {
        _view = view;
        _model = model;
    }
    public void Dispose()
    {
        
    }

    public void Initialize()
    {
        
    }
}
