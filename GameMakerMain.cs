using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectPSD
{

    internal class GameMakerMain
    {
        IInputReader inputReader = new InputReader();
        public void Run()
        {
            List<Command> commands = new List<Command>();
            bool gameRunning = true;
            while(gameRunning)
            {
                commands.Add(GetAndRunIdea());
                gameRunning = GetShouldContinue();
            }
            Console.WriteLine("\n");
            commands.ForEach(command => command.Execute());
        }
        private bool GetShouldContinue()
        {
            Console.WriteLine("\n");
            bool cont = inputReader.GetBool("Continue?");
            Console.WriteLine("\n");
            return cont;
        }
        private Command GetAndRunIdea()
        {
            Console.WriteLine("\n\n\n");
            Console.WriteLine("Initializing Generation");
            string inputtedStrategyType = inputReader.GetString("Input Genre");
            IIdeaGeneratorFactory factory = new ConcreteFactory();
            IStrategyDecorator genreStrategy = factory.GetStrategy(inputtedStrategyType);
            genreStrategy.Use();
            Console.WriteLine(genreStrategy.Use());
            Console.WriteLine("\n\n\n\n\n\n\n");
            return new LoggerCommand(genreStrategy);
        }
    }
    public interface Command
    {
        public void Execute();
    }
    public class LoggerCommand : Command
    {
        IStrategyDecorator generator;
        public LoggerCommand(IStrategyDecorator selectedGenerator)
        {
            generator = selectedGenerator;
        }

        public void Execute()
        {
            Console.WriteLine("Logging... Idea Generator: " + generator.GetType() + " | Loaded-Successfully");
        }
    }


    public interface IIdeaGenerator
    {
        public void LoadStrategy(IdeaGenerationStrategy strategy);
        public string GenerateIdea();
    }
    public interface IdeaGenerationStrategy : IStrategyDecorator
    {
        public string Generate();
    }
    public class ConcretePlatformerIdeaGenerationStrategy : IdeaGenerationStrategy
    {
        public string Generate()
        {
            return "Platformer Idea";
        }

        public string Use() { return Generate(); }
    }
    public class ConreteActionIdeaGenerationStrategy : IdeaGenerationStrategy { public string Generate() { return "Action Game Idea"; } public string Use() { return Generate(); } }
    public class ConreteFPSIdeaGenerationStrategy : IdeaGenerationStrategy { public string Generate() { return "FPS Idea"; } public string Use() { return Generate(); } }
    public class ConretePuzzleIdeaGenerationStrategy : IdeaGenerationStrategy { public string Generate() { return "Puzzle Concept"; } public string Use() { return Generate(); } }
    public class ConreteRogueliteIdeaGenerationStrategy : IdeaGenerationStrategy { public string Generate() { return "Rogue but lite"; } public string Use() { return Generate(); } }
    public class ConreteRoguelikeIdeaGenerationStrategy : IdeaGenerationStrategy { public string Generate() { return "Rogue but like"; } public string Use() { return Generate(); } }
    public class ConreteRPGIdeaGenerationStrategy : IdeaGenerationStrategy { public string Generate() { return "RPG Idea"; } public string Use() { return Generate(); } }
    public class ConreteRTSIdeaGenerationStrategy : IdeaGenerationStrategy { public string Generate() { return "RTS Idea"; } public string Use() { return Generate(); } }
    public class ConreteTDIdeaGenerationStrategy : IdeaGenerationStrategy { public string Generate() { return "Only Bloons TD"; } public string Use() { return Generate(); } }
    public interface IIdeaGeneratorFactory
    {
        public IStrategyDecorator GetStrategy(string method);
    }
    public class ConcreteFactory : IIdeaGeneratorFactory
    {
        public IStrategyDecorator GetStrategy(string method)
        {
            switch(method)
            {
                case "Platformer":      return new ConcretePlatformerIdeaGenerationStrategy();
                case "Action":          return new ConreteActionIdeaGenerationStrategy();
                case "FPS":             return new ConreteFPSIdeaGenerationStrategy();
                case "Puzzle":          return new ConcretePlatformerIdeaGenerationStrategy();
                case "Roguelite":       return new ConreteRogueliteIdeaGenerationStrategy();
                case "Roguelike":       return new ConreteRoguelikeIdeaGenerationStrategy();
                case "RPG":             return new ConreteRPGIdeaGenerationStrategy();
                case "RTS":             return new ConreteRTSIdeaGenerationStrategy();
                case "TowerDefense":    return new ConreteTDIdeaGenerationStrategy();
                default:                return new IncompatibleStategy();
            }
        }
    }

    public interface IStrategyDecorator
    {
        string Use();
    }

    public interface IActivatable
    {
        void Activate();
        void Deactivate();
    }
    public class IncompatibleStategy : IActivatable, IStrategyDecorator
    {
        public void Activate()
        {
            Console.WriteLine("Activating: This is something that should be incompatible");
        }

        public void Deactivate()
        {
            Console.WriteLine("Deactivate");
        }

        public string Use()
        {
            Activate();
            Deactivate();
            return "";
        }
    }
    public interface IInputReader
    {
      
        public int GetInt(string message);
        public string GetString(string message);
        public bool GetBool(string message);
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
        public bool GetBool(string message)
        {
            Console.WriteLine(message);
            bool valid = false;
            bool answer = false;
            while(!valid)
            {
                string typed = GetNonEmptyValue();
                
                switch(typed)
                {
                    case "true":
                    case "True":
                    case "TRUE":
                    case "yes":
                    case "y":
                    case "Yes":
                    case "Y":
                    case "YES":
                        answer = true;
                        valid = true;
                        break;
                    case "false":
                    case "False":
                    case "FALSE":
                    case "no":
                    case "n":
                    case "No":
                    case "NO":
                        answer = false;
                        valid = true;
                        break;
                    default:
                        answer = false;
                        valid = false;
                        break;
                }
            }
            return answer;
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
