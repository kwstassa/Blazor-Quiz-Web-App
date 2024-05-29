using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorApp1.Data
{
    public class Navigation
    {
        private readonly NavigationManager _navigationManager;

        public Navigation(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public void GoToStart()
        {
            _navigationManager.NavigateTo("/start");
        }

        public void GoToQuestions()
        {
            _navigationManager.NavigateTo("/questions");
        }

        public void GoToFinish()
        {
            _navigationManager.NavigateTo("/finish");
        }
    }
}
