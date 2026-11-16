using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectiveService
{
    public static class CookieHelper
    {
        //this method saves the users profile within a cookie, takes a input of username and fav movie genre
        //ouputs the cookie with the user profile info: username, fav genre and the time profile was saved
        public static void SaveProfile(HttpResponse response, string username, string genre)
        {
            HttpCookie cookie = new HttpCookie("MovieProfile");
            cookie["Username"] = username;
            cookie["Genre"] = genre;
            cookie["SavedAt"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //for now we will say the cookies will expire after 7 days
            cookie.Expires = DateTime.Now.AddDays(7);
            cookie.HttpOnly = true;
            response.Cookies.Add(cookie);
        }
        //thus method is what retreives the cookie with the saved profile 
        //takes a input of a HTTP request and will output the matching cookie with profile or if no cookie is found, returns null
        public static HttpCookie GetProfile(HttpRequest request)
        {
            return request.Cookies["MovieProfile"];
        }
    }
}
