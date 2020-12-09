using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using OMSTSystem.DAL;
using System.ComponentModel;
using OMSTSystem.ViewModels;
using OMSTSystem.Entities;
using FreeCode.Exceptions;
#endregion

namespace OMSTSystem.BLL
{
    [DataObject]
    public class SchedulingController
    {
        #region Student Work Here....
        public void ScheduleMovies(TheatreBooking bookings)
        {
            // Instructions
            //  - HINT: Make use of DateTime and TimeSpan classes
            //          such as
            //          DateTime.AddMintues() 
            //          new TimeSpan(17,30,0) which is 5:30 pm
            // RULES:
            //  - all 4 scheduling slots are are filled for the theatre
            //  - 20 mintues between all movie start/end times
            //  - No movie "overlaps" - movie cannot start in a theatre on that DATE
            //          if the TIME of the previous movie has not "ended"
            //          Each movie has a length defined in the movies table.
            //          (including the 20 minutes above)
            //  - No movies can start earlier than 11 AM
            //  - No movies can end after 11 PM
            //  - You have the option of throwing an exception for each error OR
            //      captureing all errors and throw them as a single BusinessRuleException 
            //          - e.g.: to create an error message use:
            //                      brokenRules.Add("put your error message string here");
            //                  then to throw the list of error messages use
            //                      throw new BusinessRuleException("title", brokenRules);

            #region Do NOT remove or change the following lines
            // Do NOT remove or change the following lines
            List<Exception> brokenRules = new List<Exception>();
            // Showstopper error
            if (bookings == null || !bookings.Movies.Any())
                throw new Exception("No slots were provided.");
            bookings.Movies.Sort((x, y) => x.StartTime.CompareTo(y.StartTime));
            #endregion

            #region Start of Student Code
            //    PUT YOUR CODE HERE
            #endregion
        }
        #endregion

