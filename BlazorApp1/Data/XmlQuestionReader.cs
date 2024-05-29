
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlazorApp1.Data
{
    public class XmlQuestionReader
    {
        public async Task<(List<Question>, int)> ReadQuestions(string filePath)
        {
            List<Question> questions = new List<Question>();
            int totalPossiblePoints = 0;

            try
            {
                XDocument xmlDoc = XDocument.Load(filePath);

                foreach (XElement questionElement in xmlDoc.Root.Elements("question"))
                {
                    Question question = new Question
                    {
                        Number = int.Parse(questionElement.Attribute("number").Value),
                        Type = questionElement.Attribute("type").Value,
                        Text = questionElement.Element("text").Value,
                        Mandatory = bool.Parse(questionElement.Attribute("mandatory").Value)
                    };

                    if (question.Type == "multiple_choice" || question.Type == "multiple_select")
                    {
                        foreach (XElement answerElement in questionElement.Element("answers").Elements("answer"))
                        {
                            int points = int.Parse(answerElement.Attribute("points").Value);
                            question.Answers.Add((answerElement.Value, points, false));
                        }

                        if (question.Type == "multiple_choice")
                        {
                            var maxPoints = questionElement.Element("answers").Elements("answer")
                                .Max(a => int.Parse(a.Attribute("points").Value));
                            totalPossiblePoints += maxPoints;
                        }
                        else if (question.Type == "multiple_select")
                        {
                            var points = questionElement.Element("answers").Elements("answer")
                                .Sum(a => int.Parse(a.Attribute("points").Value));
                            totalPossiblePoints += points;
                        }
                    }

                    questions.Add(question);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading XML file: " + ex.Message);
            }

            return (questions, totalPossiblePoints);
        }
    }
}
