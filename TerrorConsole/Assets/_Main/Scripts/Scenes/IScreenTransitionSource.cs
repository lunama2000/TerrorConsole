using System;

namespace TerrorConsole
{
    public interface IScreenTransitionSource
    {
        void TransitionToScene(string sceneName, TransitionType transitionType = TransitionType.Fade);
        void Transition(Action action, TransitionType transitionType = TransitionType.Fade);
    }
}
