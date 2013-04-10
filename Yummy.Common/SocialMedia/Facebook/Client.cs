using System;
using System.Collections.Generic;
using System.Linq;
using fb = Facebook;
using System.Text;

namespace Yummy.Common.SocialMedia.Facebook
{
    public class Client : fb.FacebookClient
    {
        public Client(string appId) : base()
        {
            this.AppId = appId;
        }

        public Client(string appId, string accessToken)
            : base(accessToken) 
        {
            this.AppId = appId;
        }

        /// <summary>
        /// If the Login Url should redirect to a page other than the page that the user is currently on.
        /// Use this to create the Facebook Login Url.
        /// </summary>
        /// <param name="url">new url to redirect to.</param>
        /// <param name="scope">facebook permissions</param>
        /// <returns></returns>
        public string CreateLoginUrl(string url, string scope)
        {
            return this.GetLoginUrl(new { client_id = this.AppId, scope = scope, redirect_uri = url }).ToString().ToLower();
        }

        /// <summary>
        /// Facebook Get Call /me/likes/{id} 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsLiked(string id)
        {
            if (string.IsNullOrEmpty(this.AccessToken)) throw new Exception("Get the Access Token before calling IsLiked.");
            dynamic response = this.Get(string.Format("/me/likes/{0}", id));

            if (null != response.data)
            {
                return (response.data.Count > 0);
            }
            return false;
        }


        public User GetUser(string facebookId, string fields = "")
        {
            if (string.IsNullOrEmpty(facebookId)) throw new Exception("The facebookId cannot be empty!");
            if (string.IsNullOrEmpty(this.AccessToken)) throw new Exception("Get the Access Token before calling GetUser.");

            string url = string.Format("{0}?access_token={1}", facebookId, this.AccessToken);

            if (!string.IsNullOrEmpty(fields))
            {
                url = string.Format("{0}&fields={1}", url, fields);
            }

            dynamic user = this.Get(url);

            return new User()
            {
                FacebookId = user.id,
                FullName = user.name,
                FirstName = user.first_name,
                LastName = user.last_name,
                Link = (user.link != null) ? user.link : "",
                Username = (user.username != null) ? user.username : "",
                Birthday = ((user.birthday != null) && (user.birthday is DateTime)) ? Convert.ToDateTime(user.birthday) : null,
                Gender = (user.gender != null) ? user.gender : "",
                Email = (user.email != null) ? user.email : ""
            };
        }

        /// <summary>
        /// Post to a Users wall using the API.
        /// </summary>
        /// <param name="model">Post Model. Used to set the variables for post to a user's wall.</param>
        /// <returns></returns>
        public bool PostToWall(Post model)
        {
            if (string.IsNullOrEmpty(this.AccessToken)) throw new Exception("Get the Access Token before calling PostToWall.");

            string url = "/me/feed";

            this.Post(url, new
            {
                link = model.Link,
                picture = model.Picture,
                name = model.Name,
                description = model.Description,
                caption = model.Caption
            });
            return true;
        }

        /// <summary>
        /// Gets the list of User's friends.
        /// </summary>
        /// <returns></returns>
        public List<User> GetMyFriends()
        {
            if (string.IsNullOrEmpty(this.AccessToken)) throw new Exception("Get the Access Token before calling GetMyFriends.");
            List<User> ret = new List<User>();

            dynamic friends = this.Get(string.Format("me/friends?access_token={0}", this.AccessToken));
            if (null != friends["data"])
            {
                foreach (var item in friends["data"])
                {
                    ret.Add(new User()
                    {
                        FacebookId = item["id"],
                        FullName = item["name"]
                    });
                }
            }
            return ret;
        }

