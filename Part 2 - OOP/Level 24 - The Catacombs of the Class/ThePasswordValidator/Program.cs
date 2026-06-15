PasswordValidator theUltimatePasswordValidator = new();
bool result;

while (true)
{
    result = theUltimatePasswordValidator.IsValidPassword(EnterPassword());

    if (result)
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine("This password is valid!");
        Console.ForegroundColor = ConsoleColor.White;
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("This password is invalid!");
        Console.ForegroundColor = ConsoleColor.White;
    }
}

static string? EnterPassword()
{
    Console.Write("Enter a password to validate: ");

    return Console.ReadLine();
}

public class PasswordValidator
{
    public PasswordValidator() { }

    public bool IsValidPassword(string? password)
    {
        /*
         * Valid passwords must follow these rules:
         * 
         * Rule 1: Have length [6 - 13]
         * Rule 2: Contain at least 1 uppercase letter, lowercaseletter, and number
         * Rule 3: Not contain capital letter T or an ampersand (&)
         */
        if (password == null) return false;
        else return IsPasswordRuleOneValid(password)
                    && IsPasswordRuleTwoValid(password)
                    && IsPasswordRuleThreeValid(password);
    }

    private static bool IsPasswordRuleOneValid(string password)
        => password.Length >= 6 && password.Length <= 13;

    private static bool IsPasswordRuleTwoValid(string password)
    {
        bool upperCharFlag, lowerCharFlag, numberFlag;
        upperCharFlag = lowerCharFlag = numberFlag = false;

        foreach (char letter in password)
        {
            if (char.IsUpper(letter)) upperCharFlag = true;
            else if (char.IsLower(letter)) lowerCharFlag = true;
            else if (char.IsNumber(letter)) numberFlag = true;
        }

        return upperCharFlag && lowerCharFlag && numberFlag;
    }

    private static bool IsPasswordRuleThreeValid(string password)
    {
        foreach (char letter in password)
        {
            if (letter == 'T' || letter == '&') return false;
        }

        return true;
    }
}