namespace BlazorApp1.Components.Pages
{
    public partial class WelcomeCard
    {
        void MethodToTriggerUrl()
        {
            NavigationManager.NavigateTo("/questions");


        }
    }
}
