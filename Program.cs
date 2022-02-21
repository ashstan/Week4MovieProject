using System;
using System.IO;
using System.Linq;
using NLog.Web;

namespace Week4MovieProject
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory() + "\\nlog.config";

            var logger = NLog.Web.NLogBuilder.ConfigureNLog(path).GetCurrentClassLogger();


            Console.WriteLine("Enter 1 to print list of movies.");
            Console.WriteLine("Enter 2 to add movie.");
            Console.WriteLine("Enter anything else to exit.");
            string userInput = Console.ReadLine();

            
            if(userInput == "1"){
                //if userinput == 1: read movies
                StreamReader sr = new StreamReader("movies.csv ");
                while(!sr.EndOfStream)
                {
                        //splits csv into lines
                    string line = sr.ReadLine();
                    // var values = line.Split(",");
                    // var values = line.Split(",");
                    // var values = "";
                    var values = line.Split(",");

                    if (line.IndexOf('"') >= 0)
                    {
                        string movieID = values[0];
                        //values[0] = movieID;
                       // string movieTitle = "";
                        string movieGenre = values[values.Length - 1];
                        //if line contains ", take everything between " "
                        var idx1 = line.IndexOf('"');
                        var str = line.Substring(idx1+1);
                        var idx2 = str.IndexOf('"');
                        string movieTitle = line.Substring(idx1+1, idx2);
                        

                        Console.WriteLine("Movie ID: {0}, Movie Title: {1}, Movie Genre: {2}", movieID, movieTitle, movieGenre);
                    } else {
                        Console.WriteLine("Movie id: {0}, Movie Title: {1}, Movie Genre: {2}", values[0], values[1], values[2]);

                    }
                }
                sr.Close();
            }
            else if(userInput == "2"){ 

                Console.WriteLine("Enter movie title: ");
                string movieTitle = Console.ReadLine();

                //try parse if this isn't a number then log it in the nlog
                string movieIDString = File.ReadLines("movies.csv").Last().Split(',')[0];
                int movieID;
                if (!int.TryParse(movieIDString, out movieID)) {
                    // log error
                    logger.Error("Invalid Data.");
                } else {
                    movieID += 1;
                    Console.WriteLine("Enter movie genre (seperate by | if multiple genres): ");
                    string movieGenre = Console.ReadLine();

                    StreamWriter sw = new StreamWriter("movies.csv", true);
                    sw.WriteLine(movieID + "," + movieTitle + "," + movieGenre);

                    logger.Info("New movie added");
                    sw.Close();
                }
                
            }
        }
    }
}
