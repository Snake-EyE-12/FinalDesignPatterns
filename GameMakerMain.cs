using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectPSD
{
    internal class GameMakerMain
    {
        public void Run()
        {
            Console.WriteLine("\n\n\n");
            Console.WriteLine("Initializing Generation");
            IInputReader inputReader = new InputReader();
            string inputtedStrategyType = inputReader.GetString("Input Genre");
            IIdeaGeneratorFactory factory = new ConcreteFactory();
            IdeaGenerationStrategy genreStrategy = factory.GetStrategy(inputtedStrategyType);
            Console.WriteLine(genreStrategy.Generate());


            Console.WriteLine("\n\n\n\n\n\n\n");
        }
    }
    public interface IIdeaGenerator
    {
        public void LoadStrategy(IdeaGenerationStrategy strategy);
        public string GenerateIdea();
    }
    public interface IdeaGenerationStrategy
    {
        public string Generate();
    }
    public class ConcretePlatformerIdeaGenerationStrategy : IdeaGenerationStrategy
    {
        public string Generate()
        {
            return "Platformer Idea";
        }
    }
    public interface IIdeaGeneratorFactory
    {
        public IdeaGenerationStrategy GetStrategy(string method);
    }
    public class ConcreteFactory : IIdeaGeneratorFactory
    {
        public IdeaGenerationStrategy GetStrategy(string method)
        {
            switch(method)
            {
                case "Platformer": return new ConcretePlatformerIdeaGenerationStrategy();
                default: return null;
            }
        }
    }
    public interface IInputReader
    {
      
        public int GetInt(String message);
        public string GetString(String message);
    }
    public class InputReader : IInputReader
    {
        public int GetInt(string message)
        {
            throw new NotImplementedException();
        }

        public string GetString(string message)
        {
            Console.WriteLine(message);
            return GetNonEmptyValue();
        }
        private string GetNonEmptyValue()
        {
            string answer = "";
            while (answer.Length < 1)
            {
                answer = Console.ReadLine();
            }
            return answer;
        }
    }
}
