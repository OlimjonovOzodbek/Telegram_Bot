namespace Exam_Bot
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var mybot = new MyBot("6762787972:AAEwhctjic_LoHZfqMfeF9jRA3RyBKCp9co");
            await mybot.Begin();
        }
    }
}