        #region DO NOT MODIFY
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<KeyValueOption<int>> ListTicketCounts()
        {
            var result =  new List<KeyValueOption<int>>();
            for (int count = 0; count < 10; count++)
                result.Add(new KeyValueOption<int> { Key = count, DisplayText = count.ToString() });
            return result;
        }

        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<RecentTickets> Tickets_NewlyAddedTickets()
        {
            using (var context = new OMSTContext())
            {
                return context.Tickets
                    .Where(x => x.TicketID > (context.Tickets.Max(y => y.TicketID) - 25))
                    .OrderByDescending(x => x.TicketID)
                    .Select(x => new RecentTickets 
                    {
                        TicketID = x.TicketID,
                        ShowTimeID = x.ShowTimeID,
                        TicketCategoryID = x.TicketCategoryID,
                        TicketPrice = x.TicketPrice,
                        TicketPremium = x.TicketPremium
                    })
                    .ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<Category> TicketCategory_List()
        {
            using (var context = new OMSTContext())
            {
                return context.TicketCategories.Select(x => new Category { Description = x.Description, TicketPrice = x.TicketPrice }).ToList();
            }
        }


        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<KeyValueOption<int>> Locations_List()
        {
            using (var context = new OMSTContext())
            {
                return context.Locations.Select(x => new KeyValueOption<int> { Key = x.LocationID, DisplayText = x.Description }).ToList();
            }
        }

        public List<KeyValueOption<int>> ShowTimes_MoviesByLocations(int locationid)
        {
            DateTime fakeDate = DateTime.Parse("2017-12-28");
            using (var context = new OMSTContext())
            {
                var results = (from x in context.ShowTimes
                               where x.StartDate.Year == 2017
                                  && x.StartDate.Month == 12
                                  && x.StartDate.Day == 28
                                  && x.Theatre.LocationID == locationid
                               select new KeyValueOption<int>
                               {
                                   Key = x.MovieID,
                                   DisplayText = x.Movie.Title
                               }).Distinct().OrderBy(x => x.DisplayText);
                return results.ToList();
            }
        }
        public List<MovieShowTimes> ShowTimes_ShowTimesByMoviesByLocations(int locationid, int movieid)
        {
            DateTime fakeDate = DateTime.Parse("2017-12-28");
            using (var context = new OMSTContext())
            {
                var results = (from x in context.ShowTimes
                               where x.StartDate.Year == 2017
                                  && x.StartDate.Month == 12
                                  && x.StartDate.Day == 28
                                  && x.Theatre.LocationID == locationid
                                  && x.MovieID == movieid
                               select new MovieShowTimes
                               {
                                   TheatreNumber = x.Theatre.TheatreNumber,
                                   ShowTimeID = x.ShowTimeID,
                                   Times = x.StartDate
                               }).Distinct().OrderBy(x => x.Times);
                return results.ToList();
            }
        }

        public decimal Movies_GetTicketPrices(int movieid)
        {
            using (var context = new OMSTContext())
            {
                var premiuminfo = (from x in context.Movies
                                   where x.MovieID == movieid
                                   select x.ScreenType).FirstOrDefault();
                decimal premiumticket = 0.00m;
                if (premiuminfo == null)
                {
                    throw new Exception("Movie screentype info missing");
                }
                if (premiuminfo.Premium)
                {
                    if (premiuminfo.ScreenTypeID == 2)
                    {
                        premiumticket = 3.00m;
                    }
                    else
                    {
                        premiumticket = 5.00m;
                    }
                }
                return premiumticket;
            }
        }

       [DataObjectMethod(DataObjectMethodType.Select,false)]
       public List<ShowTimesAssessment> ShowTimes_ListByStartTimes()
        {
            using (var context = new OMSTContext())
            {
                var results = (from x in context.ShowTimes
                              orderby x.StartDate descending
                              select new ShowTimesAssessment
                              {
                                  ShowTimeID = x.ShowTimeID,
                                  MovieIDTitle = x.MovieID.ToString() +
                                                 " (" + x.Movie.Title + ")",
                                  StartDate = x.StartDate,
                                  TheatreIDTheatreNumber = x.TheatreID.ToString() +
                                                 " (Number:" + x.Theatre.TheatreNumber
                                                 + ")"
                              }).Take(30);
                return results.ToList();
            }
        }
		
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<KeyValueOption<int>> ListAllLocations()
        {
            using (var context = new OMSTContext())
            {
                var result = from row in context.Locations
                             select new KeyValueOption<int>
                             {
                                 DisplayText = row.Description,
                                 Key = row.LocationID
                             };
                return result.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<KeyValueOption<int>> ListTheatres(int locationID)
        {
            using (var context = new OMSTContext())
            {
                var result = from row in context.Theatres
                             where row.LocationID == locationID
                             
                             select new KeyValueOption<int>
                             {
                                 DisplayText = row.TheatreNumber.ToString(),
                                 Key = row.TheatreID
                             };
                return result.ToList();
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<KeyValueOption<int>> GetTheatre(int locationID, int theatreID, DateTime schedulingdate)
        {
            using (var context = new OMSTContext())
            {
               
                var scheduled = (from row in context.ShowTimes
                                where row.Theatre.LocationID == locationID
                                   && row.TheatreID == theatreID
                                   && schedulingdate.Year == row.StartDate.Year
                                   && schedulingdate.Month == row.StartDate.Month
                                   && schedulingdate.Day == row.StartDate.Day
                                select row).FirstOrDefault();
                if (scheduled== null)
                {
                    var result = from row in context.Theatres
                             where row.LocationID == locationID
                                 && row.TheatreID == theatreID
                             select new KeyValueOption<int>
                             {
                                 DisplayText = row.TheatreNumber.ToString(),
                                 Key = row.TheatreID
                             };
                
                    return result.ToList();
                }
                else
                {
                    return new List<KeyValueOption<int>>();
                }
            }
        }
        [DataObjectMethod(DataObjectMethodType.Select)]
        public List<KeyValueOption<int>> ListMovies()
        {
            using (var context = new OMSTContext())
            {
                var result = from row in context.Movies
                             orderby row.Title
                             select new KeyValueOption<int>
                             {
                                 DisplayText = row.Title + " (" + row.Length.ToString() + ")",
                                 Key = row.MovieID
                             };
                return result.ToList();
            }
        }
        #endregion
    }
}