        /// <summary>
        /// Gets the list of Photo Albums belonging to the user.
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public List<Album> GetAlbums(int limit = 200)
        {
            if (string.IsNullOrEmpty(this.AccessToken)) throw new Exception("Get the Access Token before calling GetAlbums.");

            dynamic response = this.Get("/me/albums?limit=" + limit.ToString());
            List<Album> model = new List<Album>();

            if (null != response.data)
            {
                foreach (var item in response.data)
                {
                    if (!string.IsNullOrEmpty(item.cover_photo))
                    {
                        dynamic album = this.Get(item.cover_photo);
                        if (null != album.picture)
                        {
                            model.Add(new Album
                            {
                                AlbumId = item.id,
                                AlbumCoverImage = album.picture,
                                AlbumName = item.name
                            });
                        }
                    }
                }
            }
            return model;
        }

        /// <summary>
        /// Gets a specific photo album that belongs to the current user.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<Photo> GetAlbum(string Id)
        {
            if (string.IsNullOrEmpty(this.AccessToken)) throw new Exception("Get the Access Token before calling GetAlbum.");

            List<Photo> model = new List<Photo>();

            dynamic response = this.Get("/" + Id + "/photos?limit=200");
            if (null != response.data)
            {
                foreach (var item in response.data)
                {
                    model.Add(new Photo
                    {
                        Id = item.id,
                        Name = item.name,
                        Source = item.source,
                        Thumbnail = item.picture
                    });
                }
                    
            }
            return model;
        }

        /// <summary>
        /// Creates an open Graph object
        /// by posting the url to https://graph.facebook.com/me/{object-type}
        /// </summary>
        /// <param name="url"></param>
        /// <param name="action">defaults to news.reads. 
        /// Other facebook action types are 
        /// music.listens
        /// video.watches
        /// </param>
        /// <returns></returns>
        public string CreateOpenGraphReadAction(string url)
        {
            if (string.IsNullOrEmpty(this.AccessToken)) throw new Exception("Get the Access Token before calling Create Open Graph Action.");

            dynamic obj = this.Post("https://graph.facebook.com/me/news.reads", new { article = url, access_token = this.AccessToken });
            if (null != obj.id)
            {
                return obj.id.ToString();
            }
            throw new Exception("Open Graph Action was not created.");
        }

        public bool DeleteOpenGraphAction(string id)
        {
            if (string.IsNullOrEmpty(this.AccessToken)) throw new Exception("Get the Access Token before calling Delete Open Graph Action.");

            this.Delete(string.Format("https://graph.facebook.com/{0}?access_token={1}", id, this.AccessToken));
            return true;
        }

        /// <summary>
        /// Uses ParseSignedRequest to get the current Access Token.
        /// </summary>
        /// <param name="signed_request"></param>
        /// <returns></returns>
        public string GetAccessToken(string signed_request)
        {
            if (string.IsNullOrEmpty(this.AppSecret)) throw new Exception("Set the AppSecret before calling GetAccessToken.");
            if (string.IsNullOrEmpty(signed_request)) throw new Exception("The signed_request cannot be empty when calling GetAccessToken.");

            dynamic obj = this.ParseSignedRequest(signed_request);
            if (null != obj.oauth_token)
            {
                return obj.oauth_token;
            }
            throw new Exception("Cannot get the Access Token.");
        }

        /// <summary>
        /// Used when to retrieve the FacebookId who send a notification to the current User.
        /// (The user will have clicked on the Notification.)
        /// </summary>
        /// <param name="request_ids"></param>
        /// <returns></returns>
        public string GetFacebookIdFromNotification(string request_ids)
        {
            if (string.IsNullOrEmpty(this.AccessToken)) throw new Exception("Get the Access Token before calling Get FacebookId From Notification.");

            if (!string.IsNullOrEmpty(request_ids))
            {
                var id = (request_ids.Contains(",")) ? request_ids.Split(',').FirstOrDefault() : request_ids;
                dynamic app = this.Get(string.Format("/{0}?access_token={1}", id, this.AccessToken));

                if (null != app.from.id)
                {
                    return app.from.id;
                }
            }
            return "";
        }
    }
}
