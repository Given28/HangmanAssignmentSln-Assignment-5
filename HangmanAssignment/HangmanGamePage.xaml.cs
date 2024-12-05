namespace HangmanAssignment;

public partial class HangmanGamePage : ContentPage
{
    private string wordToGuess;
    private string displayedWord;
    private int wrongGuesses;
    private char[] guessedLetters;
    private string[] hangmanImages =
    {
            "hang1.png", "hang2.png", "hang3.png", "hang4.png",
            "hang5.png", "hang6.png", "hang7.png", "hang8.png"
    };
    private string[] wordList = new string[] { "LDIL", "GIVEN", "SUCCESS", "FORTUNATELY", "ACCESS", "ELECTRONIC", "SOFTWARE", "GRADE", "MOBILE", "GAME", "MAUI", "VANESSA" };
    public HangmanGamePage()
	{
		InitializeComponent();
        InitializeGame();
    }
    private void InitializeGame()
    {
        var random = new Random();
        wordToGuess = wordList[random.Next(wordList.Length)].ToUpper();

        // Set up the game
        wrongGuesses = 0;
        guessedLetters = new char[wordToGuess.Length];
        for (int i = 0; i < wordToGuess.Length; i++)
            guessedLetters[i] = '_';

        // Reveal the starting word obscured by underscores
        displayedWord = new string('_', wordToGuess.Length);
        WordToGuessLabel.Text = displayedWord;

        // Display the initial hangman image
        HangmanImage.Source = hangmanImages[wrongGuesses];
        ResultLabel.Text = string.Empty;
    }

    private void OnGuessClicked(object sender, EventArgs e)
    {
        string guess = GuessEntry.Text?.ToUpper();  // Convert to uppercase for consistency
        if (string.IsNullOrEmpty(guess) || guess.Length != 1 || !char.IsLetter(guess[0]))
        {
            ResultLabel.Text = "Please enter a valid letter!";
            return;
        }

        // Verify if the letter exists in the word
        bool found = false;
        for (int i = 0; i < wordToGuess.Length; i++)
        {
            if (wordToGuess[i] == guess[0])
            {
                guessedLetters[i] = guess[0];
                found = true;
            }
        }

        // Refresh the displayed word
        displayedWord = new string(guessedLetters);
                              
        WordToGuessLabel.Text = displayedWord;

        if (!found)
        {
            // Invalid guess
            wrongGuesses++;
            HangmanImage.Source = hangmanImages[wrongGuesses];
        }

        // Assess if the game is complete
        if (wrongGuesses == 7)
        {
            ResultLabel.Text = $"You Died! The word was: {wordToGuess}";
            return;
        }

        if (displayedWord == wordToGuess)
        {
            ResultLabel.Text = "You Survived! You guessed the word!";
            return;
        }

        GuessEntry.Text = string.Empty; // Reset the input field
    }
}