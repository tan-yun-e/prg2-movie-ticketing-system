//============================================================
// Student Number : S10222081, S10223135
// Student Name : Fun Gao Wei, Farrell , Tan Yun-E
// Module Group : T07
//============================================================

using System;
using System.Collections.Generic;
using System.IO;


namespace PRG2_T07_Team12
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            List<Screening> screeningList = new List<Screening>();
            List<Cinema> cinemaList = new List<Cinema>();
            List<Movie> movieList = new List<Movie>();
            List<Order> orderList = new List<Order>();
            bool loadCinemamovieCheck = false;
            bool loadScreening = false;
            bool loadCheck = false;

            DisplayMenu();
            while (true)
            {
                Console.Write("Choose your option: ");
                int option = OptionValidation(Console.ReadLine(), 8);
                if (option == -1) Console.WriteLine("Please enter a valid option");

                if (option == 0) break;

                if (option == 1)
                {
                    if (loadCinemamovieCheck == false)
                    {
                        LoadMovieAndCinema(movieList, cinemaList);
                        loadCinemamovieCheck = true;
                        DisplayMenu();
                    }

                    else
                    {
                        Console.WriteLine("Cinema and Movie data has already been loaded!");
                    }
                }


                if (option == 2)
                {
                    if (loadCinemamovieCheck == false)
                    {
                        Console.WriteLine("Cinema and Movie data must be loaded first before loading Screenings!");
                    }

                    else if (loadScreening == true)
                    {
                        Console.WriteLine("Screening data has already been loaded!");
                    }

                    else
                    {
                        LoadScreenings(screeningList, movieList, cinemaList);
                        loadScreening = true;
                        loadCheck = true;
                        DisplayMenu();
                    }
                }

                if (option >= 3 && loadCheck == false)
                {
                    Console.WriteLine("Please load Movie, Cinema and Screening Data using options 1 and 2");
                }
                else
                {
                    if (option == 3)
                    {
                        ListAllMovies(movieList);
                        DisplayMenu();
                    }

                    if (option == 4)
                    {
                        ListMovieScreenings(movieList);
                        DisplayMenu();
                    }

                    if (option == 5)
                    {
                        AddScreeningToMovie(screeningList, movieList, cinemaList);
                        DisplayMenu();
                    }

                    if (option == 6)
                    {
                        DeleteMovieScreening(screeningList);
                        DisplayMenu();
                    }

                    if (option == 7)
                    {
                        AddMovieTicket(movieList, orderList);
                        DisplayMenu();
                    }

                    if (option == 8)
                    {
                        CancelOrder(orderList);
                        DisplayMenu();
                    }
                }
            }

            Console.WriteLine("Bye!");
        }

        static void DisplayMenu()
        {
            Console.WriteLine("-----------------MENU-----------------");
            Console.WriteLine("[1] Load Movie and Cinema Data");
            Console.WriteLine("[2] Load Screening Data");
            Console.WriteLine("[3] List all movies");
            Console.WriteLine("[4] List movie screenings");
            Console.WriteLine("[5] Add a movie screening session");
            Console.WriteLine("[6] Delete a movie screening session");
            Console.WriteLine("[7] Order movie ticket/s");
            Console.WriteLine("[8] Cancel order of ticket");
            Console.WriteLine("[0] Exit \n");
        }

        static void LoadMovieAndCinema(List<Movie> ml, List<Cinema> cl)
        {
            string[] movies = File.ReadAllLines("Movie.csv");
            string[] cinemas = File.ReadAllLines("Cinema.csv");

            for (int i = 1; i < movies.Length; i++)
            {
                string[] moviedata = movies[i].Split(",");
                if (moviedata[0] == "")
                {
                }
                else
                {
                    Movie movie = new Movie(moviedata[0], Convert.ToInt32(moviedata[1]), moviedata[3],
                        DateTime.Parse(moviedata[4]), new List<Screening>(), 0);
                    ml.Add(movie);
                    string genre = moviedata[2];
                    movie.AddGenre(genre);
                }
            }

            for (int i = 1; i < cinemas.Length; i++)
            {
                string[] cinemaData = cinemas[i].Split(",");
                Cinema cinema = new Cinema(cinemaData[0], Convert.ToInt32(cinemaData[1]),
                    Convert.ToInt32(cinemaData[2]));
                cl.Add(cinema);
            }

            Console.WriteLine("Movie and Cinema data has been loaded!");
        }

        static void LoadScreenings(List<Screening> sl, List<Movie> ml, List<Cinema> cl)
        {
            string[] screenings = File.ReadAllLines("Screening.csv");
            for (int i = 0; i < screenings.Length; i++)
            {
                string[] screeningData = screenings[i].Split(",");
                if (screeningData[0] == "Date Time")
                {
                }
                else
                {
                    string screeningcinemaname = screeningData[2];
                    string screeningcinemahall = screeningData[3];
                    string screeningmovietitle = screeningData[4];
                    Cinema chosenCinema = null;
                    Movie chosenMovie = null;

                    foreach (Cinema cinema in cl)
                    {
                        if (screeningcinemaname == cinema.Name && Convert.ToInt32(screeningcinemahall) == cinema.HallNo)
                        {
                            chosenCinema = cinema;
                        }
                    }

                    foreach (Movie movie in ml)
                    {
                        if (screeningmovietitle == movie.Title)
                        {
                            chosenMovie = movie;
                        }
                    }

                    Screening screening = new Screening(i, DateTime.Parse(screeningData[0]), screeningData[1],
                        chosenCinema, chosenMovie);
                    sl.Add(screening);
                }
            }

            foreach (Screening screeningAmmend in sl)
            {
                foreach (Movie movieAmmend in ml)
                {
                    if (screeningAmmend.Movie.Title == movieAmmend.Title)
                    {
                        movieAmmend.ScreeningList.Add(screeningAmmend);
                        break;
                    }
                }
            }

            foreach (Screening screening in sl)
            {
                screening.SeatsRemaining = screening.Cinema.Capacity;
            }

            Console.WriteLine("Screening Data has been loaded!");
        }

        static void ListAllMovies(List<Movie> ml)
        {
            Console.WriteLine(
                $"{"No",-5}{"Title",-30}{"Duration",-15}{"Genre",-30}{"Classification",-20}Opening Date");
            for (int i = 0; i < ml.Count; i++)
            {
                Console.WriteLine($"{"[" + (i + 1) + "]",-5}{ml[i]}");
            }
        }

        static Movie ListMovieScreenings(List<Movie> ml)
        {
            int option;
            ListAllMovies(ml);
            Console.WriteLine("[0] Exit");
            Console.Write("Please select a movie using the numbers shown\n");
            Console.WriteLine();
            while (true)
            {
                Console.Write("Select a movie: ");
                option = OptionValidation(Console.ReadLine(), ml.Count);
                if (option == -1) Console.WriteLine("Please enter a valid option");
                else break;
            }

            if (option == 0) ;
            else
            {
                Movie selectedMovie = ml[option - 1];
                selectedMovie.ScreeningList.Sort();
                if (selectedMovie.ScreeningList.Count == 0)
                {
                    Console.WriteLine("This movie does not have any screenings");
                    return null;
                }

                Console.WriteLine(
                    $"{"No",-5}{"Screening Number",-20}{"Screening Date Time",-25}{"Screening Type",-15}{"Cinema",-15}{"Movie",-30}");
                for (int i = 0; i < selectedMovie.ScreeningList.Count; i++)
                {
                    Console.WriteLine(
                        $"{"[" + (i + 1) + "]",-5}{selectedMovie.ScreeningList[i]}");
                }

                return selectedMovie;
            }

            return null;
        }

        static void AddScreeningToMovie(List<Screening> sl, List<Movie> ml, List<Cinema> cl)
        {
            Movie screeningMovie;
            Cinema screeningCinema;
            DateTime screeningDateTime;
            string screeningType;
            int movieNumber;
            int option;
            bool screeningAdd = false;
            List<Screening> checkscreeninglist = new List<Screening>();
            List<DateTime> checkdatetimes = new List<DateTime>();
            Console.WriteLine(
                $"{"No",-5}{"Title",-30}{"Duration",-15}{"Genre",-30}{"Classification",-20}Opening Date");
            for (int i = 0; i < ml.Count; i++)
            {
                Console.WriteLine($"{"[" + (i + 1) + "]",-5}{ml[i]}");
            }

            Console.WriteLine("[0] Exit");
            Console.WriteLine();
            while (true)
            {
                Console.Write("Select a movie: ");
                option = OptionValidation(Console.ReadLine(), ml.Count);
                if (option == 0)
                {
                    return;
                }

                if (option != -1)
                {
                    movieNumber = option - 1;
                    screeningMovie = ml[option - 1];
                    break;
                }

                Console.WriteLine("Please enter a valid option");
            }

            while (true)
            {
                Console.Write("Enter a screening type (2D/3D): ");
                screeningType = Console.ReadLine();
                if (screeningType is "2D" or "3D")
                {
                    break;
                }

                Console.WriteLine("Please enter a valid option");
            }

            while (true)
            {
                Console.Write("Enter a screening date and time (DD/MM/YYYY 12-hour Time format (XX:YYAM/PM)): ");
                string datetimeStr = Console.ReadLine();
                try
                {
                    DateTime test = Convert.ToDateTime(datetimeStr);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid Date-Time format!");
                    continue;
                }

                DateTime datetimeScreening = Convert.ToDateTime(datetimeStr);
                if (ml[option - 1].OpeningDate > datetimeScreening)
                {
                    Console.WriteLine("Your screening date needs to be after the Movie Opening Date");
                }
                else
                {
                    screeningDateTime = datetimeScreening;
                    break;
                }
            }

            Console.WriteLine($"{"No",-5}{"Name",-15}{"Hall Number",-15}Capacity");
            for (int i = 0; i < cl.Count; i++)
            {
                Console.WriteLine($"{"[" + (i + 1) + "]",-5}{cl[i]}");
            }

            Console.WriteLine("[0] Exit");
            while (true)
            {
                Console.Write("Select a Cinema Hall: ");
                int cinemaOption = OptionValidation(Console.ReadLine(), cl.Count);

                if (cinemaOption == 0)
                {
                    return;
                }

                if (cinemaOption != -1)
                {
                    screeningCinema = cl[cinemaOption - 1];
                    foreach (Screening screening in sl)
                    {
                        if (screening.Cinema == screeningCinema &&
                            screening.ScreeningDateTime.Date == screeningDateTime.Date)
                        {
                            checkscreeninglist.Add(screening);
                        }
                    }

                    int chosenMovieDuration = screeningMovie.Duration;

                    foreach (Screening screening in checkscreeninglist)
                    {
                        checkdatetimes.Add(screening.ScreeningDateTime.AddMinutes(chosenMovieDuration + 30));
                    }

                    if (checkdatetimes.Count == 0)
                    {
                        Screening newScreeningUnconditionally =
                            new Screening(sl.Count + 1, screeningDateTime, screeningType, screeningCinema,
                                screeningMovie);
                        newScreeningUnconditionally.SeatsRemaining = newScreeningUnconditionally.Cinema.Capacity;
                        sl.Add(newScreeningUnconditionally);
                        ml[movieNumber].ScreeningList.Add(newScreeningUnconditionally);
                        Console.WriteLine("New screening was added!");
                        break;
                    }

                    for (int i = 0; i < checkscreeninglist.Count; i++)
                    {
                        if (!(screeningDateTime < checkscreeninglist[i].ScreeningDateTime &&
                              screeningDateTime.AddMinutes(chosenMovieDuration + 30) <
                              checkscreeninglist[i].ScreeningDateTime ||
                              screeningDateTime > checkdatetimes[i] &&
                              screeningDateTime.AddMinutes(chosenMovieDuration + 30) > checkdatetimes[i]))
                        {
                            Console.WriteLine(
                                "This Cinema Hall already has a screening!");
                            return;
                        }
                    }

                    Screening newScreening =
                        new Screening(sl.Count + 1, screeningDateTime, screeningType, screeningCinema,
                            screeningMovie);
                    newScreening.SeatsRemaining = newScreening.Cinema.Capacity;
                    sl.Add(newScreening);
                    newScreening.SeatsRemaining = screeningCinema.Capacity;
                    ml[movieNumber].ScreeningList.Add(newScreening);
                    Console.WriteLine("New screening was added!");
                    return;
                }

                Console.WriteLine("Please enter a valid option");
            }
        }

        static void DeleteMovieScreening(List<Screening> sl)
        {
            int deletedscreeningOption;
            List<Screening> deletableScreeninglist = new List<Screening>();
            Screening chosenDeletedScreening;
            Console.WriteLine(
                $"{"No",-5}{"Screening Number",-20}{"Screening Date Time",-25}{"Screening Type",-15}{"Cinema",-15}{"Movie",-30}");

            for (int i = 0;
                i < sl.Count;
                i++)
            {
                if (sl[i].SeatsRemaining == sl[i].Cinema.Capacity)
                {
                    Console.WriteLine($"{"[" + (i + 1) + "]",-5}{sl[i]}");
                    deletableScreeninglist.Add(sl[i]);
                }
            }

            Console.WriteLine("[0] Exit");
            while (true)
            {
                Console.Write(
                    "Select a Screening to delete using the number labels on the extreme leftward column: ");
                deletedscreeningOption = OptionValidation(Console.ReadLine(), deletableScreeninglist.Count);
                if (deletedscreeningOption == -1) Console.WriteLine("Please enter a valid option");
                else break;
            }

            if (deletedscreeningOption == 0) ;
            else
            {
                chosenDeletedScreening = deletableScreeninglist[deletedscreeningOption - 1];

                foreach (Screening screening in sl)
                {
                    if (chosenDeletedScreening == screening)
                    {
                        sl.Remove(screening);

                        screening.Movie.ScreeningList.Remove(screening);

                        Console.WriteLine(
                            $"{"Screening Number "}{chosenDeletedScreening.ScreeningNo}{" has been successfully removed from all screening lists"}");

                        foreach (Screening screening_renumber in sl)
                        {
                            screening_renumber.ScreeningNo = sl.IndexOf(screening_renumber) + 1;
                        }

                        break;
                    }
                }
            }
        }

        static void AddMovieTicket(List<Movie> ml, List<Order> ol)
        {
            Top3Movies(ol, ml);
            Ticket ticketOrder;
            int screeningOption;
            int studentOption;
            int totalticketNo = 0;
            string classificationCheck;
            double totalAmount = 0;
            int orderNumber = ol.Count;
            Movie selectedMovie = ListMovieScreenings(ml);
            if (selectedMovie == null)
            {
                return;
            }

            Console.WriteLine("[0] Exit");
            Console.WriteLine("Please select a movie screening using the numbers from the leftmost column\n");
            while (true)
            {
                Console.Write("Select a movie screening: ");
                screeningOption = OptionValidation(Console.ReadLine(), selectedMovie.ScreeningList.Count);
                if (screeningOption == -1) Console.WriteLine("Please enter a valid option");
                else break;
            }

            if (screeningOption == 0) ;

            else
            {
                Screening selectedScreening = selectedMovie.ScreeningList[screeningOption - 1];
                while (true)
                {
                    Console.Write("Enter the total number of tickets to order (Enter 0 to exit): ");
                    totalticketNo = OptionValidation(Console.ReadLine(), double.PositiveInfinity);
                    if (totalticketNo == -1) Console.WriteLine("Please enter a valid option");
                    else if (totalticketNo > selectedScreening.SeatsRemaining)
                        Console.WriteLine("There are no more seats for this screening");
                    else break;
                }

                if (totalticketNo == 0) ;

                else
                {
                    if (selectedMovie.Classification == "G");
                    else
                    {
                        while (true)
                        {
                            Console.Write("Do all ticket holders meet the movie classification requirements? [Y/N]: ");
                            classificationCheck = Console.ReadLine();
                            if (classificationCheck is "N")
                            {
                                Console.WriteLine(
                                    "Please select a movie with an appropriate Classification Requirement for your age :D");
                                return;
                            }

                            if (classificationCheck is "Y")
                            {
                                break;
                            }

                            Console.WriteLine("Please enter either Y for yes or N for no");
                        }
                    }
                    Order neworder = new Order(orderNumber + 1, DateTime.Now);
                    neworder.Status = "Unpaid";
                    ol.Add(neworder);
                    for (int i = 0; i < totalticketNo;)
                    {
                        string chosenOption;
                        int option;
                        Console.WriteLine(
                            "Please choose a ticket type\n[1] Student\n[2] Senior Citizen (must be 55 years and above)\n[3] Adult");
                        while (true)
                        {
                            Console.Write("Enter your option: ");
                            chosenOption = Console.ReadLine();
                            option = OptionValidation(chosenOption, 3);
                            if (option != -1) break;
                            Console.WriteLine("Please enter a valid option");
                        }

                        if (option == 1)
                        {
                            if (selectedMovie.Classification == "G")
                            {
                                Console.WriteLine(
                                    "Please choose your level of study\n[1] Primary\n[2] Secondary\n[3] Tertiary");
                                while (true)
                                {
                                    Console.Write("Enter your option: ");
                                    chosenOption = Console.ReadLine();
                                    studentOption = OptionValidation(chosenOption, 3);
                                    if (studentOption != -1) break;
                                    Console.WriteLine("Please enter a valid option");
                                }

                                if (studentOption == 1)
                                {
                                    ticketOrder = new Student(selectedScreening, "Primary");
                                    neworder.TicketList.Add(ticketOrder);
                                    i++;
                                }

                                if (studentOption == 2)
                                {
                                    ticketOrder = new Student(selectedScreening, "Secondary");
                                    neworder.TicketList.Add(ticketOrder);
                                    i++;
                                }

                                if (studentOption == 3)
                                {
                                    ticketOrder = new Student(selectedScreening, "Tertiary");
                                    neworder.TicketList.Add(ticketOrder);
                                    i++;
                                }
                            }
                            else
                            {
                                Console.WriteLine(
                                    "Please choose your level of study\n[1] Secondary\n[2] Tertiary");
                                while (true)
                                {
                                    Console.Write("Enter your option: ");
                                    chosenOption = Console.ReadLine();
                                    studentOption = OptionValidation(chosenOption, 2);
                                    if (studentOption != -1) break;
                                    Console.WriteLine("Please enter a valid option");
                                }

                                if (studentOption == 1)
                                {
                                    ticketOrder = new Student(selectedScreening, "Secondary");
                                    neworder.TicketList.Add(ticketOrder);
                                    i++;
                                }

                                if (studentOption == 2)
                                {
                                    ticketOrder = new Student(selectedScreening, "Tertiary");
                                    neworder.TicketList.Add(ticketOrder);
                                    i++;
                                }
                            }
                        }

                        if (option == 2)
                        {
                            int checkYear;
                            while (true)
                            {
                                Console.Write("Enter your year of birth: ");
                                string input = Console.ReadLine();
                                checkYear = YearValidation(input);
                                if (checkYear != 0) break;
                                Console.WriteLine("Please enter a valid year");
                            }

                            if (!(DateTime.Now.Year - checkYear >= 55))
                            {
                                Console.WriteLine(
                                    "You need to be at least 55 years old to be eligible for a Senior Citizen ticket");
                                continue;
                            }

                            ticketOrder = new SeniorCitizen(selectedScreening, checkYear);
                            neworder.TicketList.Add(ticketOrder);
                            i++;
                        }

                        if (option == 3)
                        {
                            string choice;
                            while (true)
                            {
                                Console.Write("Would you like popcorn? [Y/N]: ");
                                choice = Console.ReadLine();
                                if (choice == "N" || choice == "Y") break;
                                Console.WriteLine("Please enter Y for yes or N for no");
                            }

                            if (choice == "N")
                            {
                                ticketOrder = new Adult(selectedScreening, false);
                                neworder.TicketList.Add(ticketOrder);
                                i++;
                            }
                            else
                            {
                                ticketOrder = new Adult(selectedScreening, true);
                                neworder.TicketList.Add(ticketOrder);
                                i++;
                            }
                        }
                    }

                    for (int g = 0; g < neworder.TicketList.Count; g++)
                    {
                        totalAmount = totalAmount + neworder.TicketList[g].CalculatePrice();
                    }

                    Console.Write("Amount payable is ${0:0.00}\nPress any key to make payment ",
                        totalAmount);
                    Console.ReadLine();
                    neworder.Amount = totalAmount;
                    neworder.Status = "Paid";
                    selectedScreening.SeatsRemaining -= totalticketNo;
                    foreach (Order order in ol)
                    {
                        Console.WriteLine(order.Status);
                    }

                    Console.WriteLine("Your order number is {0}", neworder.OrderNo);
                }
            }
        }

        static void CancelOrder(List<Order> ol)
        {
            int cancelledOrders = 0;
            Order checkOrder;
            int orderNumber;
            foreach (Order order in ol)
            {
                if (order.Status == "Cancelled") cancelledOrders++;
            }

            if (ol.Count < 1 || cancelledOrders == ol.Count)
            {
                Console.WriteLine("No orders have been made.");
                return;
            }

            Console.Write("Enter your order number [Enter 0 to exit]: ");
            orderNumber = OptionValidation(Console.ReadLine(), ol.Count);
            if (orderNumber == 0)
            {
                Console.WriteLine("Exit requested. Cancellation unsuccessful.");
                return;
            }

            if (orderNumber == -1)
            {
                Console.WriteLine("Invalid order number. Cancellation unsuccessful.");
                return;
            }

            if (ol[orderNumber - 1].Status == "Cancelled")
            {
                Console.WriteLine("Order has already been cancelled. Cancellation unsuccessful.");
                return;
            }

            checkOrder = ol[orderNumber - 1];
            checkOrder.Status = "Cancelled";
            Console.WriteLine("Amount of ${0} has been refunded. Cancellation successful!", checkOrder.Amount);
            checkOrder.TicketList[0].Screening.SeatsRemaining += checkOrder.TicketList.Count;
        }

        static int OptionValidation(string input, double maxvalue)
        {
            try
            {
                int index = Convert.ToInt32(input);
                if (index >= 0 && index <= maxvalue) ;
                else index = -1;

                return index;
            }
            catch (FormatException)
            {
                return -1;
            }
        }

        static int YearValidation(string input)
        {
            try
            {
                int year = Convert.ToInt32(input);
                return year;
            }
            catch (FormatException)
            {
                return 0;
            }
        }

        static void Top3Movies(List<Order> ol, List<Movie> ml)
        {
            List<Order> nonCancelledOl = new List<Order>();
            foreach (Order order in ol)
            {
                if (order.Status == "Paid")
                {
                    nonCancelledOl.Add(order);
                }
            }
            if (nonCancelledOl.Count < 1)
                Console.WriteLine(
                    "--------------------\n    Top 3 Movies    \n--------------------\n -buy tickets please :)- ");
            else
            {
                Console.WriteLine("--------------------\n    Top 3 Movies    \n--------------------");
                List<Movie> movies = SortMovie(nonCancelledOl, ml);
                movies.Sort();
                for (int i = 0; i < 3; i++)
                {
                    if (movies[i].TicketCount < 1) Console.WriteLine("{0}. --- ", i + 1);
                    else Console.WriteLine("{0}. {1}", i + 1, movies[i].Title);
                }
            }
        }

        static List<Movie> SortMovie(List<Order> ol, List<Movie> ml)
        {
            ml.Sort();
            if (ol.Count < 1) ;
            else
            {
                foreach (Order order in ol)
                {
                    order.TicketList[0].Screening.Movie.TicketCount = 0;
                }

                foreach (Order order in ol)
                {
                    if (order.Status == "Paid")
                    {
                        order.TicketList[0].Screening.Movie.TicketCount += order.TicketList.Count;
                    }
                }

                ml.Sort();
            }

            return ml;
        }
    }
}