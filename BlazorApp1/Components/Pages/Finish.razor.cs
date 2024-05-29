using BlazorApp1.Data;
using Microsoft.AspNetCore.Components;

namespace BlazorApp1.Components.Pages
{
    public partial class Finish
    {

        [Parameter]
        public int Parameter1 { get; set; }

        [Parameter]
        public int Parameter2 { get; set; }


        void MethodToTriggerUrl()
        {
            NavigationManager.NavigateTo("/welcome");
            Reset();


        }

        private int displayedPercentage = 0;
        private bool animationInProgress = false;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await UpdatePercentageAsync();
            }
        }

        private async Task UpdatePercentageAsync()
        {
            int targetPercentage = (int)(((double)Parameter1 / Parameter2) * 100);

            if (animationInProgress)
            {
                return; // Don't start a new animation if one is already in progress
            }

            animationInProgress = true;
            for (int i = 0; i <= targetPercentage; i++)
            {
                displayedPercentage = i;
                StateHasChanged();
                await Task.Delay(30); // Adjust the delay for smoother animation
            }
            animationInProgress = false;
        }

        void Reset()

        {
            var questions = QuestionService.Questions;
            foreach (var question in questions)
            {
                question.Answered = false;
                question.SelectedPoints = 0;
                question.PreviousPoints = 0;
                question.UserAnswer = string.Empty;
                question.SelectedAnswers.Clear();
                question.Answers.ForEach(answer => answer.IsSelected = false);

            }
            StateHasChanged();
        }

    }
}
