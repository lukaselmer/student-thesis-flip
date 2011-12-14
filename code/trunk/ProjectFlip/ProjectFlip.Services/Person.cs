using System.Security.Cryptography;
using System.Text;
using ProjectFlip.Services.Interfaces;

namespace ProjectFlip.Services
{
    /// <summary>
    /// The person is used for the info view, where the team members are shown.
    /// </summary>
    /// <remarks></remarks>
    public class Person : IPerson
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="email">The email.</param>
        /// <remarks></remarks>
        public Person(string name, string email)
        {
            Name = name;
            Email = email;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <remarks></remarks>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the email.
        /// </summary>
        /// <remarks></remarks>
        public string Email { get; private set; }

        /// <summary>
        /// Gets the image URL to download the image from gravatar.com.
        /// </summary>
        /// <remarks></remarks>
        public string ImageUrl { get { return string.Format("http://www.gravatar.com/avatar/{0}?s=150", Md5Hash(Email)); } }

        #endregion

        #region Other

        /// <summary>
        /// Generates an md5 hash for the email. This is necessary for the gravatar download.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private static string Md5Hash(object email)
        {
            var inputBytes = Encoding.ASCII.GetBytes(email.ToString());
            var hash = MD5.Create().ComputeHash(inputBytes);
            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("x2"));
            }
            var hashedString = sb.ToString();
            return hashedString;
        }
        #endregion

    }
}