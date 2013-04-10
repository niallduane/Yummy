using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yummy.Common.SocialMedia.Facebook
{
    public class User
    {
        public string FacebookId { get; set; }

        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Link { get; set; }
        public string Username { get; set; }
        public DateTime? Birthday { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }

        public int HomeTownId { get; set; }
        public string HomeTown { get; set; }

        public int LocationId { get; set; }
        public string Location { get; set; }

        public string ProfileImageUrl
        {
            get
            {
                return string.Format("https://graph.facebook.com/{0}/picture", FacebookId);
            }
        }
    }
}
