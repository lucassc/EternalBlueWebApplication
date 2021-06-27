namespace EternalBlueWebApplication.Models
{
    public class FirstLoginModel
    {
        public FirstLoginModel(string firstPasswordAsciiForm, bool isPasswordIncorrect = false)
        {
            IsPasswordIncorrect = isPasswordIncorrect;
            FirstPasswordASCIIForm = firstPasswordAsciiForm;
        }

        public bool IsPasswordIncorrect { get; }
        public string FirstPasswordASCIIForm { get; }
    }
}