namespace BlazorApp1.Data
{
    public class Question
    {
        public int Number { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }
        public bool Mandatory { get; set; }
        public List<(string Answer, int Points, bool IsSelected)> Answers { get; set; } = new List<(string Answer, int Points, bool IsSelected)>();
        public int SelectedPoints { get; set; }
        public bool Answered { get; set; } = false;  // Indicates whether the user has answered this question
        public int PreviousPoints { get; set; } = 0;  // Stores the points of the previously selected answer
       
        public List<int> SelectedAnswers { get; set; } = new List<int>();  // Stores the points of all selected answers
        public string UserAnswer { get; set; }

    }
    


 

}
