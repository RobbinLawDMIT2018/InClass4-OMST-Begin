using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional Namespaces
using OMSTSystem.BLL;
using OMSTSystem.ViewModels;
#endregion

namespace OMSTWebApp.Assessments
{
    public partial class TheatreScheduler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void ScheduleMovies_Click(object sender, EventArgs e)
        {
            MessageUserControl.TryRun(() =>
            {
                var controller = new SchedulingController();
                controller.ScheduleMovies(
                    new TheatreBooking
                    {
                        Movies = GetMovies(),
                        TheatreID = int.Parse(TheatreNumbers.SelectedValue)
                    });
            }, "Movie Scheduling", "Movies have been scheduled");

        }

        #region Here be dragons!
        private List<MovieBooking> GetMovies()
        {
            List<MovieBooking> movies = new List<MovieBooking>();

            // Get the first row of the GridView (if it exists)
            GridViewRow row = null;

            var enumerator = BookingsGridView.Rows.GetEnumerator();
            if (enumerator.MoveNext())
                row = enumerator.Current as GridViewRow;
            else
            {
                throw new Exception("Please prepare which theatre you want before clicking Schedule Movies");
            }

            // Get the movies
            DateTime baseDate = DateTime.Parse(ShowDate.Text); // TODO: do as a tryparse
            MovieBooking show = null;

            //slot1
            if (TryBuildMovieBooking(row, baseDate, "MovieSlot_1", "StartTimeSlot_1", out show))
                movies.Add(show);

            //slot2
            if (TryBuildMovieBooking(row, baseDate, "MovieSlot_2", "StartTimeSlot_2", out show))
                movies.Add(show);

            //slot3
            if (TryBuildMovieBooking(row, baseDate, "MovieSlot_3", "StartTimeSlot_3", out show))
                movies.Add(show);

            //slot4
            if (TryBuildMovieBooking(row, baseDate, "MovieSlot_4", "StartTimeSlot_4", out show))
                movies.Add(show);
            return movies;
        }

        private static MovieBooking BuildMovieBooking(DateTime startDateTime, string movie)
        {
            return new MovieBooking
            {
                MovieID = int.Parse(movie),
                StartTime = startDateTime
            };
        }

        // return type: Tuple<T1,T2>   - but with Named properties of .MovieControl, .TimeControl
        // as a named tuple:
        //             (DropDownList MovieControl, TextBox TimeControl)
        private static (DropDownList MovieControl, TextBox TimeControl) FindRowControls(GridViewRow row, string movieControlId, string timeControlId)
        {
            (DropDownList MovieControl, TextBox TimeControl) result = (row.FindControl(movieControlId) as DropDownList, row.FindControl(timeControlId) as TextBox);
            return result;
        }
        private static bool TryBuildMovieBooking(GridViewRow row, DateTime baseDate, string movieControlId, string timeControlId, out MovieBooking show)
        {
            TimeSpan offset;
            var controls = FindRowControls(row, movieControlId, timeControlId);
            string movie = controls.MovieControl.SelectedValue, time = controls.TimeControl.Text;
            bool validTime = TimeSpan.TryParse(time, out offset);
            if (validTime)
                show = BuildMovieBooking(baseDate.Add(offset), movie);
            else
                show = null;
            return validTime;
        }
        #endregion
    }
}
