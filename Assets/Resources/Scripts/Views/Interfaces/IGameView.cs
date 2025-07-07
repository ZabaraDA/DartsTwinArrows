using System;

public interface IGameView
{
   event Action OnViewPauseButtonClicked;
   event Action OnViewContinueButtonClicked;
   void SetActivePausePanel(bool isActive);
}

