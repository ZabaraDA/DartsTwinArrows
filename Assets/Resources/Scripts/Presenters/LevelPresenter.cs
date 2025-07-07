using UnityEngine;

public class LevelPresenter : ILevelPresenter
{
    private ILevelView _view;
    private ILevelModel _model;

    public LevelPresenter(ILevelView view, ILevelModel model)
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
