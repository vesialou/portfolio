namespace Game.Features.Observer
{
    public class ScoreModel
    {
        public int CurrentScore { get; private set; }

        public void AddScore(int points)
        {
            CurrentScore += points;
        }

        public void ResetScore()
        {
            CurrentScore = 0;
        }
    }
}