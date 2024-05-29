using BlazorApp1.Data;
using BlazorBootstrap;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Reflection.Metadata;
using BlazorApp1.Services;

namespace BlazorApp1.Components.Pages
{
    public partial class QuestionCard
    {
        
        public List<Question> questions = new List<Question>();
        private int currentQuestionIndex = 0;
        int totalQuestions;
        public int totalPossiblePoints;

        

        protected override async Task OnInitializedAsync()
        {
            

            string filePath = "questions.xml";
            XmlQuestionReader reader = new XmlQuestionReader();
            (questions, totalPossiblePoints) = await reader.ReadQuestions(filePath); // Updated
            totalQuestions = questions.Count;
            QuestionService.Questions = questions;
        }

        private bool IsFirstQuestion => currentQuestionIndex == 0;
        private bool IsLastQuestion => currentQuestionIndex == questions.Count - 1;

        private void Previous()
        {
            if (!IsFirstQuestion)
            {
                currentQuestionIndex--;
            }
        }

       

        void MethodToTriggerUrl()
        {
            NavigationManager.NavigateTo("/finish");



        }


        //void Reset()
        //{
        //    foreach (var question in questions)
        //    {
        //        question.Answered = false;
        //        question.SelectedPoints = 0;
        //        question.PreviousPoints = 0;
        //        question.UserAnswer = string.Empty;
        //        question.SelectedAnswers.Clear();

        //        foreach (var answer in question.Answers)
        //        {
        //            answer.IsSelected = false;
        //        }
        //    }
        //}

        public int TotalPoints { get; set; } = 0;

        void Next()

        {
            // Check if the current question is mandatory and if it has been answered
            if (!questions[currentQuestionIndex].Mandatory ||
                (questions[currentQuestionIndex].Mandatory &&
                (questions[currentQuestionIndex].Answered || !string.IsNullOrWhiteSpace(questions[currentQuestionIndex].UserAnswer?.Trim())
 ||
                questions[currentQuestionIndex].SelectedPoints > 0 || questions[currentQuestionIndex].SelectedAnswers.Count > 0)))
            {
                // Only add points if this is the first time the user is answering this question
                if (!questions[currentQuestionIndex].Answered)
                {
                    TotalPoints += questions[currentQuestionIndex].SelectedPoints;
                    questions[currentQuestionIndex].Answered = true;  // Mark the question as answered
                }
                else
                {
                    // If the user has already answered this question, update the total points by subtracting the old points and adding the new ones
                    TotalPoints = TotalPoints - questions[currentQuestionIndex].PreviousPoints + questions[currentQuestionIndex].SelectedPoints;
                }

                // Store the points of the current answer for future reference
                questions[currentQuestionIndex].PreviousPoints = questions[currentQuestionIndex].SelectedPoints;

                Console.WriteLine("Total points so far: " + TotalPoints);
                // Reset SelectedPoints and proceed to the next question
                questions[currentQuestionIndex].SelectedPoints = 0;

                // Proceed to the next question
                currentQuestionIndex++;
                //// Reset the IsSelected property of each answer in the next question
                //if (currentQuestionIndex < questions.Count)
                //{
                //    for (int i = 0; i < questions[currentQuestionIndex].Answers.Count; i++)
                //    {
                //        var answer = questions[currentQuestionIndex].Answers[i];
                //        answer.IsSelected = false;
                //    }
                //}
                // Check if this is the last question
                if (currentQuestionIndex >= questions.Count)
                {
                    // If it is, navigate to the finish page
                    
                    int parameter1Value = TotalPoints;
                    int parameter2Value = totalPossiblePoints;
                    
                    NavigationManager.NavigateTo($"/finish/{parameter1Value}/{parameter2Value}");
                }
            }
            else
            {

                OnShowModalClick();
                Console.WriteLine("You must answer this question before proceeding to the next one.");
            }
        }



        


        void UpdateSelectedAnswers((string Answer, int Points, bool IsSelected) answer)


        {
            answer.IsSelected = !answer.IsSelected;  // Toggle the IsSelected property of the answer

            // Remove all instances of the answer's points from SelectedAnswers
            questions[currentQuestionIndex].SelectedAnswers.RemoveAll(points => points == answer.Points);

            if (answer.IsSelected)
            {
                // If the answer is selected, add its points to SelectedAnswers
                questions[currentQuestionIndex].SelectedAnswers.Add(answer.Points);
            }

            // Update the SelectedPoints of the current question to be the sum of SelectedAnswers
            questions[currentQuestionIndex].SelectedPoints = questions[currentQuestionIndex].SelectedAnswers.Sum();



            if (questions[currentQuestionIndex].SelectedAnswers.Any())
            {
                questions[currentQuestionIndex].Answered = true;
            }
            else
            {
                questions[currentQuestionIndex].Answered = false;
            }





        }

        
        private Modal modal = default!;

        private async Task OnShowModalClick()
        {
            await modal.ShowAsync();
        }

        private async Task OnHideModalClick()
        {
            await modal.HideAsync();
        }

      


    }


}







