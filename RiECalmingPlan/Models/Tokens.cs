using System;
using SQLite;

namespace RiECalmingPlan.Models
{
    public class Tokens
    {
        //Created when a user logs in

        [PrimaryKey]
        public int Id { get; set; }
        public string access_token { get; set; }         //access token from the server
        public string error_description { get; set; }
        public DateTime expire_date { get; set; }       //date that token is no longer valid

        public Tokens()
        {
        }
    }
}
